﻿using SDUnityExtension.Scripts.Manager;
using UnityEngine;

namespace SDUnityExtension.Scripts.Extension
{
    /// <summary>
    /// Created by sweetSD.
    /// 
    /// Audio 관련된 기능을 좀 더 편리하게 사용할 수 있도록 도움을 주는 함수가 선언된 Extension 클래스입니다.
    /// </summary>
    public static class SDAudioExtension
    {
        public static void FadeIn(this AudioSource source, float duration, float volume = 1f)
        {
            SDAudioManager.I.FadeIn(source, duration, volume);
        }

        public static void FadeOut(this AudioSource source, float duration, float volume = 0f)
        {
            SDAudioManager.I.FadeOut(source, duration, volume);
        }

        public static void PlayClip(this AudioSource source, AudioClip clip, float volume = 1f, float pitch = 1f)
        {
            if (clip)
            {
                source.volume = volume;
                source.pitch = pitch;
                source.clip = clip;
                source.Play();
            }
        }
    }
}
