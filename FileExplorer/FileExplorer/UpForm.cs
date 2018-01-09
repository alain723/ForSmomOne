using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;
using FileExplorer.Model;
using FileExplorer.Common;

namespace FileExplorer
{
    public partial class UpForm : MetroForm
    {
        private FileTool fileTool;
        private List<UpFileModel> fileList;
        public UpForm()
        {
            InitializeComponent();
            metroStyleManager.Theme = MetroThemeStyle.Dark;
            metroStyleManager.Style = MetroColorStyle.Teal;
            fileTool = new FileTool();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

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
                    MetroFramework.MetroMessageBox.Show(this, "这个东西你电脑不认识~~", "哎呀");
                }

            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            LogHelper.WriteLog(this.GetType(), "选择了时间---" + this.dateTimePicker1.Value);
            foreach (var item in fileList)
            {
                item.Date = this.dateTimePicker1.Value;
            }
        }
    }
}
