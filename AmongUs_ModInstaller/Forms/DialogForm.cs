using System;
using System.Drawing;
using System.Windows.Forms;

namespace AmongUs_ModInstaller.Forms
{
    public partial class DialogForm : Form
    {
        private bool allowUserToClose;

        public DialogForm(string title, string text, bool allowUserToClose)
        {
            this.allowUserToClose = allowUserToClose;
            InitializeComponent();

            Text = title;
            setFullLabelText(text);
        }

        private void setFullLabelText(string text)
        {
            //Text needs a linebreak+space at the end, so autosize works correctly.
            label1.Text = text + (allowUserToClose ? "\n\n(Press ESCAPE to close this window.)" : "") + "\n ";
        }

        private void DialogForm_Load(object sender, EventArgs e)
        {
            if (Owner != null)
                Location = new Point(Owner.Location.X + Owner.Width / 2 - Width / 2, Owner.Location.Y + Owner.Height / 2 - Height / 2);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (allowUserToClose && keyData == Keys.Escape)
            {
                Close();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
