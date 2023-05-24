using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Configuration = Monopoly.Logic.Configuration;

namespace Monopoly.Data
{
    internal class FileUploader
    {
        public string GetFilePath()
        {
            var directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            var filename = string.Empty;

            if (directoryInfo != null)
            {
                var dlg = new OpenFileDialog
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
        public void SaveData(Configuration config, string filename)
        {
            config.SaveData(filename);
        }
    }
}
