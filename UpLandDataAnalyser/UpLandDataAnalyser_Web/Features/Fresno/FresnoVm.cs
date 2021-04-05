using System;
using System.Collections.Generic;

namespace UpLandDataAnalyser_Web.Features.Fresno
{
    public class Fresno
    {
        public IEnumerable<WoodwardVm> Woodward { get; set; }

        public class WoodwardVm
        {
           //public string demo { get; set; }
           //public int price { get; set; }
            public decimal SoldPrice { get; set; }
            public DateTime DateSold { get; set; }
        }

    }
}
