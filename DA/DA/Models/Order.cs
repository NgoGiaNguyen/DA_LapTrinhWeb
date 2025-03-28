using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DA.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public float TotalPrice { get; set; }

        public bool Status { get; set; }

        [Required]
        public string userName { get; set; }

        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string SDT { get; set; }

        [Required]
        public string ShippingAddress { get; set; }
        public string? Notes { get; set; }

        public IdentityUser User { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

        public string? Code { get; set; }

        public Voucher? voucher { get; set; }
    }

}
