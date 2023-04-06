using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeCRMBeck.Model
{
    public class ProductCheck
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Product))]
        public long ProductId { get; set; }

        [ForeignKey(nameof(Сheck))]
        public long СheckId { get; set; }
    }
}
