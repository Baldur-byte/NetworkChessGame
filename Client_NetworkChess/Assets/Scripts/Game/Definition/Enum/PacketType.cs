//@LeeTools
//------------------------
//Filename：PacketType.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/12 16:33:47
//Function：Nothing
//------------------------
namespace Game
{
    /// <summary>
    /// 消息包类型
    /// </summary>
    public enum PacketType : byte
    {
        /// <summary>
        /// 未定义类型
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// 客户端发往服务器
        /// </summary>
        ClientToServer = 1,

        /// <summary>
        /// 服务器发往客户端
        /// </summary>
        ServerToClient = 2,
    }
}