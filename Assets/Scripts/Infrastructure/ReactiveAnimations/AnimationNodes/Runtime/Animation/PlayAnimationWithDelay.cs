using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.ReactiveAnimations.AnimationNodes.Runtime.Animation
{
    [Serializable]
    [Category("Animation")]
    public class PlayAnimationWithDelay : IAnimation
    {
        [SerializeField] private UnityEngine.Animation _animation;
        [SerializeField] private string _animationName;
        [SerializeField] private float _delay;

        private CancellationTokenSource _cancellationTokenSource;
        
        public void Play()
        {
            CancelAnimation();

            if (_delay == 0)
            {
                _animation.Play(_animationName);
            }
            else
            {
                SetActiveWithDelay();
            }
        }

        public async void SetActiveWithDelay()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            try
            {
                await Task.Delay(TimeSpan.FromSeconds(_delay), _cancellationTokenSource.Token);

                _animation.Play(_animationName);
            }
            catch (Exception ex)
            {
                // ignored
            }
            finally
            {
                CancelAnimation();
            }
        }

        private void CancelAnimation()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }
    }
}