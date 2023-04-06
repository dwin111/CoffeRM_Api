using MauiAppBlazor.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppBlazor.Models
{
    public class ProductCatalogModelView
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public float Price { get; set; }
        public float FullPrice { get; set; }
        public int Number { get; set; }
        public ProductType Type { get; set; }
    }
}
