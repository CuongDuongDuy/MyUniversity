using System;
using MyUniversity.Contracts.Constants;

namespace MyUniversity.Dal.Entities
{
    public class AuditTrail
    {
        public AuditTrail(AuditTrailActionType auditTrailAction, string value)
        {
            Guid = new Guid();
            ActionType = auditTrailAction.ToString();
            Value = value;
            CreatedBy = EntityConstant.CreatedBy;
            CreatedOn = DateTime.Now;
        }

        public Guid Guid { get; set; }
        public string ActionType { get; set; }
        public string Value { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public enum AuditTrailActionType
    {
        Added,
        Modified
    }
}
