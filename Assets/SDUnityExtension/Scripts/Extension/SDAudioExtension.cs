using DG.Tweening;
using SDUnityExtension.Scripts.Manager;
using UnityEngine;

namespace SDUnityExtension.Scripts.Extension
{
    public static class SDAudioExtension
    {
        public static void Fade(this AudioSource source, float duration, float volume = 1f)
        {
            source.DOFade(volume, duration);
        }

        public static void PlayClip(this AudioSource source, AudioClip clip, float volume = 1f, float pitch = 1f)
        {
            if (clip == null) return;
            source.volume = volume;
            source.pitch = pitch;
            source.clip = clip;
            source.Play();
        }
    }
}
