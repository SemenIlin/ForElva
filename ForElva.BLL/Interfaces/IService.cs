namespace ForElva.BLL.Interfaces
{
    public interface IService
    {
        void Save(string search, string fileName, string count);
        string Url { get; }
    }
}
