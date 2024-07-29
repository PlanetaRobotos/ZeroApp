using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Windows.Particles
{
    public class MainUIParticlesObserver : MonoBehaviour
    {
        private readonly List<Action<bool>> _listeners = new();

        public void AddListener(Action<bool> listener)
        {
            if (_listeners.Contains(listener))
                return;

            _listeners.Add(listener);
        }

        public void RemoveListener(Action<bool> listener)
        {
            _listeners.Remove(listener);
        }

        public void Publish(bool isVisible)
        {
            for (var i = 0; i < _listeners.Count; i++)
                _listeners[i]?.Invoke(isVisible);
        }
    }
}