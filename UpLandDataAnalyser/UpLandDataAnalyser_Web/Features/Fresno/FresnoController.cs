using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UpLandDataAnalyser_Web.Features.Fresno
{
    public class FresnoController : Controller
    {

        //public IActionResult Index()
        //{
        //    //var url = "https://api.upx.world/bigdata/query?city=0&status=All&mintMin=0&mintMax=100000000&saleMin=0&saleMax=100000000&skip=0&fsa=All&sort=mint_price&ascOrDesc=1";
        //    //var demo = _download_serialized_json_data<FresnoVm>(url);
        //    var model = new FresnoVm();
        //    return View("~/Features/Fresno/Index.cshtml", model);
        //}

        public IActionResult WoodWard()
        {
            var model = new Fresno.WoodwardVm();
            return View("~/Features/Fresno/Woodward.cshtml", model);
        }

   

        public async Task<IActionResult> IndexAsync(object sender, EventArgs e)
        {
            var model = new Fresno.WoodwardVm();
            // string url = "https://api.upx.world/bigdata/query?neighborhood=210&neighborhood=359&neighborhood=367&neighborhood=366&neighborhood=356&neighborhood=364&city=0&status=All&mintMin=0&mintMax=100000000&saleMin=0&saleMax=100000000&skip=0&fsa=All&sort=mint_price&ascOrDesc=1";
            // WebClient client = new WebClient();
            // client.Headers.Add("User-Agent: Other");
            //
            //
            // string strPageCode = client.DownloadString(url);
            // dynamic dobj = JsonConvert.DeserializeObject<dynamic>(strPageCode);
            //
            // //string price = dobj["sale_price_upx"].ToString();
            // int price = dobj["data"]["properties"]["sale_price_upx"];
            //
            // model.Price = price;

            //var getData = await DataApiController.GetData();

            // foreach (var jobs in getData)
            // {
            //System.Diagnostics.Debug.WriteLine(jobs["id"].ToString());
            //}

            return View("~/Features/Fresno/Index.cshtml", model);
        }
    }
}





//private static T _download_serialized_json_data<T>(string url) where T : new()
//{
//    using (var w = new WebClient())
//    {
//        var json_data = string.Empty;
//        // attempt to download JSON data as a string
//        try
//        {
//            json_data = w.DownloadString(url);
//        }
//        catch (Exception) { }
//        // if string with JSON data is not empty, deserialize it to class and return its instance 
//        return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<T>(json_data) : new T();
//    }
//}