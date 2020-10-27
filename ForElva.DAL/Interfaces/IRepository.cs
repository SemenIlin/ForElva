namespace ForElva.DAL.Interfaces
{
    public interface IRepository
    {
        byte[] GetData(string search, string count);
        string Url{ get; }      
    }
}