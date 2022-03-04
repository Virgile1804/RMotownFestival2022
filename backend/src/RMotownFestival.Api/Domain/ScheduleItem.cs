using System;
using System.Text.Json.Serialization;

namespace RMotownFestival.Api.Domain
{
    public class ScheduleItem
    {
        public int Id { get; set; }

        public int? ScheduleId { get; set; }
        [JsonIgnore]
        public Schedule Schedule { get; set; }

        public int? ArtistId { get; set; }
        public Artist Artist { get; set; }

        public int? StageId { get; set; }
        public Stage Stage { get; set; }

        public DateTime Time { get; set; }

        private bool isFavorite;
        public bool IsFavorite
        {
            get { return isFavorite; }
            set
            {
                if (isFavorite != value)
                {
                    isFavorite = value;
                }
            }
        }
    }
}
