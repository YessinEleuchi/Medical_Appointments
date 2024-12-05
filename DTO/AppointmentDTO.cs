using Medical_Appointments_API.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Medical_Appointments_API.DTO
{
    public class AppointmentDTO
    {
        public int AppointmentID { get; set; }

        [Required(ErrorMessage = "DoctorId is required")]
        public string DoctorId { get; set; }

        public string? PatientId { get; set; }

        [Required(ErrorMessage = "AppointmentDateTime is required")]
        public DateTime AppointmentDateTime { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [MaxLength(20, ErrorMessage = "Status cannot exceed 20 characters")]
        public string Status { get; set; } // e.g., Available, Scheduled, Canceled, Completed

        [MaxLength(250, ErrorMessage = "Notes cannot exceed 250 characters")]
        public string? Notes { get; set; }

        public string? DoctorName { get; set; }

        public string? Specialty { get; set; }

        /// <summary>
        /// Maps an Appointment entity to an AppointmentDTO
        /// </summary>
        /// <param name="appointment">The Appointment entity</param>
        /// <returns>A mapped AppointmentDTO object</returns>
        public static AppointmentDTO FromAppointment(Appointment appointment)
        {
            if (appointment == null)
                throw new ArgumentNullException(nameof(appointment), "Appointment cannot be null");

            return new AppointmentDTO
            {
                AppointmentID = appointment.AppointmentID,
                AppointmentDateTime = appointment.AppointmentDateTime,
                Status = appointment.Status,
                Notes = appointment.Notes,
                DoctorId = appointment.DoctorId,
                PatientId = appointment.PatientId,
                DoctorName = appointment.Doctor != null
                    ? $"{appointment.Doctor.FirstName} {appointment.Doctor.LastName}"
                    : "Unknown Doctor",
                Specialty = appointment.Doctor?.Specialty
            };
        }
    }
}
