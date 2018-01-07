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

namespace FileExplorer
{
    public partial class MainForm : MetroForm
    {
        public MainForm()
        {
            InitializeComponent();
            metroStyleManager.Theme = MetroThemeStyle.Dark;
            metroStyleManager.Style = MetroColorStyle.Teal;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            FileTool ft = new FileTool();
            var data = ft.GetFileByDicPath(@"C:\Users\Alain\OneDrive\中瑞\2018\FCC");
            foreach (var item in data)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text= item.Name.Substring(0, item.Name.LastIndexOf('.'));
                this.listView1.Items.Add(lvi);
            }
        }
    }
}
