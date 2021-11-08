using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;

namespace AvaloniaTest
{
    public abstract class GenericJSONProcessor<T>
    {
        // Derived from:
        // https://stackoverflow.com/questions/27108264/how-to-properly-make-a-http-web-get-request
        protected static async Task<string> GetAsync(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }

        protected static async Task<T> InitAndParse(string url)
        {
            var httpResponse = GetAsync(url);
            String contents = await httpResponse;
            T response = JsonSerializer.Deserialize<T>(contents);
            return response;
        }
    }
}
