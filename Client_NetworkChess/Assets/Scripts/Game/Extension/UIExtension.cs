//@LeeTools
//------------------------
//Filename：UIExtension.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/11 20:23:48
//Function：Nothing
//------------------------

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

        public static int? OpenUIForm(this UIComponent uiComponent, UIFormId uiFormId, object userData = null)
        {
            string assetName = AssetUtility.GetUIFormAsset(uiFormId);
            return uiComponent.OpenUIForm(assetName, userData);
            //return uiComponent.OpenUIForm((int)uiFormId, userData);
        }

        public static int? OpenUIForm(this UIComponent uiComponent, int uiFormId, object userData = null)
        {
            //通过读表获取id 对应的界面资源的名称
            string assetName = AssetUtility.GetUIFormAsset("");
            string groupName = "Default";
            return uiComponent.OpenUIForm(assetName, groupName, Constant.AssetPriority.UIFormAsset, userData);
        }

        public static int? OpenUIForm(this UIComponent uiComponent, string assetName, object userData = null)
        {
            string groupName = "Default";
            return uiComponent.OpenUIForm(assetName, groupName, Constant.AssetPriority.UIFormAsset, userData);
        }

        public static void CloseUIForm(this UIComponent uiComponent, UIBase uiBase)
        {
            uiComponent.CloseUIForm(uiBase.UIForm);
        }

        public static void OpenDialog(this UIComponent uiComponent, DialogParams dialogParams)
        {
            if (((ProcedureBase)GameRuntime.Procedure.CurrentProcedure).UseNativeDialog)
            {
                OpenNativeDialog(dialogParams);
            }
            else
            {
                uiComponent.OpenUIForm(UIFormId.DialogForm, dialogParams);
            }
        }

        public static void OpenNativeDialog(DialogParams dialogParams)
        {

        }
    }
}