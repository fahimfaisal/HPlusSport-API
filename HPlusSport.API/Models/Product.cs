using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HPlusSport.API.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Sku { get; set; }

        [Required]                            //A way of model validation.(The annotation means  the name of the product must be given)
        public string Name { get; set; }
        [MaxLength(255)]                       //The description should be within 255 character. If not the api will send HTTP 400 req. (given it is not supressed)
        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }

        public int CategoryId { get; set; }
        [JsonIgnore]
        public virtual Category Category { get; set;  }


    }
}
