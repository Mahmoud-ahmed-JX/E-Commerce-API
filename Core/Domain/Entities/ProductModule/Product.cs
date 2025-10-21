using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ProductModule
{
    public class Product:BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string PictureUrl { get; set; }
        public decimal Price { get; set; }


        //1-M ProductType-Product
        public int TypeId { get; set; }
        public ProductType ProductType { get; set; }
        //1-M ProductBrand-Product
        public int BrandId { get; set; }
        public ProductBrand ProductBrand { get; set; }
    }
}
