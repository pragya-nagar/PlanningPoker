using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningPoker.DataContract.Response
{
    public class GameDetailsResponse
    {
        public int RowNum { get; set; }
        public long GameId { get; set; }
        public string GameName { get; set; }
        public long UserStories { get; set; }
        public DateTime LastAccess { get; set; }
        public string Status { get; set; }
    }
}
