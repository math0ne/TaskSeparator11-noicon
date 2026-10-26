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

            // Load and set the transparent icon for the taskbar
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string iconPath = Path.Combine(baseDir, "icons", "dark-gray.ico");

                if (File.Exists(iconPath))
                {
                    this.Icon = new Icon(iconPath);
                }
            }
            catch
            {
                // If icon loading fails, just use the default
            }
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
