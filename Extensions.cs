using System;

namespace Reactive
{
    public static class Extensions
    {
        
        public static IObservable<TResult> Select<T, TResult>(this IObservable<T> observable, Func<T, TResult> func)
        {
            return new ObservableWrapper<T, TResult>(observable, func);
        }

        //public static TResult Select<T, TResult>(this IObserver<T> observer, Func<IObserver<T>, TResult> func)
        //{
        //    return default(TResult);
        //}

    }
}