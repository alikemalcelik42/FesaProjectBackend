using Core.Utilities.Results.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;

namespace Business.Abstarct
{
    public interface IFileService
    {
        IDataResult<FileDetailDto> Add(IFormFile file, string currentFolder);
        IDataResult<FileDetailDto> Update(string oldFilePath, IFormFile file, string currentFolder);
        IResult Delete(string filePath);
    }
}
