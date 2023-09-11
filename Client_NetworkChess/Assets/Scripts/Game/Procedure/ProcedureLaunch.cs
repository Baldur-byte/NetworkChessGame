//@LeeTools
//------------------------
//Filename：ProcedureLaunch.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/11 18:24:34
//Function：Nothing
//------------------------

using GameFramework.Localization;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Game
{
    public class ProcedureLaunch : ProcedureBase
    {
        public override bool UseNativeDialog => true;

        // 游戏初始化时执行。
        protected override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);
        }

        // 每次进入这个流程时执行。
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            //一些初始化设置  如 构建信息  语言设置  资源加载方式  声音配置  字典文件

            // 构建信息：发布版本时，把一些数据以 Json 的格式写入 Assets/GameMain/Configs/BuildInfo.txt，供游戏逻辑读取
            GameRuntime.BuiltinData.InitBuildInfo();

            // 语言配置：设置当前使用的语言，如果不设置，则默认使用操作系统语言
            InitLanguageSettings();

            // 资源变体配置：根据使用的语言，通知底层加载对应的资源变体
            InitCurrentVariant();

            // 声音配置：根据用户配置数据，设置即将使用的声音选项
            InitSoundSettings();

            // 默认字典：加载默认字典文件 Assets/GameMain/Configs/DefaultDictionary.xml
            // 此字典文件记录了资源更新前使用的各种语言的字符串，会随 App 一起发布，故不可更新
            GameRuntime.BuiltinData.InitDefaultDictionary();
        }

        // 每次轮询执行。
        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            //运行一帧切换到 Splash 展示流程
            ChangeState<ProcedureSplash>(procedureOwner);
        }

        // 每次离开这个流程时执行。
        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
        }

        // 游戏退出时执行。
        protected override void OnDestroy(ProcedureOwner procedureOwner)
        {
            base.OnDestroy(procedureOwner);
        }

        private void InitLanguageSettings()
        {
            if(GameRuntime.Base.EditorResourceMode && GameRuntime.Base.EditorLanguage != Language.Unspecified)
            {
                // 编辑器资源模式直接使用 Inspector 上设置的语言
                return;
            }

            Language language = GameRuntime.Localization.Language;
            if (GameRuntime.Setting.HasSetting(Constant.Setting.Language))
            {
                string languageString = GameRuntime.Setting.GetString(Constant.Setting.Language);
                if (string.IsNullOrEmpty(languageString))
                {
                    return;
                }
                try
                {
                    language = (Language)System.Enum.Parse(typeof(Language), languageString);
                }
                catch
                {
                }
            }

            if(language != Language.English && language != Language.ChineseSimplified && language != Language.ChineseTraditional)
            {
                language = Language.English;

                GameRuntime.Setting.SetString(Constant.Setting.Language, language.ToString());
                GameRuntime.Setting.Save();
            }

            GameRuntime.Localization.Language = language;
            Log.Info("Init language settings complete, current language is '{0}'.", language.ToString());
        }

        private static void InitCurrentVariant()
        {
            if(GameRuntime.Base.EditorResourceMode)
            {
                // 编辑器资源模式不使用 AssetBundle，也就没有变体了
                return;
            }

            string currentVariant = null;
            switch (GameRuntime.Localization.Language)
            {
                case Language.English:
                    currentVariant = "en-us";
                    break;
                case Language.ChineseSimplified:
                    currentVariant = "zh-cn";
                    break;
                case Language.ChineseTraditional:
                    currentVariant = "zh-tw";
                    break;
                default:
                    currentVariant = "zh-cn";
                    break;
            }

            GameRuntime.Resource.SetCurrentVariant(currentVariant);
            Log.Info("Init current variant complete.");
        }

        private static void InitSoundSettings()
        {
            GameRuntime.Sound.Mute("Music", GameRuntime.Setting.GetBool(Constant.Setting.MusicMuted, false));
            GameRuntime.Sound.SetVolume("Music", GameRuntime.Setting.GetFloat(Constant.Setting.MusicVolume, 0.3f));
            GameRuntime.Sound.Mute("Sound", GameRuntime.Setting.GetBool(Constant.Setting.SoundMuted, false));
            GameRuntime.Sound.SetVolume("Sound", GameRuntime.Setting.GetFloat(Constant.Setting.SoundVolume, 1f));
            Log.Info("Init sound settings complete.");
        }
    }
}