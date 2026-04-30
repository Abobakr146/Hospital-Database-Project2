namespace DBProject.Models
{
    public class PatientPhone
    {
        public string PatientID { get; set; }
        public string Phone { get; set; }

        public Patient Patient { get; set; }
    }
}