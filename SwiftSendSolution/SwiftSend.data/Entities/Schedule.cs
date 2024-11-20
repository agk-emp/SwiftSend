using MongoDB.Bson.Serialization.Attributes;
using System.Text.RegularExpressions;

namespace SwiftSend.data.Entities
{
    public class Schedule
    {
        [BsonElement("sunday")]
        public bool Sunday { get; set; }

        [BsonElement("monday")]
        public bool Monday { get; set; }

        [BsonElement("tuesday")]
        public bool Tuesday { get; set; }

        [BsonElement("wednesday")]
        public bool Wednesday { get; set; }

        [BsonElement("thursday")]
        public bool Thursday { get; set; }

        [BsonElement("friday")]
        public bool Friday { get; set; }

        [BsonElement("saturday")]
        public bool Saturday { get; set; }

        private string? _startTime;

        [BsonElement("start_time")]
        public string? StartTime
        {
            get => _startTime;
            set
            {
                ValidateTimeOnlyFormat(value, nameof(StartTime));
                _startTime = value;
            }
        }

        private string? _endTime;

        [BsonElement("end_time")]
        public string? EndTime
        {
            get => _endTime;
            set
            {
                ValidateTimeOnlyFormat(value, nameof(EndTime));
                _endTime = value;
            }
        }



        private void ValidateTimeOnlyFormat(string propertyValue, string propertyName)
        {
            if (string.IsNullOrEmpty(propertyValue))
            {
                return;
            }
            if (!Regex.IsMatch(propertyValue, @"^(?:[01]\d|2[0-3]):[0-5]\d$"))
            {
                throw new FormatException($"{propertyName} must be in HH:mm format. Invalid value: {propertyValue}");
            }
        }
    }
}
