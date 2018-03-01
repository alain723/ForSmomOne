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
using FileExplorer.Model;
using FileExplorer.Enum;

namespace FileExplorer
{
    public partial class MainForm : MetroForm
    {
        private FileTool fileTool;
        private List<UpFileModel> fileList;
        private XmlTool xmlTool;
        private XDocument xDoc;
        private string XPath = System.AppDomain.CurrentDomain.BaseDirectory + "App_Data/mapping.xml";
        public MainForm()
        {
            InitializeComponent();
            metroStyleManager.Theme = MetroThemeStyle.Dark;
            metroStyleManager.Style = MetroColorStyle.Teal;
            fileTool = new FileTool();


        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            BindData();

        }

        private void BindData()
        {
            xmlTool = new XmlTool(XPath);

            xDoc = xmlTool.xDocument;
            this.listView1.Items.Clear();
            this.listView1.BeginUpdate();

            foreach (var item in xDoc.Element("root").Elements("fileGroup").OrderByDescending(x => Convert.ToDateTime(x.Attribute("date").Value)))
            {
                if (item.HasElements)
                {
                    ListViewGroup lvg = new ListViewGroup();
                    lvg.Header = item.Attribute("date").Value;
                    foreach (var item1 in item.Elements("file"))
                    {
                        UpFileModel uf = new UpFileModel
                        {
                            FilePath = item1.Element("filePath").Value,
                            FileName = item1.Element("fileName").Value,
                            FileType = (FileType)Convert.ToInt32(item1.Element("fileType").Value),
                            Date = Convert.ToDateTime(item1.Element("date").Value)
                        };
                        ListViewItem listItem = new ListViewItem();
                        listItem.Text = uf.FileName;
                        listItem.ImageIndex = Convert.ToInt32(uf.FileType);
                        listItem.Tag = uf;
                        listItem.Group = lvg;
                        listView1.Items.Add(listItem);

                    }
                    this.listView1.Groups.Add(lvg);
                }

            }
            this.listView1.Refresh();
            this.listView1.Show();
            this.listView1.EndUpdate();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            UpForm upForm = new UpForm();
            if (upForm.ShowDialog() == DialogResult.OK)
            {
                BindData();
            }

        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            //MainForm_Load(sender, e);
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo info = listView1.HitTest(e.X, e.Y);
            if (info.Item != null)
            {
                try
                {
                    UpFileModel fileModel = (info.Item.Tag) as UpFileModel;
                    LogHelper.WriteLog(this.GetType(), "双击打开文件---" + fileModel.FilePath);
                    System.Diagnostics.Process.Start(fileModel.FilePath);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(this.GetType(), ex);
                    MetroFramework.MetroMessageBox.Show(this, "这个东西我不认识~~", "哎呀");
                }

            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                m_MBRpt = e.Location;
                this.contextMenuStrip1.Show(listView1, e.Location);
            }
        }
        Point m_MBRpt;

        private void delTsm_Click(object sender, EventArgs e)
        {
            this.listView1.BeginUpdate();
            ListViewItem lstrow = listView1.GetItemAt(m_MBRpt.X, m_MBRpt.Y);
            //MetroFramework.MetroMessageBox.Show(this, (lstrow.Tag as UpFileModel).FilePath, "哎呀");
            UpFileModel ufm = lstrow.Tag as UpFileModel;
            this.listView1.Items.Remove(lstrow);

            xDoc.Element("root")
                .Elements("fileGroup").Where(x => x.Attribute("date").Value == ufm.Date.ToString("yyyy-MM-dd"))
                .Elements("filePath").Where(x => x.Element("filePath").Value == ufm.FilePath)
                .Remove();
            xDoc.Save(XPath);
            LogHelper.WriteLog(this.GetType(), "删除文件---" + ufm.FilePath);
            this.listView1.Refresh();
            this.listView1.Show();
            this.listView1.EndUpdate();
        }
    }
}
