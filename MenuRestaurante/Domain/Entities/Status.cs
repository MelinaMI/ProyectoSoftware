using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Relacion 1:N con OrderItem - un estado puede aplicarse a muchos ítems
        public ICollection<OrderItem> OrderItems { get; set; }

        // Relacion 1:N con Order - un estado puede aplicarse a muchas ordenes
        public ICollection<Order> Orders { get; set; }
    }
}
