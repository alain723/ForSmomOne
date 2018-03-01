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
using System.IO;

namespace FileExplorer
{
    public partial class UpForm : MetroForm
    {
        private FileTool fileTool;
        //private List<UpFileModel> fileList;
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
            this.listView1.ShowItemToolTips = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.listView1.Items.Count <= 0)
            {
                MetroFramework.MetroMessageBox.Show(this, "还没有选择任何文件", "警告");
                return;
            }

            var xmlBase = xDoc.Element("root");
            foreach (ListViewItem item in this.listView1.Items)
            {

                UpFileModel uf = item.Tag as UpFileModel;
                if (xmlBase.Elements("file").Any(x => x.Element("filePath").Value == uf.FilePath))
                {
                    continue;
                }
                xmlBase.Add(new XElement("file",
                    new XElement("id", uf.Id),
                    new XElement("fileName", uf.FileName),
                    new XElement("filePath", uf.FilePath),
                    new XElement("fileType", Convert.ToInt32(uf.FileType)),
                    new XElement("date", uf.Date.ToString("yyyy-MM-dd")),
                    new XElement("group", uf.Group),
                    new XElement("extName", uf.ExtName)
                    ));
            }
            xDoc.Save(XPath);
            MetroFramework.MetroMessageBox.Show(this, "上传成功！", "消息");
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
            //更新viewlist
            this.listView1.Items.Clear();
            this.listView1.BeginUpdate();
            //这个地方开始备份文件
            var mainPath = xDoc.Element("root").Element("mainPath").Value;
            if (fileTool.ValMainPath(mainPath))
            {
                foreach (var item in this.openFileDialog1.FileNames)
                {
                    FileInfo fi0 = new FileInfo(item);
                    Guid id = Guid.NewGuid();
                    FileInfo fi = fileTool.CopyAndSave(item, mainPath + "/" + id.ToString() + fi0.Extension);
                    UpFileModel model = new UpFileModel
                    {
                        Id = id,
                        FileName = fi0.Name.Split('.')[0],
                        FilePath = fi.FullName,
                        FileType = FileTool.GetFileType(fi.Extension),
                        Date = this.dateTimePicker1.Value,
                        Group = "",
                        ExtName = fi.Extension
                    };
                    model.Date = this.dateTimePicker1.Value;
                    ListViewItem listItem = new ListViewItem();
                    listItem.Text = model.FileName;
                    listItem.ImageIndex = Convert.ToInt32(model.FileType);
                    listItem.Tag = model;
                    listItem.ToolTipText = "文件类型："+model.FileType.ToString()+"\r\n上传时间：" + model.Date.ToString("yyyy-MM-dd") + "\r\n组别：" + model.Group;
                    listView1.Items.Add(listItem);
                }
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
                    MetroFramework.MetroMessageBox.Show(this, "没有可以打开该文件的应用程序", "警告");
                }

            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (this.listView1.Items.Count > 0 && this.listView1.SelectedItems.Count == 0)
            {
                MetroFramework.MetroMessageBox.Show(this, "请至少选择一个文件", "警告");
                return;
            }
            LogHelper.WriteLog(this.GetType(), "选择了时间---" + this.dateTimePicker1.Value);
            foreach (ListViewItem item in this.listView1.SelectedItems)
            {
                ListViewItem lvi = item;
                UpFileModel ufm = lvi.Tag as UpFileModel;
                ufm.Date = this.dateTimePicker1.Value;
                lvi.Tag = ufm;
            }

        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                
                m_MBRpt = e.Location;
                this.contextMenuStrip1.Show(listView1,e.Location);
            }
        }
        Point m_MBRpt;
        private void delTsm_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.listView1.SelectedItems)
            {
                //MetroFramework.MetroMessageBox.Show(this, (lstrow.Tag as UpFileModel).FilePath, "哎呀");
                UpFileModel ufm = item.Tag as UpFileModel;
                this.listView1.Items.Remove(item);
                //fileList.Remove(ufm);
            }

        }

        private void listView1_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            ListViewItem lvi = this.listView1.Items[e.Item];
            UpFileModel ufm = lvi.Tag as UpFileModel;
            ufm.FileName = e.Label;
            lvi.Tag = ufm;
        }
    }
}
