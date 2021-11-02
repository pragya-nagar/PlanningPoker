using System;
using System.Collections.Generic;

namespace PlanningPoker.Domain.Entities
{
    public partial class Email
    {
        public long EmailId { get; set; }
        public int? EmailTypeId { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string ToEmail { get; set; }
        public string ToName { get; set; }
        public string Ccemail { get; set; }
        public string Bccname { get; set; }
        public string Subject { get; set; }
        public string EmailText { get; set; }
        public string Attachment { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Guid RowGuid { get; set; }
    }
}
