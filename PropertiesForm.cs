using FileManager.Core;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FileManager
{
    public partial class PropertiesForm : Form
    {
        private string filePath;
        private bool isDirectory;
        private FileInfoExtended fileInfo;
        private DirectoryInfoExtended dirInfo;

        public PropertiesForm(string path)
        {
            InitializeComponent();
            filePath = path;
            isDirectory = Directory.Exists(path);
            LoadProperties();
        }

        private void LoadProperties()
        {
            try
            {
                if (isDirectory)
                {
                    dirInfo = FileSearchUtils.GetDirectoryInfo(filePath);
                }
                else
                {
                    fileInfo = FileSearchUtils.GetFileInfo(filePath);
                }

                UpdateUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке свойств: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateUI()
        {
            try
            {
                // Иконка
                if (isDirectory)
                {
                    picIcon.Image = CreateFolderIcon();
                    lblName.Text = Path.GetFileName(filePath);
                    lblType.Text = "Папка с файлами";
                }
                else
                {
                    picIcon.Image = CreateFileIcon();
                    lblName.Text = Path.GetFileName(filePath);
                    lblType.Text = GetFileTypeDescription();
                }

                lblLocation.Text = Path.GetDirectoryName(filePath);

                if (!isDirectory && fileInfo != null)
                {
                    lblSize.Text = $"{fileInfo.FormattedSize} ({fileInfo.Size:N0} байт)";
                    lblCreated.Text = fileInfo.CreationTime.ToString("f");
                    lblModified.Text = fileInfo.LastWriteTime.ToString("f");
                    lblAccessed.Text = fileInfo.LastAccessTime.ToString("f");
                    lblExtension.Text = fileInfo.Extension;

                    if (fileInfo.Size > 0)
                    {
                        double kb = fileInfo.Size / 1024.0;
                        double mb = kb / 1024.0;
                        lblSizeKB.Text = $"{kb:N2} KB";
                        lblSizeMB.Text = $"{mb:N2} MB";
                    }
                }
                else if (isDirectory && dirInfo != null)
                {
                    lblSize.Text = FileSearchUtils.FormatFileSize(dirInfo.TotalSize);
                    lblContains.Text = $"{dirInfo.FileCount} файлов, {dirInfo.DirectoryCount} папок";
                    lblCreated.Text = dirInfo.CreationTime.ToString("f");
                    lblModified.Text = dirInfo.LastWriteTime.ToString("f");
                    lblAccessed.Text = dirInfo.LastAccessTime.ToString("f");
                }

                // Атрибуты
                try
                {
                    FileAttributes attributes = File.GetAttributes(filePath);

                    chkReadOnly.Checked = (attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
                    chkHidden.Checked = (attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
                    chkArchive.Checked = (attributes & FileAttributes.Archive) == FileAttributes.Archive;
                    chkSystem.Checked = (attributes & FileAttributes.System) == FileAttributes.System;
                    chkCompressed.Checked = (attributes & FileAttributes.Compressed) == FileAttributes.Compressed;
                    chkEncrypted.Checked = (attributes & FileAttributes.Encrypted) == FileAttributes.Encrypted;

                    txtAllAttributes.Text = attributes.ToString();
                }
                catch
                {
                    // В случае ошибки оставляем атрибуты по умолчанию
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении интерфейса: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Image CreateFolderIcon()
        {
            Bitmap icon = new Bitmap(48, 48);
            using (Graphics g = Graphics.FromImage(icon))
            {
                g.Clear(Color.Transparent);
                g.FillRectangle(Brushes.Yellow, 8, 12, 32, 30);
                g.DrawRectangle(Pens.Black, 8, 12, 32, 30);
            }
            return icon;
        }

        private Image CreateFileIcon()
        {
            Bitmap icon = new Bitmap(48, 48);
            using (Graphics g = Graphics.FromImage(icon))
            {
                g.Clear(Color.Transparent);
                g.FillRectangle(Brushes.White, 12, 6, 24, 36);
                g.DrawRectangle(Pens.Black, 12, 6, 24, 36);
                g.DrawLine(Pens.Black, 12, 15, 36, 15);
            }
            return icon;
        }

        private string GetFileTypeDescription()
        {
            string extension = Path.GetExtension(filePath).ToLower();

            switch (extension)
            {
                case ".txt": return "Текстовый документ";
                case ".doc":
                case ".docx": return "Документ Microsoft Word";
                case ".xls":
                case ".xlsx": return "Таблица Microsoft Excel";
                case ".pdf": return "PDF документ";
                case ".jpg":
                case ".jpeg":
                case ".png":
                case ".bmp":
                case ".gif": return "Изображение";
                case ".mp3":
                case ".wav":
                case ".wma": return "Аудио файл";
                case ".mp4":
                case ".avi":
                case ".mkv": return "Видео файл";
                case ".exe": return "Приложение";
                case ".dll": return "Библиотека";
                case ".zip":
                case ".rar":
                case ".7z": return "Архив";
                case ".cs": return "Исходный код C#";
                default: return "Файл";
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}