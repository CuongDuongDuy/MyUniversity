﻿using System;
using System.Collections.Generic;

namespace MyUniversity.Contracts.Models
{
    public class StudentModel : PersonBaseModel
    {
        public DateTime EffectiveDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public DepartmentModel Department { get; set; }
        public IEnumerable<EnrollmentModel> Enrollments { get; set; }
    }
}
