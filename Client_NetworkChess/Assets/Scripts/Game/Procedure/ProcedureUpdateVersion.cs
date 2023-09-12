//@LeeTools
//------------------------
//Filename：ProcedureUpdateVersion.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/11 16:13:20
//Function：Nothing
//------------------------

using GameFramework.Resource;
using System;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Game
{
    public class ProcedureUpdateVersion : ProcedureBase
    {
        private bool m_UpdateVersionComplete = false;

        private UpdateVersionListCallbacks m_UpdateVersionListCallbacks = null;

        public override bool UseNativeDialog => false;

        protected override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);

            m_UpdateVersionListCallbacks = new UpdateVersionListCallbacks(OnUpdateVersionListSuccess, OnUpdateVersionListFailure);
        }

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            m_UpdateVersionComplete = false;

            GameRuntime.Resource.UpdateVersionList(
                procedureOwner.GetData<VarInt32>(Constant.ProcedureData.VersionListLength),
                procedureOwner.GetData<VarInt32>(Constant.ProcedureData.VersionListHashCode),
                procedureOwner.GetData<VarInt32>(Constant.ProcedureData.VersionListCompressedLength),
                procedureOwner.GetData<VarInt32>(Constant.ProcedureData.VersionListCompressedHashCode),
                m_UpdateVersionListCallbacks);
            procedureOwner.RemoveData(Constant.ProcedureData.VersionListLength);
            procedureOwner.RemoveData(Constant.ProcedureData.VersionListHashCode);
            procedureOwner.RemoveData(Constant.ProcedureData.VersionListCompressedLength);
            procedureOwner.RemoveData(Constant.ProcedureData.VersionListCompressedHashCode);
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            if (m_UpdateVersionComplete)
            {
                ChangeState<ProcedureVerifyResources>(procedureOwner);
            }
        }

        private void OnUpdateVersionListSuccess(string downloadPath, string downloadUri)
        {
            m_UpdateVersionComplete = true;
            Log.Info("Update version list from '{0}' success.", downloadUri);
        }

        private void OnUpdateVersionListFailure(string downloadUri, string errorMessage)
        {
            Log.Error("Update version list from '{0}' failure, error message '{1}'.", downloadUri, errorMessage);
        }
    }
}