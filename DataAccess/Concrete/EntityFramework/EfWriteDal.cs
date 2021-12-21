using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfWriteDal : EfEntityRepositoryBase<Write, FesaContext>, IWriteDal
    {
        public List<WriteDetailDTO> GetWriteDetails()
        {
            using (var context = new FesaContext())
            {
                var result = from w in context.Writes
                             join u in context.Users
                             on w.UserId equals u.Id
                             select new WriteDetailDTO()
                             {
                                 UserFirstName = u.FirstName,
                                 UserLastName = u.LastName,
                                 UserEmail = u.Email,
                                 ProfilePhotoFilePath = u.ProfilePhotoFilePath,
                                 ProfilePhotoRootPath = u.ProfilePhotoRootPath,
                                 Title = w.Title,
                                 Content = w.Content,
                                 ImageFilePath = w.ImageFilePath,
                                 ImageRootPath = w.ImageRootPath,
                                 CreationDate = w.CreationDate
                             };

                return result.ToList();

            }
        }
    }
}