using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;
using FileExplorer.Common;
using System.Xml.Linq;

namespace FileExplorer
{
    public partial class MainForm : MetroForm
    {

        private FileTool fileTool;
        private XmlTool xmlTool;
        private XDocument xDoc;
        public string MainPath => System.AppDomain.CurrentDomain.BaseDirectory;
        public MainForm()
        {
            InitializeComponent();
            metroStyleManager.Theme = MetroThemeStyle.Dark;
            metroStyleManager.Style = MetroColorStyle.Teal;
           
           
            xmlTool = new XmlTool(MainPath + "App_Data/mapping.xml");

            xDoc= xmlTool.xDocument;

            fileTool = new FileTool(xDoc.Element("root")?.Element("mainPath").Value.Trim());
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            
            foreach (var item in fileTool.mainDir.GetDirectories())
            {
                Console.WriteLine(item.Name);
            }

            //ListViewGroup lvg = new ListViewGroup();
            //lvg.Header = DateTime.Now.Date.ToString();
            //foreach (var item in data)
            //{

            //    ListViewItem lvi = new ListViewItem();
            //    lvi.Text= item.Name.Substring(0, item.Name.LastIndexOf('.'));
            //    lvg.Items.Add(lvi);
            //    this.listView1.Items.Add(lvi);

            //}
            //this.listView1.Groups.Add(lvg);
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
           UpForm upForm=new UpForm();
            upForm.ShowDialog();

        }
    }
}
