using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.Services.ApplicationObservers.Runtime
{
    public interface IUpdater
    {
        /// <summary>
        /// Subscribe in order to have onUpdate called approximately every period seconds (or every frame, if period <= 0).
        /// Don't assume that onUpdate will be called in any particular order compared to other subscribers.
        /// </summary>
        void Subscribe([NotNull] Action<float> onUpdate, float updatePeriod);

        /// <summary>
        /// Safe to call even if onUpdate was not previously Subscribed.
        /// </summary>
        void Unsubscribe(Action<float> onUpdate);
    }

    /// <summary>
    /// Some objects might need to be on a slower update loop than the usual MonoBehaviour Update and without precise timing, e.g. to refresh data from services.
    /// Some might also not want to be coupled to a Unity object at all but still need an update loop.
    /// </summary>
    public class Updater : MonoBehaviour, IUpdater
    {
        private readonly Queue<Action> _pendingHandlers = new();
        private readonly HashSet<Action<float>> _subscribers = new();
        private readonly Dictionary<Action<float>, SubscriberData> _subscriberData = new();

        public void OnDestroy()
        {
            _pendingHandlers.Clear();
            _subscribers.Clear();
            _subscriberData.Clear();
        }

        /// <summary>
        /// Subscribe in order to have onUpdate called approximately every period seconds (or every frame, if period <= 0).
        /// Don't assume that onUpdate will be called in any particular order compared to other subscribers.
        /// </summary>
        public void Subscribe([NotNull] Action<float> onUpdate, float updatePeriod)
        {
#if UNITY_EDITOR
            if (onUpdate.Target ==
                null) // Detect a local function that cannot be Unsubscribed since it could go out of scope.
            {
                throw new Exception(
                    "Can't subscribe to a local function that can go out of scope and can't be unsubscribed from");
            }

            if (onUpdate.Method.ToString().Contains("<")) // Detect
            {
                throw new Exception(
                    "Can't subscribe with an anonymous function that cannot be Unsubscribed, by checking for a character that can't exist in a declared method name.");
            }
#endif

            if (!_subscribers.Contains(onUpdate))
            {
                _pendingHandlers.Enqueue(() =>
                {
                    if (_subscribers.Add(onUpdate))
                    {
                        _subscriberData.Add(onUpdate, new SubscriberData()
                        {
                            Period = updatePeriod, 
                            CallTime = Time.time,
                        });
                    }
                });
            }
        }

        /// <summary>
        /// Safe to call even if onUpdate was not previously Subscribed.
        /// </summary>
        public void Unsubscribe(Action<float> onUpdate)
        {
            _pendingHandlers.Enqueue(() =>
            {
                _subscribers.Remove(onUpdate);
                _subscriberData.Remove(onUpdate);
            });
        }

        /// <summary>
        /// Each frame, advance all subscribers. Any that have hit their period should then act, though if they take too long they could be removed.
        /// </summary>
        private void Update()
        {
            while (_pendingHandlers.Count > 0)
            {
                _pendingHandlers.Dequeue()?.Invoke();
            }
            
            foreach (var subscriber in _subscribers)
            {
                var subscriberData = _subscriberData[subscriber];
                var elapsedTime = Time.time - subscriberData.CallTime;
                
                if (subscriberData.Period <= elapsedTime)
                {
                    subscriber.Invoke(elapsedTime);
                    subscriberData.CallTime = Time.time;
                }
            }
        }
        
        private class SubscriberData
        {
            public float Period;
            public float CallTime;
        }
    }
}