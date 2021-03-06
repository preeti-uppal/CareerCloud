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
    public class CompanyDescriptionRepository : BaseADO, IDataRepository<CompanyDescriptionPoco>
    {
        public void Add(params CompanyDescriptionPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connstring);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            foreach (CompanyDescriptionPoco poco in items)
            {
                cmd.CommandText = @"Insert INTO Company_Descriptions
                                  (Id,Company,LanguageID,Company_Name,
                                    Company_Description) 
                                    VALUES
                                  (@Id,@Company,@LanguageID,@Company_Name,
                                    @Company_Description)";
                cmd.Parameters.AddWithValue("@Id", poco.Id);
                cmd.Parameters.AddWithValue("@Company", poco.Company);
                cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageId);
                cmd.Parameters.AddWithValue("@Company_Name", poco.CompanyName);
                cmd.Parameters.AddWithValue("@Company_Description", poco.CompanyDescription);
                
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyDescriptionPoco> GetAll(params System.Linq.Expressions.Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            CompanyDescriptionPoco[] pocos = new CompanyDescriptionPoco[1000];

            SqlConnection conn = new SqlConnection(_connstring);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM COMPANY_DESCRIPTIONS";

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                int position = 0;
                while (reader.Read())
                {
                    CompanyDescriptionPoco poco = new CompanyDescriptionPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Company = reader.GetGuid(1);
                    poco.LanguageId = reader.GetString(2);
                    poco.CompanyName = reader.GetString(3);
                    poco.CompanyDescription = reader.GetString(4);
                    poco.TimeStamp = (byte[])reader[5];
                    pocos[position] = poco;
                    position++;
                }

                conn.Close();
            }

            return pocos.Where(p => p != null).ToList();
        }
    

        public IList<CompanyDescriptionPoco> GetList(System.Linq.Expressions.Expression<Func<CompanyDescriptionPoco, bool>> where, params System.Linq.Expressions.Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyDescriptionPoco GetSingle(System.Linq.Expressions.Expression<Func<CompanyDescriptionPoco, bool>> where, params System.Linq.Expressions.Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyDescriptionPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connstring);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyDescriptionPoco poco in items)
                {
                    cmd.CommandText = @"DELETE FROM Company_Descriptions 
                        WHERE ID = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyDescriptionPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connstring);

            using (conn)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyDescriptionPoco poco in items)
                {
                    cmd.CommandText = @"UPDATE Company_Descriptions
                                        SET Company = @Company,
                                        LanguageID = @LanguageID,
                                        Company_Name = @Company_Name,
                                        Company_Description = @Company_Description
                                        WHERE ID = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Company", poco.Company);
                    cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageId);
                    cmd.Parameters.AddWithValue("@Company_Name", poco.CompanyName);
                    cmd.Parameters.AddWithValue("@Company_Description", poco.CompanyDescription);
                                      

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
