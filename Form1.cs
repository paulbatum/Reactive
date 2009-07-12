using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Linq.Expressions;

namespace Reactive
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Func<MouseEventArgs, int> slowOperation = args =>
            {
                System.Threading.Thread.Sleep(3000);
                if(args.Button == MouseButtons.Middle)
                    throw new Exception("MIDDLE BUTTON NOT ALLOWED!!!");
                return args.X;
            };

            IObservable<string> messages = from md in button1.GetMouseDowns()                                           
                                           from x in slowOperation.AsAsyncObservable(md)
                                           where md.Button == MouseButtons.Right
                                           select "Mouse down: " + x + "\n";

            messages.Subscribe(new TextBoxUpdater(textBox1));
        }

        public class TextBoxUpdater : IObserver<string>
        {
            private readonly TextBox _textBox;

            public TextBoxUpdater(TextBox textBox)
            {
                _textBox = textBox;
            }

            private void SetText(string text)
            {
                Action textboxUpdater = () => _textBox.AppendText(text);
                _textBox.BeginInvoke(textboxUpdater);
            }

            public void OnNext(string s)
            {
                SetText(s);
            }

            public void OnDone()
            {
                SetText("Done\n");
            }

            public void OnError(Exception e)
            {
                SetText("Error: " + e.Message);
            }
        }

    }
}