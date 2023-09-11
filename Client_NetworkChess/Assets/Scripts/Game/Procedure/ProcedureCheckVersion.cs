//@LeeTools
//------------------------
//Filename：ProcedureCheckVersion.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/11 12:06:53
//Function：Nothing
//------------------------

using GameFramework;
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Game
{
    public class ProcedureCheckVersion : ProcedureBase
    {
        private bool m_CheckVersionComplete = false;
        private bool m_NeedUpdateVersion = false;
        private VersionInfo m_VersionInfo = null;

        public override bool UseNativeDialog => true;

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            m_CheckVersionComplete = false;
            m_NeedUpdateVersion = false;
            m_VersionInfo = null;

            GameRuntime.Event.Subscribe(WebRequestSuccessEventArgs.EventId, OnVersionCheckSuccess);
            GameRuntime.Event.Subscribe(WebRequestFailureEventArgs.EventId, OnVersionCheckFailure);

            GameRuntime.WebRequest.AddWebRequest(Utility.Text.Format(GameRuntime.BuiltinData.BuildInfo.CheckVersionUrl, GetPlatformPath()), this);
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            GameRuntime.Event.Unsubscribe(WebRequestSuccessEventArgs.EventId, OnVersionCheckSuccess);
            GameRuntime.Event.Unsubscribe(WebRequestFailureEventArgs.EventId, OnVersionCheckFailure);

            base.OnLeave(procedureOwner, isShutdown);
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            if (!m_CheckVersionComplete)
            {
                return;
            }

            if (m_NeedUpdateVersion)
            {
                procedureOwner.SetData<VarInt32>(Constant.FsmDataKey.VersionListLength, m_VersionInfo.VersionListLength);
                procedureOwner.SetData<VarInt32>(Constant.FsmDataKey.VersionListHashCode, m_VersionInfo.VersionListHashCode);
                procedureOwner.SetData<VarInt32>(Constant.FsmDataKey.VersionListCompressedLength, m_VersionInfo.VersionListCompressedLength);
                procedureOwner.SetData<VarInt32>(Constant.FsmDataKey.VersionListCompressedHashCode, m_VersionInfo.VersionListCompressedHashCode);
                ChangeState<ProcedureUpdateVersion>(procedureOwner);
            }
            else
            {
                ChangeState<ProcedureVerifyResources>(procedureOwner);
            }
        }

        private void OnVersionCheckSuccess(object sender, GameEventArgs e)
        {
            WebRequestSuccessEventArgs ne = (WebRequestSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            //解析版本信息
            byte[] versionInfoBytes = ne.GetWebResponseBytes();
            string versionInfoString = Utility.Converter.GetString(versionInfoBytes);

            m_VersionInfo = Utility.Json.ToObject<VersionInfo>(versionInfoString);

            if(m_VersionInfo == null)
            {
                Log.Error("Parse version info failure.");
                return;
            }

            Log.Info("Check version complete, LastestGameVersion: {0}, InternalGameVersion: {1}, InternalResourceVersion: {2}, VersionListLength: {3}, VersionListHashCode: {4}, VersionListCompressedLength: {5}, VersionListCompressedHashCode: {6}, NeedUpdateVersion: {7}", m_VersionInfo.LastestGameVersion, m_VersionInfo.InternalGameVersion.ToString(), m_VersionInfo.InternalResourceVersion.ToString(), m_VersionInfo.VersionListLength.ToString(), m_VersionInfo.VersionListHashCode.ToString(), m_VersionInfo.VersionListCompressedLength.ToString(), m_VersionInfo.VersionListCompressedHashCode.ToString(), m_NeedUpdateVersion.ToString());
            Log.Info("Local game version is '{0} ({1})'", Version.GameVersion, Version.InternalGameVersion.ToString());

            System.Version localVersion = new System.Version(Version.GameVersion);
            System.Version onlineVersion = new System.Version(m_VersionInfo.LastestGameVersion);
            //检查版本号
            if (m_VersionInfo.IsForceUpdateGame && localVersion < onlineVersion)
            {
                //强制更新，打开弹窗，跳转到商店界面
                GameRuntime.UI.OpenDialog(new DialogParams
                {
                    Mode = 2,
                    Title = GameRuntime.Localization.GetString(Constant.LocalizationKey.ForceUpdate_Title),
                    Message = GameRuntime.Localization.GetString(Constant.LocalizationKey.ForceUpdate_Message),
                    ConfirmText = GameRuntime.Localization.GetString(Constant.LocalizationKey.ForceUpdate_ConfirmText),
                    OnClickConfirm = GotoUpdateApp,
                    CancelText = GameRuntime.Localization.GetString(Constant.LocalizationKey.ForceUpdate_CancelText),
                    OnClickCancel = delegate (object userData) { UnityGameFramework.Runtime.GameEntry.Shutdown(ShutdownType.Quit); },
                });
            }
            else
            {
                GameRuntime.Resource.UpdatePrefixUri = Utility.Path.GetRegularPath(m_VersionInfo.UpdatePrefixUri);

                m_CheckVersionComplete = true;
                m_NeedUpdateVersion = GameRuntime.Resource.CheckVersionList(m_VersionInfo.InternalResourceVersion) == GameFramework.Resource.CheckVersionListResult.NeedUpdate;
            }
        }

        private void GotoUpdateApp(object userData)
        {
            string url = null;
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            url = GameRuntime.BuiltinData.BuildInfo.WindowsAppUrl;
#elif UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
            url = GameRuntime.BuiltinData.BuildInfo.MacOSAppUrl;
#elif UNITY_IOS
            url = GameRuntime.BuiltinData.BuildInfo.IOSAppUrl;
#elif UNITY_ANDROID
            url = GameRuntime.BuiltinData.BuildInfo.AndroidAppUrl;
#endif
            if (!string.IsNullOrEmpty(url))
            {
                Application.OpenURL(url);
            }
        }

        private void OnVersionCheckFailure(object sender, GameEventArgs e)
        {
            WebRequestFailureEventArgs ne = (WebRequestFailureEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            Log.Warning("Check version failure, error message: {0}", ne.ErrorMessage);
        }

        private string GetPlatformPath()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.WindowsPlayer:
                    return "Windows";
                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.OSXPlayer:
                    return "MacOS";
                case RuntimePlatform.IPhonePlayer:
                    return "IOS";
                case RuntimePlatform.Android:
                    return "Android";
                default:
                    throw new System.NotSupportedException(Utility.Text.Format("Platform '{0} is not supported.'", Application.platform));
            }
        }
    }
}