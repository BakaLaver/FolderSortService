using SortSettings.Entities.Abstractions;
using SortSettings.Settings;

namespace SortSettings.Entities
{
    public class Sorter : ISorter
    {
        public void Sort(string path, Options extenshion) 
        {
            foreach (var ext in extenshion.Folders)
            {
                string[] files = Directory.GetFiles(path);
                foreach (string file in files)
                {
                    FileInfo fileInf = new FileInfo(file);

                    if (fileInf.Extension == ext.Extension)
                    {
                        MakeFolder(fileInf.DirectoryName, fileInf.FullName, ext.FolderName);
                    }
                }
            }
        }

        public void MakeFolder(string path, string fileName, string folderName)
        {
            FileInfo fileInf = new FileInfo(fileName);
            string testFolder = path + @"\" + folderName;
            path += @"\" + folderName;
            if (Directory.Exists(testFolder))
            {
                fileInf.MoveTo(path + @"\" + fileInf.Name);
                return;
            }
            else
            {
                Directory.CreateDirectory(path);
                fileInf.MoveTo(path + @"\" + fileInf.Name);
            }
        }
    }
}
