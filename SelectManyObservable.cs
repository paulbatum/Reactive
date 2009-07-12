using System;

namespace Reactive
{
    public class SelectManyObservable<TSource, TCollection, TResult> : IObservable<TResult>
    {
        private readonly IObservable<TSource> _source;
        private readonly Func<TSource, IObservable<TCollection>> _collectionSelector;
        private readonly Func<TSource, TCollection, TResult> _resultSelector;

        public SelectManyObservable(IObservable<TSource> source, Func<TSource, IObservable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            _source = source;
            _collectionSelector = collectionSelector;
            _resultSelector = resultSelector;
        }

        public IDisposable Subscribe(IObserver<TResult> observer)
        {
            var selectManyObserver = ObserverBuilder.Create(observer, (TSource t) =>
                                                            _collectionSelector(t).Subscribe(
                                                                ObserverBuilder.Create(observer,
                                                                                       (TCollection col) =>
                                                                                       observer.OnNext(_resultSelector(
                                                                                                           t, col)))));
            return _source.Subscribe(selectManyObserver);
        }
    }
    
}