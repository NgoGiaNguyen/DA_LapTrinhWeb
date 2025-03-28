namespace DA.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        
        public string Image {get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }
}
