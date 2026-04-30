using System.Collections.Generic;

namespace DBProject.Models
{
    public class Medication
    {
        public short MedCode { get; set; }
        public string MedName { get; set; }
        public decimal Dosage { get; set; }
        public string Unit { get; set; }

        public List<Prescribtion> Prescribtions { get; set; } = new List<Prescribtion>();

        public string FullDescription => $"{MedName} ({Dosage} {Unit})";
    }
}