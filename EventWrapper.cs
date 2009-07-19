using System;
using System.Windows.Forms;

namespace Reactive
{
    public class EventWrapper<T> : IObservable<T> where T : EventArgs
    {
        private readonly ObserverCollection<T> _observers = new ObserverCollection<T>();

        public void Handler(object sender, T eventArgs)
        {
            _observers.NotifyNext(eventArgs);
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return _observers.Subscribe(observer);
        }
    }
}