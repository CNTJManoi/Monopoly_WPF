using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Monopoly.Repository
{
    internal class FileUploader
    {
        public string GetFilePath()
        {
            var directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            var filename = string.Empty;

            if (directoryInfo != null)
            {
                var dlg = new Microsoft.Win32.OpenFileDialog();
                {
                    Filter = "Monopoly Files (*.poly)|*.poly",
                    InitialDirectory = directoryInfo.FullName + @"\Data\Saves\"
                };

                dlg.ShowDialog();
                filename = dlg.FileName;
            }

            if (!string.IsNullOrEmpty(filename) && filename.Contains(".poly"))
            {
                return filename;
            }

            return "";
        }
    }
}
