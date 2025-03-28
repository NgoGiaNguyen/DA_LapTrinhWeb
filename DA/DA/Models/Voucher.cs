using System.ComponentModel.DataAnnotations;

namespace DA.Models
{
    public class Voucher
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }

        [Required]
        public string Code {  get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Giá trị phải nằm trong khoảng từ 1 đến 100")]
        public int Value { get; set; }

        public int SoLuong { get; set; }
    }
}
