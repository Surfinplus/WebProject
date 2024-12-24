using System.ComponentModel.DataAnnotations;

namespace BerberProject.Models
{
    public class Department
    {
        [Key]
        public int? DepartmentId { get; set; }
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Department name must contain only letters.")]
        public string DepartmentName { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<berber>? Berbers { get; set; } = new List<Berber>();
        public ICollection<Appointment>? Appointments { get; set; } = new List<Appointment>();
    }
}
