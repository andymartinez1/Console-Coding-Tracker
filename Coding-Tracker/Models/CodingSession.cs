namespace Coding_Tracker.Models
{
    public class CodingSession
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Duration { get; set; }

        public CodingSession(int id, string projectName, DateTime startTime, DateTime endTime, double duration)
        {
            Id = id;
            ProjectName = projectName;
            StartTime = startTime;
            EndTime = endTime;
            Duration = duration;
        }
    }
}
