using Contracts.Domains;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.API.Entities
{
    public class CardProduct : EntityAuditBase<long>
    {
        //public long Id { get; set; }
        [Required]
        public string No { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Summary { get; set;  }

        //public int StockQuantity { get; set; }


    }
}
