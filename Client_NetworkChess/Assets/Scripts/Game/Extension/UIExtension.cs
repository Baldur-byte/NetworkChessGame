using GameFramework.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace Game
{
    public static class UIExtension
    {
        public static IEnumerator FadeToAlpha(this CanvasGroup canvasGroup, float targetAlpha, float duration)
        {
            float time = 0;
            float originalAlpha = 0;
            while (time < duration)
            {
                time += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(originalAlpha, targetAlpha, time / duration);
                yield return new WaitForEndOfFrame();
            }

            canvasGroup.alpha = targetAlpha;
        }

        public static IEnumerator SoomthValue(this Slider slider, float targetValue, float duration)
        {
            float time = 0;
            float originalValue = slider.value;
            while (time < duration)
            {
                time += Time.deltaTime;
                slider.value = Mathf.Lerp(originalValue, targetValue, time / duration);
                yield return new WaitForEndOfFrame();
            }

            slider.value = targetValue;
        }

        public static bool HasUIForm(this UIComponent uiComponent, UIFormId uiFormId, string uiGroupName = null)
        {
            return uiComponent.HasUIForm((int)uiFormId, uiGroupName);
        }

        public static bool HasUIForm(this UIComponent uiComponent, int uiFormId, string uiGroupName = null)
        {
            //通过读表获取id 对应的界面资源的名称
            string assetName = AssetUtility.GetUIFormAsset("");
            if (string.IsNullOrEmpty(uiGroupName))
            {
                return uiComponent.HasUIForm
                    (assetName);
            }

            IUIGroup uiGroup = uiComponent.GetUIGroup(uiGroupName);
            if (uiGroup == null)
            {
                return false;
            }

            return uiGroup.HasUIForm(assetName);
        }

        public static UIBase GetUIBase(this UIComponent uiComponent, UIFormId uiFormId, string uiGroupName = null)
        {
            return uiComponent.GetUIBase((int)uiFormId, uiGroupName);
        }

        public static UIBase GetUIBase(this UIComponent uiComponent, int uiFormId, string uiGroupName = null)
        {
            //通过读表获取id 对应的界面资源的名称
            string assetName = AssetUtility.GetUIFormAsset("");

            UIForm result = null;

            if(string.IsNullOrEmpty(uiGroupName))
            {
                result = uiComponent.GetUIForm(assetName);
            }
            else
            {
                IUIGroup uiGroup = uiComponent.GetUIGroup(uiGroupName);
                if(uiGroup != null)
                {
                    result = (UIForm)uiGroup.GetUIForm(assetName);
                }
            }

            return (UIBase)result.Logic;
        }

        public static void CloseUIForm(this UIComponent uiComponent, UIBase uiBase)
        {
            uiComponent.CloseUIForm(uiBase.UIForm);
        }
    }
}