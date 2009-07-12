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

    //public class ObservableBuilder
    //{
    //    public static IObservable<T> Create<T>(Func<IObserver<T>, IDisposable> subscribe)
    //    {
    //        return new ObservableWrapper<T>(subscribe);
    //    }

    //    private class ObservableWrapper<T> : IObservable<T>
    //    {
    //        private readonly Func<IObserver<T>, IDisposable> _subscribe;

    //        public ObservableWrapper(Func<IObserver<T>, IDisposable> subscribe)
    //        {
    //            _subscribe = subscribe;
    //        }

    //        public IDisposable Subscribe(IObserver<T> observer)
    //        {
    //            return _subscribe(observer);
    //        }
    //    }
    //}

}