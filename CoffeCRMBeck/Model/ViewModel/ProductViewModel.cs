using CoffeCRMBeck.Model.Enums;

namespace CoffeCRMBeck.Model.ViewModel
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public long Amount { get; set; }
        public DateTime Date { get; set; }
        public double TotalPrice { get; set; }
        public ProductType Type { get; set; }
    }
}
