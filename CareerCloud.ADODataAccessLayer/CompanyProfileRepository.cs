using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyProfileRepository : BaseADO, IDataRepository<CompanyProfilePoco>
    {
        public void Add(params CompanyProfilePoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connstring);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            foreach (CompanyProfilePoco poco in items)
            {
                cmd.CommandText = @"Insert INTO Company_Profiles
                                  (Id,Registration_Date,Company_Website,
                                   Contact_Phone,Contact_Name,Company_Logo)                                    
                                    VALUES
                                  (@Id,@Registration_Date,@Company_Website,
                                   @Contact_Phone,@Contact_Name,@Company_Logo)";
                cmd.Parameters.AddWithValue("@Id", poco.Id);
                cmd.Parameters.AddWithValue("@Registration_Date", poco.RegistrationDate);
                cmd.Parameters.AddWithValue("@Company_Website", poco.CompanyWebsite);
                cmd.Parameters.AddWithValue("@Contact_Phone", poco.ContactPhone);
                cmd.Parameters.AddWithValue("@Contact_Name", poco.ContactName);
                cmd.Parameters.AddWithValue("@Company_Logo", poco.CompanyLogo);
                

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyProfilePoco> GetAll(params System.Linq.Expressions.Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            CompanyProfilePoco[] pocos = new CompanyProfilePoco[1000];

            SqlConnection conn = new SqlConnection(_connstring);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM COMPANY_PROFILES";

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                int position = 0;
                while (reader.Read())
                {
                    CompanyProfilePoco poco = new CompanyProfilePoco();
                    poco.Id = reader.GetGuid(0);
                    poco.RegistrationDate = reader.GetDateTime(1);
                    poco.CompanyWebsite = reader.IsDBNull(2) ? null : (string)reader[2];
                    poco.ContactPhone = reader.GetString(3);
                    poco.ContactName = reader.IsDBNull(4) ? null : (string)reader[4];
                    poco.CompanyLogo = reader.IsDBNull(5) ? null : (byte[])reader[5];
                    poco.TimeStamp = (byte[])reader[6];
                    pocos[position] = poco;
                    position++;
                }

                conn.Close();
            }

            return pocos.Where(p => p != null).ToList();
        }
    

        public IList<CompanyProfilePoco> GetList(System.Linq.Expressions.Expression<Func<CompanyProfilePoco, bool>> where, params System.Linq.Expressions.Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(System.Linq.Expressions.Expression<Func<CompanyProfilePoco, bool>> where, params System.Linq.Expressions.Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connstring);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyProfilePoco poco in items)
                {
                    cmd.CommandText = @"DELETE FROM Company_Profiles
                        WHERE ID = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyProfilePoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connstring);

            using (conn)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyProfilePoco poco in items)
                {
                    cmd.CommandText = @"UPDATE Company_Profiles
                                        SET Registration_Date = @Registration_Date,
                                        Company_Website = @Company_Website,
                                        Contact_Phone = @Contact_Phone,
                                        Contact_Name = @Contact_Name,
                                        Company_Logo = @Company_Logo                        
                                        WHERE ID = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Registration_Date", poco.RegistrationDate);
                    cmd.Parameters.AddWithValue("@Company_Website", poco.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@Contact_Phone", poco.ContactPhone);
                    cmd.Parameters.AddWithValue("@Contact_Name", poco.ContactName);
                    cmd.Parameters.AddWithValue("@Company_Logo", poco.CompanyLogo);


                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }

}

