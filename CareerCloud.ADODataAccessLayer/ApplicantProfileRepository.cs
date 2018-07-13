﻿using CareerCloud.DataAccessLayer;
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
    public class ApplicantProfileRepository : BaseADO, IDataRepository<ApplicantProfilePoco>
    {
        public void Add(params ApplicantProfilePoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connstring);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            foreach (ApplicantProfilePoco poco in items)
            {
                cmd.CommandText = @"Insert INTO Applicant_Profiles
                                  (Id, Login, Current_Salary, Current_Rate, Currency, Country_Code,
                                  State_Province_Code, Street_Address, City_Town, Zip_Postal_Code)
                                  VALUES
                                  (@Id, @Login, @Current_Salary, @Current_Rate, @Currency, @Country_Code,
                                  @State_Province_Code, @Street_Address, @City_Town, @Zip_Postal_Code)";
                cmd.Parameters.AddWithValue("@Id", poco.Id);
                cmd.Parameters.AddWithValue("@Login", poco.Login);
                cmd.Parameters.AddWithValue("@Current_Salary", poco.CurrentSalary);
                cmd.Parameters.AddWithValue("@Current_Rate", poco.CurrentRate);
                cmd.Parameters.AddWithValue("@Currency", poco.Currency);
                cmd.Parameters.AddWithValue("@Country_Code", poco.Country);
                cmd.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                cmd.Parameters.AddWithValue("@Street_Address", poco.Street);
                cmd.Parameters.AddWithValue("@City_Town", poco.City);
                cmd.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);


                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantProfilePoco> GetAll(params System.Linq.Expressions.Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            ApplicantProfilePoco[] pocos = new ApplicantProfilePoco[1000];

            SqlConnection conn = new SqlConnection(_connstring);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM APPLICANT_PROFILES";

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                int position = 0;
                while (reader.Read())
                {
                    ApplicantProfilePoco poco = new ApplicantProfilePoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Login = reader.GetGuid(1);
                    poco.CurrentSalary = reader.GetDecimal(2);
                    poco.CurrentRate = reader.GetDecimal(3);
                    poco.Currency = reader.GetString(4);
                    poco.Country = reader.GetString(5);
                    poco.Province = reader.GetString(6);
                    poco.Street = reader.GetString(7);
                    poco.City = reader.GetString(8);
                    poco.PostalCode = reader.GetString(9);
                    poco.TimeStamp = (byte[])reader[10];
                    pocos[position] = poco;
                    position++;
                }

                conn.Close();
            }

            return pocos.Where(p => p != null).ToList();
        }
    

        public IList<ApplicantProfilePoco> GetList(System.Linq.Expressions.Expression<Func<ApplicantProfilePoco, bool>> where, params System.Linq.Expressions.Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantProfilePoco GetSingle(System.Linq.Expressions.Expression<Func<ApplicantProfilePoco, bool>> where, params System.Linq.Expressions.Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }
    

        public void Remove(params ApplicantProfilePoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connstring);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantProfilePoco poco in items)
                {
                    cmd.CommandText = @"DELETE FROM Applicant_Profiles
                        WHERE ID = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params ApplicantProfilePoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connstring);

            using (conn)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            foreach (ApplicantProfilePoco poco in items)
            {
                cmd.CommandText = @"UPDATE Applicant_Profiles
                                        SET Login = @Login,
                                        Current_Salary = @Current_Salary,
                                        Current_Rate=@Current_Rate,
                                        Currency=@Currency,
                                        Country_Code=@Country_Code,
                                        State_Province_Code=@State_Province_Code,
                                        Street_Address=@Street_Address,
                                        City_Town=@City_Town,
                                        Zip_Postal_Code=@Zip_Postal_Code
                                        WHERE ID = @Id";
                cmd.Parameters.AddWithValue("@Id", poco.Id);
                cmd.Parameters.AddWithValue("@Login", poco.Login);
                cmd.Parameters.AddWithValue("@Current_Salary", poco.CurrentSalary);
                cmd.Parameters.AddWithValue("@Current_Rate", poco.CurrentRate);
                cmd.Parameters.AddWithValue("@Currency", poco.Currency);
                cmd.Parameters.AddWithValue("@Country_Code", poco.Country);
                cmd.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                cmd.Parameters.AddWithValue("@Street_Address", poco.Street);
                cmd.Parameters.AddWithValue("@City_Town", poco.City);
                cmd.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
    }
}
