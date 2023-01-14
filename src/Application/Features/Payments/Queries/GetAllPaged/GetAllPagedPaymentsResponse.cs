﻿using SSDB.Domain.Entities.Catalog;
using System;
using System.ComponentModel.DataAnnotations;

namespace SSDB.Application.Features.Payments.Queries
{
    public class GetAllPagedPaymentsResponse
    {
        public DateTime Date { get; set; }
        public string StudentNumber { get; set; }
        public string Name { get; set; }
        public double RegistrationFees { get; set; }
        public string CurrencyName { get; set; }
        public bool NoStudyFees { get; set; }
        public string Note { get; set; }
        public string FucultyName { get; set; }
        public string Semester { get; set; }
        public string PaymentNo { get; set; }
        public string Status { get; set; }
        public int UniversityId { get; set; }
        public string BranchId { get; set; }
    }
}