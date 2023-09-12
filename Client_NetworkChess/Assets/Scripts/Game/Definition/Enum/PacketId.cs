//@LeeTools
//------------------------
//Filename：PacketId.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/12 16:43:23
//Function：Nothing
//------------------------

namespace Game
{
    public enum PacketId : byte
    {
        /// <summary>
        /// 未知消息类型
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 心跳包 C->S
        /// </summary>
        CSHeartBeat = 1,

        /// <summary>
        /// 心跳包 S->C
        /// </summary>
        SCHeartBeat = 2,

        /// <summary>
        /// 登录请求 C->S
        /// </summary>
        CSLogin = 3,

        /// <summary>
        /// 登录请求 S->C
        /// </summary>
        SCLogin = 4,

        /// <summary>
        /// 注册请求 C->S
        /// </summary>
        CSRegister = 5,

        /// <summary>
        /// 注册请求 S->C
        /// </summary>
        SCRegister = 6,

        /// <summary>
        /// 获取玩家信息请求 C->S
        /// </summary>
        CSGetPlayerInfo = 7,

        /// <summary>
        /// 获取玩家信息请求 S->C
        /// </summary>
        SCGetPlayerInfo = 8,

        /// <summary>
        /// 获取玩家列表请求 C->S
        /// </summary>
        CSGetPlayerList = 9,

        /// <summary>
        /// 获取玩家列表请求 S->C
        /// </summary>
        SCGetPlayerList = 10,

        /// <summary>
        /// 获取房间列表请求 C->S
        /// </summary>
        CSGetRoomList = 11,

        /// <summary>
        /// 获取房间列表请求 S->C
        /// </summary>
        SCGetRoomList = 12,

        /// <summary>
        /// 获取房间信息请求 C->S
        /// </summary>
        CSGetRoomInfo = 13,

        /// <summary>
        /// 获取房间信息请求 S->C
        /// </summary>
        SCGetRoomInfo = 14,

        /// <summary>
        /// 获取房间玩家列表请求 C->S
        /// </summary>
        CSGetRoomPlayerList = 15,

        /// <summary>
        /// 获取房间玩家列表请求 S->C
        /// </summary>
        SCGetRoomPlayerList = 16,

        /// <summary>
        /// 更新玩家信息请求 C->S
        /// </summary>
        CSUpdatePlayerInfo = 17,

        /// <summary>
        /// 更新玩家信息请求 S->C
        /// </summary>
        SCUpdatePlayerInfo = 18,

        /// <summary>
        /// 更新玩家状态请求 C->S
        /// </summary>
        CSUpdatePlayerState = 19,

        /// <summary>
        /// 更新玩家状态请求 S->C
        /// </summary>
        SCUpdatePlayerState = 20,

        /// <summary>
        /// 更新房间信息请求 C->S
        /// </summary>
        CSUpdateRoomInfo = 21,

        /// <summary>
        /// 更新房间信息请求 S->C
        /// </summary>
        SCUpdateRoomInfo = 22,

        /// <summary>
        /// 更新房间状态请求 C->S
        /// </summary>
        CSUpdateRoomState = 23,

        /// <summary>
        /// 更新房间状态请求 S->C
        /// </summary>
        SCUpdateRoomState = 24,

        /// <summary>
        /// 创建房间请求 C->S
        /// </summary>
        CSCreateRoom = 25,

        /// <summary>
        /// 创建房间请求 S->C
        /// </summary>
        SCCreateRoom = 26,

        /// <summary>
        /// 加入房间请求 C->S
        /// </summary>
        CSJoinRoom = 27,

        /// <summary>
        /// 加入房间请求 S->C
        /// </summary>
        SCJoinRoom = 28,

        /// <summary>
        /// 离开房间请求 C->S
        /// </summary>
        CSLeaveRoom = 29,

        /// <summary>
        /// 离开房间请求 S->C
        /// </summary>
        SCLeaveRoom = 30,

        /// <summary>
        /// 开始游戏请求 C->S
        /// </summary>
        CSStartGame = 31,

        /// <summary>
        /// 开始游戏请求 S->C
        /// </summary>
        SCStartGame = 32,

        /// <summary>
        /// 退出游戏请求 C->S
        /// </summary>
        CSExitGame = 33,

        /// <summary>
        /// 退出游戏请求 S->C
        /// </summary>
        SCExitGame = 34,

        /// <summary>
        /// 添加朋友请求 C->S
        /// </summary>
        CSAddFriend = 35,

        /// <summary>
        /// 添加朋友请求 S->C
        /// </summary>
        SCAddFriend = 36,

        /// <summary>
        /// 获取朋友列表请求 C->S
        /// </summary>
        CSGetFriendList = 37,

        /// <summary>
        /// 获取朋友列表请求 S->C
        /// </summary>
        SCGetFriendList = 38,

        /// <summary>
        /// 获取朋友信息请求 C->S
        /// </summary>
        CSGetFriendInfo = 39,

        /// <summary>
        /// 获取朋友信息请求 S->C
        /// </summary>
        SCGetFriendInfo = 42,

        /// <summary>
        /// 获取好友请求列表 C->S
        /// </summary>
        CSGetFriendRequestList = 43,

        /// <summary>
        /// 获取好友请求列表 S->C
        /// </summary>
        SCGetFriendRequestList = 44,

        /// <summary>
        /// 删除朋友请求 C->S
        /// </summary>
        CSRemoveFriend = 45,

        /// <summary>
        /// 删除朋友请求 S->C
        /// </summary>
        SCRemoveFriend = 46,
    }
}