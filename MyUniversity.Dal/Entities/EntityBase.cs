﻿using System;
using System.ComponentModel.DataAnnotations;

namespace MyUniversity.Dal.Entities
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Deactive { get; set; }
        [Timestamp]
        [ConcurrencyCheck]
        public byte[] RowVersion { get; set; }
    }
}