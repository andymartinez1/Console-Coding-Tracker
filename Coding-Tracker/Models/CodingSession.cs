namespace Coding_Tracker.Models
{
    public class CodingSession
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
