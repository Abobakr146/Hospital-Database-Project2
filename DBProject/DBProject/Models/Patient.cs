using System;
using System.Collections.Generic;

namespace DBProject.Models
{
    public class Patient
    {
        public string PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DOB { get; set; }

        public List<PatientPhone> PhoneNumbers { get; set; } = new List<PatientPhone>();
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
        public List<Prescribtion> Prescribtions { get; set; } = new List<Prescribtion>();

        public string FullName => $"{FirstName} {LastName}";
        public int? Age => DOB.HasValue ? (int)((DateTime.Now - DOB.Value).TotalDays / 365.25) : (int?)null;
    }
}