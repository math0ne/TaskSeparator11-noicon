using System.Diagnostics;
using System.Runtime.InteropServices;
using WindowsShortcutFactory;
using File = System.IO.File;

namespace TaskSplitter11
{
    public partial class MainForm : Form
    {
        public enum ShowWindowCommands : int
        {

            SW_HIDE = 0,
            SW_SHOWNORMAL = 1,
            SW_NORMAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMAXIMIZED = 3,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10,
            SW_MAX = 10
        }
        [DllImport("shell32.dll")]
        public static extern IntPtr ShellExecute(
            IntPtr hwnd,
            string lpszOp,
            string lpszFile,
            string lpszParams,
            string lpszDir,
            ShowWindowCommands FsShowCmd
        );



        public MainForm()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            CreateSeparator("Splitter.exe");
        }

        private void btnCreateBlank_Click(object sender, EventArgs e)
        {
            CreateSeparator("SplitterBlank.exe");
        }

        private void CreateSeparator(string splitterExeName)
        {
            try
            {
                //Setup & config
                var docsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string appPath = Path.GetDirectoryName(Application.ExecutablePath);
                string shortcutsPath = Path.Join(docsPath, "TaskSeparator11", "Shortcuts");
                Directory.CreateDirectory(shortcutsPath);

                //Ensure files are in the right place
                EnsureSplitterFilesAreAvailable(appPath, shortcutsPath);

                //Make a copy of the Splitter exe
                string splitterExe = Path.Join(appPath, splitterExeName);
                string splitterExeLocation = GetNewLinkName(shortcutsPath, "exe");
                File.Copy(splitterExe, splitterExeLocation);

                //Create a shortcut to the Splitter exe
                string linkLocation = GetNewLinkName(shortcutsPath, "lnk");

                // Determine which icon to use based on which splitter we're creating
                string iconFileName = splitterExeName == "SplitterBlank.exe" ? "dark-gray.ico" : "separator.ico";
                string iconPath = Path.GetFullPath(Path.Join(shortcutsPath, "icons", iconFileName));

                //Create shortcut
                using var shortcut = new WindowsShortcut
                {
                    Path = splitterExeLocation,
                    Arguments = "--gui",
                    IconLocation = iconPath
                };
                shortcut.Save(linkLocation);

                ShellExecute(IntPtr.Zero, "open", linkLocation, null, null, ShowWindowCommands.SW_NORMAL);

                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating separator:\n\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EnsureSplitterFilesAreAvailable(string appPath, string shortcutsPath)
        {
            var dir = new DirectoryInfo(appPath);

            // Copy both Splitter and SplitterBlank files
            var splitterFiles = dir.GetFiles("Splitter*");
            var splitterBlankFiles = dir.GetFiles("SplitterBlank*");

            var allFiles = splitterFiles.Concat(splitterBlankFiles);

            foreach(var f in allFiles)
            {
                var source = Path.Join(appPath, f.Name);
                var dest = Path.Join(shortcutsPath, f.Name);

                File.Copy(source, dest, true);
            }

            // Copy icons folder if it exists
            string iconsSource = Path.Join(appPath, "icons");
            string iconsDest = Path.Join(shortcutsPath, "icons");
            if (Directory.Exists(iconsSource))
            {
                Directory.CreateDirectory(iconsDest);
                foreach (var file in Directory.GetFiles(iconsSource))
                {
                    string fileName = Path.GetFileName(file);
                    File.Copy(file, Path.Join(iconsDest, fileName), true);
                }
            }
        }

        private string GetNewLinkName(string path, string ext)
        {
            int count = 1;
            // Use underscore for both exe and lnk files to ensure valid Windows filenames
            char c = '_';

            string shortcutLink = Path.Join(path, $"{c}.{ext}");
            do
            {
                shortcutLink = Path.Join(path, $"{new string(c, count)}.{ext}");
                count++;
            } while (File.Exists(shortcutLink));

            return shortcutLink;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}