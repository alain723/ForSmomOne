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
        public List<FileInfo> GetFileByDicPath(string path)
        {
            DirectoryInfo dic = new DirectoryInfo(path);
            
            return dic.GetFiles().ToList();
        }

    }
}
