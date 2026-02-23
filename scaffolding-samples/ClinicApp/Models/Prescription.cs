using System.ComponentModel.DataAnnotations;

namespace ClinicApp.Models;

public class Prescription
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public int DoctorId { get; set; }

    [Required]
    [StringLength(200)]
    public string MedicationName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Dosage { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Instructions { get; set; }

    [Required]
    public DateTime PrescribedDate { get; set; }

    public DateTime? ExpiryDate { get; set; }
}
