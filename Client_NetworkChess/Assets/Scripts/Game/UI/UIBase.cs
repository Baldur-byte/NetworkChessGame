using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game
{
    public class UIBase : UIFormLogic
    {
        public const int DepthFactor = 100;
        private const float FadeTime = 0.3f;

        private static Font s_MainFont = null;
        private Canvas m_CachedCanvas = null;
        private CanvasGroup m_CachedCanvasGroup = null;

        private RectTransform m_CachedRectTransform = null;

        public int Depth
        {
            get
            {
                return m_CachedCanvas.sortingOrder;
            }
            set
            {
                m_CachedCanvas.sortingOrder = value;
            }
        }

        public void Close(bool ignoreFade = true)
        {
            StopAllCoroutines();

            if(ignoreFade)
            {
                GameRuntime.UI.CloseUIForm(this);
            }
        }
    }
}