//@LeeTools
//------------------------
//Filename：ProcedureBase.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/11 14:55:51
//Function：Nothing
//------------------------

namespace Game
{
    public abstract class ProcedureBase : GameFramework.Procedure.ProcedureBase
    {
        // 获取流程是否使用原生对话框
        // 在一些特殊的流程（如游戏逻辑对话框资源更新完成前的流程）中，可以考虑调用原生对话框进行消息提示行为
        public abstract bool UseNativeDialog
        {
            get;
        }
    }
}