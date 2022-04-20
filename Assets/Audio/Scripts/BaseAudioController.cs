using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Audio.Scripts
{
    /// <summary>
    ///     Inspired by https://answers.unity.com/questions/642431/dictionary-in-inspector.html
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class BaseAudioController : MonoBehaviour
    {
        [Serializable]
        public struct NamedSound
        {
            public string clipName;
            public AudioClip clip;
        }

        [SerializeField] private NamedSound[] _clips = { };
        private AudioSource _audioSource;
        private readonly Dictionary<string, AudioSource> _auxiliaryAudioSources = new Dictionary<string, AudioSource>();

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayOnce(string clipName, float volumeModifier = 0)
        {
            var audioClip = _clips.FirstOrDefault(clip => string.Equals(clip.clipName, clipName, StringComparison.CurrentCultureIgnoreCase)).clip;
            if (audioClip != null)
            {
                _audioSource.volume = 1 - volumeModifier;
                _audioSource.PlayOneShot(audioClip);
            }
            else
            {
                Debug.LogError($"Clip with name {clipName} is not registered!");
            }
        }

        public void PlayLooping(string clipName, float delayInSeconds = 0.08f)
        {
            var audioClip = _clips.FirstOrDefault(clip => string.Equals(clip.clipName, clipName, StringComparison.CurrentCultureIgnoreCase)).clip;
            if (audioClip != null)
            {
                if (!_auxiliaryAudioSources.ContainsKey(clipName))
                {
                    var auxiliaryAudioSource =
                        _auxiliaryAudioSources[clipName] = gameObject.AddComponent<AudioSource>();
                    auxiliaryAudioSource.clip = audioClip;
                    auxiliaryAudioSource.loop = true;
                    auxiliaryAudioSource.PlayDelayed(delayInSeconds);
                }
            }
            else
            {
                Debug.LogError($"Clip with name {clipName} is not registered!");
            }
        }

        public void StopLooping(string clipName)
        {
            if(_auxiliaryAudioSources.TryGetValue(clipName, out var auxAudioSource))
            {
                auxAudioSource.Stop();
                _auxiliaryAudioSources.Remove(clipName);
                Destroy(auxAudioSource);
            }
        }
    }
}