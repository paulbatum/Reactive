using System;

namespace Reactive
{
    public class WhereObservable<T> : IObservable<T>
    {
        private readonly IObservable<T> _source;
        private readonly Func<T, bool> _predicate;

        public WhereObservable(IObservable<T> source, Func<T, bool> predicate)
        {
            _source = source;
            _predicate = predicate;
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            IObserver<T> whereObserver = ObserverBuilder.Create(observer, (T t) => { if (_predicate(t)) observer.OnNext(t); });
            return _source.Subscribe(whereObserver);
        }
    }
    
}