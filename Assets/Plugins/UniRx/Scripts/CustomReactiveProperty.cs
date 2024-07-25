using UniRx;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CustomReactiveProperty<T> : IObservable<T>, IDisposable
{
    private readonly ReactiveProperty<T> reactiveProperty;
    private readonly List<IObserver<T>> subscribers = new();
    
    private bool disposed;

    public CustomReactiveProperty()
    {
        reactiveProperty = new ReactiveProperty<T>();
    }

    public CustomReactiveProperty(T initialValue)
    {
        reactiveProperty = new ReactiveProperty<T>(initialValue);
    }

    public IDisposable Subscribe(IObserver<T> observer)
    {
        if (disposed) throw new ObjectDisposedException(nameof(CustomReactiveProperty<T>));

        subscribers.Add(observer);
        IDisposable subscription = reactiveProperty.Subscribe(observer);
        return Disposable.Create(() =>
        {
            subscription.Dispose();
            subscribers.Remove(observer);
        });
    }

    public T Value
    {
        get => reactiveProperty.Value;
        set => reactiveProperty.Value = value;
    }

    public bool HasObservers => subscribers.Count > 0;

    public IEnumerable<IObserver<T>> Subscribers => subscribers;

    public void Dispose()
    {
        if (!disposed)
        {
            reactiveProperty.Dispose();
            subscribers.Clear();
            disposed = true;
        }
    }
}