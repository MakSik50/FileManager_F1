using System;
using System.Collections.Generic;
using System.IO;

namespace FileManager.Core
{
    // Базовый интерфейс команды
    public interface IFileCommand
    {
        void Execute();
        void Undo();
        string Description { get; }
    }

    // Абстрактный базовый класс для файловых команд
    public abstract class FileCommandBase : IFileCommand
    {
        protected string sourcePath;
        protected string destinationPath;
        protected string backupPath;
        protected bool executed = false;

        public abstract string Description { get; }

        public abstract void Execute();

        public abstract void Undo();

        protected void EnsureDirectoryExists(string path)
        {
            string directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }

    // Конкретные реализации команд
    public class CopyCommand : FileCommandBase
    {
        public CopyCommand(string source, string destination)
        {
            sourcePath = source;
            destinationPath = destination;
            backupPath = null;
        }

        public override string Description => $"Копирование: {Path.GetFileName(sourcePath)}";

        public override void Execute()
        {
            if (!File.Exists(sourcePath))
                throw new FileNotFoundException($"Файл не найден: {sourcePath}");

            EnsureDirectoryExists(destinationPath);

            // Создаем резервную копию, если файл уже существует
            if (File.Exists(destinationPath))
            {
                backupPath = destinationPath + ".backup";
                File.Copy(destinationPath, backupPath, true);
            }

            File.Copy(sourcePath, destinationPath, true);
            executed = true;
        }

        public override void Undo()
        {
            if (!executed) return;

            if (backupPath != null && File.Exists(backupPath))
            {
                File.Copy(backupPath, destinationPath, true);
                File.Delete(backupPath);
            }
            else if (File.Exists(destinationPath))
            {
                File.Delete(destinationPath);
            }
        }
    }

    public class MoveCommand : FileCommandBase
    {
        public MoveCommand(string source, string destination)
        {
            sourcePath = source;
            destinationPath = destination;
        }

        public override string Description => $"Перемещение: {Path.GetFileName(sourcePath)}";

        public override void Execute()
        {
            if (!File.Exists(sourcePath))
                throw new FileNotFoundException($"Файл не найден: {sourcePath}");

            EnsureDirectoryExists(destinationPath);

            // Сохраняем оригинал для отмены
            backupPath = sourcePath;

            File.Move(sourcePath, destinationPath);
            executed = true;
        }

        public override void Undo()
        {
            if (!executed) return;

            if (File.Exists(destinationPath))
            {
                File.Move(destinationPath, backupPath);
            }
        }
    }

    public class DeleteCommand : FileCommandBase
    {
        private string tempBackupPath;

        public DeleteCommand(string filePath)
        {
            sourcePath = filePath;
            destinationPath = null;
        }

        public override string Description => $"Удаление: {Path.GetFileName(sourcePath)}";

        public override void Execute()
        {
            if (!File.Exists(sourcePath))
                throw new FileNotFoundException($"Файл не найден: {sourcePath}");

            // Создаем временную резервную копию для отмены
            tempBackupPath = Path.Combine(Path.GetTempPath(),
                Guid.NewGuid().ToString() + Path.GetExtension(sourcePath));

            File.Copy(sourcePath, tempBackupPath, true);
            File.Delete(sourcePath);

            executed = true;
        }

        public override void Undo()
        {
            if (!executed || !File.Exists(tempBackupPath)) return;

            File.Copy(tempBackupPath, sourcePath, true);
            File.Delete(tempBackupPath);
        }
    }

    public class CreateDirectoryCommand : FileCommandBase
    {
        public CreateDirectoryCommand(string directoryPath)
        {
            sourcePath = directoryPath;
        }

        public override string Description => $"Создание каталога: {Path.GetFileName(sourcePath)}";

        public override void Execute()
        {
            if (Directory.Exists(sourcePath))
                throw new IOException($"Каталог уже существует: {sourcePath}");

            Directory.CreateDirectory(sourcePath);
            executed = true;
        }

        public override void Undo()
        {
            if (!executed) return;

            if (Directory.Exists(sourcePath))
            {
                Directory.Delete(sourcePath, true);
            }
        }
    }

    // Менеджер команд (для истории операций)
    public class CommandManager
    {
        private Stack<IFileCommand> history = new Stack<IFileCommand>();
        private Stack<IFileCommand> redoStack = new Stack<IFileCommand>();

        public void ExecuteCommand(IFileCommand command)
        {
            try
            {
                command.Execute();
                history.Push(command);
                redoStack.Clear(); // Очищаем стек повтора при новой команде
                OnCommandExecuted?.Invoke(command, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка выполнения команды: {ex.Message}", ex);
            }
        }

        public void Undo()
        {
            if (history.Count == 0) return;

            var command = history.Pop();
            command.Undo();
            redoStack.Push(command);
            OnCommandUndone?.Invoke(command, EventArgs.Empty);
        }

        public void Redo()
        {
            if (redoStack.Count == 0) return;

            var command = redoStack.Pop();
            command.Execute();
            history.Push(command);
            OnCommandRedone?.Invoke(command, EventArgs.Empty);
        }

        public bool CanUndo => history.Count > 0;
        public bool CanRedo => redoStack.Count > 0;

        public event EventHandler OnCommandExecuted;
        public event EventHandler OnCommandUndone;
        public event EventHandler OnCommandRedone;
    }
}