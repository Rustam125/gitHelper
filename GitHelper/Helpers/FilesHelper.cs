using System.Windows.Forms;

namespace GitHelper.Helpers
{
    internal class FilesHelper
    {
        public static string SelectScriptFullFileName()
        {
            string filename = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.DefaultExt = ".bat";
                openFileDialog.Filter = "Bat-файл (*.bat)|*.bat|PowerShell-файл (*.ps1)|*.ps1";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filename = openFileDialog.FileName;
                }
            }

            return filename;
        }
    }
}
