using System.ComponentModel.DataAnnotations;

namespace ClinicApp.Models;

public class Appointment
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public int DoctorId { get; set; }

    [Required]
    public DateTime AppointmentDate { get; set; }

    [Range(5, 480)]
    public int DurationMinutes { get; set; }

    [Required]
    [StringLength(50)]
    public string Status { get; set; } = "Scheduled";

    [StringLength(1000)]
    public string? Notes { get; set; }
}
