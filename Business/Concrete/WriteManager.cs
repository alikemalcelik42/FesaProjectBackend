using Business.Abstarct;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Exception;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class WriteManager : IWriteService
    {
        IWriteDal _writeDal;
        IFileService _fileService;
        public readonly string currentFolder = "WriteImages";

        public WriteManager(IWriteDal writeDal, IFileService fileService)
        {
            _writeDal = writeDal;
            _fileService = fileService;
        }

        [SecuredOperation("admin,editor,write.add")]
        [CacheRemoveAspect("IWriteService.Get")]
        [ValidationAspect(typeof(WriteValidator))]
        [TransactionScopeAspect]
        [LogAspect(typeof(FileLogger))]
        [ExceptionLogAspect(typeof(FileLogger))]
        public IResult Add(Write write, IFormFile imageFile)
        {
            var result = _fileService.Add(imageFile, currentFolder);
            write.ImageFilePath = result.Data.FilePath;
            write.ImageRootPath = result.Data.RootPath;

            _writeDal.Add(write);
            return new SuccessResult(Messages.WriteAdded);
        }

        [SecuredOperation("admin,editor,write.delete")]
        [CacheRemoveAspect("IWriteService.Get")]
        [TransactionScopeAspect]
        [LogAspect(typeof(FileLogger))]
        [ExceptionLogAspect(typeof(FileLogger))]
        public IResult Delete(Write write)
        {
            _fileService.Delete(write.ImageFilePath);
            _writeDal.Delete(write);
            return new SuccessResult(Messages.WriteDeleted);
        }

        [CacheAspect]
        public IDataResult<List<Write>> GetAll()
        {
            return new SuccessDataResult<List<Write>>(_writeDal.GetAll(), Messages.WritesListed);
        }

        public IDataResult<List<Write>> GetAllByUserId(int userId)
        {
            return new SuccessDataResult<List<Write>>(_writeDal.GetAll(w => w.UserId == userId), Messages.WritesListed);
        }

        public IDataResult<List<Write>> GetAllOrderByCreationDate()
        {
            return new SuccessDataResult<List<Write>>(_writeDal.GetAll().OrderByDescending(w => w.CreationDate).ToList(),
                Messages.WritesListed);
        }

        public IDataResult<Write> GetById(int id)
        {
            var result = _writeDal.Get(w => w.Id == id);
            return new SuccessDataResult<Write>(result, Messages.WritesListed);
        }

        [PerformanceAspect(3)]
        [CacheAspect]
        public IDataResult<List<WriteDetailDTO>> GetWriteDetails()
        {
            var result = _writeDal.GetWriteDetails();
            return new SuccessDataResult<List<WriteDetailDTO>>(result, Messages.WritesListed);
        }

        [SecuredOperation("admin,editor,write.update")]
        [CacheRemoveAspect("IWriteService.Get")]
        [ValidationAspect(typeof(WriteValidator))]
        [LogAspect(typeof(FileLogger))]
        [ExceptionLogAspect(typeof(FileLogger))]
        public IResult Update(Write write)
        {
            _writeDal.Update(write);
            return new SuccessResult(Messages.WriteUpdated);
        }

        [SecuredOperation("admin,editor,write.update")]
        [CacheRemoveAspect("IWriteService.Get")]
        [ValidationAspect(typeof(WriteValidator))]
        [TransactionScopeAspect]
        [LogAspect(typeof(FileLogger))]
        [ExceptionLogAspect(typeof(FileLogger))]
        public IResult UpdateWithFile(Write write, IFormFile imageFile)
        {
            var result = _fileService.Update(GetById(write.Id).Data.ImageFilePath, imageFile, currentFolder);
            write.ImageFilePath = result.Data.FilePath;
            write.ImageRootPath = result.Data.RootPath;

            _writeDal.Update(write);
            return new SuccessResult(Messages.WriteUpdated);
        }
    }
}
