using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PlanningPoker.DataContract.Request
{
    public class GameInsertRequest
    {
        [Required]
        public string GameName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool IsChangeVote { get; set; }
        [Required]
        public bool IsDefinitionOfEstimation { get; set; }
        [Required]
        public bool IsStoryTimer { get; set; }
        [Required]
        public bool IsBot { get; set; }
        public long UserId { get; set; }
    }
}
