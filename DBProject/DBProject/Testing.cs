
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
            //AppointmentRepository appRepo = new AppointmentRepository();

            //appRepo.TestGetAllAppointments();

            //Console.WriteLine("\n=== Inserting New Appointment ===\n");
            //bool insertResult = appRepo.InsertAppointment(
            //    new DateTime(2026, 6, 15),
            //    new TimeSpan(10, 30, 0),
            //    "Scheduled",
            //    "123456789",
            //    "456789123"
            //);
            //Console.WriteLine(insertResult ? "Insert successful!" : "Insert failed!");

            //appRepo.TestGetAllAppointments();

            //Console.WriteLine("\n=== Updating Appointment ===\n");
            //bool updateResult = appRepo.UpdateAppointment(
            //    4,
            //    new DateTime(2026, 6, 15),
            //    new TimeSpan(11, 0, 0),
            //    "Completed",
            //    "123456789",
            //    "456789123"
            //);
            //Console.WriteLine(updateResult ? "Update successful!" : "Update failed!");

            //appRepo.TestGetAllAppointments();

            //Console.WriteLine("\n=== Deleting Appointment ===\n");
            //bool deleteResult = appRepo.DeleteAppointment(4);
            //Console.WriteLine(deleteResult ? "Delete successful!" : "Delete failed!");

            //appRepo.TestGetAllAppointments();

            //Console.WriteLine("\n=== Appointments by Doctor ===\n");
            //appRepo.TestGetAppointmentsByDoctor("456789123");
            #endregion

            #region Doctor Repository
            //DoctorRepository docRepo = new DoctorRepository();

            //docRepo.TestGetAllDoctors();
            //Console.WriteLine("\n=== Inserting New Doctor ===\n");
            //bool insertResult = docRepo.InsertDoctor("999999999", "Mohamed", "Salah", "Surgery", 1);
            //Console.WriteLine(insertResult ? "Insert successful!" : "Insert failed!");

            //docRepo.TestGetAllDoctors();

            //Console.WriteLine("\n=== Updating Doctor ===\n");
            //docRepo.UpdateDoctor("999999999", specialty: "Cardiology");
            //docRepo.TestGetAllDoctors();
            //docRepo.UpdateDoctor("999999999", firstName: "Ali", deptId: 3);
            //docRepo.TestGetAllDoctors();
            //docRepo.UpdateDoctor("999999999", lastName: "Ahmed", specialty: "Neurology", deptId: 1);
            //docRepo.TestGetAllDoctors();

            //Console.WriteLine("\n=== Deleting Doctor ===\n");
            //bool deleteResult = docRepo.DeleteDoctor("999999999");
            //Console.WriteLine(deleteResult ? "Delete successful!" : "Delete failed!");

            //docRepo.TestGetAllDoctors();

            #endregion

            #region Presciption Repository
            //PrescribtionRepository presRepo = new PrescribtionRepository();
            //presRepo.TestGetAllPrescribtions();
            #endregion

            #region Medication Repository
            //MedicationRepository medRepo = new MedicationRepository();
            //medRepo.TestGetAllMedications();
            #endregion

            #region Department Repository
            //DepartmentRepository depRepo = new DepartmentRepository();
            //depRepo.TestGetAllDepartments();
            //depRepo.TestGetDoctorCountByDepartment();
            #endregion
        }
    }
}
