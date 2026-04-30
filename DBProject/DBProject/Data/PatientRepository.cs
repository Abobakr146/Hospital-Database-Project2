
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DBProject.Models;

namespace DBProject.Data
{
    public class PatientRepository
    {
        public DataTable GetAllPatientsAsDataTable()
        {
            var dt = new DataTable();

            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new SqlCommand(@"
            SELECT 
                p.PatientID AS [Patient ID],
                p.FirstName AS [First Name],
                p.LastName AS [Last Name],
                p.DOB AS [Date of Birth],
                STUFF((
                    SELECT ', ' + pp2.Phone
                    FROM Patient_Phone pp2
                    WHERE pp2.PatientID = p.PatientID
                    FOR XML PATH('')
                ), 1, 2, '') AS [Phone Numbers]
            FROM Patient p
            ORDER BY p.LastName, p.FirstName", conn);

                conn.Open();
                using (var adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }

            return dt;
        }
    }
}
