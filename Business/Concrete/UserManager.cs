using Business.Abstarct;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public List<OperationClaim> GetClaims(FesaUser user)
        {
            return _userDal.GetClaims(user);
        }

        [ValidationAspect(typeof(UserValidator))]
        public void Add(FesaUser user)
        {
            _userDal.Add(user);
        }

        [ValidationAspect(typeof(UserValidator))]
        public void Update(FesaUser user)
        {
            _userDal.Update(user);
        }

        public FesaUser GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }
    }
}
