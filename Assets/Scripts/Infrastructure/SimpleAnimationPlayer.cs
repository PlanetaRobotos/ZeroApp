using System;
using System.Collections;
using UnityEngine;

namespace _Project.Scripts.Infrastructure
{
    public class SimpleAnimationPlayer : MonoBehaviour
    {
        [SerializeField] private Animation _animation;

        public void Play(string animName) => Play(animName, null);

        public void Play(string animName, Action onComplete)
        {
            var clip = _animation.GetClip(animName);

            if (clip != null)
            {
                if (IsPlaying())
                {
                    Stop();
                    StopAllCoroutines();
                }

                _animation.Play(animName);
                _animation.Sample();
                StartCoroutine(PlayingAnimCoroutine(onComplete));
            }
        }

        public void Play(string animName, float delay, Action onComplete = null)
        {
            StartCoroutine(PlayingAnimCoroutine(animName, delay, onComplete));
        }

        private IEnumerator PlayingAnimCoroutine(string animName, float delay, Action onComplete = null)
        {
            while (delay > 0f)
            {
                delay -= Time.deltaTime;
                yield return null;
            }

            Play(animName, onComplete);
        }

        private IEnumerator PlayingAnimCoroutine(Action onComplete = null)
        {
            while (_animation.isPlaying)
                yield return null;

            onComplete?.Invoke();
        }

        public bool IsPlaying() => _animation.isPlaying;

        public bool IsPlaying(string animName) => _animation.IsPlaying(animName);

        public float GetAnimLength(string animName) => _animation.GetClip(animName)?.length ?? 0f;

        public void SetClipDuration(string animName, float targetDuration)
        {
            if (_animation[animName] == null)
            {
                Debug.LogError("Animation clip not found.");
                return;
            }

            float currentDuration = GetAnimLength(animName);
            float newSpeed = currentDuration / targetDuration;

            _animation[animName].speed = newSpeed;
        }

        public void Stop()
        {
            _animation.Stop();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_animation == null)
                _animation = GetComponent<Animation>();
        }
#endif
    }
}