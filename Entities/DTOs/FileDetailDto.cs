using Core.Entites.Abstract;

namespace Entities.DTOs
{
    public class FileDetailDto : IDto
    {
        public string FilePath { get; set; }
        public string RootPath { get; set; }
    }
}
