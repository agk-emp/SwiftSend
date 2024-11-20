namespace SwiftSend.app.Dtos.RestaurantDtos.Inputs
{
    public class ScheduleDto
    {
        public bool Sunday { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
    }
}
