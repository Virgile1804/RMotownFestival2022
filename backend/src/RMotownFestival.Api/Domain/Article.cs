using System;

namespace RMotownFestival.Api.Domain
{
    public class Article
    {
        public string Id { get; set; }

        public string Tag { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public string ImagePath { get; set; }

        public string Status { get; set; }

        public DateTime Date { get; set; }
    }

    public enum Status
    {
        Published,
        Unpublished
    }
}