namespace SIS_2_.Models
{
    public class ActivityLogModel
    {
        public string UserId { get; set; }
        public DateTime Timestamp { get; set; }
        public string ActionType { get; set; }
    }
}
