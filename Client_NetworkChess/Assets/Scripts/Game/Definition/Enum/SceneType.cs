//@LeeTools
//------------------------
//Filename：SceneType.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/11 20:24:36
//Function：Nothing
//------------------------

namespace Game
{
    public enum SceneType : byte
    {
        /// <summary>
        /// 无效场景
        /// </summary>
        None = 0,

        /// <summary>
        /// 登录场景
        /// </summary>
        Login,

        /// <summary>
        /// 大厅场景
        /// </summary>
        Lobby,

        /// <summary>
        /// 游戏场景
        /// </summary>
        Game,
    }
}