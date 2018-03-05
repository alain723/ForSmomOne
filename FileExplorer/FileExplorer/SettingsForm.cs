using FileExplorer.Common;
using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.ListViewItem;

namespace FileExplorer
{
    public partial class SettingsForm : MetroForm
    {
        private XmlTool xmlTool;
        private XDocument xDoc;
        private string XPath = System.AppDomain.CurrentDomain.BaseDirectory + "App_Data/mapping.xml";
        public SettingsForm()
        {
            InitializeComponent();
            metroStyleManager.Theme = MetroThemeStyle.Dark;
            metroStyleManager.Style = MetroColorStyle.Teal;
            xmlTool = new XmlTool(XPath);

            xDoc = xmlTool.xDocument;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            //修改路径后

            this.DialogResult = DialogResult.OK;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            BindGroup();
        }

        private void BindGroup()
        {
            this.listView1.Items.Clear();
            this.txtMainPath.Text = xDoc.Element("root").Element("mainPath").Value;
            //this.listBox1.
            var listData = xDoc.Element("root").Element("groups").Elements("item");

            this.listView1.BeginUpdate();
            foreach (var item in listData)
            {
                int count = xDoc.Element("root").Elements("file").Count(x => x.Element("group")?.Value == item.Value);
                if (item.HasAttributes)
                {

                    //this.listBox1.Items.Add("默认分组    " + item.Value);
                    ListViewItem lvi = new ListViewItem("✔");
                    lvi.SubItems.Add(new ListViewSubItem(lvi, item.Value));
                    lvi.SubItems.Add(new ListViewSubItem(lvi, count.ToString()));

                    this.listView1.Items.Add(lvi);
                }
                else
                {
                    //this.listBox1.Items.Add("            " + item.Value);

                    ListViewItem lvi = new ListViewItem();
                    lvi.SubItems.Add(new ListViewSubItem(lvi, item.Value));
                    lvi.SubItems.Add(new ListViewSubItem(lvi, count.ToString()));

                    this.listView1.Items.Add(lvi);
                }
            }
            this.listView1.EndUpdate();
        }

        private void btnSelDir_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtMainPath.Text = this.folderBrowserDialog1.SelectedPath;
                if (xDoc.Element("root").Element("mainPath").Value != this.txtMainPath.Text)
                {
                    if (MetroFramework.MetroMessageBox.Show(this, "您已经改变备份路径，是否迁移文件到新路径？", "警告", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        FileTool ft = new FileTool();
                        ft.CopyDirectory(xDoc.Element("root").Element("mainPath").Value, this.txtMainPath.Text);
                    }
                }
                xDoc.Element("root").Element("mainPath").Value = this.txtMainPath.Text;
                xDoc.Save(XPath);
            }
        }

        /// <summary>
        /// 分组增加事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddList_Click(object sender, EventArgs e)
        {

            var listData = xDoc.Element("root").Element("groups");
            if (string.IsNullOrEmpty(this.txtAdd.Text))
            {
                MetroFramework.MetroMessageBox.Show(this, "请在文本框输入分组名称", "警告");
                return;
            }
            if (listData.Elements("item").Any(x => x.Value == this.txtAdd.Text))
            {
                MetroFramework.MetroMessageBox.Show(this, "已经存在该分组，请勿重复添加", "警告");

                this.txtAdd.Clear();
                return;


            }

            if (listData.Elements("item").Any())
            {
                listData.Add(new XElement("item", this.txtAdd.Text));
            }
            else
            {
                listData.Add(new XElement("item", this.txtAdd.Text, new XAttribute("default", "true")));
            }

            xDoc.Save(XPath);
            BindGroup();
            this.txtAdd.Clear();
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.contextMenuStrip1.Show(listView1, e.Location);
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var listData = xDoc.Element("root").Element("groups").Elements("item");
            foreach (ListViewItem item in this.listView1.SelectedItems)
            {
                if (Convert.ToInt32(item.SubItems[2].Text) > 0)
                {
                    MetroFramework.MetroMessageBox.Show(this, "改分组下还有文件，请先移动或删除", "警告");
                    return;
                }
                item.Remove();
                var ele = listData.SingleOrDefault(x => x.Value == item.SubItems[1].Text);
                if (ele.HasAttributes)
                {
                    var shen = listData.Where(x => x.Value != item.SubItems[1].Text);
                    if (shen.Any())
                    {
                        shen.First().Add(new XAttribute("default", "true"));
                    }
                }
                ele.Remove();
            }
            xDoc.Save(XPath);
            BindGroup();
        }

        private void 默认ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var listData = xDoc.Element("root").Element("groups").Elements("item");
            listData.SingleOrDefault(x => x.HasAttributes).RemoveAttributes();
            ListViewItem lvi = this.listView1.SelectedItems[0];
            listData.SingleOrDefault(x => x.Value == lvi.SubItems[1].Text).Add(new XAttribute("default", "true"));
            xDoc.Save(XPath);
            BindGroup();
        }
    }
}
