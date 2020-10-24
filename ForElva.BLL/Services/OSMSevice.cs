using ForElva.BLL.Interfaces;
using ForElva.DAL.Interfaces;

namespace ForElva.BLL.Services
{
    public class OSMSevice : IService
    {
        private readonly IRepository _repository;
        public OSMSevice(IRepository repository)
        {
            _repository = repository;
        }

        public string Url{ get { return _repository.Url; } }

        public void Save(string search, string fileName, string count)
        {
            _repository.Save(search, fileName, count);
        }
    }
}