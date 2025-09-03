using Domain.Entities;

namespace Application.Response
{
    public class DishResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public CategoryResponse Category { get; set; }
        public string? ImageUrl { get; set; }
        public bool Available { get; set; }
     
        
    }
}
