namespace Domain.Entities
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Order> Orders { get; set; } // Relacion 1:N con Order - un estado puede aplicarse a muchas ordene
        public ICollection<OrderItem> OrderItems { get; set; } // Relacion 1:N con OrderItem - un estado puede aplicarse a muchos ítems
    }
}
