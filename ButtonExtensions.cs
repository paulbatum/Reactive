using System.Windows.Forms;

namespace Reactive
{
    public static class ButtonExtensions
    {
        public static IObservable<EventResult<Button, MouseEventArgs>> GetMouseDowns(this Button b)
        {
            var wrapper = new EventWrapper<Button, MouseEventArgs>();
            b.MouseDown += wrapper.Handle;
            return wrapper;
        }
    }
}