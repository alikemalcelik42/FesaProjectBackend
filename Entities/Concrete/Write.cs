using Core.Entites.Abstract;

namespace Entities.Concrete
{
    public class Write : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageFilePath { get; set; }
        public string ImageRootPath { get; set; }
        public DateTime CreationDate { get; set; } 
    }
}
