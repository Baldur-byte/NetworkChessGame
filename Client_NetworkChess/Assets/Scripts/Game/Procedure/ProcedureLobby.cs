//@LeeTools
//------------------------
//Filename：ProcedureLobby.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/11 18:19:25
//Function：Nothing
//------------------------

using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Game
{
    public class ProcedureLobby : ProcedureBase
    {
        public override bool UseNativeDialog => false;

        private UILobbyForm m_LobbyForm = null;
    }
}