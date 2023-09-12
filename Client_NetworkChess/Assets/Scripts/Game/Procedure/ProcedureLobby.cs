//@LeeTools
//------------------------
//Filename：ProcedureLobby.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/11 18:19:25
//Function：Nothing
//------------------------

using GameFramework.Event;
using System;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Game
{
    public class ProcedureLobby : ProcedureBase
    {
        public override bool UseNativeDialog => false;

        private UILobbyForm m_LobbyForm = null;

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            GameRuntime.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

            GameRuntime.UI.OpenUIForm(UIFormId.LobbyForm, this);
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            GameRuntime.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

            if(m_LobbyForm != null)
            {
                m_LobbyForm.Close(isShutdown);
                m_LobbyForm = null;
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

            m_LobbyForm = (UILobbyForm)ne.UIForm.Logic;
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
        }

        private void ConnectToServer()
        {
            GameRuntime.Network.runInEditMode = true;
            //GameRuntime.Network.
        }

        private void OnStartGameButtonClick()
        {

        }

        private void OnQuitButtonClick()
        {
            //ChangeState<ProcedureLogin>(procedureOwner);
        }

        private void OnExitGameButtonClick()
        {
            GameEntry.Shutdown(ShutdownType.Quit);
        }
    }
}