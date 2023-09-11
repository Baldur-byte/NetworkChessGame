//@LeeTools
//------------------------
//Filename：ProcedureGame.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/11 20:33:21
//Function：Nothing
//------------------------

using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Game
{
    public class ProcedureGame : ProcedureBase
    {
        public override bool UseNativeDialog => false;
    }
}