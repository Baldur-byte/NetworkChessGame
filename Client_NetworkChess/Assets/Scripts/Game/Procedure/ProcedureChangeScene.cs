using GameFramework.Event;
using GameFramework.Procedure;
using System.Runtime.CompilerServices;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Game
{
    public class ProcedureChangeScene : ProcedureBase
    {
        private bool m_IsChangeSceneComplete = false;

        private SceneType m_TargetSceneType;

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            m_IsChangeSceneComplete = false;

            GameRuntime.Event.Subscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
            GameRuntime.Event.Subscribe(LoadSceneFailureEventArgs.EventId, OnLoadSceneFailure);
            GameRuntime.Event.Subscribe(LoadSceneUpdateEventArgs.EventId, OnLoadSceneUpdate);
            GameRuntime.Event.Subscribe(LoadSceneDependencyAssetEventArgs.EventId, OnLoadSceneDependencyAsset);

            //停止所有声音
            GameRuntime.Sound.StopAllLoadingSounds();
            GameRuntime.Sound.StopAllLoadedSounds();

            //隐藏所有实体
            GameRuntime.Entity.HideAllLoadingEntities();
            GameRuntime.Entity.HideAllLoadedEntities();

            //卸载所有场景
            string[] loadedSceneAssetNames = GameRuntime.Scene.GetLoadedSceneAssetNames();
            for(int i = 0; i < loadedSceneAssetNames.Length; i++)
            {
                GameRuntime.Scene.UnloadScene(loadedSceneAssetNames[i]);
            }

            //还原游戏速度
            GameRuntime.Base.ResetNormalGameSpeed();

            m_TargetSceneType = (SceneType)procedureOwner.GetData<VarInt32>(Constant.FsmDataKey.NextSceneId).Value;

            //加载场景
            GameRuntime.Scene.LoadScene(AssetUtility.GetSceneAsset(m_TargetSceneType.ToString()), Constant.AssetPriority.SceneAsset, this);
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            GameRuntime.Event.Unsubscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
            GameRuntime.Event.Unsubscribe(LoadSceneFailureEventArgs.EventId, OnLoadSceneFailure);
            GameRuntime.Event.Unsubscribe(LoadSceneUpdateEventArgs.EventId, OnLoadSceneUpdate);
            GameRuntime.Event.Unsubscribe(LoadSceneDependencyAssetEventArgs.EventId, OnLoadSceneDependencyAsset);

            base.OnLeave(procedureOwner, isShutdown);
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            if(m_IsChangeSceneComplete)
            {

            }
        }

        private void OnLoadSceneSuccess(object sender, GameEventArgs e)
        {

        }

        private void OnLoadSceneFailure(object sender, GameEventArgs e)
        {
        }

        private void OnLoadSceneUpdate(object sender, GameEventArgs e)
        {
        }

        private void OnLoadSceneDependencyAsset(object sender, GameEventArgs e)
        {
        }
    }
}