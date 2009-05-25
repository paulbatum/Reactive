using System;
using System.Collections.Generic;

namespace Reactive
{
    public class EventWrapper<TSender, TArgs> : IObservable<EventResult<TSender,TArgs>>
    {
        private List<Action<EventResult<TSender, TArgs>>> _attached = new List<Action<EventResult<TSender, TArgs>>>();

        public void Handle(object sender, TArgs e)
        {
            foreach (var action in _attached)
                action(new EventResult<TSender,TArgs>((TSender)sender, e ));
        }

        public void Attach(Action<EventResult<TSender, TArgs>> action)
        {
            _attached.Add(action);
        }
    }
}