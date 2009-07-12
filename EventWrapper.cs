using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Reactive
{
    public class ObserverCollection<T> : IObservable<T>
    {
        protected IList<IObserver<T>> _observers;

        public ObserverCollection()
        {
            _observers = new List<IObserver<T>>();
        }

        public virtual IDisposable Subscribe(IObserver<T> observer)
        {
            _observers.Add(observer);
            return new Disposer(() => Remove(observer));
        }

        public void NotifyNext(T t)
        {
            foreach (IObserver<T> observer in _observers)
                observer.OnNext(t);
        }

        public void NotifyDone()
        {
            foreach (IObserver<T> observer in _observers)
                observer.OnDone();
        }

        public void NotifyError(Exception e)
        {
            foreach (IObserver<T> observer in _observers)
                observer.OnError(e);
        }

        protected virtual void Remove(IObserver<T> observer)
        {
            _observers.Remove(observer);
        }

        public void Clear()
        {
            _observers.Clear();
        }

        protected class Disposer : IDisposable
        {
            private readonly Action _disposeAction;

            public Disposer(Action disposeAction)
            {
                _disposeAction = disposeAction;
            }

            public void Dispose()
            {
                _disposeAction();
            }
        }
    }

    public class EventWrapper<T> : IObservable<T> where T : EventArgs
    {
        private readonly ObserverCollection<T> _observers = new ObserverCollection<T>();

        public void Handle(object sender, T t)
        {
            _observers.NotifyNext(t);
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return _observers.Subscribe(observer);
        }
    }
}