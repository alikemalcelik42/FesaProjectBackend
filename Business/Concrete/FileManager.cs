using Business.Abstarct;
using Business.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class FileManager : IFileService
    {
        public readonly string root = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        public readonly string url = "https://localhost:7180";

        public IDataResult<FileDetailDto> Add(IFormFile file, string currentFolder)
        {
            string fileName = Path.GetFileName(file.FileName);
            string guid = Guid.NewGuid().ToString();
            string fileExtension = Path.GetExtension(fileName);
            string newFileName = guid + fileExtension;

            string filePath = Path.Combine(root, currentFolder, newFileName);
            string rootPath = $@"{url}/{currentFolder}/{newFileName}";

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
                return new SuccessDataResult<FileDetailDto>(new FileDetailDto()
                {
                    FilePath = filePath,
                    RootPath = rootPath
                }, Messages.FileCreated);
            }
        }

        public IResult Delete(string path)
        {
            File.Delete(path);
            return new SuccessResult(Messages.FileDeleted);
        }

        public IDataResult<FileDetailDto> Update(string oldFilePath, IFormFile file, string currentFolder)
        {
            Delete(oldFilePath);
            return Add(file, currentFolder);
        }
    }
}
