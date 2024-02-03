using SortSettings.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortSettings.Entities.Abstractions
{
    public interface ISorter
    {
        void Sort(string filePath, Options extenhion);

        void MakeFolder(string path, string file, string folderName);

    }
}
