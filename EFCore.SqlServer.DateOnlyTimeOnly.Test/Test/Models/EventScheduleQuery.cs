using System;

namespace Microsoft.EntityFrameworkCore.SqlServer.Test.Models
{
    class EventScheduleQuery : EventSchedule
    {
        public DateTime LegacyDateTime { get; set; }
    }
}
