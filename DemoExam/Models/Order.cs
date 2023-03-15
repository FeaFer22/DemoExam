namespace OTKInformationSystem.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string? Date { get; set; }
        public int Discount { get; set; }
        public string? Status { get; set; }
        public string? ClientName { get; set; }
        public string? EmployeeName { get; set; }
        public string? ServiceName { get; set; }
    }
}
