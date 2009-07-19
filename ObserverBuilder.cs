using System;

namespace Reactive
{
    public class ObserverBuilder
    {
        public static IObserver<T2> Create<T1, T2>(IObserver<T1> observer, Action<T2> onNext)
        {
            return new ObserverWrapper<T1, T2>(observer, onNext);
        }

        private class ObserverWrapper<T1, T2> : IObserver<T2>
        {
            private readonly IObserver<T1> _observer;
            private readonly Action<T2> _onNext;

            public ObserverWrapper(IObserver<T1> observer, Action<T2> onNext)
            {
                _observer = observer;
                _onNext = onNext;
            }

            public void OnNext(T2 item)
            {
                _onNext(item);
            }

            public void OnDone()
            {
                _observer.OnDone();
            }

            public void OnError(Exception e)
            {
                _observer.OnError(e);
            }
        }
    }
}