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
    public class CompanyJobDescriptionRepository : BaseADO, IDataRepository<CompanyJobDescriptionPoco>
    {
        public void Add(params CompanyJobDescriptionPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connstring);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            foreach (CompanyJobDescriptionPoco poco in items)
            {
                cmd.CommandText = @"Insert INTO Company_Jobs_Descriptions
                                  (Id,Job,Job_Name,Job_Descriptions) 
                                    VALUES
                                  (@Id,@Job,@Job_Name,@Job_Descriptions)";
                cmd.Parameters.AddWithValue("@Id", poco.Id);
                cmd.Parameters.AddWithValue("@Job", poco.Job);
                cmd.Parameters.AddWithValue("@Job_Name", poco.JobName);
                cmd.Parameters.AddWithValue("@Job_Descriptions", poco.JobDescriptions);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyJobDescriptionPoco> GetAll(params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            CompanyJobDescriptionPoco[] pocos = new CompanyJobDescriptionPoco[5000];

            SqlConnection conn = new SqlConnection(_connstring);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM COMPANY_JOBS_DESCRIPTIONS";

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                int position = 0;
                while (reader.Read())
                {
                    CompanyJobDescriptionPoco poco = new CompanyJobDescriptionPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Job = reader.GetGuid(1);
                    poco.JobName = reader.IsDBNull(2) ?null : (string)reader[2];
                    poco.JobDescriptions = reader.IsDBNull(3) ? null : (string)reader[3];
                    poco.TimeStamp = (byte[])reader[4];
                    pocos[position] = poco;
                    position++;
                }

                conn.Close();
            }

            return pocos.Where(p => p != null).ToList();
        }


        public IList<CompanyJobDescriptionPoco> GetList(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobDescriptionPoco GetSingle(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobDescriptionPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connstring);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyJobDescriptionPoco poco in items)
                {
                    cmd.CommandText = @"DELETE FROM Company_Jobs_Descriptions
                        WHERE ID = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyJobDescriptionPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connstring);

            using (conn)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyJobDescriptionPoco poco in items)
                {
                    cmd.CommandText = @"UPDATE Company_Jobs_Descriptions
                                        SET Job = @Job,
                                        Job_Name = @Job_Name,
                                        Job_Descriptions = @Job_Descriptions
                                        WHERE ID = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Job", poco.Job);
                    cmd.Parameters.AddWithValue("@Job_Name", poco.JobName);
                    cmd.Parameters.AddWithValue("@Job_Descriptions", poco.JobDescriptions);


                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}