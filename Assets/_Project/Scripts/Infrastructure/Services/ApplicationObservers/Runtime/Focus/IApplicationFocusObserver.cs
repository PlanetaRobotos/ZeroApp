using System;

namespace _Project.Scripts.Infrastructure.Services.ApplicationObservers.Runtime.Focus
{
    public interface IApplicationFocusObserver
    {
        void AddSubscriber(Action<bool> subscriber);
        void RemoveSubscriber(Action<bool> subscriber);
    }
}