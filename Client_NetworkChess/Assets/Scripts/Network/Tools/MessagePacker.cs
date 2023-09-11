//@LeeTools
//------------------------
//Filename：MessagePacker.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/11 20:26:05
//Function：Nothing
//------------------------

using System;
using System.Collections.Generic;

//消息封装
public class MessagePacker
{
    private List<byte> bytes = new List<byte>();

    public byte[] Package
    {
        get { return bytes.ToArray(); }
    }

    public MessagePacker Add(byte[] data)
    {
        bytes.AddRange(data);
        return this;
    }

    public MessagePacker Add(ushort value)
    {
        byte[] data = BitConverter.GetBytes(value);
        bytes.AddRange(data);
        return this;
    }

    public MessagePacker Add(uint value)
    {
        byte[] data = BitConverter.GetBytes(value);
        bytes.AddRange(data);
        return this;
    }

    public MessagePacker Add(ulong value)
    {
        byte[] data = BitConverter.GetBytes(value);
        bytes.AddRange(data);
        return this;
    }
}
