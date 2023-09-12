//@LeeTools
//------------------------
//Filename：ProcedureLogin.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/11 18:21:10
//Function：Nothing
//------------------------

using System;
using GameFramework;
using GameFramework.Event;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Game
{
    public class ProcedureLogin : ProcedureBase
    {
        private UILoginForm m_LoginForm = null;

        private bool m_LoggedIn = false;

        public override bool UseNativeDialog => false;

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            m_LoggedIn = false;

            GameRuntime.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

            GameRuntime.UI.OpenUIForm(UIFormId.LoginForm, this);
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            GameRuntime.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

            if(m_LoginForm != null)
            {
                m_LoginForm.Close(isShutdown);
                m_LoginForm = null;
            }
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            if(m_LoggedIn)
            {
                procedureOwner.SetData<VarInt32>(Constant.ProcedureData.NextSceneId, (int)SceneType.Lobby);
                ChangeState<ProcedureChangeScene>(procedureOwner);
            }
        }

        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs) e;
            if (ne.UserData != this) return;

            m_LoginForm = (UILoginForm) ne.UIForm.Logic;
        }

        public void Login(string userName, string password)
        {
            m_LoggedIn = true;
        }

        public void Register(string userName, string password)
        {
            m_LoggedIn = true;
        }
    }
}