
using System;
using DBProject.Data;

namespace DBProject
{
    public class Testing
    {
        static void Main(string[] args)
        {
            #region Patient Testing
            PatientRepository patientRepository = new PatientRepository();
            //patientRepository.InsertPatient("4563728", "Ali", "Hamed", new DateTime(2000, 5, 23));
            //patientRepository.InsertPatientPhone("4563728", "01501566889");
            //patientRepository.InsertPatientPhone("4563728", "01201166889");
            //patientRepository.DeletePatientPhone("4563728", "01201166889");
            //patientRepository.DeletePatient("4563728");
            patientRepository.TestGetAllPatientsAlongWithTheirPhoneNumbers();
            Console.WriteLine("----------------------------------------------------------------------------------------");
            patientRepository.TestGetAllPatients();
            Console.WriteLine("----------------------------------------------------------------------------------------");
            #endregion
        }
    }
}
