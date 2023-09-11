//@LeeTools
//------------------------
//Filename：UIBase.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/11 20:17:07
//Function：Nothing
//------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace Game
{
    public class UIBase : UIFormLogic
    {
        public const int DepthFactor = 100;
        private const float FadeTime = 0.3f;

        private static Font s_MainFont = null;
        private Canvas m_CachedCanvas = null;
        private CanvasGroup m_CanvasGroup = null;

        private List<Canvas> m_CachedCanvasContainer = new List<Canvas>();

        public int OriginalDepth
        {
            get; 
            private set; 
        }

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
            else
            {
                StartCoroutine(CloseCoroutine(FadeTime));
            }
        }

        private IEnumerator CloseCoroutine(float FadeTime)
        {
            yield return m_CanvasGroup.FadeToAlpha(0, FadeTime);
            GameRuntime.UI.CloseUIForm(this);
        }

        public static void SetMainFont(Font font)
        {
            if(s_MainFont == null)
            {
                Log.Error("Main font is invalid.");
                return;
            }

            s_MainFont = font;
        }

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

            m_CachedCanvas = gameObject.GetOrAddComponent<Canvas>();
            m_CachedCanvas.overrideSorting = true;
            OriginalDepth = m_CachedCanvas.sortingOrder;

            m_CanvasGroup = gameObject.GetOrAddComponent<CanvasGroup>();

            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.sizeDelta = Vector2.zero;

            gameObject.GetOrAddComponent<GraphicRaycaster>();

            Text[] texts = gameObject.GetComponentsInChildren<Text>(true);
            foreach(Text text in texts)
            {
                text.font = s_MainFont;
                if (string.IsNullOrEmpty(text.text))
                {
                    text.text = GameRuntime.Localization.GetString(text.text);
                }
            }
        }

        protected override void OnRecycle()
        {
            base.OnRecycle();
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);

            m_CanvasGroup.alpha = 0f;
            StopAllCoroutines();
            StartCoroutine(m_CanvasGroup.FadeToAlpha(1, FadeTime));
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
        }

        protected override void OnPause()
        {
            base.OnPause();
        }

        protected override void OnResume()
        {
            base.OnResume();

            m_CanvasGroup.alpha = 0f;
            StopAllCoroutines();
            StartCoroutine(m_CanvasGroup.FadeToAlpha(1, FadeTime));
        }

        protected override void OnCover()
        {
            base.OnCover();
        }

        protected override void OnReveal()
        {
            base.OnReveal();
        }

        protected override void OnRefocus(object userData)
        {
            base.OnRefocus(userData);
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
        }

        protected override void OnDepthChanged(int uiGroupDepth, int depthInUIGroup)
        {
            int oldDepth = Depth;
            base.OnDepthChanged(uiGroupDepth, depthInUIGroup);
            int deltaDepth = UIBaseGroupHelper.DepthFactor * uiGroupDepth + DepthFactor * depthInUIGroup - oldDepth + OriginalDepth;
            GetComponentsInChildren(true, m_CachedCanvasContainer);
            for(int i = 0; i < m_CachedCanvasContainer.Count; i++)
            {
                m_CachedCanvasContainer[i].sortingOrder += deltaDepth;
            }

            m_CachedCanvasContainer.Clear();
        }
    }
}