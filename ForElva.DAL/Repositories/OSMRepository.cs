using ForElva.DAL.Interfaces;
using System.IO;
using System.Net;
using System.Text;

namespace ForElva.DAL.Repositories
{
    public class OSMRepository : IRepository
    {
        private const string URL_FOR_DATA = "https://nominatim.openstreetmap.org/search?q=";
        private const string SAVE_FORMAT = "&format=json&polygon_geojson=1";

        public string Url { get; private set; } = "https://nominatim.openstreetmap.org/ui/search.html?q=";
        public byte[] GetData(string search, string count)
        {
            int.TryParse(count, out int countSave); 
            var result = new StringBuilder();

            if (search != null)
                search = search.Replace(" ", "%20");

            var res = string.Format("{0}{1}{2}", URL_FOR_DATA , search, SAVE_FORMAT);

            var request = (HttpWebRequest)WebRequest.Create(res);
            request.UserAgent = "anyone";
            var response = (HttpWebResponse)request.GetResponse();

            using (Stream responseStream = response.GetResponseStream())
            using (StreamReader htmlStream = new StreamReader(responseStream, Encoding.UTF8)) 
            {
                var isStart = false;
                var isEnd = false;
                var isWrite = true;
                var coordinates = string.Empty;
                var countCoordinates = 0;
                var previousSymbol = ' ';
                var currentSymbol = ' ';
                while (!htmlStream.EndOfStream)
                {
                    previousSymbol = currentSymbol;
                    currentSymbol = (char)htmlStream.Read();

                    if ((previousSymbol == '[') && (currentSymbol == '[') ||
                        (previousSymbol == ':') && (currentSymbol == '['))
                    {
                        countCoordinates = 0;
                    }

                    if ((previousSymbol == '[') && char.IsDigit(currentSymbol))
                    {
                        var resultLength = result.Length;

                        isStart = true;
                        isWrite = false;
                        coordinates += previousSymbol;

                        // remove excess ',' or ',['
                        if (result[resultLength - 2] == ',')
                        {
                            result.Remove(resultLength - 2, 2);
                        }
                        else
                        {
                            result.Remove(resultLength - 1, 1);
                        }
                    }
                    if (isStart)
                    {
                        coordinates += currentSymbol;
                    }
                    if (char.IsDigit(previousSymbol) && (currentSymbol == ']') && isStart)
                    {
                        isEnd = true;
                        countCoordinates++;
                    }

                    if (isWrite)
                        result.Append(currentSymbol);

                    if (isStart && isEnd)
                    {
                        if (countCoordinates == 1)
                        {
                            result.Append(coordinates);
                        }
                        else if(countCoordinates % countSave == 1)
                        {
                            result.Append(',');
                            result.Append(coordinates);
                        }
                        isEnd = false;
                        isStart = false;
                        isWrite = true;
                        coordinates = string.Empty;
                    }

                }
                return Encoding.UTF8.GetBytes(result.ToString()); ;
            }
        }        
    }
}