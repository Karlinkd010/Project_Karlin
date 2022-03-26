namespace BackendWebApi.Models
{
    public class Product
    {
        public int  Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public DateTime? Creation { get; set; }
        public DateTime? Modification { get; set; }
    }
}
