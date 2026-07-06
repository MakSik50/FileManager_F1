using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileManager.Core;

namespace FileManager
{
    public partial class MainForm : Form
    {
        // Менеджеры и вспомогательные объекты
        private CommandManager commandManager;
        private AdvancedFileSystemWatcher watcher;
        private string currentLeftDirectory;
        private string currentRightDirectory;
        private CancellationTokenSource searchCancellationTokenSource;
        private bool leftPanelActive = true;
        private bool isRenaming = false;
        private TextBox renameTextBox = null;
        private ListViewItem renameItem = null;


        // ToolTip для кнопок
        private ToolTip toolTip;

        // Для отображения "Этот компьютер"
        private const string THIS_COMPUTER = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";

        public MainForm()
        {
            InitializeComponent();
            CreateImageLists();
            InitializeCoreComponents();
            LoadDrives();
            SetupEventHandlers();
            SetInitialDirectories();
            InitializeRenameControls();

            // Устанавливаем подсказки для кнопок
            SetToolTips();

            // Устанавливаем минимальный размер формы
            this.MinimumSize = new Size(800, 600);
        }
        private void InitializeRenameControls()
        {
            renameTextBox = new TextBox
            {
                Visible = false,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 10),
                BackColor = Color.LightYellow
            };

            renameTextBox.KeyDown += RenameTextBox_KeyDown;
            renameTextBox.LostFocus += RenameTextBox_LostFocus;

