using System.Windows.Forms;

namespace FileManager
{
    partial class PropertiesForm
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

            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.lblName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblContains = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblCreated = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblModified = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblAccessed = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblExtension = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lblSizeKB = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblSizeMB = new System.Windows.Forms.Label();
            this.tabAttributes = new System.Windows.Forms.TabPage();
            this.txtAllAttributes = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.chkEncrypted = new System.Windows.Forms.CheckBox();
            this.chkCompressed = new System.Windows.Forms.CheckBox();
            this.chkSystem = new System.Windows.Forms.CheckBox();
            this.chkArchive = new System.Windows.Forms.CheckBox();
            this.chkHidden = new System.Windows.Forms.CheckBox();
            this.chkReadOnly = new System.Windows.Forms.CheckBox();
            this.tabSecurity = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.tabAttributes.SuspendLayout();
            this.tabSecurity.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabGeneral);
            this.tabControl.Controls.Add(this.tabAttributes);
            this.tabControl.Controls.Add(this.tabSecurity);
            this.tabControl.Location = new System.Drawing.Point(10, 10);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(470, 500);
            this.tabControl.TabIndex = 0;
            // 
            // tabGeneral
            // 
            this.tabGeneral.AutoScroll = true;
            this.tabGeneral.Controls.Add(this.label19);
            this.tabGeneral.Controls.Add(this.lblSizeMB);
            this.tabGeneral.Controls.Add(this.label17);
            this.tabGeneral.Controls.Add(this.lblSizeKB);
            this.tabGeneral.Controls.Add(this.label15);
            this.tabGeneral.Controls.Add(this.lblExtension);
            this.tabGeneral.Controls.Add(this.label13);
            this.tabGeneral.Controls.Add(this.lblAccessed);
            this.tabGeneral.Controls.Add(this.label11);
            this.tabGeneral.Controls.Add(this.lblModified);
            this.tabGeneral.Controls.Add(this.label9);
            this.tabGeneral.Controls.Add(this.lblCreated);
            this.tabGeneral.Controls.Add(this.label7);
            this.tabGeneral.Controls.Add(this.lblContains);
            this.tabGeneral.Controls.Add(this.label5);
            this.tabGeneral.Controls.Add(this.lblSize);
            this.tabGeneral.Controls.Add(this.label3);
            this.tabGeneral.Controls.Add(this.lblLocation);
            this.tabGeneral.Controls.Add(this.label1);
            this.tabGeneral.Controls.Add(this.lblType);
            this.tabGeneral.Controls.Add(this.lblName);
            this.tabGeneral.Controls.Add(this.picIcon);
            this.tabGeneral.Location = new System.Drawing.Point(4, 24);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(462, 472);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "Общие";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // picIcon
            // 
            this.picIcon.Location = new System.Drawing.Point(10, 10);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(48, 48);
            this.picIcon.TabIndex = 0;
            this.picIcon.TabStop = false;
            // 
            // lblName
            // 
            this.lblName.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblName.Location = new System.Drawing.Point(70, 10);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(380, 25);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Имя файла";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Тип:";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(180, 70);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(28, 15);
            this.lblType.TabIndex = 3;
            this.lblType.Text = "Тип";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Расположение:";
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(180, 95);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(43, 15);
            this.lblLocation.TabIndex = 5;
            this.lblLocation.Text = "Путь";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 15);
            this.label5.TabIndex = 6;
            this.label5.Text = "Размер:";
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(180, 120);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(47, 15);
            this.lblSize.TabIndex = 7;
            this.lblSize.Text = "0 байт";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 145);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 15);
            this.label7.TabIndex = 8;
            this.label7.Text = "Содержит:";
            // 
            // lblContains
            // 
            this.lblContains.AutoSize = true;
            this.lblContains.Location = new System.Drawing.Point(180, 145);
            this.lblContains.Name = "lblContains";
            this.lblContains.Size = new System.Drawing.Size(13, 15);
            this.lblContains.TabIndex = 9;
            this.lblContains.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 170);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 15);
            this.label9.TabIndex = 10;
            this.label9.Text = "Создан:";
            // 
            // lblCreated
            // 
            this.lblCreated.AutoSize = true;
            this.lblCreated.Location = new System.Drawing.Point(180, 170);
            this.lblCreated.Name = "lblCreated";
            this.lblCreated.Size = new System.Drawing.Size(34, 15);
            this.lblCreated.TabIndex = 11;
            this.lblCreated.Text = "Дата";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(20, 195);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 15);
            this.label11.TabIndex = 12;
            this.label11.Text = "Изменен:";
            // 
            // lblModified
            // 
            this.lblModified.AutoSize = true;
            this.lblModified.Location = new System.Drawing.Point(180, 195);
            this.lblModified.Name = "lblModified";
            this.lblModified.Size = new System.Drawing.Size(34, 15);
            this.lblModified.TabIndex = 13;
            this.lblModified.Text = "Дата";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(20, 220);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(57, 15);
            this.label13.TabIndex = 14;
            this.label13.Text = "Открыт:";
            // 
            // lblAccessed
            // 
            this.lblAccessed.AutoSize = true;
            this.lblAccessed.Location = new System.Drawing.Point(180, 220);
            this.lblAccessed.Name = "lblAccessed";
            this.lblAccessed.Size = new System.Drawing.Size(34, 15);
            this.lblAccessed.TabIndex = 15;
            this.lblAccessed.Text = "Дата";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(20, 245);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(74, 15);
            this.label15.TabIndex = 16;
            this.label15.Text = "Расширение:";
            // 
            // lblExtension
            // 
            this.lblExtension.AutoSize = true;
            this.lblExtension.Location = new System.Drawing.Point(180, 245);
            this.lblExtension.Name = "lblExtension";
            this.lblExtension.Size = new System.Drawing.Size(74, 15);
            this.lblExtension.TabIndex = 17;
            this.lblExtension.Text = "Расширение";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(20, 270);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(88, 15);
            this.label17.TabIndex = 18;
            this.label17.Text = "В килобайтах:";
            // 
            // lblSizeKB
            // 
            this.lblSizeKB.AutoSize = true;
            this.lblSizeKB.Location = new System.Drawing.Point(180, 270);
            this.lblSizeKB.Name = "lblSizeKB";
            this.lblSizeKB.Size = new System.Drawing.Size(13, 15);
            this.lblSizeKB.TabIndex = 19;
            this.lblSizeKB.Text = "0";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(20, 295);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(91, 15);
            this.label19.TabIndex = 20;
            this.label19.Text = "В мегабайтах:";
            // 
            // lblSizeMB
            // 
            this.lblSizeMB.AutoSize = true;
            this.lblSizeMB.Location = new System.Drawing.Point(180, 295);
            this.lblSizeMB.Name = "lblSizeMB";
            this.lblSizeMB.Size = new System.Drawing.Size(13, 15);
            this.lblSizeMB.TabIndex = 21;
            this.lblSizeMB.Text = "0";
            // 
            // tabAttributes
            // 
            this.tabAttributes.AutoScroll = true;
            this.tabAttributes.Controls.Add(this.txtAllAttributes);
            this.tabAttributes.Controls.Add(this.label21);
            this.tabAttributes.Controls.Add(this.chkEncrypted);
            this.tabAttributes.Controls.Add(this.chkCompressed);
            this.tabAttributes.Controls.Add(this.chkSystem);
            this.tabAttributes.Controls.Add(this.chkArchive);
            this.tabAttributes.Controls.Add(this.chkHidden);
            this.tabAttributes.Controls.Add(this.chkReadOnly);
            this.tabAttributes.Location = new System.Drawing.Point(4, 24);
            this.tabAttributes.Name = "tabAttributes";
            this.tabAttributes.Padding = new System.Windows.Forms.Padding(3);
            this.tabAttributes.Size = new System.Drawing.Size(462, 472);
            this.tabAttributes.TabIndex = 1;
            this.tabAttributes.Text = "Атрибуты";
            this.tabAttributes.UseVisualStyleBackColor = true;
            // 
            // txtAllAttributes
            // 
            this.txtAllAttributes.Location = new System.Drawing.Point(20, 200);
            this.txtAllAttributes.Multiline = true;
            this.txtAllAttributes.Name = "txtAllAttributes";
            this.txtAllAttributes.ReadOnly = true;
            this.txtAllAttributes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAllAttributes.Size = new System.Drawing.Size(400, 100);
            this.txtAllAttributes.TabIndex = 7;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label21.Location = new System.Drawing.Point(20, 175);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(101, 15);
            this.label21.TabIndex = 6;
            this.label21.Text = "Все атрибуты:";
            // 
            // chkEncrypted
            // 
            this.chkEncrypted.AutoSize = true;
            this.chkEncrypted.Enabled = false;
            this.chkEncrypted.Location = new System.Drawing.Point(20, 150);
            this.chkEncrypted.Name = "chkEncrypted";
            this.chkEncrypted.Size = new System.Drawing.Size(112, 19);
            this.chkEncrypted.TabIndex = 5;
            this.chkEncrypted.Text = "Зашифрованный";
            this.chkEncrypted.UseVisualStyleBackColor = true;
            // 
            // chkCompressed
            // 
            this.chkCompressed.AutoSize = true;
            this.chkCompressed.Enabled = false;
            this.chkCompressed.Location = new System.Drawing.Point(20, 125);
            this.chkCompressed.Name = "chkCompressed";
            this.chkCompressed.Size = new System.Drawing.Size(78, 19);
            this.chkCompressed.TabIndex = 4;
            this.chkCompressed.Text = "Сжатый";
            this.chkCompressed.UseVisualStyleBackColor = true;
            // 
            // chkSystem
            // 
            this.chkSystem.AutoSize = true;
            this.chkSystem.Enabled = false;
            this.chkSystem.Location = new System.Drawing.Point(20, 100);
            this.chkSystem.Name = "chkSystem";
            this.chkSystem.Size = new System.Drawing.Size(80, 19);
            this.chkSystem.TabIndex = 3;
            this.chkSystem.Text = "Системный";
            this.chkSystem.UseVisualStyleBackColor = true;
            // 
            // chkArchive
            // 
            this.chkArchive.AutoSize = true;
            this.chkArchive.Enabled = false;
            this.chkArchive.Location = new System.Drawing.Point(20, 75);
            this.chkArchive.Name = "chkArchive";
            this.chkArchive.Size = new System.Drawing.Size(76, 19);
            this.chkArchive.TabIndex = 2;
            this.chkArchive.Text = "Архивный";
            this.chkArchive.UseVisualStyleBackColor = true;
            // 
            // chkHidden
            // 
            this.chkHidden.AutoSize = true;
            this.chkHidden.Enabled = false;
            this.chkHidden.Location = new System.Drawing.Point(20, 50);
            this.chkHidden.Name = "chkHidden";
            this.chkHidden.Size = new System.Drawing.Size(69, 19);
            this.chkHidden.TabIndex = 1;
            this.chkHidden.Text = "Скрытый";
            this.chkHidden.UseVisualStyleBackColor = true;
            // 
            // chkReadOnly
            // 
            this.chkReadOnly.AutoSize = true;
            this.chkReadOnly.Enabled = false;
            this.chkReadOnly.Location = new System.Drawing.Point(20, 25);
            this.chkReadOnly.Name = "chkReadOnly";
            this.chkReadOnly.Size = new System.Drawing.Size(116, 19);
            this.chkReadOnly.TabIndex = 0;
            this.chkReadOnly.Text = "Только для чтения";
            this.chkReadOnly.UseVisualStyleBackColor = true;
            // 
            // tabSecurity
            // 
            this.tabSecurity.Controls.Add(this.label2);
            this.tabSecurity.Location = new System.Drawing.Point(4, 24);
            this.tabSecurity.Name = "tabSecurity";
            this.tabSecurity.Size = new System.Drawing.Size(462, 472);
            this.tabSecurity.TabIndex = 2;
            this.tabSecurity.Text = "Разрешения";
            this.tabSecurity.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(20, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(400, 50);
            this.label2.TabIndex = 0;
            this.label2.Text = "Информация о разрешениях доступна только для администраторов системы";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(395, 520);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(310, 520);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // PropertiesForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(490, 555);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PropertiesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Свойства";
            this.tabControl.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.tabAttributes.ResumeLayout(false);
            this.tabAttributes.PerformLayout();
            this.tabSecurity.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabControl;
        private TabPage tabGeneral;
        private TabPage tabAttributes;
        private TabPage tabSecurity;
        private Button btnOK;
        private Button btnCancel;
        private PictureBox picIcon;
        private Label lblName;
        private Label label1;
        private Label lblType;
        private Label label3;
        private Label lblLocation;
        private Label label5;
        private Label lblSize;
        private Label label7;
        private Label lblContains;
        private Label label9;
        private Label lblCreated;
        private Label label11;
        private Label lblModified;
        private Label label13;
        private Label lblAccessed;
        private Label label15;
        private Label lblExtension;
        private Label label17;
        private Label lblSizeKB;
        private Label label19;
        private Label lblSizeMB;
        private CheckBox chkReadOnly;
        private CheckBox chkEncrypted;
        private CheckBox chkCompressed;
        private CheckBox chkSystem;
        private CheckBox chkArchive;
        private CheckBox chkHidden;
        private TextBox txtAllAttributes;
        private Label label21;
        private Label label2;
    }
}   