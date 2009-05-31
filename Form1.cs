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
                return args.X;
            };            

            IObservable<int> observable = from md in button1.GetMouseDowns()
                                          where md.EventArgs.Button == MouseButtons.Right
                                          from x in slowOperation.AsAsyncObservable(md.EventArgs)                                            
                                          select x;
                                          

            Action<string> textboxUpdater = s => textBox1.AppendText(s);
            observable.Attach(x => textBox1.BeginInvoke(textboxUpdater, "Mouse down: " + x + "\n"));

        }

    }
}