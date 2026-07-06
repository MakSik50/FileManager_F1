using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileManager
{
    public class SearchManager
    {
        public event EventHandler<SearchProgressEventArgs> SearchProgress;
        public event EventHandler<SearchCompletedEventArgs> SearchCompleted;

        private CancellationTokenSource cancellationTokenSource;

        public async Task SearchAsync(string directory, string pattern, string content,
            bool includeSubdirectories, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                PerformSearch(directory, pattern, content, includeSubdirectories, cancellationToken);
            }, cancellationToken);
        }

        private void PerformSearch(string directory, string pattern, string content,
            bool includeSubdirectories, CancellationToken cancellationToken)
        {
            try
            {
                if (!Directory.Exists(directory))
                {
                    throw new DirectoryNotFoundException($"Директория не найдена: {directory}");
                }

                var regex = FileManager.Core.FileSearchUtils.MaskToRegex(pattern);
                var searchOption = includeSubdirectories
                    ? SearchOption.AllDirectories
                    : SearchOption.TopDirectoryOnly;

                var files = Directory.EnumerateFiles(directory, "*", searchOption);
                int processed = 0;
                int found = 0;

                foreach (var file in files)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        OnSearchCompleted(new SearchCompletedEventArgs
                        {
                            WasCancelled = true,
                            TotalFound = found
                        });
                        return;
                    }

                    processed++;

                    // Проверяем маску
                    if (regex.IsMatch(Path.GetFileName(file)))
                    {
                        bool contentMatches = string.IsNullOrEmpty(content);

                        // Если указан текст для поиска в содержимом
                        if (!contentMatches)
                        {
                            contentMatches = CheckFileContent(file, content);
                        }

                        if (contentMatches)
                        {
                            found++;
                            OnSearchProgress(new SearchProgressEventArgs
                            {
                                FileFound = file,
                                ProcessedCount = processed,
                                FoundCount = found
                            });
                        }
                    }

                    // Отправляем прогресс каждые 100 файлов
                    if (processed % 100 == 0)
                    {
                        OnSearchProgress(new SearchProgressEventArgs
                        {
                            ProcessedCount = processed,
                            FoundCount = found,
                            FileFound = null
                        });
                    }
                }

                OnSearchCompleted(new SearchCompletedEventArgs
                {
                    WasCancelled = false,
                    TotalFound = found,
                    TotalProcessed = processed
                });
            }
            catch (Exception ex)
            {
                OnSearchCompleted(new SearchCompletedEventArgs
                {
                    WasCancelled = false,
                    TotalFound = 0,
                    Error = ex
                });
            }
        }

        private bool CheckFileContent(string filePath, string searchText)
        {
            try
            {
                string extension = Path.GetExtension(filePath).ToLower();
                string[] textExtensions = { ".txt", ".cs", ".xml", ".json", ".html", ".htm", ".css", ".js", ".config", ".md", ".ini", ".log" };

                if (textExtensions.Contains(extension))
                {
                    string content = File.ReadAllText(filePath);
                    return content.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public void CancelSearch()
        {
            cancellationTokenSource?.Cancel();
        }

        protected virtual void OnSearchProgress(SearchProgressEventArgs e)
        {
            SearchProgress?.Invoke(this, e);
        }

        protected virtual void OnSearchCompleted(SearchCompletedEventArgs e)
        {
            SearchCompleted?.Invoke(this, e);
        }
    }

    public class SearchProgressEventArgs : EventArgs
    {
        public string FileFound { get; set; }
        public int ProcessedCount { get; set; }
        public int FoundCount { get; set; }
    }

    public class SearchCompletedEventArgs : EventArgs
    {
        public bool WasCancelled { get; set; }
        public int TotalFound { get; set; }
        public int TotalProcessed { get; set; }
        public Exception Error { get; set; }
    }
}