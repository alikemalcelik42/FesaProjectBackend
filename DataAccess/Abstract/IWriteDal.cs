using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IWriteDal : IEntityRepository<Write>
    {
        List<WriteDetailDTO> GetWriteDetails();
    }
}
