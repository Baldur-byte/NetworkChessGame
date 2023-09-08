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