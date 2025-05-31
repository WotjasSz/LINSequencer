using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequencerUI.Services
{
    public class FileDialogService : IFileDialogService
    {
        public string GetPath()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }
            return string.Empty;
        }
    }
}
