namespace LazyGuy.ClientDemo.DataAccess.Northwind.Entities
{
    public partial class OrderDetail
    {
        public string Id { get; set; }
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public long Quantity { get; set; }
        public double Discount { get; set; }
    }
}
