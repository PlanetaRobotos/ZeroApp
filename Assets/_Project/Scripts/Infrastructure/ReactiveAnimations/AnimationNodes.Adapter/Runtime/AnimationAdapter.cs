using System;
using _Project.Scripts.Infrastructure.ReactiveAnimations.AnimationNodes.Runtime;
using Adapters.Core;
using ManagedReference;
using MVVM.Adapters.Core;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.ReactiveAnimations.AnimationNodes.Adapter.Runtime
{
    [Serializable]
    public class AnimationAdapter : IAdapter<bool>, IAdapter<int>, IAdapter<float>, IAdapter<string>, IReactiveEventAdapter
    {
        [SerializeReference, ManagedReference] private IAnimation _animation;

        public void OnValueChanged(bool value)
        {
            _animation.Play();
        }

        public void OnValueChanged(int value)
        {
            _animation.Play();
        }

        public void OnValueChanged(float value)
        {
            _animation.Play();
            
        }

        public void OnValueChanged(string value)
        {
            _animation.Play();
        }

        public void OnNotify()
        {
            _animation.Play();
        }
    }
}