//@LeeTools
//------------------------
//Filename：GameRuntime.Custom.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/11 20:23:22
//Function：Nothing
//------------------------

using ChessGame;
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

        public static ChessLogicComponent ChessLogic
        {
            get;
            private set;
        }

        public static PlayerComponent Player
        {
            get;
            private set;
        }

        private static void InitCustomComponents()
        {
            BuiltinData = UnityGameFramework.Runtime.GameEntry.GetComponent<BuiltinDataComponent>();
            UIFormTemplate = UnityGameFramework.Runtime.GameEntry.GetComponent<UIFormTemplate>();
            Protobuf = UnityGameFramework.Runtime.GameEntry.GetComponent<ProtobufComponent>();
            ChessLogic = UnityGameFramework.Runtime.GameEntry.GetComponent<ChessLogicComponent>();
            Player = UnityGameFramework.Runtime.GameEntry.GetComponent<PlayerComponent>();
        }

        private static void InitCustomDebuggers()
        {
        }
    }
}