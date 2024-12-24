using System.ComponentModel.DataAnnotations;

namespace BarberProject.Models
{
    public class Appointment
    {
        [Key]
        public int? Id { get; set; }
        public DateTime Date { get; set; }
        public Barber? Barber { get; set; }
        public int? BarberId {  get; set; }   // foreign key
        public Department? Department { get; set; }
        public int? DepartmentId { get; set; }       // foreign key
        public Customer? Customer { get; set; }
        public string CustomerId { get; set; }   // foreign key

    }
}
