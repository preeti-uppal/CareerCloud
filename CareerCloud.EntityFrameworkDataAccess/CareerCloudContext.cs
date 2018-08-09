﻿using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class CareerCloudContext : DbContext
    {
        public CareerCloudContext() : base(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString)
        {
        }

        public DbSet<ApplicantEducationPoco> ApplicantEducations { get; set; }
        public DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
        public DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        public DbSet<ApplicantResumePoco> ApplicantResumes { get; set; }
        public DbSet<ApplicantSkillPoco> ApplicantSkills { get; set; }
        public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistories { get; set; }
        public DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
        public DbSet<CompanyJobDescriptionPoco> CompanyJobDescriptions { get; set; }
        public DbSet<CompanyJobEducationPoco> CompanyJobEducations { get; set; }
        public DbSet<CompanyJobPoco> CompanyJobs { get; set; }
        public DbSet<CompanyJobSkillPoco> CompanyJobSkills { get; set; }
        public DbSet<CompanyLocationPoco> CompanyLocations { get; set; }
        public DbSet<CompanyProfilePoco> CompanyProfiles { get; set; }
        public DbSet<SecurityLoginPoco> SecurityLogins { get; set; }
        public DbSet<SecurityLoginsLogPoco> SecurityLoginsLogs { get; set; }
        public DbSet<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
        public DbSet<SecurityRolePoco> SecurityRoles { get; set; }
        public DbSet<SystemCountryCodePoco> SystemCountryCodes { get; set; }
        public DbSet<SystemLanguageCodePoco> SystemLanguageCodePocos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //CompanyProfilePoco
            modelBuilder.Entity<CompanyProfilePoco>()
                .HasMany(e => e.CompanyDescriptions)
                .WithRequired(e => e.CompanyProfile)
                .HasForeignKey(e => e.Company)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CompanyProfilePoco>()
                .HasMany(e => e.CompanyJobs)
                .WithRequired(e => e.CompanyProfile)
                .HasForeignKey(e => e.Company)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CompanyProfilePoco>()
                .HasMany(e => e.CompanyLocations)
                .WithRequired(e => e.CompanyProfile)
                .HasForeignKey(e => e.Company)
                .WillCascadeOnDelete(false);

            //SecurityLoginPoco
            modelBuilder.Entity<SecurityLoginPoco>()
                .HasMany(e => e.ApplicantProfiles)
                .WithRequired(e => e.SecurityLogin)
                .HasForeignKey(e => e.Login)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<SecurityLoginPoco>()
                .HasMany(e => e.SecurityLoginsRoles)
                .WithRequired(e => e.SecurityLogin)
                .HasForeignKey(e => e.Login)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SecurityLoginPoco>()
                .HasMany(e => e.SecurityLoginsLogs)
                .WithRequired(e => e.SecurityLogin)
                .HasForeignKey(e => e.Login)
                .WillCascadeOnDelete(false);

            //ApplicantProfilePoco
            modelBuilder.Entity<ApplicantProfilePoco>()
               .HasMany(e => e.ApplicantWorkHistories)
               .WithRequired(e => e.ApplicantProfile)
               .HasForeignKey(e => e.Applicant)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(e => e.ApplicantResumes)
                .WithRequired(e => e.ApplicantProfile)
                .HasForeignKey(e => e.Applicant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(e => e.ApplicantSkills)
                .WithRequired(e => e.ApplicantProfile)
                .HasForeignKey(e => e.Applicant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(e => e.ApplicantEducations)
                .WithRequired(e => e.ApplicantProfile)
                .HasForeignKey(e => e.Applicant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(e => e.ApplicantJobApplications)
                .WithRequired(e => e.ApplicantProfile)
                .HasForeignKey(e => e.Applicant)
                .WillCascadeOnDelete(false);

           


            // CompanyJobPoco
            modelBuilder.Entity<CompanyJobPoco>()
                .HasMany(e => e.ApplicantJobApplications)
                .WithRequired(e => e.CompanyJob)
                .HasForeignKey(e => e.Job)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CompanyJobPoco>()
                .HasMany(e => e.CompanyJobEducations)
                .WithRequired(e => e.CompanyJob)
                .HasForeignKey(e => e.Job)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CompanyJobPoco>()
                .HasMany(e => e.CompanyJobSkills)
                .WithRequired(e => e.CompanyJob)
                .HasForeignKey(e => e.Job)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CompanyJobPoco>()
                .HasMany(e => e.CompanyJobDescriptions)
                .WithRequired(e => e.CompanyJob)
                .HasForeignKey(e => e.Job)
                .WillCascadeOnDelete(false);



            //SystemCountryCodePoco
            modelBuilder.Entity<SystemCountryCodePoco>()
                .HasMany(e => e.ApplicantProfiles)
                .WithRequired(e => e.SystemCountryCode)
                .HasForeignKey(e => e.Country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SystemCountryCodePoco>()
                .HasMany(e => e.ApplicantWorkHistories)
                .WithRequired(e => e.SystemCountryCode)
                .HasForeignKey(e => e.CountryCode)
                .WillCascadeOnDelete(false);

            //SystemLanguageCodePoco
            modelBuilder.Entity<SystemLanguageCodePoco>()
                .HasMany(e => e.CompanyDescriptions)
                .WithRequired(e => e.SystemLanguageCode)
                .HasForeignKey(e => e.LanguageId)
                .WillCascadeOnDelete(false);


            //SecurityRolePoco
            modelBuilder.Entity<SecurityRolePoco>()
             .HasMany(e => e.SecurityLoginsRoles)
             .WithRequired(e => e.SecurityRole)
             .HasForeignKey(e => e.Login)
             .WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);
        }

    }
}
