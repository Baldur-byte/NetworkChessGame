//@LeeTools
//------------------------
//Filename：UILoginForm.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/11 20:18:14
//Function：Nothing
//------------------------

using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace Game
{
    public class UILoginForm : UIBase
    {
        [SerializeField]
        private InputField m_UserName = null;

        [SerializeField]
        private InputField m_Password = null;

        [SerializeField]
        private Button m_LoginButton = null;

        [SerializeField]
        private Button m_RegisterButton = null;

        [SerializeField]
        private Button m_QuitButton = null;

        [SerializeField]
        private Text m_Message = null;

        private ProcedureLogin m_ProcedureLogin = null;

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);

            m_ProcedureLogin = (ProcedureLogin)userData;

            if (m_ProcedureLogin == null )
            {
                Log.Error("ProcedureLogin is invalid when open UILoginForm");
                return;
            }

            m_LoginButton.onClick.AddListener(OnLoginButtonClick);
            m_RegisterButton.onClick.AddListener(OnRegisterButtonClick);
            m_QuitButton.onClick.AddListener(OnQuitButtonClick);
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            m_ProcedureLogin = null;
            base.OnClose(isShutdown, userData);
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
        }

        public void OnLoginButtonClick()
        {
            if (string.IsNullOrEmpty(m_UserName.text) || string.IsNullOrEmpty(m_Password.text))
            {
                m_Message.text = "用户名或密码不能为空";
                return;
            }
            m_ProcedureLogin.Login(m_UserName.text, m_Password.text);
        }

        public void OnRegisterButtonClick()
        {
            if (string.IsNullOrEmpty(m_UserName.text) || string.IsNullOrEmpty(m_Password.text))
            {
                m_Message.text = "用户名或密码不能为空";
                return;
            }
            m_ProcedureLogin.Register(m_UserName.text, m_Password.text);
            return;
            Close();
            GameRuntime.UI.OpenUIForm(UIFormId.RegisterForm, m_ProcedureLogin);
        }

        public void OnQuitButtonClick()
        {
            GameEntry.Shutdown(ShutdownType.Quit);
        }
    }
}