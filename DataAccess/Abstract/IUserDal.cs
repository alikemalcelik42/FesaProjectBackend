using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<FesaUser>
    {
        List<OperationClaim> GetClaims(FesaUser user);
    }
}
