using System;

namespace Reactive
{
    public interface IObservable<T>
    {
        IDisposable Subscribe(IObserver<T> observer);
    }

    public interface IObserver<T>
    {        
        void OnNext(T t);
        void OnDone();
        void OnError(Exception e);
    }

    public class DefaultObserver<T> : IObserver<T>
    {
        private readonly Action<T> _action;

        public DefaultObserver(Action<T> action)
        {
            _action = action;
        }

        public void OnNext(T t)
        {
            _action(t);
        }


        public void OnDone()
        {

        }

        public void OnError(Exception e)
        {
            
        }
    }
}