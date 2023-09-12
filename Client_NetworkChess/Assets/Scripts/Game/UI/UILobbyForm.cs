//@LeeTools
//------------------------
//Filename：UILobbyForm.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/11 20:36:09
//Function：Nothing
//------------------------
namespace Game
{
    public class UILobbyForm : UIBase
    {
        private ProcedureLobby m_ProcedureLobby = null;

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);

            m_ProcedureLobby = (ProcedureLobby)userData;
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
        }
    }
}