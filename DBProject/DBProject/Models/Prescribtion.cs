using System;

namespace DBProject.Models
{
    public class Prescribtion
    {
        // Composite Primary Key
        public string PatientID { get; set; }
        public short MedCode { get; set; }
        public string DocID { get; set; }

        public DateTime PrescribtionDate { get; set; }

        public Patient Patient { get; set; }
        public Medication Medication { get; set; }
        public Doctor Doctor { get; set; }
    }
}