using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FileManager
{
    public partial class DevelopmentForm : Form
    {
        public DevelopmentForm()
        {
            InitializeComponent();
            LoadImages();
        }

        private void LoadImages()
        {
            try
            {
                LoadImagesFromFiles();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось загрузить изображения: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadImagesFromFiles()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string resourcesPath = Path.Combine(basePath, "Resources");

            // Создаем папку Resources, если она не существует
            if (!Directory.Exists(resourcesPath))
            {
                Directory.CreateDirectory(resourcesPath);
                MessageBox.Show("Папка Resources создана. Пожалуйста, добавьте файлы dev1.jpg и dev2.jpg",
                    "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            string image1Path = Path.Combine(resourcesPath, "C:\\Users\\Gigabyte-PC\\OneDrive\\Desktop\\FileManager\\FileManager\\FileManager\\bin\\Debug\\Resources\\Develop1.jpg");
            string image2Path = Path.Combine(resourcesPath, "C:\\Users\\Gigabyte-PC\\OneDrive\\Desktop\\FileManager\\FileManager\\FileManager\\bin\\Debug\\Resources\\Develop3.jpg");

            // Если файлы существуют, загружаем их
            if (File.Exists(image1Path) && File.Exists(image2Path))
            {
                try
                {
                    Bitmap image1 = new Bitmap(image1Path);
                    Bitmap image2 = new Bitmap(image2Path);

                    // Устанавливаем изображения
                    pbDevelopment1.Image = image1;
                    pbDevelopment2.Image = image2;

                    lblImage1.Text = "";
                    lblImage2.Text = "";

                    return; // Успешно загружены файлы
                }
                catch (Exception)
                {

                }
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Декоративная рамка
            using (Pen borderPen = new Pen(Color.FromArgb(0, 51, 102), 2))
            {
                e.Graphics.DrawRectangle(borderPen,
                    ClientRectangle.X + 1,
                    ClientRectangle.Y + 1,
                    ClientRectangle.Width - 3,
                    ClientRectangle.Height - 3);
            }
        }
    }
}