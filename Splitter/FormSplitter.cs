using System.Diagnostics;

namespace Splitter
{
    public partial class FormSplitter : Form
    {
        private readonly string? _launcherPath;

        public FormSplitter(string? launcherPath = null)
        {
            _launcherPath = launcherPath;
            InitializeComponent();
            (new Core.DropShadow()).ApplyShadows(this);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (_launcherPath != null && File.Exists(_launcherPath))
            {
                var result = MessageBox.Show(
                    "Would you like to create another separator?",
                    "Create Another Separator",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Process.Start(_launcherPath);
                }
            }

            Application.Exit();
        }
    }
}
