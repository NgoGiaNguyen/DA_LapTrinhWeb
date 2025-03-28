using System.ComponentModel.DataAnnotations;

namespace DA.Models
{
    public class Hotline
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string SDT { get; set; }

    }
}
