using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Core.Helpers
{
    public static class ApiHelper<T>
    {
        public static async Task<T> GetData(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                var model = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(model);
            }
        }

        public static async Task<T> GetDataByFilter(string url, IDictionary<string,string> encoded)
        {
            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(encoded), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                var model = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(model);
            }
        }
    }
}
