using System.ComponentModel.DataAnnotations;

namespace DA.Models
{
    public class Product
    {

        public int id { get; set; }
        [Required, StringLength(100)]
        public string name { get; set; }
        public string? description {  get; set; }

        public float price {  get; set; }
        public string? imageULR {  get; set; }

        public int categoryId { get; set; }
        public Category? category { get; set; }


    }
}
