using System;

namespace _Project.Scripts.Infrastructure.Services.ApplicationObservers.Runtime.Quit
{
  public interface IApplicationQuitObserver
  {
    void AddSubscriber(Action subscriber);
    void RemoveSubscriber(Action subscriber);
  }
}