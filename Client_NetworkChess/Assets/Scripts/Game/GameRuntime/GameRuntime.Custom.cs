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

        private static void InitCustomComponents()
        {
            BuiltinData = UnityGameFramework.Runtime.GameEntry.GetComponent<BuiltinDataComponent>();
        }

        private static void InitCustomDebuggers()
        {
        }
    }
}