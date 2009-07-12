using System.Windows.Forms;

namespace Reactive
{
    public static class ButtonExtensions
    {
        public static IObservable<MouseEventArgs> GetMouseDowns(this Button b)
        {
            var wrapper = new EventWrapper<MouseEventArgs>();
            b.MouseDown += wrapper.Handle;
            return wrapper;
        }
    }
}