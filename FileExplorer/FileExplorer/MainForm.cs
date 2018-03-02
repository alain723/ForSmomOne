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

        private IEnumerable<string> groups;
        private IEnumerable<string> times;
        private IEnumerable<XElement> files;


        private void MainForm_Load(object sender, EventArgs e)
        {
            xmlTool = new XmlTool(XPath);

            xDoc = xmlTool.xDocument;
            this.listView1.ShowItemToolTips = true;
            groups = xDoc.Element("root").Element("groups").Elements("item").Select(x => x.Value);
            files = xDoc.Element("root").Elements("file");
            times = xDoc.Element("root").Elements("file").Select(x => x.Element("date").Value).Distinct();
            BindData(groups, files);

        }

        private void BindData(IEnumerable<string> group, IEnumerable<XElement> data)
        {

            this.listView1.Items.Clear();
            this.listView1.BeginUpdate();

            var groups = group;
            foreach (var item0 in groups)
            {
                ListViewGroup lvg = new ListViewGroup();
                lvg.Header = item0;
                foreach (var item in data.Where(x => x.Element("group").Value == item0).OrderByDescending(x => Convert.ToDateTime(x.Element("date").Value)))
                {
                    if (item.HasElements)
                    {

                        //foreach (var item1 in item.Elements("file"))
                        //{
                        UpFileModel uf = new UpFileModel
                        {
                            Id = Guid.Parse(item.Element("id").Value),
                            FilePath = item.Element("filePath").Value,
                            FileName = item.Element("fileName").Value,
                            FileType = (FileType)Convert.ToInt32(item.Element("fileType").Value),
                            Date = Convert.ToDateTime(item.Element("date").Value),
                            Group = item.Element("group").Value,
                            ExtName = item.Element("extName").Value,
                        };
                        ListViewItem listItem = new ListViewItem();
                        listItem.Text = uf.FileName;
                        listItem.ImageIndex = Convert.ToInt32(uf.FileType);
                        listItem.ToolTipText = "上传时间：" + uf.Date.ToString("yyyy-MM-dd") + "\r\n组别：" + uf.Group;
                        listItem.Tag = uf;
                        listItem.Group = lvg;
                        listView1.Items.Add(listItem);

                        //}

                    }

                }
                this.listView1.Groups.Add(lvg);
            }


            this.listView1.Refresh();
            this.listView1.Show();
            this.listView1.EndUpdate();
        }

        private void BindData1(IEnumerable<string> group, IEnumerable<XElement> data)
        {
            this.listView1.Items.Clear();
            this.listView1.BeginUpdate();

            var groups = group;
            foreach (var item0 in groups.OrderByDescending(x => Convert.ToDateTime(x)))
            {
                ListViewGroup lvg = new ListViewGroup();
                lvg.Header = item0;
                foreach (var item in data.Where(x => x.Element("date").Value == item0).OrderByDescending(x => Convert.ToDateTime(x.Element("date").Value)))
                {
                    if (item.HasElements)
                    {

                        //foreach (var item1 in item.Elements("file"))
                        //{
                        UpFileModel uf = new UpFileModel
                        {
                            Id = Guid.Parse(item.Element("id").Value),
                            FilePath = item.Element("filePath").Value,
                            FileName = item.Element("fileName").Value,
                            FileType = (FileType)Convert.ToInt32(item.Element("fileType").Value),
                            Date = Convert.ToDateTime(item.Element("date").Value),
                            Group = item.Element("group").Value,
                            ExtName = item.Element("extName").Value,
                        };
                        ListViewItem listItem = new ListViewItem();
                        listItem.Text = uf.FileName;
                        listItem.ImageIndex = Convert.ToInt32(uf.FileType);
                        listItem.ToolTipText = "上传时间：" + uf.Date.ToString("yyyy-MM-dd") + "\r\n组别：" + uf.Group;
                        listItem.Tag = uf;
                        listItem.Group = lvg;
                        listView1.Items.Add(listItem);

                        //}

                    }

                }
                this.listView1.Groups.Add(lvg);
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
                if (this.rdbGroup.Checked)
                {
                    BindData(groups, files);
                }
                else
                {
                    BindData1(times, files);
                }
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
                    MetroFramework.MetroMessageBox.Show(this, "没有可以打开该文件的应用程序", "警告");
                }

            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                m_MBRpt = e.Location;
                this.groCms.DropDownItems.Clear();
                var groups = xDoc.Element("root").Element("groups").Elements("item");
                ListViewItem lvi = this.listView1.GetItemAt(m_MBRpt.X, m_MBRpt.Y);
                foreach (var item in groups)
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(item.Value);
                    if (item.Value == (lvi.Tag as UpFileModel).Group)
                    {
                        tsmi.Checked = true;
                    }
                    tsmi.Click += groupList_Click;
                    this.groCms.DropDownItems.Add(tsmi);
                }

                this.contextMenuStrip1.Show(listView1, e.Location);
            }
        }


        Point m_MBRpt;

        private void groupList_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            if (tsmi.Checked)
            {
                return;
            }
            //ListViewItem lvi = this.listView1.GetItemAt(m_MBRpt.X, m_MBRpt.Y);
            foreach (ListViewItem item in this.listView1.SelectedItems)
            {
                UpFileModel fum = item.Tag as UpFileModel;
                //var a = xDoc.Element("root").Elements("file").Where(x => x.Element("id").Value == "fb49e57e-e0e8-4209-b915-efec499c6755");
                xDoc.Element("root").Elements("file").Where(x => x.Element("id").Value == fum.Id.ToString()).SingleOrDefault().Element("group").Value = tsmi.Text;
            }

            xDoc.Save(XPath);
            if (this.rdbGroup.Checked)
            {
                BindData(groups, files);
            }
            else
            {
                BindData1(times, files);
            }

        }

        private void delTsm_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 0)
            {
                MetroFramework.MetroMessageBox.Show(this, "请至少选择一个文件", "警告");
                return;
            }

            if (MetroFramework.MetroMessageBox.Show(this, "确认要删除这些文件？", "警告", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.listView1.BeginUpdate();
                foreach (ListViewItem item in this.listView1.SelectedItems)
                {
                    ListViewItem lstrow = item;

                    UpFileModel ufm = lstrow.Tag as UpFileModel;
                    this.listView1.Items.Remove(lstrow);

                    //var aa = xDoc.Element("root")
                    //.Elements("fileGroup").Where(x => x.Attribute("date").Value == ufm.Date.ToString("yyyy-MM-dd"));

                    xDoc.Element("root")
                        .Elements("file")
                        .SingleOrDefault(x => x.Element("id").Value == ufm.Id.ToString())
                        .Remove();
                    xDoc.Save(XPath);
                    LogHelper.WriteLog(this.GetType(), "删除文件---" + ufm.FilePath);
                }

                this.listView1.Refresh();
                this.listView1.Show();
                this.listView1.EndUpdate();
            }

        }

        private void rdbGroup_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdbGroup.Checked)
            {
                BindData(groups, files);
            }
        }

        private void rdbTime_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdbTime.Checked)
            {
                BindData1(times, files);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var key = this.txtSearch.Text;
            var data = xDoc.Element("root").Elements("file").Where(x => x.Element("fileName").Value.IndexOfAny(key.ToCharArray()) >= 0);
            var groups = data.Elements("group").Select(x => x.Value).Distinct();
            var times = data.Elements("date").Select(x => x.Value).Distinct();
            if (this.rdbGroup.Checked)
            {
                if (string.IsNullOrEmpty(key))
                {
                    BindData(this.groups, this.files);
                }
                else
                {
                    BindData(groups, data);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(key))
                {
                    BindData1(this.times, this.files);
                }
                else
                {
                    BindData1(times, data);
                }

            }

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            this.txtSearch.Clear();
            var data = xDoc.Element("root").Elements("file").Where(x => x.Element("date").Value==this.dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            var times = data.Elements("date").Select(x => x.Value).Distinct();
            this.rdbTime.Checked = true;
            BindData1(times, data);
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            LogHelper.WriteLog(this.GetType(), "设置");
            new SettingsForm().ShowDialog();
        }
    }
}
