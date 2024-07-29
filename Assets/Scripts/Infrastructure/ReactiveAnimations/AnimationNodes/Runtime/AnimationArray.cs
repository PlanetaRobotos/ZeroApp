using System;
using ManagedReference;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.ReactiveAnimations.AnimationNodes.Runtime
{
    [Serializable]
    public class AnimationArray : IAnimation
    {
        [SerializeReference, ManagedReference]
        private IAnimation[] _animations; 
        public void Play()
        {
            foreach (var animation in _animations)
            {
                animation.Play();
            }
        }
    }
}