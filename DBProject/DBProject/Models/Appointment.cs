using System;

namespace DBProject.Models
{
    public class Appointment
    {
        public short ApptID { get; set; }
        public DateTime ApptDate { get; set; }
        public TimeSpan ApptTime { get; set; }
        public string Status { get; set; }

        public string PatientID { get; set; }
        public string DoctorID { get; set; }

        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }

        public DateTime AppointmentDateTime => ApptDate.Add(ApptTime);
    }
}