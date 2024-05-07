namespace OrderApi.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime Orderdate { get; set; }
        public string ClientId { get; set; }
    }
}
