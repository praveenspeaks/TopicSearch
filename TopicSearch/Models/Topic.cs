﻿namespace TopicSearch.Models
{
    public class Topic
    {    
        public string? Name { get; set; }

        public string? Version { get; set; }

        public string? TopicData { get; set; }

        public string TimeStamp { get; } = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();
    }    
}
