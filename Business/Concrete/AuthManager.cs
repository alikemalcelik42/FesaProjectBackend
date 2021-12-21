using Business.Abstarct;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Aspects.Autofac.Transaction;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        private IFileService _fileService;
        public readonly string currentFolder = "UserProfilePhotos";

        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IFileService fileService)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _fileService = fileService;
        }

        public IDataResult<FesaUser> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new FesaUser
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                ProfilePhotoFilePath = "/",
                ProfilePhotoRootPath = "/",
                Status = true
            };
            _userService.Add(user);
            return new SuccessDataResult<FesaUser>(user, Messages.UserRegistered);
        }

        public IDataResult<FesaUser> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<FesaUser>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<FesaUser>(Messages.PasswordError);
            }

            return new SuccessDataResult<FesaUser>(userToCheck, Messages.SuccessfulLogin);
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(FesaUser user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        [SecuredOperation("admin,editor,user,user.update")]
        [TransactionScopeAspect]
        public IResult UpdateProfilePhoto(string userEmail, IFormFile imageFile)
        {
            var oldUser = _userService.GetByMail(userEmail);
            IDataResult<FileDetailDto> result;
            
            if (oldUser.ProfilePhotoFilePath != "/")
                result = _fileService.Update(oldUser.ProfilePhotoFilePath, imageFile, currentFolder);
            else
                result = _fileService.Add(imageFile, currentFolder);

            oldUser.ProfilePhotoFilePath = result.Data.FilePath;
            oldUser.ProfilePhotoRootPath = result.Data.RootPath;

            _userService.Update(oldUser);

            return new SuccessResult(Messages.UserUpdated);
        }
    }
}
