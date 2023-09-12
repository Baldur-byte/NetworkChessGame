//@LeeTools
//------------------------
//Filename：ProcedureCheckResources.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/11 16:41:41
//Function：Nothing
//------------------------

using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Game
{
    public class ProcedureCheckResources : ProcedureBase
    {
        private bool m_CheckResourcesComplete = false;
        private bool m_NeedUpdateResources = false;
        private int m_UpdateResourceCount = 0;
        private long m_UpdateResourceTotalCompressedLength = 0L;

        public override bool UseNativeDialog => true;


        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            m_CheckResourcesComplete = false;
            m_NeedUpdateResources = false;
            m_UpdateResourceCount = 0;
            m_UpdateResourceTotalCompressedLength = 0L;

            GameRuntime.Resource.CheckResources(OnCheckResourcesComplete);
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            if(m_CheckResourcesComplete)
            {
                if (m_NeedUpdateResources)
                {
                    procedureOwner.SetData<VarInt32>(Constant.ProcedureData.UpdateResourceCount, m_UpdateResourceCount);
                    procedureOwner.SetData<VarInt64>(Constant.ProcedureData.UpdateResourceTotalCompressedLength, m_UpdateResourceTotalCompressedLength);
                    ChangeState<ProcedureUpdateResources>(procedureOwner);
                }
                else
                {
                    ChangeState<ProcedurePreload>(procedureOwner);
                }
            }
        }

        private void OnCheckResourcesComplete(int movedCount, int removedCount, int updateCount, long updateTotalLength, long updateTotalCompressedLength)
        {
            m_CheckResourcesComplete = true;
            m_NeedUpdateResources = updateCount > 0;
            m_UpdateResourceCount = updateCount;
            m_UpdateResourceTotalCompressedLength = updateTotalCompressedLength;
            Log.Info("Check resources complete, movedCount '{0}', remove count '{1}', update count '{2}', update total length '{3}', update total compressed length '{4}'.", movedCount.ToString(), removedCount.ToString(), updateCount.ToString(), updateTotalLength.ToString(), updateTotalCompressedLength.ToString());
        }
    }
}