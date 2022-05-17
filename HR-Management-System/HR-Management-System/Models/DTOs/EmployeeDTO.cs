namespace HR_Management_System.Models.DTOs
{
    public class EmployeeDTO
    {
        public int ID { get; set; }
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Level { get; set; }
        public int LeaveCredit { get; set; }
        public int VacationCredit { get; set; }
        public string DepartmentName { get; set; }
    }
}