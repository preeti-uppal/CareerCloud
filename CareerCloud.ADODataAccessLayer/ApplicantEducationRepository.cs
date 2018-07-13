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
    public class ApplicantEducationRepository : BaseADO, IDataRepository<ApplicantEducationPoco>
    {
        public void Add(params ApplicantEducationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connstring);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            foreach (ApplicantEducationPoco poco in items)
            {
                cmd.CommandText = @"Insert INTO Applicant_Educations
                                  (Id,Applicant,Major,Certificate_Diploma,Start_Date,
                                  Completion_Date,Completion_Percent)
                                  VALUES
                                  (@Id, @Applicant, @Major, @Certificate_Diploma, @Start_Date,
                                  @Completion_Date, @Completion_Percent)";
                cmd.Parameters.AddWithValue("@Id", poco.Id);
                cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                cmd.Parameters.AddWithValue("@Major", poco.Major);
                cmd.Parameters.AddWithValue("@Certificate_Diploma", poco.CertificateDiploma);
                cmd.Parameters.AddWithValue("@Start_Date", poco.StartDate);
                cmd.Parameters.AddWithValue("@Completion_Date", poco.CompletionDate);
                cmd.Parameters.AddWithValue("@Completion_Percent", poco.CompletionPercent);


                conn.Open();
                cmd.ExecuteNonQuery();

                conn.Close();
            }

        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantEducationPoco> GetAll(params System.Linq.Expressions.Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            ApplicantEducationPoco[] pocos = new ApplicantEducationPoco[1000];
            SqlConnection conn = new SqlConnection(_connstring);

            using (conn)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM APPLICANT_EDUCATIONS";

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                int position = 0;
                while (reader.Read())
                {
                    ApplicantEducationPoco poco = new ApplicantEducationPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Applicant = reader.GetGuid(1);
                    poco.Major = reader.GetString(2);
                    poco.CertificateDiploma = reader.GetString(3);
                    poco.StartDate = reader.GetDateTime(4);
                    poco.CompletionDate = reader.GetDateTime(5);
                    poco.CompletionPercent = (byte?)reader.GetByte(6);
                    poco.TimeStamp = (byte[])reader[7];
                    pocos[position] = poco;
                    position++;
                }

                conn.Close();
            }

            return pocos.Where(p => p!=null).ToList();
        } 

        public IList<ApplicantEducationPoco> GetList(System.Linq.Expressions.Expression<Func<ApplicantEducationPoco, bool>> where, params System.Linq.Expressions.Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantEducationPoco GetSingle(System.Linq.Expressions.Expression<Func<ApplicantEducationPoco, bool>> where, params System.Linq.Expressions.Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantEducationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantEducationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connstring);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantEducationPoco poco in items)
                {
                    cmd.CommandText = @"DELETE FROM Applicant_Educations 
                        WHERE ID = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params ApplicantEducationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connstring);

            using (conn)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantEducationPoco poco in items)
                {
                    cmd.CommandText = @"UPDATE Applicant_Educations
                                        SET Applicant = @Applicant,
                                        Major = @Major,
                                        Certificate_Diploma=@Certificate_Diploma,
                                        Start_Date=@Start_Date,
                                        Completion_Date=@Completion_Date,
                                        Completion_Percent=@Completion_Percent
                                        WHERE ID = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Major", poco.Major);
                    cmd.Parameters.AddWithValue("@Certificate_Diploma", poco.CertificateDiploma);
                    cmd.Parameters.AddWithValue("@Start_Date", poco.StartDate);
                    cmd.Parameters.AddWithValue("@Completion_Date", poco.CompletionDate);
                    cmd.Parameters.AddWithValue("@Completion_Percent", poco.CompletionPercent);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}

