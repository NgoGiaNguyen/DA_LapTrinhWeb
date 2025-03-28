namespace DA.Models
{
    public class ThongKe
    {
        public List<Order> Orders { get; set; } = new List<Order>();

        public int TongOrders;
        public float TotalPrice;
    }
}
