using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RMotownFestival.Api.Domain
{
    public class Schedule
    {
        public int Id { get; set; }

        public int FestivalId { get; set; }
        [JsonIgnore]
        public Festival Festival { get; set; }

        public ICollection<ScheduleItem> Items { get; set; }

        public Schedule()
        {
            Items = new List<ScheduleItem>();
        }
    }
}
