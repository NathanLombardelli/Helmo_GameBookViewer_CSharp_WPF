using Microsoft.Win32;

namespace GameBookWPF.Files
{
    class FileResourceChooser : IChooseResource
    {

        public string ResourceIdentifier
        {
            get
            {
                OpenFileDialog dlg = new OpenFileDialog();

                string filePath = string.Empty;
                if ((bool) dlg.ShowDialog())
                {
                    filePath = dlg.FileName;
                }

                return filePath;
            }
        }

    }


}
