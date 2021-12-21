using Core.Utilities.Results.Abstract;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;

namespace Business.Abstarct
{
    public interface IAuthService
    {
        IDataResult<FesaUser> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<FesaUser> Login(UserForLoginDto userForLoginDto);
        IResult UpdateProfilePhoto(string userMail, IFormFile imageFile);
        IResult UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(FesaUser user);
    }
}
