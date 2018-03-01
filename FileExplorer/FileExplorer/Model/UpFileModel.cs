using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileExplorer.Enum;

namespace FileExplorer.Model
{
    public class UpFileModel
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath  { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public FileType FileType { get; set; }

        public DateTime Date { get; set; }

        public string Group { get; set; }

        public string ExtName { get; set; }

    }
}
