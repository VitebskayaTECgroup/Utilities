using System;
using System.Windows.Forms;

namespace Looker
{
    public partial class LookForm : Form
    {
        public bool AutoClose { get; set; } = false;

        public LookForm()
        {
            InitializeComponent();

            Timer.Interval = Program.Configuration.VisibleInterval;
            if (AutoClose) Timer.Enabled = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Hide();
            Dispose();
        }

        private void LookForm_Load(object sender, EventArgs e)
        {
            Top = Program.Configuration.Top;
            Left = Program.Configuration.Left;
            Height = Program.Configuration.Height;
            Width = Program.Configuration.Width;
        }

        private void Save()
        {
            Program.Configuration.Top = Top;
            Program.Configuration.Left = Left;
            Program.Configuration.Height = Height;
            Program.Configuration.Width = Width;
            Program.Configuration.Save();
        }

        private void ChangeDisplay(object sender, MouseEventArgs e) => Save();

        private void LookForm_ResizeEnd(object sender, EventArgs e) => Save();
    }
}