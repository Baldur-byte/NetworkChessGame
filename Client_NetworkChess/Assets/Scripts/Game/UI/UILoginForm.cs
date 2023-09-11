//@LeeTools
//------------------------
//Filename：UILoginForm.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/11 20:18:14
//Function：Nothing
//------------------------

using UnityGameFramework.Runtime;

namespace Game
{
    public class UILoginForm : UIBase
    {
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
    }
}