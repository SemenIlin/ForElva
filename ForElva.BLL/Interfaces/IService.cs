namespace ForElva.BLL.Interfaces
{
    public interface IService
    {
        byte[] GetData(string search, string count);
        string Url { get; }
    }
}
