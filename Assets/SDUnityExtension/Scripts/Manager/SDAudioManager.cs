using System.Collections;
using SDUnityExtension.Scripts.Pattern;
using UnityEngine;

namespace SDUnityExtension.Scripts.Manager
{
    /// <summary>
    /// Created by sweetSD. (with Singleton)
    /// 
    /// 사운드 관련 기능을 제공하는 사운드 매니저 클래스입니다.
    /// 
    /// </summary>
    public class SDAudioManager : SDSingleton<SDAudioManager>
    {
        private void Awake()
        {
            SetInstance(this);
        }

        public void FadeIn(AudioSource source, float duration, float volume = 1f)
        {
            StartCoroutine(CO_FadeIn(source, duration, volume));
        }

        public void FadeOut(AudioSource source, float duration, float volume = 0f)
        {
            StartCoroutine(CO_FadeOut(source, duration, volume));
        }

        private IEnumerator CO_FadeIn(AudioSource source, float duration, float volume)
        {
            float elapsedTime = 0f;
            float beginVolume = 0;
            source.Play();
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                source.volume = SDMath.Map(elapsedTime, 0, duration, beginVolume, volume);
                yield return null;
            }
            source.volume = volume;
        }

        private IEnumerator CO_FadeOut(AudioSource source, float duration, float volume)
        {
            float elapsedTime = 0f;
            float beginVolume = 1;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                source.volume = beginVolume - SDMath.Map(elapsedTime, 0, duration, 0, 1 - volume);
                yield return null;
            }
            source.volume = volume;
            source.Stop();
        }
    }
}