using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MauiAppBlazor.Models.Enums;

namespace MauiAppBlazor.Models
{
	public class ProductCatalog
	{
        public long Id { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public float Price { get; set; }
        public ProductType Type { get; set; }

    }
}

