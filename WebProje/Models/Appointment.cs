public class Appointment
{
    public int Id { get; set; }
    public DateTime AppointmentTime { get; set; }
    public int EmployeeId { get; set; }
    public int ServiceId { get; set; }
    public string? UserId { get; set; }
    public string? CustomerName { get; set; }
    public virtual Employee? Employee { get; set; }
    public virtual Service? Service { get; set; }
}
