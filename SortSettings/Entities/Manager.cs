using SortSettings.Entities.Abstractions;
using SortSettings.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortSettings.Entities
{
    public class Manager
    {
        public Manager(ISorter sort, Options options) 
        {
            string pathReslt;
            pathReslt = options.Path.Replace(@"\\", @"\");
            Sorter = sort;

            Sort(pathReslt, options);
        }

        public ISorter Sorter { get; set; }

        public void Sort(string folder, Options extenhion) 
        {
            Sorter.Sort(folder, extenhion);
        }

    }
}
