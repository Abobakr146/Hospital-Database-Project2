
using System;
using DBProject.Data;

namespace DBProject
{
    public class Testing
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            #region Patient Testing
            //PatientRepository patientRepository = new PatientRepository();
            //patientRepository.InsertPatient("4563728", "Ali", "Hamed", new DateTime(2000, 5, 23));
            //patientRepository.InsertPatientPhone("4563728", "01501566889");
            //patientRepository.InsertPatientPhone("4563728", "01201166889");
            //patientRepository.DeletePatientPhone("4563728", "01201166889");
            //patientRepository.DeletePatient("4563728");
            //patientRepository.TestGetAllPatientsAlongWithTheirPhoneNumbers();
            //Console.WriteLine("----------------------------------------------------------------------------------------");
            //patientRepository.TestGetAllPatients();
            //Console.WriteLine("----------------------------------------------------------------------------------------");
            #endregion

            #region Appointment Repository
            //AppointmentRepository repo = new AppointmentRepository();

            //repo.TestGetAllAppointments();

            //Console.WriteLine("\n=== Inserting New Appointment ===\n");
            //bool insertResult = repo.InsertAppointment(
            //    new DateTime(2026, 6, 15),
            //    new TimeSpan(10, 30, 0),
            //    "Scheduled",
            //    "123456789",
            //    "456789123"
            //);
            //Console.WriteLine(insertResult ? "Insert successful!" : "Insert failed!");

            //repo.TestGetAllAppointments();

            //Console.WriteLine("\n=== Updating Appointment ===\n");
            //bool updateResult = repo.UpdateAppointment(
            //    4,
            //    new DateTime(2026, 6, 15),
            //    new TimeSpan(11, 0, 0),
            //    "Completed",
            //    "123456789",
            //    "456789123"
            //);
            //Console.WriteLine(updateResult ? "Update successful!" : "Update failed!");

            //repo.TestGetAllAppointments();

            //Console.WriteLine("\n=== Deleting Appointment ===\n");
            //bool deleteResult = repo.DeleteAppointment(4);
            //Console.WriteLine(deleteResult ? "Delete successful!" : "Delete failed!");

            //repo.TestGetAllAppointments();

            //Console.WriteLine("\n=== Appointments by Doctor ===\n");
            //repo.TestGetAppointmentsByDoctor("456789123");
            #endregion

            #region Doctor Repository
            //DoctorRepository repo = new DoctorRepository();

            //repo.TestGetAllDoctors();

            #endregion

            #region Presciption Repository
            PrescribtionRepository repo = new PrescribtionRepository();
            repo.TestGetAllPrescribtions();
            #endregion


        }
    }
}
