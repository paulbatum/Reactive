using System;

namespace Reactive
{
    public class SelectObservable<A, B> : IObservable<B>
    {
        private readonly IObservable<A> _inner;
        private readonly Func<A, B> _selector;

        public SelectObservable(IObservable<A> inner, Func<A, B> selector)
        {
            _inner = inner;
            _selector = selector;
        }

        public IDisposable Subscribe(IObserver<B> observer)
        {
            return _inner.Subscribe(new ObserverConversion<B, A>(observer, _selector));
        }
    }

    public class ObserverConversion<A, B> : IObserver<B>
    {
        private readonly IObserver<A> _inner;
        private readonly Func<B, A> _selector;

        public ObserverConversion(IObserver<A> inner, Func<B, A> selector)
        {
            _inner = inner;
            _selector = selector;
        }

        public void OnNext(B b)
        {
            _inner.OnNext(_selector(b));
        }

        public void OnDone()
        {
            _inner.OnDone();
        }

        public void OnError(Exception e)
        {
            _inner.OnError(e);
        }
    }
}