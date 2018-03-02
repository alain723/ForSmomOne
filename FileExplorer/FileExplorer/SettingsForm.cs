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
    }
}
