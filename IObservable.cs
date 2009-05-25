using System;

namespace Reactive
{
    public interface IObservable<T>
    {
        void Attach(Action<T> action);
    }
}