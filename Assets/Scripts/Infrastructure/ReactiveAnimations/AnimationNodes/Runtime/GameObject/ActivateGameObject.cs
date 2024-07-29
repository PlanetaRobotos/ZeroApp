using System;
using System.ComponentModel;
using System.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.ReactiveAnimations.AnimationNodes.Runtime.GameObject
{
    [Serializable]
    [Category("GameObject")]
    public class ActivateGameObject : IAnimation
    {
        [SerializeField] private UnityEngine.GameObject _target;
        [SerializeField] private bool _value;
        [SerializeField] private float _delay;

        public void Play()
        {
            if (_delay == 0)
            {
                _target.SetActive(_value);
            }
            else
            {
                SetActiveWithDelay();
            }
        }

        public async void SetActiveWithDelay()
        {
            await Task.Delay(TimeSpan.FromSeconds(_delay));
            _target.SetActive(_value);
        }
    }
}