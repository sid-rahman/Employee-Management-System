namespace PracticalMVC.Models
{
    public class Employee
    {
        public int UniqueId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime JoinDate { get; set; }
        public string? Designation {  get; set; }
    }
}
