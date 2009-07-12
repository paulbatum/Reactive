using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace Reactive
{
    public class AsyncWrapper<TResult> : IObservable<TResult>
    {
        private readonly Func<TResult> _operation;
        private readonly ObserverCollection<TResult> _observers;

        public AsyncWrapper(Func<TResult> operation)
        {
            _observers = new ObserverCollection<TResult>();
            _operation = () => Execute(operation);
        }

        private void CompletedCallback(IAsyncResult asyncResult)
        {
            TResult calculatedValue = ((Func<TResult>) ((AsyncResult) asyncResult).AsyncDelegate).EndInvoke(
                asyncResult);

            _observers.NotifyNext(calculatedValue);
            _observers.NotifyDone();
            _observers.Clear();
        }

        private TResult Execute(Func<TResult> operation)
        {
            try
            {
                return operation();
            }
            catch (Exception e)
            {
                _observers.NotifyError(e);
                return default(TResult);
            }
        }

        public IDisposable Subscribe(IObserver<TResult> observer)
        {
            var subscription = _observers.Subscribe(observer);
            _operation.BeginInvoke(CompletedCallback, null);
            return subscription;
        }
    }
}