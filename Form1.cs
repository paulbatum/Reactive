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
                             select md.EventArgs;

            //var q = from md in mouseDowns
            //        from x in GetX(md)
            //        select x;

            mouseDowns.Attach(e => textBox1.AppendText("Mouse down: " + e.X + "\n"));
            
        }
    }
}