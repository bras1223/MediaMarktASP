using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace MediaMarkt.Models.Services
{
    public class ApiService
    {
        readonly string baseUri = "http://localhost/4launch/api/productsapi";

        public List<ApiProductDescription> GetProducten()
        {
            string uri = baseUri;

            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync(uri);
                
                return JsonConvert.DeserializeObjectAsync<List<ApiProductDescription>>(response.Result).Result;
            }
        }
    }
}