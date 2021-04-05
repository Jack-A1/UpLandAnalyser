using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UpLandDataAnalyser_Web.Features.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataApiController : ControllerBase
    {
        public static async Task<JArray> GetData1()
        {
            var httpClientRequest = new HttpClient();
            try
            {
                var result = await httpClientRequest.GetAsync("https://api.upx.world/bigdata/query?neighborhood=210&neighborhood=359&neighborhood=367&neighborhood=366&neighborhood=356&neighborhood=364&city=0&status=All&mintMin=0&mintMax=100000000&saleMin=0&saleMax=100000000&skip=0&fsa=All&sort=mint_price&ascOrDesc=1");
                var resultString = await result.Content.ReadAsStringAsync();
                var jsonResult = JArray.Parse(resultString);
                return jsonResult;
            }
            catch
            {
                return null;
            }
        }

        public static IEnumerable<dynamic> GetData2()
        {
            string url = "https://api.upx.world/bigdata/query?neighborhood=210&neighborhood=359&neighborhood=367&neighborhood=366&neighborhood=356&neighborhood=364&city=0&status=All&mintMin=0&mintMax=100000000&saleMin=0&saleMax=100000000&skip=0&fsa=All&sort=mint_price&ascOrDesc=1";
            WebClient client = new WebClient();
            client.Headers.Add("User-Agent: Other");
            string strPageCode = client.DownloadString(url);
            dynamic dobj = JsonConvert.DeserializeObject<dynamic>(strPageCode);

            //string price = dobj["sale_price_upx"].ToString();
            //int price = dobj["data"]["properties"]["sale_price_upx"];

            return dobj;
        }

        public static T GetFromJson<T>(string url, JsonSerializerSettings settings = default)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = "Other";

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (var stream = response.GetResponseStream())
                    using (var textReader = new StreamReader(stream))
                    {
                        settings = settings ?? new JsonSerializerSettings { CheckAdditionalContent = true };
                        return (T)JsonSerializer.CreateDefault(settings).Deserialize(textReader, typeof(T));
                    }
                }
                else
                {
                    throw new WebException(); // Throw some exception with a message of your choice
                }
            }
        }
    }
}
