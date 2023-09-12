//@LeeTools
//------------------------
//Filename：UIGameForm.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/11 20:37:22
//Function：Nothing
//------------------------
namespace Game
{
    public class UIGameForm : UIBase
    {
        private ProcedureGame m_ProcedureGame = null;

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            m_ProcedureGame = (ProcedureGame)userData;
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
        }
    }
}