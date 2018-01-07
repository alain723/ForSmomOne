using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileExplorer.Common
{
    public class FileTool
    {
        public DirectoryInfo mainDir { get; set; }

        public FileTool(string dirName)
        {
            if (!Directory.Exists(dirName))
            {
                mainDir = Directory.CreateDirectory(dirName);
            }
            mainDir = new DirectoryInfo(dirName);
        }
    }
}
