namespace HR_Management_System.Models.DTOs
{
    public class AddEmployeeDTO
    {
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public double Salary { get; set; }
        public Level Level { get; set; }
    }
}
