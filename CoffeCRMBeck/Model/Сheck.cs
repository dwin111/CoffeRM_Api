using CoffeCRMBeck.Model.Enums;

namespace CoffeCRMBeck.Model
{
    public class Сheck
    {
        public int Id { get; set; }
        public List<Product> Products { get; set; }
        public float Price { get; set; }
        public long TotalProducts { get; set; }
        public PaymentType Payment { get; set; }
        public PaymentExecution PaymentExecut { get; set; }
        public DateTime Date { get; set; }
        public Worker Worker { get; set; }
    }
}
