using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;

namespace Business.Abstarct
{
    public interface IWriteService
    {
        IDataResult<List<Write>> GetAll();
        IDataResult<Write> GetById(int id);
        IDataResult<List<Write>> GetAllByUserId(int userId);
        IDataResult<List<Write>> GetAllOrderByCreationDate();
        IDataResult<List<WriteDetailDTO>> GetWriteDetails();
        IResult Add(Write write, IFormFile imageFile);
        IResult Update(Write write);
        IResult UpdateWithFile(Write write, IFormFile imageFile);
        IResult Delete(Write write);
    }
}
