using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantJobApplicationRepository : BaseADO, IDataRepository <ApplicantJobApplicationPoco>
    {
        public void Add(params ApplicantJobApplicationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connstring);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;


            foreach (ApplicantJobApplicationPoco poco in items)
            {
                cmd.CommandText = @"Insert INTO Applicant_Job_Applications
                                  (Id, Applicant, Job, Application_Date)  
                                    VALUES
                                  (@Id, @Applicant, @Job, @Application_Date)";
                cmd.Parameters.AddWithValue("@Id", poco.Id);
                cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                cmd.Parameters.AddWithValue("@Job", poco.Job);
                cmd.Parameters.AddWithValue("@Application_Date", poco.ApplicationDate);


                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantJobApplicationPoco> GetAll(params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            ApplicantJobApplicationPoco[] pocos = new ApplicantJobApplicationPoco[1000];

            SqlConnection conn = new SqlConnection(_connstring);

            using (conn)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM APPLICANT_JOB_APPLICATIONS";

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                int position = 0;
                while (reader.Read())
                {
                    ApplicantJobApplicationPoco poco = new ApplicantJobApplicationPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Applicant = reader.GetGuid(1);
                    poco.Job = reader.GetGuid(2);
                    poco.ApplicationDate = reader.GetDateTime(3);
                    poco.TimeStamp = (byte[])reader[4];
                    pocos[position] = poco;
                    position++;
                }

                conn.Close();
            }

            return pocos.Where(p => p != null).ToList();

        }

        public IList<ApplicantJobApplicationPoco> GetList(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantJobApplicationPoco GetSingle(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantJobApplicationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }
    

        public void Remove(params ApplicantJobApplicationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connstring);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantJobApplicationPoco poco in items)
                {
                    cmd.CommandText = @"DELETE FROM Applicant_Job_Applications 
                        WHERE ID = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params ApplicantJobApplicationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connstring);

            using (conn)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantJobApplicationPoco poco in items)
                {
                    cmd.CommandText = @"UPDATE Applicant_Job_Applications
                                        SET Applicant = @Applicant,
                                        Job = @Job,
                                        Application_Date=@Application_Date
                                        WHERE ID = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Job", poco.Job);
                    cmd.Parameters.AddWithValue("@Application_Date", poco.ApplicationDate);
                   
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }

    
}
