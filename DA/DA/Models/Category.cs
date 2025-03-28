using System.ComponentModel.DataAnnotations;

namespace DA.Models
{
    public class Category
    {

        public int id { get; set; }

        [Required, StringLength(50)]
        public string name { get; set; }
        public List<Product>? products { get; set; }
    }
}
