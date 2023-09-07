using NetworkChess;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

public class UIGameEntryForm : UIFormLogic
{
    [SerializeField]
    private Slider m_slider;

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        m_slider.value = 0;
    }

    protected override void OnRecycle()
    {
        base.OnRecycle();
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
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
        m_slider.value += elapseSeconds / 100;
        if(m_slider.value >=1)
        {
            GameRuntime.Scene.LoadScene(SceneAssetName.Login);
            GameRuntime.UI.OpenUIForm(UIFormAssetName.LoginForm, UIGroupName.Default);
        }
    }

    protected override void OnDepthChanged(int uiGroupDepth, int depthInUIGroup)
    {
        base.OnDepthChanged(uiGroupDepth, depthInUIGroup);
    }

    protected override void InternalSetVisible(bool visible)
    {
        base.InternalSetVisible(visible);
    }
}