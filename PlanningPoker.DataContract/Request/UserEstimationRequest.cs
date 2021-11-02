using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static PlanningPoker.Common.Enums;

namespace PlanningPoker.DataContract.Request
{
    public class UserEstimationRequest
    {
        [Required]
        public KeyWordType KeywordType { get; set; }
        [Required]
        public string KeyWord { get; set; }
        [Required]
        public int BotPoints { get; set; }
        [Required]
        public int UserPoints { get; set; }
        public string Reason { get; set; }
    }
}
