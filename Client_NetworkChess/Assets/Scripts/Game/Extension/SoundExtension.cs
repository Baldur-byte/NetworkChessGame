using GameFramework;
using GameFramework.Sound;
using UnityGameFramework.Runtime;

namespace Game
{
    public static class SoundExtension
    {
        private const float DefaultFadeVolumeDuration = 1f;

        private static int? s_MusicSerialId = null;

        public static void StopMusic(this SoundComponent soundComponent, float fadeOutSeconds = DefaultFadeVolumeDuration)
        {
            if (s_MusicSerialId.HasValue)
            {
                soundComponent.StopSound(s_MusicSerialId.Value, fadeOutSeconds);
                s_MusicSerialId = null;
            }
        }

        public static int? PlayMusic(this SoundComponent soundComponent, int musicId, object userData = null)
        {
            soundComponent.StopMusic();

            PlaySoundParams playSoundParams = new PlaySoundParams();
            playSoundParams.Priority = 64;
            playSoundParams.Loop = true;
            playSoundParams.VolumeInSoundGroup = 1f;
            playSoundParams.FadeInSeconds = DefaultFadeVolumeDuration;
            playSoundParams.SpatialBlend = 0f;

            s_MusicSerialId = soundComponent.PlaySound(AssetUtility.GetMusicAsset(""), "Music", Constant.AssetPriority.MusicAsset, playSoundParams, null, userData);
            
            return s_MusicSerialId;
        }

        public static int? PlaySound(this SoundComponent soundComponent, int soundId, EntityObject bindingEntity = null, object userData = null)
        {
            PlaySoundParams soundParams = new PlaySoundParams();
            soundParams.Priority = 64;
            soundParams.Loop = false;
            soundParams.VolumeInSoundGroup = 1f;
            soundParams.SpatialBlend= 0f;
            return soundComponent.PlaySound(AssetUtility.GetSoundAsset(""), "Sound", Constant.AssetPriority.SoundAsset, soundParams, bindingEntity != null ? bindingEntity.Entity : null, userData);
        }

        public static bool IsMuted(this SoundComponent soundComponent, string soundGroupName)
        {
            if(string.IsNullOrEmpty(soundGroupName))
            {
                Log.Warning("Sound group is invalid.");
                return true;
            }

            ISoundGroup soundGroup = soundComponent.GetSoundGroup(soundGroupName);
            if (soundGroup == null)
            {
                Log.Warning("Sound group '{0}' is invalid.", soundGroupName);
                return true;
            }

            return soundGroup.Mute;
        }

        public static void Mute(this SoundComponent soundComponent, string soundGroupName, bool mute)
        {
            if (string.IsNullOrEmpty(soundGroupName))
            {
                Log.Warning("Sound group is invalid.");
                return;
            }

            ISoundGroup soundGroup = soundComponent.GetSoundGroup(soundGroupName);
            if (soundGroup == null)
            {
                Log.Warning("Sound group '{0}' is invalid.", soundGroupName);
                return;
            }

            soundGroup.Mute = mute;

            GameRuntime.Setting.SetBool(Utility.Text.Format(Constant.Setting.SoundGroupMuted, soundGroupName), mute);
            GameRuntime.Setting.Save();
        }

        public static float GetVolume(this SoundComponent soundComponent, string soundGroupName)
        {
            if (string.IsNullOrEmpty(soundGroupName))
            {
                Log.Warning("Sound group is invalid.");
                return 0f;
            }
            ISoundGroup soundGroup = soundComponent.GetSoundGroup(soundGroupName);
            if (soundGroup == null)
            {
                Log.Warning("Sound group '{0}' is invalid.", soundGroupName);
                return 0f;
            }

            return soundGroup.Volume;
        }

        public static void SetVolume(this SoundComponent soundComponent, string soundGroupName, float volume)
        {
            if (string.IsNullOrEmpty(soundGroupName))
            {
                Log.Warning("Sound group is invalid.");
                return;
            }
            ISoundGroup soundGroup = soundComponent.GetSoundGroup(soundGroupName);
            if (soundGroup == null)
            {
                Log.Warning("Sound group '{0}' is invalid.", soundGroupName);
                return;
            }
            soundGroup.Volume = volume;
            GameRuntime.Setting.SetFloat(Utility.Text.Format(Constant.Setting.SoundGroupVolume, soundGroupName), volume);
            GameRuntime.Setting.Save();
        }
    }
}