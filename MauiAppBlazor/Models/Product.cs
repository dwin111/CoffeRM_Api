using MauiAppBlazor.Models.Enums;

namespace MauiAppBlazor.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public long Amount { get; set; }
        public DateTime Date { get; set; }
        public double TotalPrice { get; set; }
        public ProductType Type { get; set; }
    }
}
