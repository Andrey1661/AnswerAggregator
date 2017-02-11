using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BL.DTO;
using BL.Enviroment;

namespace BL.Services.Interfaces
{
    public interface IFileManager
    {
        IFileService FileService { get; }

        OperationResult SetUserAvatar(string userName, FileModel file);

        Task<OperationResult> SetUserAvatarAsync(string userName, FileModel file);
            
        OperationResult AddPostFiles(Guid postId, IEnumerable<FileModel> files);

        Task<OperationResult> AddPostFilesAsync(Guid postId, IEnumerable<FileModel> files);

        string GetAvatar(string userName, bool cutRootPath = true);

        IEnumerable<ServerFileInfo> GetPostFiles(Guid postId);
    }
}
