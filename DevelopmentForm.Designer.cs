using System.Windows.Forms;

namespace FileManager
{
    partial class DevelopmentForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DevelopmentForm));
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.panelImages = new System.Windows.Forms.Panel();
            this.lblImage2 = new System.Windows.Forms.Label();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.lblImage1 = new System.Windows.Forms.Label();
            this.pbDevelopment2 = new System.Windows.Forms.PictureBox();
            this.pbDevelopment1 = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelImages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDevelopment2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDevelopment1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(776, 54);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🚧 Still in Development 🚧";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.lblSubtitle.Location = new System.Drawing.Point(12, 63);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(776, 34);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Программа находится в активной(нет) разработке. \r\n";
            this.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelImages
            // 
            this.panelImages.BackColor = System.Drawing.Color.White;
            this.panelImages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelImages.Controls.Add(this.lblImage2);
            this.panelImages.Controls.Add(this.txtInfo);
            this.panelImages.Controls.Add(this.lblImage1);
            this.panelImages.Controls.Add(this.pbDevelopment2);
            this.panelImages.Controls.Add(this.pbDevelopment1);
            this.panelImages.Location = new System.Drawing.Point(17, 100);
            this.panelImages.Name = "panelImages";
            this.panelImages.Size = new System.Drawing.Size(760, 397);
            this.panelImages.TabIndex = 2;
            // 
            // lblImage2
            // 
            this.lblImage2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblImage2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.lblImage2.Location = new System.Drawing.Point(564, 246);
            this.lblImage2.Name = "lblImage2";
            this.lblImage2.Size = new System.Drawing.Size(200, 20);
            this.lblImage2.TabIndex = 3;
            this.lblImage2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtInfo
            // 
            this.txtInfo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtInfo.Location = new System.Drawing.Point(161, 109);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.ReadOnly = true;
            this.txtInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInfo.Size = new System.Drawing.Size(419, 134);
            this.txtInfo.TabIndex = 3;
            this.txtInfo.Text = "В разработке:\r\n• Поддержка облачных хранилищ\r\n• Расширенный поиск по содержимому\r" +
    "\n• Интеграция с архиваторами\r\n• Поддержка FTP/SFTP протоколов\r\n• Улучшенная исто" +
    "рия операций";
            // 
            // lblImage1
            // 
            this.lblImage1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblImage1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.lblImage1.Location = new System.Drawing.Point(3, 246);
            this.lblImage1.Name = "lblImage1";
            this.lblImage1.Size = new System.Drawing.Size(200, 20);
            this.lblImage1.TabIndex = 2;
            this.lblImage1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbDevelopment2
            // 
            this.pbDevelopment2.BackColor = System.Drawing.Color.White;
            this.pbDevelopment2.Image = ((System.Drawing.Image)(resources.GetObject("pbDevelopment2.Image")));
            this.pbDevelopment2.Location = new System.Drawing.Point(611, 109);
            this.pbDevelopment2.Name = "pbDevelopment2";
            this.pbDevelopment2.Size = new System.Drawing.Size(148, 118);
            this.pbDevelopment2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDevelopment2.TabIndex = 1;
            this.pbDevelopment2.TabStop = false;
            // 
            // pbDevelopment1
            // 
            this.pbDevelopment1.BackColor = System.Drawing.Color.White;
            this.pbDevelopment1.Image = ((System.Drawing.Image)(resources.GetObject("pbDevelopment1.Image")));
            this.pbDevelopment1.Location = new System.Drawing.Point(18, 156);
            this.pbDevelopment1.Name = "pbDevelopment1";
            this.pbDevelopment1.Size = new System.Drawing.Size(46, 60);
            this.pbDevelopment1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDevelopment1.TabIndex = 0;
            this.pbDevelopment1.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(350, 470);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 35);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // DevelopmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(800, 520);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.panelImages);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DevelopmentForm";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "В разработке";
            this.panelImages.ResumeLayout(false);
            this.panelImages.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDevelopment2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDevelopment1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Label lblTitle;
        private Label lblSubtitle;
        private Panel panelImages;
        private PictureBox pbDevelopment2;
        private PictureBox pbDevelopment1;
        private Label lblImage2;
        private Label lblImage1;
        private TextBox txtInfo;
        private Button btnClose;
    }
}