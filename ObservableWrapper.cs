using System;

namespace Reactive
{
    public class ObservableWrapper<TInner,TOuter> : IObservable<TOuter>
    {
        private IObservable<TInner> _inner;
        private Func<TInner, TOuter> _selector;

        public ObservableWrapper(IObservable<TInner> inner, Func<TInner, TOuter> selector)
        {
            _inner = inner;
            _selector = selector;
        }

        public void Attach(Action<TOuter> action)
        {
            _inner.Attach(inner => action(_selector(inner)));
        }
    }
}