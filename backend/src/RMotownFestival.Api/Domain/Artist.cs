using System;
using System.Text.Json.Serialization;

namespace RMotownFestival.Api.Domain
{
    public class Artist
    {
        public int Id { get; set; }

        public int FestivalId { get; set; }
        [JsonIgnore]
        public Festival Festival { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public Uri Website { get; set; }
    }

}
