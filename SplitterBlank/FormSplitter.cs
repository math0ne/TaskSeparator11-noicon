namespace Splitter
{
    public partial class FormSplitter : Form
    {
        public FormSplitter()
        {
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
            Application.Exit();
        }
    }
}