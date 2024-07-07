namespace RESTfulAPIProject.Dtos
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Title { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
