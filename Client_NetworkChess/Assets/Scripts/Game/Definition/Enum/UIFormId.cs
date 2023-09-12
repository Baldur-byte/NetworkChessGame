//@LeeTools
//------------------------
//Filename：UIFormId.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/11 20:24:44
//Function：Nothing
//------------------------

namespace Game
{
    /// <summary>
    /// 界面编号
    /// </summary>
    public enum UIFormId : byte
    {
        Undefined = 0,

        /// <summary>
        /// 提示窗口
        /// </summary>
        DialogForm = 1,

        /// <summary>
        /// 登录界面
        /// </summary>
        LoginForm = 2,

        /// <summary>
        /// 注册界面
        /// </summary>
        RegisterForm = 3,

        /// <summary>
        /// 大厅界面
        /// </summary>
        LobbyForm = 4,

        /// <summary>
        /// 游戏界面
        /// </summary>
        GameForm = 5,
    }
}