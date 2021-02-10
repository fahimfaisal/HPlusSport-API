using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPlusSport.API.Models
{
    public class Category
    {
        public int id { get; set; }

        public string MyProperty { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}
