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
            //保存xml



            //修改路径后
            if (xDoc.Element("root").Element("mainPath").Value != this.txtMainPath.Text)
            {
                if (MetroFramework.MetroMessageBox.Show(this, "您已经改变备份路径，是否迁移文件到新路径？", "警告", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    //MessageBox.Show(this, "asd");
                }
            }
            this.DialogResult = DialogResult.OK;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            this.txtMainPath.Text = xDoc.Element("root").Element("mainPath").Value;
            //this.listBox1.
            var listData = xDoc.Element("root").Element("groups").Elements("item");
            foreach (var item in listData)
            {
                if (item.HasAttributes)
                {
                    this.listBox1.Items.Add("默认分组    " + item.Value);
                }
                else
                {
                    this.listBox1.Items.Add("            " + item.Value);
                }
            }
        }

        private void btnSelDir_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtMainPath.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        /// <summary>
        /// 分组增加事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddList_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtAdd.Text))
            {
                MetroFramework.MetroMessageBox.Show(this, "请在文本框输入分组名称", "警告");
                return;
            }
            if (this.listBox1.Items.Contains("默认分组    " + this.txtAdd.Text)|| this.listBox1.Items.Contains("            " + this.txtAdd.Text))
            {
                if (MetroFramework.MetroMessageBox.Show(this, "已经存在该分组，是否继续添加？", "警告", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    this.txtAdd.Clear();
                    return;
                }
                
            }
            this.listBox1.Items.Add("            " + this.txtAdd.Text);
            this.txtAdd.Clear();
        }
    }
}
