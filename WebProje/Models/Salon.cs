public class Salon
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public List<Employee> Employees { get; set; }
    public List<Service> Services { get; set; }
}
