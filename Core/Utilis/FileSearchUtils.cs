using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace FileManager.Core
{
    public static class FileSearchUtils
    {
        // Преобразование маски файла в регулярное выражение
        public static Regex MaskToRegex(string mask)
        {
            if (string.IsNullOrEmpty(mask))
                mask = "*.*";

            string pattern = "^" +
                Regex.Escape(mask)
                     .Replace("\\*", ".*")
                     .Replace("\\?", ".") + "$";

            return new Regex(pattern, RegexOptions.IgnoreCase);
        }

        // Поиск файлов по маске с рекурсией
        public static IEnumerable<string> SearchFiles(string directory, string mask,
            bool searchSubdirectories = true,
            CancellationToken cancellationToken = default)
        {
            if (!Directory.Exists(directory))
                throw new DirectoryNotFoundException($"Директория не найдена: {directory}");

            var regex = MaskToRegex(mask);
            var searchOption = searchSubdirectories ?
                SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

            return Directory.EnumerateFiles(directory, "*.*", searchOption)
                .Where(file => regex.IsMatch(Path.GetFileName(file)))
                .Where(_ => !cancellationToken.IsCancellationRequested);
        }

        // Получение размера директории
        public static long GetDirectorySize(string path, bool includeSubdirectories = true)
        {
            if (!Directory.Exists(path))
                return 0;

            long size = 0;
            var searchOption = includeSubdirectories ?
                SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

            try
            {
                var files = Directory.GetFiles(path, "*.*", searchOption);
                foreach (var file in files)
                {
                    try
                    {
                        var info = new FileInfo(file);
                        size += info.Length;
                    }
                    catch
                    {
                        // Пропускаем файлы без доступа
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Пропускаем директории без доступа
            }
            catch (Exception)
            {
                // Другие ошибки
            }

            return size;
        }

        // Форматирование размера файла
        public static string FormatFileSize(long bytes)
        {
            if (bytes == 0) return "0 B";

            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;

            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len /= 1024;
            }

            return string.Format("{0:0.##} {1}", len, sizes[order]);
        }

        // Получение информации о файле
        public static FileInfoExtended GetFileInfo(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"Файл не найден: {path}");

            var fileInfo = new FileInfo(path);

            return new FileInfoExtended
            {
                FullPath = fileInfo.FullName,
                Name = fileInfo.Name,
                Size = fileInfo.Length,
                FormattedSize = FormatFileSize(fileInfo.Length),
                CreationTime = fileInfo.CreationTime,
                LastWriteTime = fileInfo.LastWriteTime,
                LastAccessTime = fileInfo.LastAccessTime,
                Attributes = fileInfo.Attributes,
                Extension = fileInfo.Extension,
                DirectoryName = fileInfo.DirectoryName
            };
        }

        // Получение информации о директории
        public static DirectoryInfoExtended GetDirectoryInfo(string path)
        {
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException($"Директория не найдена: {path}");

            var dirInfo = new DirectoryInfo(path);

            try
            {
                var files = dirInfo.GetFiles();
                var directories = dirInfo.GetDirectories();

                return new DirectoryInfoExtended
                {
                    FullPath = dirInfo.FullName,
                    Name = dirInfo.Name,
                    FileCount = files.Length,
                    DirectoryCount = directories.Length,
                    CreationTime = dirInfo.CreationTime,
                    LastWriteTime = dirInfo.LastWriteTime,
                    LastAccessTime = dirInfo.LastAccessTime,
                    Attributes = dirInfo.Attributes,
                    TotalSize = GetDirectorySize(path)
                };
            }
            catch (UnauthorizedAccessException)
            {
                // Возвращаем базовую информацию, если нет доступа
                return new DirectoryInfoExtended
                {
                    FullPath = dirInfo.FullName,
                    Name = dirInfo.Name,
                    FileCount = 0,
                    DirectoryCount = 0,
                    CreationTime = dirInfo.CreationTime,
                    LastWriteTime = dirInfo.LastWriteTime,
                    LastAccessTime = dirInfo.LastAccessTime,
                    Attributes = dirInfo.Attributes,
                    TotalSize = 0
                };
            }
        }
    }

    // Расширенные классы информации
    public class FileInfoExtended
    {
        public string FullPath { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }
        public string FormattedSize { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastWriteTime { get; set; }
        public DateTime LastAccessTime { get; set; }
        public FileAttributes Attributes { get; set; }
        public string Extension { get; set; }
        public string DirectoryName { get; set; }

        public override string ToString()
        {
            return $"{Name} ({FormattedSize})";
        }
    }

    public class DirectoryInfoExtended
    {
        public string FullPath { get; set; }
        public string Name { get; set; }
        public int FileCount { get; set; }
        public int DirectoryCount { get; set; }
        public long TotalSize { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastWriteTime { get; set; }
        public DateTime LastAccessTime { get; set; }
        public FileAttributes Attributes { get; set; }

        public override string ToString()
        {
            return $"{Name} (Файлов: {FileCount}, Папок: {DirectoryCount})";
        }
    }
}