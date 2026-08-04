using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.DTOs.Baskets
{
    public class BasketItemDto
    {

        [Required(ErrorMessage ="Product Id Is Required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Name Is Required.")]
        public string ProductName { get; set; } = default!;
        public string PicturUrl { get; set; } = default!;

        [Range(1 ,double.MaxValue)]
        public decimal Price { get; set; }

        [Range(1, 50)]
        [Required(ErrorMessage = "Product Quantity Is Between 1 and 50.")]
        public int Quantity { get; set; }

    }
}
