using CoffeCRMBeck.Model.Enums;

namespace CoffeCRMBeck.Model
{
    public class Bill
    {
        public int Id { get; set; }
        public List<Order> Orders { get; set; }
        public float Price { get; set; }
        public long TotalProducts { get; set; }
        public PaymentType Payment { get; set; }
        public PaymentExecution PaymentExecut { get; set; }
        public DateTime Date { get; set; }
        public Staff Staff { get; set; }
    }
}
