using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class FesaUser : User
    {
        public string ProfilePhotoFilePath { get; set; }
        public string ProfilePhotoRootPath { get; set;}
    }
}
