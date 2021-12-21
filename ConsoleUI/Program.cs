using Business.Concrete;
using DataAccess.Concrete.EntityFramework;

WriteManager writeManager = new WriteManager(new EfWriteDal(), new FileManager());

var data = writeManager.GetAll();

if(data.Success)
{
    foreach(var write in data.Data)
    {
        Console.WriteLine(write.Title);
    }
}