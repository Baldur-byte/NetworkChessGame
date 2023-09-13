//@LeeTools
//------------------------
//Filename：ProtobufComponent.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/13 19:48:06
//Function：Nothing
//------------------------
using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityGameFramework.Runtime;

namespace Game
{
    public class ProtobufComponent : GameFrameworkComponent
    {
        private Dictionary<string , MessageParser> m_MessageParsers = new Dictionary<string , MessageParser>();
        private Dictionary<string, MessageDescriptor> m_MessageDescriptors = new Dictionary<string, MessageDescriptor>();

        private List<string> protoFiles = new List<string>
        {
            "BaseMessage",
            "CommMessage",
            "FriendMessage",
            "GameMessage",
            "PlayerMessage",
            "RoomMessage",
        };

        public void InitProtobuf()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            foreach (var type in assembly.GetTypes())
            {
                if (type.BaseType == typeof(IMessage) && !type.IsAbstract)
                {
                    //利用反射实例化泛型类型
                    Type type1 = typeof(MessageParser<>);
                    Type[] typeArgs = { type };
                    Type constructed = type1.MakeGenericType(typeArgs);
                    IMessage factory = (IMessage)Activator.CreateInstance(type);
                    Func<IMessage> func = () => factory;
                    MessageParser messageParser = (MessageParser)Activator.CreateInstance(constructed, func);
                    m_MessageParsers.Add(type.Name, messageParser);

                    Log.Info("MessageParser: " + type.Name);
                }
                else if (protoFiles.Contains(type.Name))
                {
                    BindingFlags flags = BindingFlags.Static | BindingFlags.Public;

                    FieldInfo field = type.GetField("Descriptor", flags);
                    if (field != null)
                    {
                        FileDescriptor fileDescriptor = (FileDescriptor)field.GetValue(null);
                        foreach (var des in fileDescriptor.MessageTypes)
                        {
                            m_MessageDescriptors.Add(des.ClrType.Name, des);
                            Log.Info("MessageDescriptor: " + des.Name);
                        }
                    }
                }
            }
        }

        public IMessage CreateMessage(string typename, byte[] data)
        {
            IMessage message = null;
            MessageParser parser = null;
            if (m_MessageParsers.TryGetValue(typename, out parser))
            {
                message = parser.ParseFrom(data);
            }

            if(message != null)
            {
                return message;
            }

            Log.Error("Can not parse message: {0} through method MessageParse.", typename);

            MessageDescriptor descriptor;
            if (m_MessageDescriptors.TryGetValue(typename, out descriptor))
            {
                message = descriptor.Parser.ParseFrom(data);
            }

            if(message == null)
            {
                Log.Error("Can not parse message: {0} through method Descriptor.Parser.ParseFrom.", typename);
            }

            return message;
        }
    }
}