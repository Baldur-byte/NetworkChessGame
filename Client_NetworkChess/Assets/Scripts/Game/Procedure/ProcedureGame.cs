//@LeeTools
//------------------------
//Filename：ProcedureGame.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/11 20:33:21
//Function：Nothing
//------------------------

using GameFramework.Event;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Game
{
    public class ProcedureGame : ProcedureBase
    {
        public override bool UseNativeDialog => false;

        private UIGameForm m_GameForm = null;

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameRuntime.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            GameRuntime.UI.OpenUIForm(UIFormId.GameForm, this);
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            GameRuntime.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            if(m_GameForm != null)
            {
                m_GameForm.Close(isShutdown);
                m_GameForm = null;
            }
            base.OnLeave(procedureOwner, isShutdown);
        }

        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;
            if(ne.UserData != this)
            {
                return;
            }
            m_GameForm = (UIGameForm)ne.UIForm.Logic;
        }
    }
}