using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using UpLandDataAnalyser_Web.Features.Admin;
using UpLandDataAnalyser_Web.Features.Fresno;


namespace ApiDataTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            var dateTime = DateTime.Now.AddDays(-7);

            // Searches for all houses sold in woodward
            var frensoWoodwardSoldHouses = "https://api.upx.world/bigdata/history?neighborhood=293&city=0&skip=0&sort=timestamp&ascOrDesc=-1&priceMin=0&priceMax=100000000&upx_fiat=UPX&actionType=buy";
            // Searches Current Listing in woodward
            var frenso_woodward_listing = "https://api.upx.world/bigdata/query?city=0&status=ForSaleUpx&mintMin=0&mintMax=100000000&saleMin=0&saleMax=100000000&skip=0&fsa=All&sort=mint_price&ascOrDesc=1&collections=52";

            var woodwardSoldData = DataApiController.GetFromJson<JToken>(frensoWoodwardSoldHouses);
            var woodwardActiveListings = DataApiController.GetFromJson<JToken>(frenso_woodward_listing);

            // Processing Sold Data
            var SoldLastSevenDaysPrices = woodwardSoldData["data"]
                .Select(t => new Fresno.WoodwardVm()
                {
                    SoldPrice = (decimal)t["price_paid"],
                    DateSold = (DateTime)t["timestamp"]
                }).ToList()
                .Where(w => w.DateSold >= dateTime);

            var soldall = woodwardSoldData["data"]
                .Select(t => new Fresno.WoodwardVm()
                {
                    SoldPrice = (decimal)t["price_paid"],
                    DateSold = (DateTime)t["timestamp"]
                }).ToList();

            Console.WriteLine(JsonConvert.SerializeObject(SoldLastSevenDaysPrices));

            // Avg Price of houses sold over the last 7days / Amount of houses sold (last 7days)
            var woodwardAvgSoldPriceSevenDays = SoldLastSevenDaysPrices.Average(s => s.SoldPrice);
            var woodwardAmountSoldSevenDays = SoldLastSevenDaysPrices.Count();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Woodward - Avg Price 7 days");
            Console.WriteLine(JsonConvert.SerializeObject(woodwardAvgSoldPriceSevenDays));
            Console.WriteLine("Woodward - Number of houses sold in the last 7 days");
            Console.WriteLine(JsonConvert.SerializeObject(woodwardAmountSoldSevenDays));
            // Avg Price of houses sold / Amount of houses sold (last 7days)
            var woodwardAvgSoldPriceAllTime = soldall.Average(s => s.SoldPrice);
            var woodwardAmountSold = soldall.Count();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Avg Price all time");
            Console.WriteLine(JsonConvert.SerializeObject(woodwardAvgSoldPriceAllTime));
            Console.WriteLine("Number of houses sold (total)");
            Console.WriteLine(JsonConvert.SerializeObject(woodwardAmountSold));

            // Processing Active Listings 
            var woodwardActiveListingsPrices = woodwardActiveListings["data"]["properties"].Select(t => (decimal?)t["sale_price_upx"]).ToList();
            var woodwardActiveListingsProcess = woodwardActiveListingsPrices.OrderByDescending(p => p.Value).Where(w => w.Value <= 500000).Take(1000);
            var woodwardAvgActiveListings = woodwardActiveListingsProcess.Average();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(JsonConvert.SerializeObject(woodwardAvgActiveListings));
        }

        static void Ignore()
        {
          //  NOTE THE FOLLOWING URL IS A BIG DATA SEARCH FOR WOODWARD PARK COLLECTION
          // var frenso_woodward_listing = "https://api.upx.world/bigdata/query?city=0&status=ForSaleUpx&mintMin=0&mintMax=100000000&saleMin=0&saleMax=100000000&skip=0&fsa=All&sort=mint_price&ascOrDesc=1&collections=52";

          //  NOTE THE FOLLOWING URL IS A BIG DATA SEARCH FOR SOLD PROPERTIES(UPX) IN THE WOODWARD PARK COLLECTION
          //var frenso_woodward_soldhouses = "https://api.upx.world/bigdata/history?neighborhood=293&city=0&skip=0&sort=timestamp&ascOrDesc=-1&priceMin=0&priceMax=100000000&upx_fiat=UPX&actionType=buy";

          //  var solddata = DataApiController.GetFromJson<JToken>(frenso_woodward_soldhouses);
          //  var prices = solddata["data"].Select(t => (decimal?)t["price_paid"]).ToList();
          //  var solddata_process = prices.OrderByDescending(p => p.Value).Where(w => w.Value >= 5000).Take(1000);
          //  var avgsoldprice = solddata_process.Average();

          //  var activelistings = DataApiController.GetFromJson<JToken>(frenso_woodward_listing);
          //  var activelistingsprices = activelistings["data"]["properties"].Select(t => (decimal?)t["sale_price_upx"]).ToList();
          //  var activelistings_process = activelistingsprices.OrderByDescending(p => p.Value).Where(w => w.Value <= 500000).Take(1000);
          //  var avgactivelistings = activelistings_process.Average();

          //  var prices = dobj["data"]["properties"].Select(t => (decimal?)t["last_paid_price_upx"]).ToList();
          //  var prices = dobj["data"]["properties"].Select(t => (decimal?)t["sale_price_upx"]).ToList();
          //  Console.WriteLine(JsonConvert.SerializeObject(prices.Where(prices != null)));


          //  Notes for later TODO:
          //   No need for to filter for nulls, just make sure your making the correct api calls!
          //   Cyan == Woodward


          //  Console.ForegroundColor = ConsoleColor.Cyan;
          //  Console.WriteLine("Avg Price of houses sold - Woodward Park");
          //  Console.WriteLine(JsonConvert.SerializeObject(avgsoldprice));

          //  Console.ForegroundColor = ConsoleColor.Cyan;
          //  Console.WriteLine("Avg Price of current houses listed - Woodward Park");
          //  Console.WriteLine(JsonConvert.SerializeObject(avgactivelistings));


          //  var model = new FresnoVm();
          //  var dateTime = DateTime.Now.AddDays(-7);

          //  var soldlastsevendaysprices = solddata["data"]
          //      .Select(t => new Fresno.WoodwardVm()
          //      {
          //          SoldPrice = (decimal?)t["price_paid"],
          //          DateSold = (DateTime)t["timestamp"]
          //      }).ToList()
          //      .Where(w => w.DateSold >= dateTime);

          //  Console.ForegroundColor = ConsoleColor.Red;
          //  Console.WriteLine(soldlastsevendaysprices);


        }

    }
}
