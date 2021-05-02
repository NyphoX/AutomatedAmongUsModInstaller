using System;
using System.Drawing;
using System.Windows.Forms;

namespace AmongUs_ModInstaller.Forms
{
    public partial class DialogForm : Form
    {
        public DialogForm(string title, string text)
        {
            InitializeComponent();

            Text = title;
            label1.Text = text;
        }

        private void DialogForm_Load(object sender, EventArgs e)
        {
            if (Owner != null)
                Location = new Point(Owner.Location.X + Owner.Width / 2 - Width / 2, Owner.Location.Y + Owner.Height / 2 - Height / 2);
        }
    }
}
