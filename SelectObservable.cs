using System;

namespace Reactive
{
    public class SelectObservable<T1, T2> : IObservable<T2>
    {
        private readonly IObservable<T1> _inner;
        private readonly Func<T1, T2> _selector;

        public SelectObservable(IObservable<T1> inner, Func<T1, T2> selector)
        {
            _inner = inner;
            _selector = selector;
        }

        public IDisposable Subscribe(IObserver<T2> observer)
        {
            return _inner.Subscribe(ObserverBuilder.Create(observer, (T1 a) => observer.OnNext(_selector(a))));
        }
    }

}