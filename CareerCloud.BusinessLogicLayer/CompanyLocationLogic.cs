﻿using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyLocationLogic : BaseLogic<CompanyLocationPoco>
    {
        public CompanyLocationLogic(IDataRepository<CompanyLocationPoco> repository) : base(repository)
        {
        }

        public override void Add(CompanyLocationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyLocationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(CompanyLocationPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (CompanyLocationPoco poco in pocos)
            {
                if (String.IsNullOrEmpty(poco.CountryCode))
                {
                    exceptions.Add(new ValidationException(500, $"CountryCode Cannot be empty - {poco.Id}"));
                }

                if (String.IsNullOrEmpty(poco.Province))
                {
                    exceptions.Add(new ValidationException(501, $"Province Cannot be empty - {poco.Id}"));
                }

                if (String.IsNullOrEmpty(poco.Street))
                {
                    exceptions.Add(new ValidationException(502, $"Street Cannot be empty - {poco.Id}"));
                }

                if (String.IsNullOrEmpty(poco.City))
                {
                    exceptions.Add(new ValidationException(503, $"City Cannot be empty - {poco.Id}"));
                }

                if (String.IsNullOrEmpty(poco.PostalCode))
                {
                    exceptions.Add(new ValidationException(504, $"PostalCode Cannot be empty - {poco.Id}"));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
