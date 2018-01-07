using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FileExplorer.Common
{
    
    public class XmlTool
    {
        public XDocument xDocument { get; set; }
        public XmlTool(string xmlPath)
        {
            xDocument = XDocument.Load(xmlPath);
        }
    }
}
