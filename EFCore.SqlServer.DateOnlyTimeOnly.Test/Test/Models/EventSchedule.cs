using System;

namespace Microsoft.EntityFrameworkCore.SqlServer.Test.Models
{
    class EventSchedule
    {
        public int Id { get; set; }
        public DateOnly StartDate { get; set; }
        public TimeOnly StartTime { get; set; }
    }
}
