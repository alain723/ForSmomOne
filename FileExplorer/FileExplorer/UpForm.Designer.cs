using MetroFramework.Components;
using MetroFramework.Controls;
namespace FileExplorer
{
    partial class UpForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpForm));
            this.metroStyleManager = new MetroFramework.Components.MetroStyleManager(this.components);
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.btnCancel = new MetroFramework.Controls.MetroButton();
            this.btnOk = new MetroFramework.Controls.MetroButton();
            this.btnSelFile = new MetroFramework.Controls.MetroButton();
            this.listView1 = new System.Windows.Forms.ListView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager)).BeginInit();
            this.metroPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroStyleManager
            // 
            this.metroStyleManager.Owner = null;
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.dateTimePicker1);
            this.metroPanel1.Controls.Add(this.metroLabel1);
            this.metroPanel1.Controls.Add(this.listView1);
            this.metroPanel1.Controls.Add(this.btnSelFile);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(23, 63);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(689, 314);
            this.metroPanel1.TabIndex = 0;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // btnCancel
            // 
            this.btnCancel.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnCancel.Location = new System.Drawing.Point(422, 392);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 36);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "再见";
            this.btnCancel.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnCancel.UseSelectable = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnOk.Location = new System.Drawing.Point(533, 391);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(95, 36);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "好";
            this.btnOk.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnOk.UseSelectable = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnSelFile
            // 
            this.btnSelFile.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnSelFile.Location = new System.Drawing.Point(460, 16);
            this.btnSelFile.Name = "btnSelFile";
            this.btnSelFile.Size = new System.Drawing.Size(115, 49);
            this.btnSelFile.TabIndex = 2;
            this.btnSelFile.Text = "选一些文件...";
            this.btnSelFile.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btnSelFile.UseSelectable = true;
            this.btnSelFile.Click += new System.EventHandler(this.btnSelFile_Click);
            // 
            // listView1
            // 
            this.listView1.LargeImageList = this.imageList1;
            this.listView1.Location = new System.Drawing.Point(3, 4);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(445, 308);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "blank.png");
            this.imageList1.Images.SetKeyName(1, "excel.png");
            this.imageList1.Images.SetKeyName(2, "image.png");
            this.imageList1.Images.SetKeyName(3, "pdf.png");
            this.imageList1.Images.SetKeyName(4, "word.png");
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel1.Location = new System.Drawing.Point(460, 77);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(102, 25);
            this.metroLabel1.TabIndex = 5;
            this.metroLabel1.Text = "选个好日子";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(460, 105);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 21);
            this.dateTimePicker1.TabIndex = 6;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // UpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 442);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.metroPanel1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(736, 442);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(736, 442);
            this.Name = "UpForm";
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Default;
            this.Text = "存";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager)).EndInit();
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private MetroStyleManager metroStyleManager;
        private MetroPanel metroPanel1;
        private MetroButton btnCancel;
        private MetroButton btnOk;
        private System.Windows.Forms.ListView listView1;
        private MetroButton btnSelFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ImageList imageList1;
        private MetroLabel metroLabel1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}