namespace ForElva.DAL.Interfaces
{
    public interface IRepository
    {
        void Save(string search, string fileName, string count);
        string Url{ get; }
    }
}