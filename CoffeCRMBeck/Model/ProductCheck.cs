using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeCRMBeck.Model
{
    public class ProductCheck
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Order))]
        public long OrderId { get; set; }

        [ForeignKey(nameof(Bill))]
        public long BillId { get; set; }
    }
}
