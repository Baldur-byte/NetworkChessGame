//@LeeTools
//------------------------
//Filename：GameRuntime.Custom.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/11 20:23:22
//Function：Nothing
//------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 游戏入口
    /// </summary>
    public partial class GameRuntime : MonoBehaviour
    {
        public static BuiltinDataComponent BuiltinData
        {
            get; 
            private set; 
        }

        public static UIFormTemplate UIFormTemplate
        {
            get; 
            private set; 
        }

        public static ProtobufComponent Protobuf
        {
            get; 
            private set; 
        }

        private static void InitCustomComponents()
        {
            BuiltinData = UnityGameFramework.Runtime.GameEntry.GetComponent<BuiltinDataComponent>();
            UIFormTemplate = UnityGameFramework.Runtime.GameEntry.GetComponent<UIFormTemplate>();
            Protobuf = UnityGameFramework.Runtime.GameEntry.GetComponent<ProtobufComponent>();
        }

        private static void InitCustomDebuggers()
        {
        }
    }
}