﻿namespace Domain.Entities
{    
    public class DeliveryType
    {
        public int Id { get; set; } //PK
        public string Name { get; set; }

        // Relacion 1:N con Order - un tipo de entrega se aplica a muchas órdenes
        public ICollection<Order> Orders { get; set; }
    }
}
