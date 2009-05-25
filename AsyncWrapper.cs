using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace Reactive
{
    public class AsyncWrapper<TInput, TResult> : IObservable<TResult>
    {
        private List<Action<TResult>> _attached = new List<Action<TResult>>();

        public AsyncWrapper(Func<TInput, TResult> funcToObserve, TInput input)
        {
            funcToObserve.BeginInvoke(input, CompletedCallback, null);
        }

        public void Attach(Action<TResult> action)
        {
            _attached.Add(action);
        }

        private void CompletedCallback(IAsyncResult asyncResult)
        {
            TResult calculatedValue = ((Func<TInput, TResult>) ((AsyncResult) asyncResult).AsyncDelegate).EndInvoke(
                asyncResult);

            foreach (var action in _attached)
                action(calculatedValue);
        }
    }
}