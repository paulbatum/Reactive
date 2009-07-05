using System;

namespace Reactive
{
    public static class Extensions
    {
        public static IObservable<TResult> AsAsyncObservable<TInput, TResult>(this Func<TInput,TResult> funcToObserve,TInput input)
        {
            return new AsyncWrapper<TInput, TResult>(funcToObserve, input);
        }
        
        public static IObservable<TResult> Select<T, TResult>(this IObservable<T> observable, Func<T, TResult> func)
        {
            return new ObservableWrapper<T, TResult>(observable, func);
        }

        public static IObservable<TResult> SelectMany<TSource, TCollection, TResult>(this IObservable<TSource> source, Func<TSource, IObservable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            return new SelectManyObservable<TSource, TCollection, TResult>(source, collectionSelector, resultSelector);
        }

        public static IObservable<T> Where<T>(this IObservable<T> source, Func<T, bool> predicate)
        {
            return new WhereObservable<T>(source, predicate);
        }


        //private static Func<T, IObservable<TResult>> Convert<T, TResult>(Func<T, TResult> selector)
        //{
        //    return t => 
        //}


        //public static TResult Select<T, TResult>(this IObserver<T> observer, Func<IObserver<T>, TResult> func)
        //{
        //    return default(TResult);
        //}

    }

    public class WhereObservable<T> : IObservable<T>
    {
        private readonly IObservable<T> _source;
        private readonly Func<T, bool> _predicate;

        public WhereObservable(IObservable<T> source, Func<T, bool> predicate)
        {
            _source = source;
            _predicate = predicate;
        }

        public void Attach(Action<T> action)
        {
            _source.Attach(t => { if (_predicate(t)) action(t); });
        }
    }

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

        public void Attach(Action<TResult> action)
        {
            _source.Attach(
                s => _collectionSelector(s).Attach(
                    c => action(_resultSelector(s, c))
                )
            );            
        }
    }
}