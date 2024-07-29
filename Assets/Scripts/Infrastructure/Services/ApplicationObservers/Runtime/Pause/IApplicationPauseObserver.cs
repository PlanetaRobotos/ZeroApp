using System;

namespace _Project.Scripts.Infrastructure.Services.ApplicationObservers.Runtime.Pause
{
    public interface IApplicationPauseObserver
    {
        void AddSubscriber(Action<bool> subscriber);
        void RemoveSubscriber(Action<bool> subscriber);
    }
}