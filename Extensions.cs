using System;

namespace Reactive
{
    public static class Extensions
    {
        public static IObservable<TResult> Select<T, TResult>(this IObservable<T> observable, Func<T, TResult> func)
        {
            return new SelectObservable<T, TResult>(observable, func);
            //return ObservableBuilder.Create((IObserver<TResult> observer) => observable.Subscribe(ObserverBuilder.Create(observer, (T a) => observer.OnNext(func(a)))));
        }

        public static IObservable<TResult> SelectMany<TSource, TCollection, TResult>(this IObservable<TSource> source, Func<TSource, IObservable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            return new SelectManyObservable<TSource, TCollection, TResult>(source, collectionSelector, resultSelector);
        }

        public static IObservable<T> Where<T>(this IObservable<T> source, Func<T, bool> predicate)
        {
            return new WhereObservable<T>(source, predicate);
        }

        public static IObservable<TResult> AsAsyncObservable<TInput, TResult>(this Func<TInput, TResult> funcToObserve, TInput input)
        {
            return new AsyncWrapper<TResult>(() => funcToObserve(input));
        }
    }
}