using System.Windows.Forms;

namespace Reactive
{
    public static class ButtonExtensions
    {
        public static IObservable<MouseEventArgs> GetMouseDowns(this Button button)
        {
            var wrapper = new EventWrapper<MouseEventArgs>();
            button.MouseDown += wrapper.Handler;
            return wrapper;
        }
    }
}