//@LeeTools
//------------------------
//Filename：ProcedureUpdateResources.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/11 16:49:19
//Function：Nothing
//------------------------

using GameFramework;
using GameFramework.Event;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Game
{
    public class ProcedureUpdateResources : ProcedureBase
    {
        private bool m_UpdateResourcesComplete = false;
        private int m_UpdateResourceCount = 0;
        private long m_UpdateResourceTotalCompressedLength = 0L;
        private int m_UpdateResourceSuccessCount = 0;

        private List<UpdateLengthData> m_UpdateLengthData = new List<UpdateLengthData>();
        private UpdateResourceForm m_UpdateResourceForm = null;

        public override bool UseNativeDialog => true;

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            m_UpdateResourcesComplete = false;

            m_UpdateResourceCount = procedureOwner.GetData<VarInt32>(Constant.ProcedureData.UpdateResourceCount).Value;
            m_UpdateResourceTotalCompressedLength =
                procedureOwner.GetData<VarInt64>(Constant.ProcedureData.UpdateResourceTotalCompressedLength).Value;
            m_UpdateResourceSuccessCount = 0;
            m_UpdateLengthData.Clear();
            m_UpdateResourceForm = null;

            procedureOwner.RemoveData(Constant.ProcedureData.UpdateResourceCount);
            procedureOwner.RemoveData(Constant.ProcedureData.UpdateResourceTotalCompressedLength);

            GameRuntime.Event.Subscribe(ResourceUpdateStartEventArgs.EventId, OnResourceUpdateStart);
            GameRuntime.Event.Subscribe(ResourceUpdateChangedEventArgs.EventId, OnResourceUpdateChanged);
            GameRuntime.Event.Subscribe(ResourceUpdateSuccessEventArgs.EventId, OnResourceUpdateSuccessEventArgs);
            GameRuntime.Event.Subscribe(ResourceUpdateFailureEventArgs.EventId, OnResourceUpdateFailureEventArgs);

            if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
            {
                GameRuntime.UI.OpenDialog(new DialogParams
                {
                    Mode = 2,
                    Title = GameRuntime.Localization.GetString(Constant.LocalizationKey.UpdateResourceViaCarrierDataNetwork_Titile),
                    Message = GameRuntime.Localization.GetString(Constant.LocalizationKey.UpdateResourceViaCarrierDataNetwork_Message),
                    ConfirmText = GameRuntime.Localization.GetString(Constant.LocalizationKey.UpdateResourceViaCarrierDataNetwork_ConfirmText),
                    OnClickConfirm = StartUpdateResources,
                    CancelText = GameRuntime.Localization.GetString(Constant.LocalizationKey.UpdateResourceViaCarrierDataNetwork_CancelText),
                    OnClickCancel = delegate (object userData) { GameEntry.Shutdown(ShutdownType.Quit); },
                });
            }
            else
            {
                StartUpdateResources(null);
            }
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            if(m_UpdateResourceForm != null)
            {
                Object.Destroy(m_UpdateResourceForm.gameObject);
                m_UpdateResourceForm = null;
            }

            GameRuntime.Event.Unsubscribe(ResourceUpdateStartEventArgs.EventId, OnResourceUpdateStart);
            GameRuntime.Event.Unsubscribe(ResourceUpdateChangedEventArgs.EventId, OnResourceUpdateChanged);
            GameRuntime.Event.Unsubscribe(ResourceUpdateSuccessEventArgs.EventId, OnResourceUpdateSuccessEventArgs);
            GameRuntime.Event.Unsubscribe(ResourceUpdateFailureEventArgs.EventId, OnResourceUpdateFailureEventArgs);

            base.OnLeave(procedureOwner, isShutdown);
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if (m_UpdateResourcesComplete)
            {
                ChangeState<ProcedurePreload>(procedureOwner);
            }
        }

        private void OnResourceUpdateStart(object sender, GameEventArgs e)
        {
            ResourceUpdateStartEventArgs ne = (ResourceUpdateStartEventArgs)e;

            // 重置更新长度
            for(int i =0; i < m_UpdateLengthData.Count; i++)
            {
                if (m_UpdateLengthData[i].Name == ne.Name)
                {
                    Log.Warning("Update resource '{0}' is invalid.", ne.Name);
                    m_UpdateLengthData[i].Length = 0;
                    RefreshProgress();
                    return;
                }
            }
            m_UpdateLengthData.Add(new UpdateLengthData(ne.Name));
        }

        private void OnResourceUpdateChanged(object sender, GameEventArgs e)
        {
            ResourceUpdateChangedEventArgs ne = (ResourceUpdateChangedEventArgs)e;

            // 更新更新长度
            for(int i = 0; i < m_UpdateLengthData.Count; i++)
            {
                if (m_UpdateLengthData[i].Name == ne.Name)
                {
                    m_UpdateLengthData[i].Length = ne.CurrentLength;
                    RefreshProgress();
                    return;
                }
            }

            Log.Warning("Update resource '{0}' is invalid.", ne.Name);
        }

        private void OnResourceUpdateSuccessEventArgs(object sender, GameEventArgs e)
        {
            ResourceUpdateSuccessEventArgs ne = (ResourceUpdateSuccessEventArgs)e;
            
            Log.Info("Update resource '{0}' success.", ne.Name);

            for(int i = 0; i < m_UpdateLengthData.Count; i++)
            {
                if (m_UpdateLengthData[i].Name == ne.Name)
                {
                    m_UpdateLengthData[i].Length = ne.CompressedLength;
                    m_UpdateResourceSuccessCount++;
                    RefreshProgress();
                    break;
                }
            }

            Log.Warning("Update resource '{0}' is invalid.", ne.Name);
        }

        private void OnResourceUpdateFailureEventArgs(object sender, GameEventArgs e)
        {
            ResourceUpdateFailureEventArgs ne = (ResourceUpdateFailureEventArgs)e;
            if(ne.RetryCount >= ne.TotalRetryCount)
            {
                Log.Error("Update resource '{0}' failure from {1}, retry count has max. error {2}", ne.Name, ne.DownloadUri, ne.ErrorMessage);
                return;
            }
            else
            {
                Log.Error("Update resource '{0}' failure from {1}, with error {2}", ne.Name, ne.DownloadUri, ne.ErrorMessage);
            }

            for(int i = 0; i <= m_UpdateLengthData.Count;i++)
            {
                if (m_UpdateLengthData[i].Name == ne.Name)
                {
                    m_UpdateLengthData.Remove(m_UpdateLengthData[i]);
                    RefreshProgress();
                    return;
                }
            }

            Log.Warning("Update resource '{0}' is invalid.", ne.Name);
        }

        private void StartUpdateResources(object value)
        {
            if (m_UpdateResourceForm == null)
            {
                m_UpdateResourceForm = Object.Instantiate(GameRuntime.UIFormTemplate.UpdateResourceFormTemplate);
            }

            Log.Info("Start update resources...");
            GameRuntime.Resource.UpdateResources(OnUpdateResourcesComplete);
        }

        private void OnUpdateResourcesComplete(GameFramework.Resource.IResourceGroup resourceGroup, bool result)
        {
            if(result)
            {
                m_UpdateResourcesComplete = true;
                Log.Info("Update resources complete.");
            }
            else
            {
                Log.Error("Update resources complete with errors.");
            }
        }

        private void RefreshProgress()
        {
            long currentTotalUpdateLength = 0L;
            for(int i = 0; i < m_UpdateLengthData.Count; i++)
            {
                currentTotalUpdateLength += m_UpdateLengthData[i].Length;
            }

            float progressTotal = (float)currentTotalUpdateLength / m_UpdateResourceTotalCompressedLength;
            string descriptionText = GameRuntime.Localization.GetString(
                Constant.LocalizationKey.UpdateResource_Tips,
                m_UpdateResourceSuccessCount.ToString(),
                m_UpdateResourceCount.ToString(),
                GetByteLengthString(currentTotalUpdateLength),
                GetByteLengthString(m_UpdateResourceTotalCompressedLength), progressTotal,
                GetByteLengthString((int)GameRuntime.Download.CurrentSpeed));
            m_UpdateResourceForm.SetProgress(progressTotal, descriptionText);
        }

        private string GetByteLengthString(long byteLength)
        {
            if(byteLength < 1024L)
            {
                return Utility.Text.Format("{0} Bytes", byteLength.ToString());
            }
            else if(byteLength < 1024L * 1024L)
            {
                return Utility.Text.Format("{0} KB", (byteLength / 1024f).ToString("F2"));
            }
            else if(byteLength < 1024L * 1024L * 1024L)
            {
                return Utility.Text.Format("{0} MB", (byteLength / 1024f / 1024f).ToString("F2"));
            }
            else if (byteLength < 1024L * 1024L * 1024L * 1024L)
            {
                return Utility.Text.Format("{0} GB", (byteLength / 1024f / 1024f / 1024f / 1024f).ToString("F2"));
            }
            else if (byteLength < 1024L * 1024L * 1024L * 1024L * 1024L)
            {
                return Utility.Text.Format("{0} TB", (byteLength / 1024f / 1024f / 1024f / 1024f / 1024f).ToString("F2"));
            }
            else if (byteLength < 1024L * 1024L * 1024L * 1024L * 1024L * 1024L)
            {
                return Utility.Text.Format("{0} PB", (byteLength / 1024f / 1024f / 1024f / 1024f / 1024f / 1024f).ToString("F2"));
            }
            else
            {
                return Utility.Text.Format("{0} EB", (byteLength / 1024f / 1024f / 1024f / 1024f / 1024f / 1024f / 1024f).ToString("F2"));
            }
        }

        private class UpdateLengthData
        {
            private readonly string m_Name;

            public UpdateLengthData(string name)
            {
                m_Name = name;
            }

            public string Name => m_Name;

            public int Length { get; set; }
        }
    }
}