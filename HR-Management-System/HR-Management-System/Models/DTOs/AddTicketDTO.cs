namespace HR_Management_System.Models.DTOs
{
    public class AddTicketDTO
    {
        public int Emp_id { get; set; }
        public Type Type { get; set; }
        public int Amount { get; set; }
        public string Comment { get; set; }
    }
}
