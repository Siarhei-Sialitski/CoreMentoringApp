using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CoreMentoringApp.Core.Models;
using CoreMentoringApp.WebSite.ViewModels.Validators;

namespace CoreMentoringApp.WebSite.ViewModels
{
    public class ProductViewModel
    {
        [DisplayName("Id")]
        public int ProductId { get; set; }

        [DisplayName("Name")]
        [StringLength(40, MinimumLength = 4)]
        [Required]
        public string ProductName { get; set; }

        public int? SupplierId { get; set; }

        public int? CategoryId { get; set; }

        [DisplayName("Quantity Per Unit")]
        [MaxLength(20)]
        public string QuantityPerUnit { get; set; }

        [DisplayName("Unit Price")]
        [DataType(DataType.Currency)]
        public decimal? UnitPrice { get; set; }

        [DisplayName("Units In Stock")]
        [MinimumValue(0)]
        public short? UnitsInStock { get; set; }

        [DisplayName("Units On Order")]
        public short? UnitsOnOrder { get; set; }

        [DisplayName("Reorder Level")]
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        public virtual Category Category { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
