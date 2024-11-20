using CoffeCRMBeck.Model.Enums;

namespace CoffeCRMBeck.Model
{
    public class Menu
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public float Price { get; set; }
        public ProductType Type { get; set; }

    }
}

