namespace DA.Models
{
    public class TrangChu
    {
        public List<Product> products { get; set; } = new List<Product>();
        public List<Banner> Banners { get; set; } = new List<Banner>();
        public List<Product> Chuot { get; set; } = new List<Product>();
        public List<Product> BanPhim { get; set; } = new List<Product>();
        public List<Product> ManHinh{ get; set; } = new List<Product>();
        public List<Product> LapTop { get; set; } = new List<Product>();
       
    }
}
