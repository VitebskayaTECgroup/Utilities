using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherParser
{
    public class Loader
    {
        public static async Task<string> LoadAsync(string url)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string source = null;

                if (response != null && response.IsSuccessStatusCode)
                {
                    source = await response.Content.ReadAsStringAsync();
                }

                return source;
            }
        }
    }
}