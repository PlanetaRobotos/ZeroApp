using System;
using System.ComponentModel;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.ReactiveAnimations.AnimationNodes.Runtime.Animation
{
    [Serializable]
    [Category("Animation")]
    public class PlayAnimation : IAnimation
    {
        [SerializeField] private UnityEngine.Animation _animation;
        [SerializeField] private string _animationName;

        public void Play()
        {
            _animation.Stop();
            _animation.Play(_animationName);
        }
    }
}