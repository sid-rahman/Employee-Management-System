namespace PracticalMVC.Models
{
    public class Employees_View
    {
        public int UniqueId {  get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime JoinDate { get; set; }
        public string? Designation {  get; set; }
    }
}
