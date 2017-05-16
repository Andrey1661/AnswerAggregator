using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BL.DTO;
using BL.Enviroment;
using BL.Services.Interfaces;

namespace BL.Services
{
    internal class ServerFileManager : IFileManager
    {
        // ReSharper disable once InconsistentNaming
        private const string _userAvatarFolder = @"Users\";
        // ReSharper disable once InconsistentNaming
        private const string _postFilesFolder = @"Posts\";

        protected readonly string InitialPath;

        protected virtual string UserAvatarFolder
        {
            get { return Path.Combine(InitialPath, _userAvatarFolder); }
        }
        protected virtual string PostFilesFolder
        {
            get { return Path.Combine(InitialPath, _postFilesFolder); }
        }

        public IFileService FileService { get; protected set; }

        /// <summary>
        /// Создает новый экземпляр класса
        /// </summary>
        /// <param name="initialPath">Корневой каталог, в котором будут проводиться все операции с файлами</param>
        /// <param name="service">Интерфейс файлового сервиса</param>
        public ServerFileManager(string initialPath, IFileService service)
        {
            InitialPath = initialPath;
            FileService = service;
        }

        public OperationResult SetUserAvatar(string userName, FileModel file)
        {
            var folder = Path.Combine(UserAvatarFolder, userName);
            var result = FileService.SaveFile(folder, file, FileMode.Replace);

            return result;
        }

        public async Task<OperationResult> SetUserAvatarAsync(string userName, FileModel file)
        {
            var folder = Path.Combine(UserAvatarFolder, userName);
            var result = await FileService.SaveFileAsync(folder, file, FileMode.Replace);

            return result;
        }

        public OperationResult AddPostFiles(Guid postId, IEnumerable<FileModel> files)
        {
            var folder = Path.Combine(PostFilesFolder, postId.ToString());
            var result = FileService.SaveFiles(folder, files);

            return result;
        }

        public async Task<OperationResult> AddPostFilesAsync(Guid postId, IEnumerable<FileModel> files)
        {
            var folder = Path.Combine(PostFilesFolder, postId.ToString());
            var result = await FileService.SaveFilesAsync(folder, files);

            return result;
        }

        public string GetAvatar(string userName, bool cutRootPath = false)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return string.Empty;

            var folder = Path.Combine(UserAvatarFolder, userName);
            var result = FileService.GetFileInfo(folder).FirstOrDefault();

            if (result != null && cutRootPath)
            {
                result.VirtualPath = result.PhysicalPath.Replace(InitialPath, "").Replace(@"\", "/");
            }

            return result != null ? result.VirtualPath : string.Empty;
        }

        public IEnumerable<ServerFileInfo> GetPostFiles(Guid postId)
        {
            var folder = Path.Combine(PostFilesFolder, postId.ToString());
            var result = FileService.GetFileInfo(folder).ToArray();

            foreach (var fileInfo in result)
            {
                fileInfo.VirtualPath = fileInfo.PhysicalPath.Replace(InitialPath, "").Replace(@"\", "/");
            }

            return result;
        }
    }
}