            // Добавляем TextBox в Controls, чтобы он был доступен во всех панелях
            this.Controls.Add(renameTextBox);
            renameTextBox.BringToFront();
        }
        private void StartRenaming()
        {
            try
            {
                ListView listView = leftPanelActive ? listViewLeft : listViewRight;

                if (listView.SelectedItems.Count != 1)
                {
                    MessageBox.Show("Выберите один элемент для переименования", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                renameItem = listView.SelectedItems[0];

                if (renameItem.Tag is string filePath)
                {
                    // Проверяем, не диск ли это
                    if (IsDrivePath(filePath))
                    {
                        MessageBox.Show("Нельзя переименовать диск", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    isRenaming = true;

                    // Получаем позицию элемента
                    Rectangle rect = renameItem.GetBounds(ItemBoundsPortion.Label);
                    Point screenPos = listView.PointToScreen(rect.Location);
                    Point formPos = this.PointToClient(screenPos);

                    // Настраиваем TextBox
                    renameTextBox.Text = renameItem.Text;
                    renameTextBox.Bounds = new Rectangle(formPos.X, formPos.Y, rect.Width, rect.Height);
                    renameTextBox.Visible = true;
                    renameTextBox.Focus();
                    renameTextBox.SelectAll();
                }
            }
            catch (Exception ex)
            {
                ShowError("Ошибка при начале переименования", ex);
            }
        }

        private void RenameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CommitRename();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                CancelRename();
                e.Handled = true;
            }
        }

        private void RenameTextBox_LostFocus(object sender, EventArgs e)
        {
            if (isRenaming && renameTextBox.Visible)
            {
                CommitRename();
            }
        }

        private void CommitRename()
        {
            if (!isRenaming || renameItem == null) return;

            try
            {
                string newName = renameTextBox.Text.Trim();

                if (string.IsNullOrEmpty(newName))
                {
                    MessageBox.Show("Имя не может быть пустым", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CancelRename();
                    return;
                }

                if (renameItem.Tag is string oldPath)
                {
                    string directory = Path.GetDirectoryName(oldPath);
                    string newPath = Path.Combine(directory, newName);

                    // Проверяем, изменилось ли имя
                    if (Path.GetFileName(oldPath).Equals(newName, StringComparison.OrdinalIgnoreCase))
                    {
                        CancelRename();
                        return;
                    }

                    // Проверяем, существует ли уже файл/папка с таким именем
                    if (File.Exists(newPath) || Directory.Exists(newPath))
                    {
                        MessageBox.Show($"Элемент с именем '{newName}' уже существует", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        CancelRename();
                        return;
                    }

                    // Переименовываем файл или папку
                    if (File.Exists(oldPath))
                    {
                        File.Move(oldPath, newPath);
                        UpdateStatusBar($"Файл переименован: {Path.GetFileName(oldPath)} → {newName}");
                    }
                    else if (Directory.Exists(oldPath))
                    {
                        Directory.Move(oldPath, newPath);
                        UpdateStatusBar($"Папка переименована: {Path.GetFileName(oldPath)} → {newName}");

                        // Обновляем дерево, если переименовали папку в левой панели
                        if (leftPanelActive)
                        {
                            UpdateTreeViewAfterRename(oldPath, newPath);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Элемент не найден", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        CancelRename();
                        return;
                    }

                    // Обновляем отображение
                    renameItem.Text = newName;
                    renameItem.Tag = newPath;

                    // Обновляем информацию в подэлементах
                    if (renameItem.SubItems.Count > 3)
                    {
                        string type = renameItem.SubItems[2].Text;
                        if (type == "Папка" && File.Exists(newPath))
                        {
                            renameItem.SubItems[2].Text = Path.GetExtension(newPath);
                            var fileInfo = new FileInfo(newPath);
                            renameItem.SubItems[1].Text = FormatFileSize(fileInfo.Length);
                            renameItem.SubItems[3].Text = fileInfo.LastWriteTime.ToString("g");
                        }
                        else if (type != "Папка" && Directory.Exists(newPath))
                        {
                            renameItem.SubItems[2].Text = "Папка";
                            renameItem.SubItems[1].Text = "";
                            var dirInfo = new DirectoryInfo(newPath);
                            renameItem.SubItems[3].Text = dirInfo.LastWriteTime.ToString("g");
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Нет прав для переименования", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException ioEx)
            {
                MessageBox.Show($"Ошибка при переименовании: {ioEx.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                ShowError("Ошибка при переименовании", ex);
            }
            finally
            {
                CancelRename();
                RefreshCurrentPanel(); // Обновляем панель, чтобы увидеть изменения
            }
        }

        private void UpdateTreeViewAfterRename(string oldPath, string newPath)
        {
            try
            {
                TreeNode node = FindTreeNodeByPath(treeViewDirectories.Nodes, oldPath);
                if (node != null)
                {
                    node.Text = Path.GetFileName(newPath);
                    node.Tag = newPath;
                }
            }
            catch (Exception ex)
            {
                ShowError("Ошибка при обновлении дерева", ex);
            }
        }

        private void CancelRename()
        {
            isRenaming = false;
            renameTextBox.Visible = false;
            renameTextBox.Text = "";
            renameItem = null;
        }

        private void CreateImageLists()
        {
            // Создаем простые иконки для папки
            Bitmap folderIcon = new Bitmap(16, 16);
            using (Graphics g = Graphics.FromImage(folderIcon))
            {
                g.Clear(Color.Transparent);
                g.FillRectangle(Brushes.Yellow, 2, 4, 12, 10);
                g.DrawRectangle(Pens.Black, 2, 4, 12, 10);
            }

            // Создаем простые иконки для файла
            Bitmap fileIcon = new Bitmap(16, 16);
            using (Graphics g = Graphics.FromImage(fileIcon))
            {
                g.Clear(Color.Transparent);
                g.FillRectangle(Brushes.White, 3, 2, 10, 12);
                g.DrawRectangle(Pens.Black, 3, 2, 10, 12);
                g.DrawLine(Pens.Black, 3, 5, 13, 5);
            }

            // Создаем простые иконки для диска
            Bitmap driveIcon = new Bitmap(16, 16);
            using (Graphics g = Graphics.FromImage(driveIcon))
            {
                g.Clear(Color.Transparent);
                g.FillEllipse(Brushes.LightGray, 2, 2, 12, 12);
                g.DrawEllipse(Pens.Black, 2, 2, 12, 12);
                g.FillEllipse(Brushes.White, 5, 5, 6, 6);
            }

            // Создаем иконку "Этот компьютер"
            Bitmap computerIcon = new Bitmap(16, 16);
            using (Graphics g = Graphics.FromImage(computerIcon))
            {
                g.Clear(Color.Transparent);
                g.FillRectangle(Brushes.LightBlue, 2, 2, 12, 12);
                g.DrawRectangle(Pens.Black, 2, 2, 12, 12);
                g.FillRectangle(Brushes.White, 5, 5, 6, 6);
            }

            // Добавляем иконки в smallImageList
            if (smallImageList.Images.Count == 0)
            {
                smallImageList.Images.Add("folder", folderIcon);
                smallImageList.Images.Add("file", fileIcon);
                smallImageList.Images.Add("drive", driveIcon);
                smallImageList.Images.Add("computer", computerIcon);
            }

            // Для больших иконок масштабируем
            if (largeImageList.Images.Count == 0)
            {
                Bitmap folderIconLarge = new Bitmap(folderIcon, new Size(32, 32));
                Bitmap fileIconLarge = new Bitmap(fileIcon, new Size(32, 32));
                Bitmap driveIconLarge = new Bitmap(driveIcon, new Size(32, 32));
                Bitmap computerIconLarge = new Bitmap(computerIcon, new Size(32, 32));

                largeImageList.Images.Add("folder", folderIconLarge);
                largeImageList.Images.Add("file", fileIconLarge);
                largeImageList.Images.Add("drive", driveIconLarge);
                largeImageList.Images.Add("computer", computerIconLarge);
            }
        }

        private void SetToolTips()
        {
            toolTip = new ToolTip();
            toolTip.SetToolTip(btnCopyToRight, "Копировать в правую панель");
            toolTip.SetToolTip(btnMoveToRight, "Переместить в правую панель");
            toolTip.SetToolTip(btnDelete, "Удалить выбранное");
            toolTip.SetToolTip(btnProperties, "Свойства");
            toolTip.SetToolTip(btnRefresh, "Обновить");
            toolTip.SetToolTip(btnSearchPanel, "Поиск файлов");
            toolTip.SetToolTip(btnSearch, "Начать поиск файлов");
            toolTip.SetToolTip(chkCaseSensitive, "Учитывать регистр при поиске");
            toolTip.SetToolTip(chkSearchSubdirs, "Искать в подпапках");
            toolTip.SetToolTip(txtFileName, "Имя файла или маска (например: *.txt, документ.*)");
            toolTip.SetToolTip(txtExtension, "Расширение файла (например: txt, docx)");
        }

        private void InitializeCoreComponents()
        {
            commandManager = new CommandManager();
            watcher = new AdvancedFileSystemWatcher();

            // Подписываемся на события менеджера команд
            commandManager.OnCommandExecuted += CommandManager_OnCommandExecuted;
            commandManager.OnCommandUndone += CommandManager_OnCommandUndone;

            // Подписываемся на события FileSystemWatcher
            watcher.OnFileCreated += Watcher_OnFileCreated;
            watcher.OnFileDeleted += Watcher_OnFileDeleted;
            watcher.OnFileChanged += Watcher_OnFileChanged;
            watcher.OnFileRenamed += Watcher_OnFileRenamed;
            watcher.OnWatcherError += Watcher_OnWatcherError;
        }

        private void LoadDrives()
        {
            try
            {
                cboLeftDrive.Items.Clear();
                cboRightDrive.Items.Clear();

                // Добавляем "Этот компьютер" первым элементом
                cboLeftDrive.Items.Add(new DriveItem("Этот компьютер", THIS_COMPUTER));
                cboRightDrive.Items.Add(new DriveItem("Этот компьютер", THIS_COMPUTER));

                // Добавляем диски
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.IsReady)
                    {
                        string displayText = $"{drive.Name}";
                        if (!string.IsNullOrEmpty(drive.VolumeLabel))
                        {
                            displayText += $" ({drive.VolumeLabel})";
                        }
                        displayText += $" - {GetDriveTypeName(drive.DriveType)}";

                        cboLeftDrive.Items.Add(new DriveItem(displayText, drive.Name));
                        cboRightDrive.Items.Add(new DriveItem(displayText, drive.Name));
                    }
                }

                if (cboLeftDrive.Items.Count > 0)
                    cboLeftDrive.SelectedIndex = 0;

                if (cboRightDrive.Items.Count > 0)
                    cboRightDrive.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ShowError("Ошибка при загрузке дисков", ex);
            }
        }

        private string GetDriveTypeName(DriveType type)
        {
            switch (type)
            {
                case DriveType.Fixed: return "Локальный диск";
                case DriveType.Removable: return "Съемный диск";
                case DriveType.Network: return "Сетевой диск";
                case DriveType.CDRom: return "CD/DVD";
                case DriveType.Ram: return "RAM диск";
                default: return "Неизвестный";
            }
        }

        private void SetInitialDirectories()
        {
            try
            {
                // Устанавливаем текущие директории
                currentLeftDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                currentRightDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                txtLeftPath.Text = currentLeftDirectory;
                txtRightPath.Text = currentRightDirectory;

                // Загружаем содержимое
                LoadDirectoryContents(currentLeftDirectory, listViewLeft);
                LoadDirectoryContents(currentRightDirectory, listViewRight);

                // Загружаем дерево директорий
                LoadDirectoryTree();

                // Начинаем наблюдение за левой директорией
                watcher.Path = currentLeftDirectory;
                watcher.Start();
            }
            catch (Exception ex)
            {
                ShowError("Ошибка при инициализации директорий", ex);
            }
        }

        private void LoadDirectoryTree()
        {
            try
            {
                treeViewDirectories.BeginUpdate();
                treeViewDirectories.Nodes.Clear();

                // Добавляем корневой узел "Этот компьютер"
                var rootNode = new TreeNode("Этот компьютер")
                {
                    Tag = THIS_COMPUTER,
                    ImageIndex = 3, // индекс иконки компьютера
                    SelectedImageIndex = 3
                };

                treeViewDirectories.Nodes.Add(rootNode);

                // Добавляем диски как дочерние узлы
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.IsReady)
                    {
                        var driveNode = new TreeNode(drive.Name)
                        {
                            Tag = drive.Name,
                            ImageIndex = 2, // индекс иконки диска
                            SelectedImageIndex = 2
                        };

                        rootNode.Nodes.Add(driveNode);
                    }
                }

                rootNode.Expand();
            }
            catch (Exception ex)
            {
                ShowError("Ошибка при загрузке дерева директорий", ex);
            }
            finally
            {
                treeViewDirectories.EndUpdate();
            }
        }

        private void LoadSubdirectories(TreeNode parentNode)
        {
            try
            {
                string path = parentNode.Tag as string;
                if (string.IsNullOrEmpty(path) || path == THIS_COMPUTER) return;

                foreach (string dir in Directory.GetDirectories(path))
                {
                    try
                    {
                        var dirInfo = new DirectoryInfo(dir);
                        var node = new TreeNode(dirInfo.Name)
                        {
                            Tag = dir,
                            ImageIndex = 0,
                            SelectedImageIndex = 0
                        };

                        parentNode.Nodes.Add(node);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        // Пропускаем директории без доступа
                    }
                }
            }
            catch (Exception)
            {
                // Игнорируем ошибки при построении дерева
            }
        }

        private void SetupEventHandlers()
        {
            // Обработчики для левой панели
            cboLeftDrive.SelectedIndexChanged += CboDrive_SelectedIndexChanged;
            txtLeftPath.KeyPress += TxtPath_KeyPress;
            treeViewDirectories.AfterSelect += TreeViewDirectories_AfterSelect;
            treeViewDirectories.BeforeExpand += TreeViewDirectories_BeforeExpand;
            listViewLeft.SelectedIndexChanged += ListViewLeft_SelectedIndexChanged;
            listViewLeft.MouseClick += ListView_MouseClick;
            listViewLeft.DoubleClick += ListView_DoubleClick;
            listViewLeft.KeyDown += ListView_KeyDown;

            // Обработчики для правой панели
            cboRightDrive.SelectedIndexChanged += CboDrive_SelectedIndexChanged;
            txtRightPath.KeyPress += TxtPath_KeyPress;
            listViewRight.SelectedIndexChanged += ListViewRight_SelectedIndexChanged;
            listViewRight.MouseClick += ListView_MouseClick;
            listViewRight.DoubleClick += ListView_DoubleClick;
            listViewRight.KeyDown += ListView_KeyDown;

            // Обработчики кнопок
            btnCopyToRight.Click += BtnCopyToRight_Click;
            btnMoveToRight.Click += BtnMoveToRight_Click;
            btnDelete.Click += BtnDelete_Click;
            btnProperties.Click += BtnProperties_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnSearchPanel.Click += BtnSearchPanel_Click;

            // Обработчики поиска
            btnSearch.Click += BtnSearch_Click;
            btnCancelSearch.Click += BtnCancelSearch_Click;
            listViewSearchResults.DoubleClick += ListViewSearchResults_DoubleClick;

            // Контекстные меню
            CreateContextMenus();

            // Обработка изменения размера формы
            this.Resize += MainForm_Resize;

            // Меню
            menuFileExit.Click += MenuFileExit_Click;
            menuFileNewFolder.Click += MenuFileNewFolder_Click;
            menuEditCopy.Click += MenuEditCopy_Click;
            menuEditMove.Click += MenuEditMove_Click;
            menuEditDelete.Click += MenuEditDelete_Click;
            menuEditRename.Click += MenuEditRename_Click; // ДОБАВЛЕНО
            menuEditSelectAll.Click += MenuEditSelectAll_Click;
            menuEditDeselectAll.Click += MenuEditDeselectAll_Click;
            menuViewLargeIcons.Click += MenuViewLargeIcons_Click;
            menuViewSmallIcons.Click += MenuViewSmallIcons_Click;
            menuViewList.Click += MenuViewList_Click;
            menuViewDetails.Click += MenuViewDetails_Click;
            menuViewToggleTree.Click += MenuViewToggleTree_Click;
            menuViewToggleSearch.Click += MenuViewToggleSearch_Click;
            menuToolsSearch.Click += MenuToolsSearch_Click;
            menuToolsProperties.Click += MenuToolsProperties_Click;
            menuToolsRefresh.Click += MenuToolsRefresh_Click;
            menuHelpAbout.Click += MenuHelpAbout_Click;
            menuHelpDevelopment.Click += MenuHelpDevelopment_Click;

            // ДОБАВЛЕНО: обработчик изменения размера панели поиска
            panelSearch.Resize += panelSearch_Resize;
        }

        private void MenuHelpDevelopment_Click(object sender, EventArgs e)
        {
            ShowDevelopmentWindow();
        }
        private void MenuEditRename_Click(object sender, EventArgs e)
        {
            StartRenaming();
        }

        // ДОБАВЛЕНО: Метод для отображения окна разработки
        private void ShowDevelopmentWindow()
        {
            try
            {
                using (var devForm = new DevelopmentForm())
                {
                    devForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось открыть окно разработки: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateContextMenus()
        {
            contextMenuLeft.Items.Clear();
            contextMenuLeft.Items.Add("Копировать", null, (s, e) => CopySelectedItems(false));
            contextMenuLeft.Items.Add("Переместить", null, (s, e) => CopySelectedItems(true));
            contextMenuLeft.Items.Add("Удалить", null, (s, e) => DeleteSelectedItems());
            contextMenuLeft.Items.Add("Переименовать", null, (s, e) => StartRenaming());
            contextMenuLeft.Items.Add("Свойства", null, (s, e) => ShowProperties());
            contextMenuLeft.Items.Add("-");
            contextMenuLeft.Items.Add("Обновить", null, (s, e) => RefreshCurrentPanel());
            contextMenuLeft.Items.Add("Новая папка", null, (s, e) => CreateNewFolder());

            contextMenuRight.Items.Clear();
            contextMenuRight.Items.Add("Копировать", null, (s, e) => CopySelectedItems(false));
            contextMenuRight.Items.Add("Переместить", null, (s, e) => CopySelectedItems(true));
            contextMenuRight.Items.Add("Удалить", null, (s, e) => DeleteSelectedItems());
            contextMenuRight.Items.Add("Переименовать", null, (s, e) => StartRenaming());
            contextMenuRight.Items.Add("Свойства", null, (s, e) => ShowProperties());
            contextMenuRight.Items.Add("-");
            contextMenuRight.Items.Add("Обновить", null, (s, e) => RefreshCurrentPanel());
            contextMenuRight.Items.Add("Новая папка", null, (s, e) => CreateNewFolder());
        }

        #region Методы загрузки данных

        private void LoadDirectoryContents(string path, ListView listView)
        {
            try
            {
                listView.BeginUpdate();
                listView.Items.Clear();

                // Если путь равен "Этот компьютер", показываем диски
                if (path == THIS_COMPUTER)
                {
                    LoadDrivesToListView(listView);
                    UpdateStatusBar("Этот компьютер");
                    UpdateItemCount(listView);
                    return;
                }

                if (!Directory.Exists(path))
                {
                    UpdateStatusBar($"Директория не существует: {path}");
                    return;
                }

                // Загружаем папки
                foreach (string dir in Directory.GetDirectories(path))
                {
                    try
                    {
                        var dirInfo = new DirectoryInfo(dir);
                        var item = new ListViewItem(dirInfo.Name, 0);
                        item.SubItems.Add("");
                        item.SubItems.Add("Папка");
                        item.SubItems.Add(dirInfo.LastWriteTime.ToString("g"));
                        item.SubItems.Add(dirInfo.Attributes.ToString());
                        item.Tag = dir;
                        listView.Items.Add(item);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        // Пропускаем директории без доступа
                    }
                }

                // Загружаем файлы
                foreach (string file in Directory.GetFiles(path))
                {
                    try
                    {
                        var fileInfo = new FileInfo(file);
                        var item = new ListViewItem(fileInfo.Name, 1);
                        item.SubItems.Add(FormatFileSize(fileInfo.Length));
                        item.SubItems.Add(fileInfo.Extension);
                        item.SubItems.Add(fileInfo.LastWriteTime.ToString("g"));
                        item.SubItems.Add(fileInfo.Attributes.ToString());
                        item.Tag = file;
                        listView.Items.Add(item);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        // Пропускаем файлы без доступа
                    }
                }

                // Обновляем статус
                UpdateStatusBar($"Загружено: {path}");
                UpdateItemCount(listView);
            }
            catch (UnauthorizedAccessException)
            {
                ShowError("Отказано в доступе к директории", null);
            }
            catch (Exception ex)
            {
                ShowError("Ошибка при загрузке директории", ex);
            }
            finally
            {
                listView.EndUpdate();
            }
        }

        private void LoadDrivesToListView(ListView listView)
        {
            try
            {
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.IsReady)
                    {
                        try
                        {
                            string driveName = drive.Name;
                            string displayName = $"{driveName}";

                            if (!string.IsNullOrEmpty(drive.VolumeLabel))
                            {
                                displayName += $" ({drive.VolumeLabel})";
                            }

                            var item = new ListViewItem(displayName, 2); // индекс иконки диска
                            item.SubItems.Add("");
                            item.SubItems.Add(GetDriveTypeName(drive.DriveType));
                            item.SubItems.Add("");
                            item.SubItems.Add("");
                            item.Tag = driveName;
                            listView.Items.Add(item);
                        }
                        catch
                        {
                            // Пропускаем недоступные диски
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError("Ошибка при загрузке списка дисков", ex);
            }
        }

        #endregion

        #region Обработчики событий UI

        private void CboDrive_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox == null) return;

            if (comboBox.SelectedItem is DriveItem driveItem)
            {
                string path = driveItem.Path;

                if (comboBox == cboLeftDrive)
                {
                    leftPanelActive = true;
                    currentLeftDirectory = path;
                    txtLeftPath.Text = path;
                    LoadDirectoryContents(path, listViewLeft);

                    // Если выбран "Этот компьютер", очищаем дерево и загружаем заново
                    if (path == THIS_COMPUTER)
                    {
                        LoadDirectoryTree();
                    }
                }
                else if (comboBox == cboRightDrive)
                {
                    leftPanelActive = false;
                    currentRightDirectory = path;
                    txtRightPath.Text = path;
                    LoadDirectoryContents(path, listViewRight);
                }
            }
        }

        private void TxtPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                var textBox = sender as TextBox;
                if (textBox == txtLeftPath)
                {
                    NavigateToPath(txtLeftPath.Text, listViewLeft, ref currentLeftDirectory);
                }
                else if (textBox == txtRightPath)
                {
                    NavigateToPath(txtRightPath.Text, listViewRight, ref currentRightDirectory);
                }
            }
        }

        private void TreeViewDirectories_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag is string path)
            {
                leftPanelActive = true;
                currentLeftDirectory = path;
                txtLeftPath.Text = path;
                LoadDirectoryContents(path, listViewLeft);

                // Обновляем ComboBox
                UpdateComboBoxSelection(cboLeftDrive, path);
            }
        }

        private void TreeViewDirectories_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            // Динамическая загрузка поддиректорий при раскрытии узла
            if (e.Node.Tag is string path && path != THIS_COMPUTER && e.Node.Nodes.Count == 0)
            {
                LoadSubdirectories(e.Node);
            }
        }

        private void ListViewLeft_SelectedIndexChanged(object sender, EventArgs e)
        {
            leftPanelActive = true;
            UpdateSelectedSize(listViewLeft);
        }

        private void ListViewRight_SelectedIndexChanged(object sender, EventArgs e)
        {
            leftPanelActive = false;
            UpdateSelectedSize(listViewRight);
        }

        private void ListView_MouseClick(object sender, MouseEventArgs e)
        {
            var listView = sender as ListView;
            if (listView == listViewLeft)
                leftPanelActive = true;
            else if (listView == listViewRight)
                leftPanelActive = false;
        }

        private void ListView_DoubleClick(object sender, EventArgs e)
        {
            var listView = sender as ListView;
            if (listView.SelectedItems.Count > 0)
            {
                var selectedItem = listView.SelectedItems[0];
                if (selectedItem.Tag is string path)
                {
                    if (Directory.Exists(path))
                    {
                        // Навигация в директорию
                        if (listView == listViewLeft)
                        {
                            NavigateToPath(path, listViewLeft, ref currentLeftDirectory);
                            UpdateTreeViewSelection(path);
                            UpdateComboBoxSelection(cboLeftDrive, path);
                        }
                        else
                        {
                            NavigateToPath(path, listViewRight, ref currentRightDirectory);
                            UpdateComboBoxSelection(cboRightDrive, path);
                        }
                    }
                    else if (File.Exists(path))
                    {
                        // Открытие файла
                        try
                        {
                            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                            {
                                FileName = path,
                                UseShellExecute = true
                            });
                        }
                        catch (Exception ex)
                        {
                            ShowError("Не удалось открыть файл", ex);
                        }
                    }
                }
            }
        }

        private void ListView_KeyDown(object sender, KeyEventArgs e)
        {
            var listView = sender as ListView;
            if (listView == listViewLeft) leftPanelActive = true;
            else if (listView == listViewRight) leftPanelActive = false;

            if (e.KeyCode == Keys.F5)
            {
                RefreshCurrentPanel();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Delete && e.Control == false)
            {
                DeleteSelectedItems();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                ListView_DoubleClick(sender, e);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Back)
            {
                NavigateToParentDirectory();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F2)
            {
                StartRenaming();
                e.Handled = true;
            }
        }

        private void BtnCopyToRight_Click(object sender, EventArgs e)
        {
            CopySelectedItems(false);
        }

        private void BtnMoveToRight_Click(object sender, EventArgs e)
        {
            CopySelectedItems(true);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DeleteSelectedItems();
        }

        private void BtnProperties_Click(object sender, EventArgs e)
        {
            ShowProperties();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            RefreshCurrentPanel();
        }

        private void BtnSearchPanel_Click(object sender, EventArgs e)
        {
            ToggleSearchPanel();
        }

        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            await StartSearch();
        }

        private void BtnCancelSearch_Click(object sender, EventArgs e)
        {
            CancelSearch();
        }

        private void ListViewSearchResults_DoubleClick(object sender, EventArgs e)
        {
            if (listViewSearchResults.SelectedItems.Count > 0)
            {
                if (listViewSearchResults.SelectedItems[0].Tag is string filePath)
                {
                    if (File.Exists(filePath))
                    {
                        try
                        {
                            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                            {
                                FileName = Path.GetDirectoryName(filePath),
                                UseShellExecute = true
                            });
                        }
                        catch (Exception ex)
                        {
                            ShowError("Не удалось открыть директорию", ex);
                        }
                    }
                }
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            // Автоматическая подстройка размеров и позиций
            AdjustLayout();
        }

        private void AdjustLayout()
        {
            if (this.ClientSize.Width < 600) return;

            // Вычисляем ширину панелей (каждая занимает 45% ширины окна)
            int panelWidth = (int)(this.ClientSize.Width * 0.45);
            int spacing = 10;

            // Позиционируем левую панель
            panelLeft.Location = new Point(spacing, menuStrip.Height + spacing);
            panelLeft.Size = new Size(panelWidth, this.ClientSize.Height - menuStrip.Height - statusStrip.Height - spacing * 3 - (panelSearch.Visible ? panelSearch.Height : 0));

            // Позиционируем правую панель
            panelRight.Location = new Point(this.ClientSize.Width - panelWidth - spacing, menuStrip.Height + spacing);
            panelRight.Size = new Size(panelWidth, this.ClientSize.Height - menuStrip.Height - statusStrip.Height - spacing * 3 - (panelSearch.Visible ? panelSearch.Height : 0));

            // Позиционируем панель с кнопками посередине между панелями
            int centerX = panelLeft.Right + (panelRight.Left - panelLeft.Right) / 2;
            controlPanel.Location = new Point(centerX - controlPanel.Width / 2, menuStrip.Height + spacing + (panelLeft.Height - controlPanel.Height) / 2);

            // Позиционируем панель поиска
            if (panelSearch.Visible)
            {
                panelSearch.Location = new Point(spacing, panelLeft.Bottom + spacing);
                panelSearch.Size = new Size(this.ClientSize.Width - spacing * 2, 180);

                // Автоматически настраиваем ширину колонок listViewSearchResults
                AdjustSearchResultsColumns();
            }
        }
        private void panelSearch_Resize(object sender, EventArgs e)
        {
            if (panelSearch.Visible)
            {
                AdjustSearchResultsColumns();
            }
        }
        private void AdjustSearchResultsColumns()
        {
            if (listViewSearchResults.Columns.Count < 5) return;

            int totalWidth = listViewSearchResults.ClientSize.Width;

            if (totalWidth > 0)
            {
                // Рассчитываем ширину колонок в процентах
                // Имя: 25%, Путь: 35%, Размер: 15%, Тип: 10%, Изменен: 15%
                int[] columnWidths = {
            (int)(totalWidth * 0.25), // Имя
            (int)(totalWidth * 0.35), // Путь
            (int)(totalWidth * 0.15), // Размер
            (int)(totalWidth * 0.10), // Тип
            totalWidth - (int)(totalWidth * 0.25) - (int)(totalWidth * 0.35) - (int)(totalWidth * 0.15) - (int)(totalWidth * 0.10) // Оставшееся для "Изменен"
        };

                // Убедимся, что последняя колонка не отрицательная
                if (columnWidths[4] < 50)
                {
                    // Перераспределяем, если слишком мало места
                    int minWidth = 50;
                    int needed = minWidth - columnWidths[4];
                    if (needed > 0)
                    {
                        // Берем понемногу от каждой колонки
                        int perColumn = needed / 4;
                        for (int i = 0; i < 4; i++)
                        {
                            columnWidths[i] -= perColumn;
                            if (columnWidths[i] < 50) columnWidths[i] = 50;
                        }
                        columnWidths[4] = minWidth;
                    }
                }

                // Устанавливаем ширину колонок
                for (int i = 0; i < Math.Min(5, listViewSearchResults.Columns.Count); i++)
                {
                    listViewSearchResults.Columns[i].Width = columnWidths[i];
                }
            }
        }

        private void MenuFileExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MenuFileNewFolder_Click(object sender, EventArgs e)
        {
            CreateNewFolder();
        }

        private void MenuEditCopy_Click(object sender, EventArgs e)
        {
            CopySelectedItems(false);
        }

        private void MenuEditMove_Click(object sender, EventArgs e)
        {
            CopySelectedItems(true);
        }

        private void MenuEditDelete_Click(object sender, EventArgs e)
        {
            DeleteSelectedItems();
        }

        private void MenuEditSelectAll_Click(object sender, EventArgs e)
        {
            SelectAllItems();
        }

        private void MenuEditDeselectAll_Click(object sender, EventArgs e)
        {
            DeselectAllItems();
        }

        private void MenuViewLargeIcons_Click(object sender, EventArgs e)
        {
            ChangeView(View.LargeIcon);
        }

        private void MenuViewSmallIcons_Click(object sender, EventArgs e)
        {
            ChangeView(View.SmallIcon);
        }

        private void MenuViewList_Click(object sender, EventArgs e)
        {
            ChangeView(View.List);
        }

        private void MenuViewDetails_Click(object sender, EventArgs e)
        {
            ChangeView(View.Details);
        }

        private void MenuViewToggleTree_Click(object sender, EventArgs e)
        {
            ToggleTreeView();
        }

        private void MenuViewToggleSearch_Click(object sender, EventArgs e)
        {
            ToggleSearchPanel();
        }

        private void MenuToolsSearch_Click(object sender, EventArgs e)
        {
            ShowSearchPanel();
        }

        private void MenuToolsProperties_Click(object sender, EventArgs e)
        {
            ShowProperties();
        }

        private void MenuToolsRefresh_Click(object sender, EventArgs e)
        {
            RefreshCurrentPanel();
        }

        private void MenuHelpAbout_Click(object sender, EventArgs e)
        {
            ShowAbout();
        }

        #endregion

        #region Методы операций с файлами

        private void CopySelectedItems(bool move)
        {
            try
            {
                ListView sourceListView = leftPanelActive ? listViewLeft : listViewRight;
                string sourceDir = leftPanelActive ? currentLeftDirectory : currentRightDirectory;
                string destinationDir = leftPanelActive ? currentRightDirectory : currentLeftDirectory;

                if (sourceListView.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Не выбраны элементы для копирования", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (destinationDir == THIS_COMPUTER)
                {
                    MessageBox.Show("Нельзя копировать в 'Этот компьютер'", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!Directory.Exists(destinationDir))
                {
                    MessageBox.Show("Целевая директория не существует", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (sourceDir.Equals(destinationDir, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Исходная и целевая директории совпадают", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                foreach (ListViewItem item in sourceListView.SelectedItems)
                {
                    if (item.Tag is string sourcePath)
                    {
                        // Проверяем, что это не диск
                        if (IsDrivePath(sourcePath))
                        {
                            MessageBox.Show($"Нельзя копировать диск напрямую", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }

                        string destinationPath = Path.Combine(destinationDir, Path.GetFileName(sourcePath));

                        // Проверяем, не совпадают ли пути
                        if (sourcePath.Equals(destinationPath, StringComparison.OrdinalIgnoreCase))
                        {
                            MessageBox.Show($"Нельзя скопировать файл сам в себя: {Path.GetFileName(sourcePath)}",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }

                        if (File.Exists(sourcePath))
                        {
                            if (move)
                            {
                                var command = new MoveCommand(sourcePath, destinationPath);
                                commandManager.ExecuteCommand(command);
                            }
                            else
                            {
                                var command = new CopyCommand(sourcePath, destinationPath);
                                commandManager.ExecuteCommand(command);
                            }
                        }
                        else if (Directory.Exists(sourcePath))
                        {
                            // Рекурсивное копирование/перемещение директории
                            CopyDirectory(sourcePath, destinationPath, move);
                        }
                    }
                }

                RefreshBothPanels();
            }
            catch (Exception ex)
            {
                ShowError("Ошибка при копировании/перемещении", ex);
            }
        }

        private bool IsDrivePath(string path)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.Name.Equals(path, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        private void CopyDirectory(string sourceDir, string destinationDir, bool move)
        {
            try
            {
                // Создаем целевую директорию
                Directory.CreateDirectory(destinationDir);

                // Копируем файлы
                foreach (string file in Directory.GetFiles(sourceDir))
                {
                    string destFile = Path.Combine(destinationDir, Path.GetFileName(file));
                    if (move)
                    {
                        var command = new MoveCommand(file, destFile);
                        commandManager.ExecuteCommand(command);
                    }
                    else
                    {
                        var command = new CopyCommand(file, destFile);
                        commandManager.ExecuteCommand(command);
                    }
                }

                // Рекурсивно копируем поддиректории
                foreach (string dir in Directory.GetDirectories(sourceDir))
                {
                    string destSubDir = Path.Combine(destinationDir, Path.GetFileName(dir));
                    CopyDirectory(dir, destSubDir, move);
                }

                // Если это перемещение, удаляем исходную директорию
                if (move)
                {
                    Directory.Delete(sourceDir, true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при копировании директории {sourceDir}: {ex.Message}", ex);
            }
        }

        private void DeleteSelectedItems()
        {
            try
            {
                ListView listView = leftPanelActive ? listViewLeft : listViewRight;
                string currentDir = leftPanelActive ? currentLeftDirectory : currentRightDirectory;

                if (listView.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Не выбраны элементы для удаления", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Проверяем, не пытаемся ли удалить диск
                foreach (ListViewItem item in listView.SelectedItems)
                {
                    if (item.Tag is string path && IsDrivePath(path))
                    {
                        MessageBox.Show("Нельзя удалить диск", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string message = listView.SelectedItems.Count == 1
                    ? $"Удалить выбранный элемент?"
                    : $"Удалить выбранные элементы ({listView.SelectedItems.Count})?";

                if (MessageBox.Show(message, "Подтверждение удаления",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    foreach (ListViewItem item in listView.SelectedItems)
                    {
                        if (item.Tag is string path)
                        {
                            try
                            {
                                if (File.Exists(path))
                                {
                                    var command = new DeleteCommand(path);
                                    commandManager.ExecuteCommand(command);
                                }
                                else if (Directory.Exists(path))
                                {
                                    // Проверяем, не пытаемся ли удалить текущую директорию
                                    if (path.Equals(currentDir, StringComparison.OrdinalIgnoreCase))
                                    {
                                        MessageBox.Show("Нельзя удалить текущую директорию", "Ошибка",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        continue;
                                    }

                                    Directory.Delete(path, true);
                                    UpdateStatusBar($"Удалена директория: {Path.GetFileName(path)}");
                                }
                            }
                            catch (UnauthorizedAccessException)
                            {
                                MessageBox.Show($"Нет прав для удаления: {Path.GetFileName(path)}", "Ошибка",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            catch (IOException ioEx)
                            {
                                MessageBox.Show($"Ошибка при удалении {Path.GetFileName(path)}: {ioEx.Message}", "Ошибка",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }

                    RefreshCurrentPanel();
                }
            }
            catch (Exception ex)
            {
                ShowError("Ошибка при удалении", ex);
            }
        }

        private void CreateNewFolder()
        {
            try
            {
                string currentDir = leftPanelActive ? currentLeftDirectory : currentRightDirectory;

                if (currentDir == THIS_COMPUTER)
                {
                    MessageBox.Show("Нельзя создать папку в 'Этот компьютер'", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string newFolderName = "Новая папка";
                string newFolderPath = Path.Combine(currentDir, newFolderName);

                int counter = 1;
                while (Directory.Exists(newFolderPath))
                {
                    newFolderName = $"Новая папка ({counter})";
                    newFolderPath = Path.Combine(currentDir, newFolderName);
                    counter++;
                }

                Directory.CreateDirectory(newFolderPath);
                RefreshCurrentPanel();
                UpdateStatusBar($"Создана новая папка: {newFolderName}");
            }
            catch (Exception ex)
            {
                ShowError("Ошибка при создании папки", ex);
            }
        }

        private void ShowProperties()
        {
            try
            {
                ListView listView = leftPanelActive ? listViewLeft : listViewRight;

                if (listView.SelectedItems.Count == 1)
                {
                    if (listView.SelectedItems[0].Tag is string path)
                    {
                        // Проверяем, не диск ли это
                        if (IsDrivePath(path))
                        {
                            ShowDriveProperties(path);
                            return;
                        }

                        using (var propertiesForm = new PropertiesForm(path))
                        {
                            propertiesForm.ShowDialog();
                        }
                    }
                }
                else if (listView.SelectedItems.Count > 1)
                {
                    ShowMultipleItemsProperties(listView.SelectedItems);
                }
                else
                {
                    MessageBox.Show("Не выбран ни один элемент", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ShowError("Ошибка при отображении свойств", ex);
            }
        }

        private void ShowDriveProperties(string drivePath)
        {
            try
            {
                var driveInfo = new DriveInfo(drivePath);

                string properties = $"Свойства диска: {driveInfo.Name}\n\n" +
                    $"Тип диска: {GetDriveTypeName(driveInfo.DriveType)}\n" +
                    $"Метка тома: {driveInfo.VolumeLabel}\n" +
                    $"Файловая система: {driveInfo.DriveFormat}\n" +
                    $"Всего места: {FileSearchUtils.FormatFileSize(driveInfo.TotalSize)}\n" +
                    $"Свободно: {FileSearchUtils.FormatFileSize(driveInfo.TotalFreeSpace)}\n" +
                    $"Занято: {FileSearchUtils.FormatFileSize(driveInfo.TotalSize - driveInfo.TotalFreeSpace)}";

                MessageBox.Show(properties, "Свойства диска", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowError("Ошибка при получении свойств диска", ex);
            }
        }

        private void RefreshCurrentPanel()
        {
            if (leftPanelActive)
            {
                LoadDirectoryContents(currentLeftDirectory, listViewLeft);
            }
            else
            {
                LoadDirectoryContents(currentRightDirectory, listViewRight);
            }
        }

        private void RefreshBothPanels()
        {
            LoadDirectoryContents(currentLeftDirectory, listViewLeft);
            LoadDirectoryContents(currentRightDirectory, listViewRight);
        }

        private void SelectAllItems()
        {
            ListView listView = leftPanelActive ? listViewLeft : listViewRight;
            foreach (ListViewItem item in listView.Items)
            {
                item.Selected = true;
            }
        }

        private void DeselectAllItems()
        {
            ListView listView = leftPanelActive ? listViewLeft : listViewRight;
            listView.SelectedItems.Clear();
        }

        private void ChangeView(View view)
        {
            listViewLeft.View = view;
            listViewRight.View = view;
        }

        private void ToggleTreeView()
        {
            treeViewDirectories.Visible = !treeViewDirectories.Visible;
            AdjustLayout();
        }

        private void ToggleSearchPanel()
        {
            panelSearch.Visible = !panelSearch.Visible;
            AdjustLayout();
        }

        private void ShowSearchPanel()
        {
            if (!panelSearch.Visible)
            {
                panelSearch.Visible = true;
                AdjustLayout();
            }
        }

        private void ShowAbout()
        {
            MessageBox.Show("Файловый менеджер\n\n" +
                "Разработано студентами:\n" +
                "Максим и Елисей\n" +
                "Версия 1.1.1", "О программе",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Методы навигации

        private void NavigateToPath(string path, ListView listView, ref string currentDirectory)
        {
            try
            {
                if (path == THIS_COMPUTER || Directory.Exists(path))
                {
                    currentDirectory = path;
                    LoadDirectoryContents(path, listView);

                    if (listView == listViewLeft)
                    {
                        txtLeftPath.Text = path;
                        UpdateTreeViewSelection(path);
                        UpdateComboBoxSelection(cboLeftDrive, path);
                    }
                    else
                    {
                        txtRightPath.Text = path;
                        UpdateComboBoxSelection(cboRightDrive, path);
                    }
                }
                else if (File.Exists(path))
                {
                    MessageBox.Show("Указанный путь ведет к файлу, а не к директории", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Указанный путь не существует", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                ShowError("Ошибка при навигации", ex);
            }
        }

        private void NavigateToParentDirectory()
        {
            try
            {
                string currentDir = leftPanelActive ? currentLeftDirectory : currentRightDirectory;

                // Если текущая директория - "Этот компьютер", ничего не делаем
                if (currentDir == THIS_COMPUTER) return;

                string parentDir = Directory.GetParent(currentDir)?.FullName;

                if (!string.IsNullOrEmpty(parentDir) && Directory.Exists(parentDir))
                {
                    if (leftPanelActive)
                    {
                        currentLeftDirectory = parentDir;
                        txtLeftPath.Text = parentDir;
                        LoadDirectoryContents(parentDir, listViewLeft);
                        UpdateTreeViewSelection(parentDir);
                        UpdateComboBoxSelection(cboLeftDrive, parentDir);
                    }
                    else
                    {
                        currentRightDirectory = parentDir;
                        txtRightPath.Text = parentDir;
                        LoadDirectoryContents(parentDir, listViewRight);
                        UpdateComboBoxSelection(cboRightDrive, parentDir);
                    }
                }
                else
                {
                    // Если родительской директории нет, переходим к "Этот компьютер"
                    if (leftPanelActive)
                    {
                        currentLeftDirectory = THIS_COMPUTER;
                        txtLeftPath.Text = "Этот компьютер";
                        LoadDirectoryContents(THIS_COMPUTER, listViewLeft);
                        UpdateTreeViewSelection(THIS_COMPUTER);
                        UpdateComboBoxSelection(cboLeftDrive, THIS_COMPUTER);
                    }
                    else
                    {
                        currentRightDirectory = THIS_COMPUTER;
                        txtRightPath.Text = "Этот компьютер";
                        LoadDirectoryContents(THIS_COMPUTER, listViewRight);
                        UpdateComboBoxSelection(cboRightDrive, THIS_COMPUTER);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError("Ошибка при переходе в родительскую директорию", ex);
            }
        }

        private void UpdateTreeViewSelection(string path)
        {
            TreeNode node = FindTreeNodeByPath(treeViewDirectories.Nodes, path);
            if (node != null)
            {
                treeViewDirectories.SelectedNode = node;
                node.EnsureVisible();
            }
        }

        private TreeNode FindTreeNodeByPath(TreeNodeCollection nodes, string path)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Tag as string == path)
                    return node;

                TreeNode found = FindTreeNodeByPath(node.Nodes, path);
                if (found != null)
                    return found;
            }
            return null;
        }

        private void UpdateComboBoxSelection(ComboBox comboBox, string path)
        {
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                if (comboBox.Items[i] is DriveItem driveItem && driveItem.Path == path)
                {
                    comboBox.SelectedIndex = i;
                    return;
                }
            }
        }

        #endregion

        #region Методы поиска

        private async Task StartSearch()
        {
            try
            {
                if (searchCancellationTokenSource != null)
                {
                    MessageBox.Show("Поиск уже выполняется", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string searchDirectory = currentLeftDirectory;
                string fileName = txtFileName.Text.Trim();
                string extension = txtExtension.Text.Trim();
                bool caseSensitive = chkCaseSensitive.Checked;
                bool searchSubdirectories = chkSearchSubdirs.Checked;

                if (string.IsNullOrEmpty(fileName) && string.IsNullOrEmpty(extension))
                {
                    fileName = "*.*"; // По умолчанию ищем все файлы
                }
                else if (string.IsNullOrEmpty(fileName))
                {
                    fileName = "*"; // Ищем все файлы с указанным расширением
                }

                // Формируем паттерн для поиска
                string pattern = fileName;
                if (!string.IsNullOrEmpty(extension))
                {
                    if (!pattern.Contains("."))
                    {
                        pattern += $".{extension}";
                    }
                }

                if (searchDirectory == THIS_COMPUTER)
                {
                    MessageBox.Show("Нельзя выполнить поиск в 'Этот компьютер'", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrEmpty(searchDirectory) || !Directory.Exists(searchDirectory))
                {
                    MessageBox.Show("Укажите корректную директорию для поиска", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Показываем панель поиска
                panelSearch.Visible = true;
                listViewSearchResults.Items.Clear();
                progressBarSearch.Visible = true;
                progressBarSearch.Style = ProgressBarStyle.Marquee;
                btnSearch.Enabled = false;
                btnCancelSearch.Enabled = true;
                lblSearchStatus.Text = $"Поиск файлов по паттерну: {pattern}...";

                searchCancellationTokenSource = new CancellationTokenSource();

                // Запускаем асинхронный поиск
                await Task.Run(() => PerformSearchAsync(
                    searchDirectory,
                    pattern,
                    caseSensitive,
                    searchSubdirectories,
                    searchCancellationTokenSource.Token));
            }
            catch (Exception ex)
            {
                ShowError("Ошибка при запуске поиска", ex);
                ResetSearchUI();
            }
        }

        private void CancelSearch()
        {
            searchCancellationTokenSource?.Cancel();
            ResetSearchUI();
        }

        private Regex CreateMaskRegex(string mask, bool ignoreCase)
        {
            try
            {
                // Если маска пустая, возвращаем regex, соответствующий всем файлам
                if (string.IsNullOrEmpty(mask) || mask == "*.*" || mask == "*")
                {
                    return new Regex(".*", ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None);
                }

                // Экранируем специальные символы, кроме * и ?
                string pattern = Regex.Escape(mask);

                // Заменяем экранированные * и ? на соответствующие шаблоны
                pattern = pattern
                    .Replace("\\*", ".*")
                    .Replace("\\?", ".");

                // Если маска не содержит расширение, добавляем возможность любого расширения
                if (!pattern.Contains("\\.") && !pattern.EndsWith(".*"))
                {
                    pattern = "^" + pattern + "(\\.\\w+)?$";
                }
                else
                {
                    pattern = "^" + pattern + "$";
                }

                return new Regex(pattern, ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None);
            }
            catch
            {
                // Если ошибка, возвращаем паттерн для всех файлов
                return new Regex(".*", ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None);
            }
        }

        private async Task PerformSearchAsync(string directory, string pattern,
            bool caseSensitive, bool searchSubdirectories, CancellationToken cancellationToken)
        {
            try
            {
                Regex regex = CreateMaskRegex(pattern, !caseSensitive);

                // Используем класс для хранения состояния поиска
                var searchState = new SearchState();

                await Task.Run(() =>
                {
                    SafeSearchFiles(directory, regex, searchSubdirectories,
                        cancellationToken, searchState);
                }, cancellationToken);

                this.Invoke((MethodInvoker)delegate
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        lblSearchStatus.Text = "Поиск отменен";
                    }
                    else
                    {
                        string status = $"Поиск завершен. Найдено: {searchState.FoundCount} файлов";
                        if (searchState.ErrorCount > 0)
                        {
                            status += $" (файлов с ошибками доступа: {searchState.ErrorCount})";
                        }
                        lblSearchStatus.Text = status;
                    }
                });
            }
            catch (OperationCanceledException)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    lblSearchStatus.Text = "Поиск отменен";
                });
            }
            catch (Exception ex)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    ShowError("Ошибка при поиске", ex);
                });
            }
            finally
            {
                this.Invoke((MethodInvoker)delegate
                {
                    ResetSearchUI();
                });
            }
        }

        private class SearchState
        {
            public int FoundCount { get; set; }
            public int ProcessedCount { get; set; }
            public int ErrorCount { get; set; }
        }

        private void SafeSearchFiles(string directory, Regex regex, bool searchSubdirectories,
            CancellationToken cancellationToken, SearchState state)
        {
            try
            {
                // Получаем все файлы в текущей директории
                string[] files;
                try
                {
                    files = Directory.GetFiles(directory);
                }
                catch (UnauthorizedAccessException)
                {
                    state.ErrorCount++;
                    return;
                }

                foreach (string file in files)
                {
                    if (cancellationToken.IsCancellationRequested)
                        return;

                    state.ProcessedCount++;

                    string fileName = Path.GetFileName(file);

                    // Проверяем, соответствует ли имя файла регулярному выражению
                    if (regex.IsMatch(fileName))
                    {
                        state.FoundCount++;

                        // Получаем информацию о файле
                        bool isAccessible = true;
                        long size = 0;
                        DateTime lastModified = DateTime.MinValue;
                        string fileStatus = "Доступен";

                        try
                        {
                            var fileInfo = new FileInfo(file);
                            size = fileInfo.Length;
                            lastModified = fileInfo.LastWriteTime;
                            fileStatus = "Доступен";
                        }
                        catch (UnauthorizedAccessException)
                        {
                            isAccessible = false;
                            fileStatus = "Отказано в доступе";
                            state.ErrorCount++;
                        }
                        catch (IOException)
                        {
                            isAccessible = false;
                            fileStatus = "Ошибка доступа";
                            state.ErrorCount++;
                        }

                        // Обновляем UI
                        this.Invoke((MethodInvoker)delegate
                        {
                            AddSearchResult(file, size, lastModified, fileStatus);

                            // Обновляем статус каждые 10 найденных файлов
                            if (state.FoundCount % 10 == 0)
                            {
                                lblSearchStatus.Text = $"Найдено: {state.FoundCount} (ошибок: {state.ErrorCount})";
                            }
                        });
                    }

                    // Обновляем прогресс каждые 100 файлов
                    if (state.ProcessedCount % 100 == 0)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            lblSearchStatus.Text = $"Обработано: {state.ProcessedCount}, Найдено: {state.FoundCount}";
                        });
                    }
                }

                // Если нужно искать в поддиректориях
                if (searchSubdirectories)
                {
                    string[] subdirectories;
                    try
                    {
                        subdirectories = Directory.GetDirectories(directory);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        return;
                    }

                    foreach (string subdir in subdirectories)
                    {
                        if (cancellationToken.IsCancellationRequested)
                            return;

                        try
                        {
                            SafeSearchFiles(subdir, regex, true, cancellationToken, state);
                        }
                        catch (UnauthorizedAccessException)
                        {
                            state.ErrorCount++;
                            continue;
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                state.ErrorCount++;
            }
        }

        private void AddSearchResult(string filePath, long size, DateTime lastModified, string status)
        {
            try
            {
                var item = new ListViewItem(Path.GetFileName(filePath));
                item.SubItems.Add(Path.GetDirectoryName(filePath));
                item.SubItems.Add(status == "Доступен" ? FormatFileSize(size) : "Нет доступа");
                item.SubItems.Add(Path.GetExtension(filePath));
                item.SubItems.Add(lastModified.ToString("g"));
                item.SubItems.Add(status);
                item.Tag = filePath;

                if (status == "Отказано в доступе")
                {
                    item.ForeColor = Color.Red;
                }
                else if (status == "Ошибка доступа")
                {
                    item.ForeColor = Color.Orange;
                }

                listViewSearchResults.Items.Add(item);
            }
            catch
            {
                // Пропускаем файлы с ошибками
            }
        }

        private void ResetSearchUI()
        {
            progressBarSearch.Visible = false;
            btnSearch.Enabled = true;
            btnCancelSearch.Enabled = false;
            searchCancellationTokenSource?.Dispose();
            searchCancellationTokenSource = null;
        }

        #endregion

        #region Методы отображения свойств

        private void ShowMultipleItemsProperties(ListView.SelectedListViewItemCollection items)
        {
            try
            {
                long totalSize = 0;
                int fileCount = 0;
                int dirCount = 0;

                foreach (ListViewItem item in items)
                {
                    if (item.Tag is string path)
                    {
                        if (File.Exists(path))
                        {
                            fileCount++;
                            totalSize += new FileInfo(path).Length;
                        }
                        else if (Directory.Exists(path))
                        {
                            dirCount++;
                            totalSize += FileSearchUtils.GetDirectorySize(path, true);
                        }
                    }
                }

                string properties = $"Свойства выбранных элементов:\n\n" +
                    $"Всего элементов: {items.Count}\n" +
                    $"Файлов: {fileCount}\n" +
                    $"Папок: {dirCount}\n" +
                    $"Общий размер: {FileSearchUtils.FormatFileSize(totalSize)}";

                MessageBox.Show(properties, "Свойства элементов", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowError("Ошибка при получении свойств элементов", ex);
            }
        }

        #endregion

        #region Вспомогательные методы

        private void UpdateStatusBar(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => lblStatus.Text = message));
            }
            else
            {
                lblStatus.Text = message;
            }
        }

        private void UpdateItemCount(ListView listView)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    lblItemCount.Text = $"Элементов: {listView.Items.Count}";
                }));
            }
            else
            {
                lblItemCount.Text = $"Элементов: {listView.Items.Count}";
            }
        }

        private void UpdateSelectedSize(ListView listView)
        {
            try
            {
                long totalSize = 0;
                int selectedCount = listView.SelectedItems.Count;

                foreach (ListViewItem item in listView.SelectedItems)
                {
                    if (item.Tag is string path && File.Exists(path))
                    {
                        totalSize += new FileInfo(path).Length;
                    }
                }

                lblSelectedSize.Text = $"Выбрано: {selectedCount} ({FormatFileSize(totalSize)})";
            }
            catch
            {
                lblSelectedSize.Text = "Выбрано: -";
            }
        }

        private string FormatFileSize(long bytes)
        {
            return FileSearchUtils.FormatFileSize(bytes);
        }

        private void ShowError(string message, Exception ex)
        {
            string errorMessage = message;
            if (ex != null)
            {
                errorMessage += $"\n\nДетали: {ex.Message}";
            }

            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                    MessageBox.Show(errorMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)));
            }
            else
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Обработчики событий CommandManager

        private void CommandManager_OnCommandExecuted(object sender, EventArgs e)
        {
            if (sender is IFileCommand command)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    UpdateStatusBar($"Выполнено: {command.Description}");
                    RefreshBothPanels();
                });
            }
        }

        private void CommandManager_OnCommandUndone(object sender, EventArgs e)
        {
            if (sender is IFileCommand command)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    UpdateStatusBar($"Отменено: {command.Description}");
                    RefreshBothPanels();
                });
            }
        }

        #endregion

        #region Обработчики событий FileSystemWatcher

        private void Watcher_OnFileCreated(object sender, FileSystemEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                UpdateStatusBar($"Создан файл: {e.Name}");
                RefreshBothPanels();
            });
        }

        private void Watcher_OnFileDeleted(object sender, FileSystemEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                UpdateStatusBar($"Удален файл: {e.Name}");
                RefreshBothPanels();
            });
        }

        private void Watcher_OnFileChanged(object sender, FileSystemEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                UpdateStatusBar($"Изменен файл: {e.Name}");
                RefreshBothPanels();
            });
        }

        private void Watcher_OnFileRenamed(object sender, RenamedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                UpdateStatusBar($"Переименован: {e.OldName} → {e.Name}");
                RefreshBothPanels();
            });
        }

        private void Watcher_OnWatcherError(object sender, AdvancedFileSystemWatcher.FileSystemErrorEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                MessageBox.Show($"Ошибка FileSystemWatcher: {e.Exception.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            });
        }

        #endregion

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            searchCancellationTokenSource?.Cancel();
            watcher?.Dispose();
            base.OnFormClosing(e);
        }

        // Вспомогательный класс для ComboBox дисков
        private class DriveItem
        {
            public string DisplayText { get; }
            public string Path { get; }

            public DriveItem(string displayText, string path)
            {
                DisplayText = displayText;
                Path = path;
            }

            public override string ToString()
            {
                return DisplayText;
            }
        }
    }
}