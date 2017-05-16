using System.Collections.Generic;
using System.Threading.Tasks;
using BL.DTO;
using BL.Enviroment;

namespace BL.Services.Interfaces
{
    public interface IFileService
    {
        /// <summary>
        /// Сохраняет файл из данных переданной модели на сервере
        /// </summary>
        /// <param name="path">Часть пути, являющая собой каталог(и) нижнего уровня</param>
        /// <param name="file">Модель данных файла</param>
        /// <returns></returns>
        OperationResult SaveFile(string path, FileModel file);

        OperationResult SaveFile(string path, FileModel file, FileMode mode);

        OperationResult SaveFiles(string path, IEnumerable<FileModel> files);

        Task<OperationResult> SaveFileAsync(string path, FileModel file);

        Task<OperationResult> SaveFileAsync(string path, FileModel file, FileMode mode);

        Task<OperationResult> SaveFilesAsync(string path, IEnumerable<FileModel> files);

        IEnumerable<FileModel> GetFiles(string path);

        IEnumerable<ServerFileInfo> GetFileInfo(string path);
    }
}
