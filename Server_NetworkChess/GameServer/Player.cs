﻿using System.Net.Sockets;

public class Player
{
    public Socket Socket; //网络套接字

    public string Name; //玩家名字

    public bool InRoom; //是否在房间中

    public int RoomId;  //所处房间号码

    public Player(Socket socket)
    {
        Socket = socket;
        Name = "Player Unknown";
        InRoom = false;
        RoomId = 0;
    }

    //进入房间
    public void EnterRoom(int roomId)
    {
        InRoom = true;
        RoomId = roomId;
    }

    //退出房间
    public void ExitRoom()
    {
        InRoom = false;
    }
}
