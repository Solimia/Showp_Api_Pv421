using System.ComponentModel.DataAnnotations;
using DataAccess.Data.Entities;

namespace BusinessLogic.DTOs
{
    public class CreateProductDto
    {

        //[Required, MinLength(3, ErrorMessage = "Title must has least 3 charactes")]
        //[RegularExpression(@"^[A-Z].*", ErrorMessage = "Title must start with a capital letter")]

        public string Title { get; set; }
        public string? ImageUrl { get; set; }

        public decimal Price { get; set; }

        [Range(0, 100)]

        public int Discount { get; set; }
        public int Quantity { get; set; }

        [MinLength(10), MaxLength(3000)]

        public string? Description { get; set; }
        public int CategoryId { get; set; }

        // ----- navigation properties
    }
}
