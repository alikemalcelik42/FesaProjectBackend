using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Abstarct
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(FesaUser user);
        void Add(FesaUser user);
        void Update(FesaUser user);
        FesaUser GetByMail(string email);
    }
}
