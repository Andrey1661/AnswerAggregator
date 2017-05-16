using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BL.DTO;
using BL.Enviroment;
using BL.Services.Interfaces;

namespace BL.Services
{
    public enum FileMode
    {
        Add,
        Replace,
        ReplaceEqual
    }

    internal class FileService : IFileService
    {
        public OperationResult SaveFile(string path, FileModel file)
        {
            return SaveFile(path, file, FileMode.ReplaceEqual);
        }

        public OperationResult SaveFile(string path, FileModel file, FileMode mode)
        {
            try
            {
                CheckDirectory(path, mode);

                var fullPath = Path.Combine(path, file.FileName);

                using (var stream = File.Create(fullPath))
                {
                    stream.Write(file.Data, 0, file.Data.Length);
                }

                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                return new OperationResult(ex.Message);
            }
        }

        public OperationResult SaveFiles(string path, IEnumerable<FileModel> files)
        {
            try
            {
                CheckDirectory(path, FileMode.ReplaceEqual);

                Parallel.ForEach(files, file =>
                {
                    var fullPath = Path.Combine(path, file.FileName);

                    using (var stream = File.Create(fullPath))
                    {
                        stream.Write(file.Data, 0, file.Data.Length);
                    }
                });

                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                return new OperationResult(ex.Message);
            }
        }

        public async Task<OperationResult> SaveFileAsync(string path, FileModel file)
        {
            return await SaveFileAsync(path, file, FileMode.ReplaceEqual);
        }

        public async Task<OperationResult> SaveFileAsync(string path, FileModel file, FileMode mode)
        {
            try
            {
                CheckDirectory(path, mode);

                var fullPath = Path.Combine(path, file.FileName);

                using (var stream = File.Create(fullPath))
                {
                    await stream.WriteAsync(file.Data, 0, file.Data.Length);
                }

                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                return new OperationResult(ex.Message);
            }
        }

        public async Task<OperationResult> SaveFilesAsync(string path, IEnumerable<FileModel> files)
        {
            try
            {
                CheckDirectory(path, FileMode.ReplaceEqual);

                var tasks = files.Select(file => SaveFileTask(path, file));
                await Task.WhenAll(tasks);

                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                return new OperationResult(ex.Message);
            }
        }

        public IEnumerable<FileModel> GetFiles(string path)
        {
            if (!Directory.Exists(path)) return Enumerable.Empty<FileModel>();

            var files = Directory.GetFiles(path);
            var model = new ConcurrentBag<FileModel>();

            Parallel.ForEach(files, t =>
            {
                model.Add(new FileModel
                {
                    FileName = Path.GetFileName(t),
                    Data = File.ReadAllBytes(t)
                });
            });

            return model;
        }

        public IEnumerable<ServerFileInfo> GetFileInfo(string path)
        {
            if (!Directory.Exists(path))
                return Enumerable.Empty<ServerFileInfo>();

            var files = Directory.GetFiles(path);
            var result = files.Select(t => new FileInfo(t)).Select(file => new ServerFileInfo
            {
                Name = file.Name,
                PhysicalPath = file.FullName,
                Size = file.Length
            });

            return result;
        }


        private async Task SaveFileTask(string folderPath, FileModel file)
        {
            var fullPath = Path.Combine(folderPath, file.FileName);

            using (var stream = File.Create(fullPath))
            {
                await stream.WriteAsync(file.Data, 0, file.Data.Length);
            }
        }

        private static void CheckDirectory(string path, FileMode mode)
        {
            if (Directory.Exists(path))
            {
                if (mode == FileMode.Replace)
                {
                    Directory.Delete(path, true);
                }
                else
                {
                    return;
                }
            }

            Directory.CreateDirectory(path);
        }
    }
}
