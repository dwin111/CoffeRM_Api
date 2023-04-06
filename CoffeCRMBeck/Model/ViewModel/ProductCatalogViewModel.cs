using CoffeCRMBeck.Model.Enums;

namespace CoffeCRMBeck.Model.ViewModel
{
    public class ProductCatalogViewModel
    {
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public float Price { get; set; }
        public ProductType Type { get; set; }

    }
}
