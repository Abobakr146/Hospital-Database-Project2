using System.Collections.Generic;

namespace DBProject.Models
{
    public class Department
    {
        public short DeptID { get; set; }
        public string DeptName { get; set; }
        public string Location { get; set; }
        public string Manager_DocID { get; set; }

        public Doctor Manager { get; set; }
        public List<Doctor> Doctors { get; set; } = new List<Doctor>();
    }
}