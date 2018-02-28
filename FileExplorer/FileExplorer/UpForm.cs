using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;
using FileExplorer.Model;
using FileExplorer.Common;
using System.Xml.Linq;
using System.Drawing;

namespace FileExplorer
{
    public partial class UpForm : MetroForm
    {
        private FileTool fileTool;
        private List<UpFileModel> fileList;
        private XmlTool xmlTool;
        private XDocument xDoc;
        private string XPath = System.AppDomain.CurrentDomain.BaseDirectory + "App_Data/mapping.xml";
        //public string MainPath => ;
        public UpForm()
        {
            InitializeComponent();
            metroStyleManager.Theme = MetroThemeStyle.Dark;
            metroStyleManager.Style = MetroColorStyle.Teal;
            fileTool = new FileTool();
            xmlTool = new XmlTool(XPath);

            xDoc = xmlTool.xDocument;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.listView1.Items.Count <= 0)
            {
                MetroFramework.MetroMessageBox.Show(this, "这里面没东西，不要瞎点~~", "哎呀");
                return;
            }

            var xmlBase = xDoc.Element("root").Elements("fileGroup").SingleOrDefault(x => x.Attribute("date").Value == this.dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            if (xmlBase == null)
            {
                XElement xe = new XElement("fileGroup", new XAttribute("date", this.dateTimePicker1.Value.ToString("yyyy-MM-dd")));
                xmlBase = xe;
                xDoc.Element("root").Add(xe);

            }
            //MetroFramework.MetroMessageBox.Show(this, xmlBase.ToString(), "哎呀");
            foreach (ListViewItem item in this.listView1.Items)
            {
               
                UpFileModel uf = item.Tag as UpFileModel;
                if (xmlBase.Elements("file").Any(x=>x.Element("filePath").Value==uf.FilePath))
                {
                    continue;
                }
                xmlBase.Add(new XElement("file",
                    new XElement("fileName", uf.FileName),
                    new XElement("filePath", uf.FilePath),
                    new XElement("fileType", Convert.ToInt32(uf.FileType)),
                    new XElement("date", uf.Date.ToString("yyyy-MM-dd"))
                    ));
            }
            xDoc.Save(XPath);
            MetroFramework.MetroMessageBox.Show(this, "成功了！", "恭喜");
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnSelFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            this.openFileDialog1.Filter = "图片|*.jgp;*.png;*.jpeg;*.bmp;*.gif|pdf文件|*.pdf|Word|*.doc;*.docx|Excel|*.xls;*.xlsx|所有文件(*.*)|*.*";
            this.openFileDialog1.Multiselect = true;
            this.openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            fileList = fileList ?? new List<UpFileModel>();

            foreach (var item in this.openFileDialog1.FileNames)
            {
                if (!fileList.Any(x => x.FilePath == item))
                {
                    fileList.Add(fileTool?.GetFileInfo(item));
                    LogHelper.WriteLog(this.GetType(), "选择文件---" + item);
                }
                else
                {
                    LogHelper.WriteLog(this.GetType(), "重复选择文件---" + item);
                }


            }
            this.listView1.Items.Clear();
            this.listView1.BeginUpdate();
            foreach (var item in fileList)
            {
                item.Date = this.dateTimePicker1.Value;
                ListViewItem listItem = new ListViewItem();
                listItem.Text = item.FileName;
                listItem.ImageIndex = Convert.ToInt32(item.FileType);
                listItem.Tag = item;
                listView1.Items.Add(listItem);
            }
            listView1.Show();
            this.listView1.EndUpdate();

           
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            LogHelper.WriteLog(this.GetType(), "选择了时间---" + this.dateTimePicker1.Value);
            if (fileList != null)
            {
                foreach (var item in fileList)
                {
                    item.Date = this.dateTimePicker1.Value;
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
            ListViewItem lstrow = listView1.GetItemAt(m_MBRpt.X, m_MBRpt.Y);
            //MetroFramework.MetroMessageBox.Show(this, (lstrow.Tag as UpFileModel).FilePath, "哎呀");
            UpFileModel ufm = lstrow.Tag as UpFileModel;
            this.listView1.Items.Remove(lstrow);
            fileList.Remove(ufm);
        }
    }
}
