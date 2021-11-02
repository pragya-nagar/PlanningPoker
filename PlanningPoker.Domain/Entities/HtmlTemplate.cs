using System;
using System.Collections.Generic;

namespace PlanningPoker.Domain.Entities
{
    public partial class HtmlTemplate
    {
        public long TamplateId { get; set; }
        public int? TamplateTypeId { get; set; }
        public string TamplateName { get; set; }
        public string Heading { get; set; }
        public string Subject { get; set; }
        public string HtmlText { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Guid RowGuid { get; set; }
    }
}
