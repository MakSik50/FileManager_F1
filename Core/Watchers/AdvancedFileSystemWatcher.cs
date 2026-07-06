using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace FileManager.Core
{
    public class AdvancedFileSystemWatcher : IDisposable
    {
        private FileSystemWatcher watcher;
        private Timer debounceTimer;
        private string lastChangedPath;
        private const int DEBOUNCE_DELAY = 500; // мс

        private Dictionary<string, FileSystemEventInfo> pendingEvents = new Dictionary<string, FileSystemEventInfo>();

        public string Path
        {
            get => watcher.Path;
            set
            {
                watcher.Path = value;
                watcher.EnableRaisingEvents = !string.IsNullOrEmpty(value);
            }
        }

        public bool IncludeSubdirectories
        {
            get => watcher.IncludeSubdirectories;
            set => watcher.IncludeSubdirectories = value;
        }

        public AdvancedFileSystemWatcher()
        {
            watcher = new FileSystemWatcher();
            ConfigureWatcher();
            debounceTimer = new Timer(DebounceTimerCallback, null, Timeout.Infinite, Timeout.Infinite);
        }

        public AdvancedFileSystemWatcher(string path) : this()
        {
            Path = path;
        }

        private void ConfigureWatcher()
        {
            watcher.NotifyFilter = NotifyFilters.FileName
                | NotifyFilters.DirectoryName
                | NotifyFilters.Size
                | NotifyFilters.LastWrite
                | NotifyFilters.CreationTime
                | NotifyFilters.Attributes;

            watcher.Created += OnCreated;
            watcher.Deleted += OnDeleted;
            watcher.Renamed += OnRenamed;
            watcher.Changed += OnChanged;

            watcher.Error += OnError;
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            DebounceEvent(e.FullPath, FileSystemEventType.Created, null, e);
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            DebounceEvent(e.FullPath, FileSystemEventType.Deleted, null, e);
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            DebounceEvent(e.FullPath, FileSystemEventType.Changed, null, e);
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            DebounceEvent(e.FullPath, FileSystemEventType.Renamed, e.OldFullPath, e);
        }

        private void OnError(object sender, ErrorEventArgs e)
        {
            OnWatcherError?.Invoke(this, new FileSystemErrorEventArgs(e.GetException()));
        }

        private void DebounceEvent(string path, FileSystemEventType eventType, string oldPath, FileSystemEventArgs originalArgs)
        {
            lastChangedPath = path;

            // Сбрасываем таймер и запускаем заново
            debounceTimer.Change(DEBOUNCE_DELAY, Timeout.Infinite);

            // Сохраняем информацию о событии для обработки
            var eventInfo = new FileSystemEventInfo
            {
                Path = path,
                OldPath = oldPath,
                EventType = eventType,
                Timestamp = DateTime.Now,
                OriginalEventArgs = originalArgs
            };

            lock (pendingEvents)
            {
                pendingEvents[path] = eventInfo;
            }
        }

        private void DebounceTimerCallback(object state)
        {
            Dictionary<string, FileSystemEventInfo> eventsToProcess;

            lock (pendingEvents)
            {
                eventsToProcess = new Dictionary<string, FileSystemEventInfo>(pendingEvents);
                pendingEvents.Clear();
            }

            foreach (var eventInfo in eventsToProcess.Values)
            {
                switch (eventInfo.EventType)
                {
                    case FileSystemEventType.Created:
                        OnFileCreated?.Invoke(this, (FileSystemEventArgs)eventInfo.OriginalEventArgs);
                        break;
                    case FileSystemEventType.Deleted:
                        OnFileDeleted?.Invoke(this, (FileSystemEventArgs)eventInfo.OriginalEventArgs);
                        break;
                    case FileSystemEventType.Changed:
                        OnFileChanged?.Invoke(this, (FileSystemEventArgs)eventInfo.OriginalEventArgs);
                        break;
                    case FileSystemEventType.Renamed:
                        OnFileRenamed?.Invoke(this, (RenamedEventArgs)eventInfo.OriginalEventArgs);
                        break;
                }
            }
        }

        public void Start()
        {
            if (!string.IsNullOrEmpty(watcher.Path))
            {
                watcher.EnableRaisingEvents = true;
            }
        }

        public void Stop()
        {
            watcher.EnableRaisingEvents = false;
        }

        public void Dispose()
        {
            debounceTimer?.Dispose();
            watcher?.Dispose();
        }

        // События
        public event FileSystemEventHandler OnFileCreated;
        public event FileSystemEventHandler OnFileDeleted;
        public event FileSystemEventHandler OnFileChanged;
        public event RenamedEventHandler OnFileRenamed;
        public event EventHandler<FileSystemErrorEventArgs> OnWatcherError;

        // Вспомогательные классы
        public enum FileSystemEventType
        {
            Created,
            Deleted,
            Changed,
            Renamed
        }

        public class FileSystemEventInfo
        {
            public string Path { get; set; }
            public string OldPath { get; set; }
            public FileSystemEventType EventType { get; set; }
            public DateTime Timestamp { get; set; }
            public FileSystemEventArgs OriginalEventArgs { get; set; }
        }

        public class FileSystemErrorEventArgs : EventArgs
        {
            public Exception Exception { get; }

            public FileSystemErrorEventArgs(Exception ex)
            {
                Exception = ex;
            }
        }
    }
}