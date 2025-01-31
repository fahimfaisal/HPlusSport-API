﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPlusSport.API.Classes
{
    public class ProductQueryParameter : QueryParameters
    {
        public string Sku { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public string Name { get; set; }

        public string SearchTerm { get; set; }
    }
}
