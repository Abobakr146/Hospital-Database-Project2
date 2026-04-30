using System.Collections.Generic;

namespace DBProject.Models
{
    public class Doctor
    {
        public string DoctorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialty { get; set; }
        public short DeptID { get; set; }

        public Department Department { get; set; }
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
        public List<Prescribtion> Prescribtions { get; set; } = new List<Prescribtion>();

        // Computed Property
        public string FullName => $"{FirstName} {LastName}";
    }
}