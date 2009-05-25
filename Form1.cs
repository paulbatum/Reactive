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

            var mouseDowns = from md in button1.GetMouseDowns()
                             select md;

            var results = from md in mouseDowns
                          from x in SlowOperation(md.EventArgs)
                          select x;

            Action<string> textboxUpdater = s => textBox1.AppendText(s);
            results.Attach(x => textBox1.BeginInvoke(textboxUpdater, "Mouse down: " + x + "\n"));
            
            
        }

        public IObservable<int> SlowOperation(MouseEventArgs eventArgs)
        {
            Func<MouseEventArgs, int> operation = e =>
                                                 {
                                                     System.Threading.Thread.Sleep(3000);
                                                     return e.X;
                                                 };

            return operation.AsObservable(eventArgs);
        }


    }
}