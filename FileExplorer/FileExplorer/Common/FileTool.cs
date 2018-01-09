using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FileExplorer.Model;
using FileExplorer.Enum;

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
        public FileTool() { }

        public UpFileModel GetFileInfo(string path)
        {
            FileInfo fi = new FileInfo(path);
            return new UpFileModel
            {
                FileName = fi.Name.Split('.')[0],
                FilePath = fi.FullName,
                FileType = GetFileType(fi.Extension)
            };
        }

        private static FileType GetFileType(string fileName)
        {
            //"图片|*.jgp;*.png;*.jpeg;*.bmp;*.gif|pdf文件|*.pdf|Word|*.doc;*.docx|Excel|*.xls;*.xlsx|所有文件(*.*)|*.*";
            switch (fileName)
            {
                case ".pdf":
                    return FileType.Pdf;
                case ".jgp":
                case ".png":
                case ".jpeg":
                case ".bmp":
                case ".gif":
                    return FileType.Image;
                case ".xls":
                case ".xlsx":
                    return FileType.Excel;
                case ".doc":
                case ".docx":
                    return FileType.Word;
                default:
                    return FileType.Blank;
            }
        }
    }
}
