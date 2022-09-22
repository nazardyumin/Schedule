namespace Schedule.Model
{
    public class Lesson
    {
        public string? Subject { get; set; }
        public string? Teacher { get; set; }
        public string? Auditorium { get; set; }
        public string? Time { get; set; }
        public int PositionInWeek { get; set; }
        public int PositionInDayStart { get; set; }
        public int PositionInDayEnd { get; set; }
        public Lesson (string subject, string teacher, string auditorium, string time)
        {
            Subject = $"Subject: {subject}";
            Teacher = $"Teacher: {teacher}";
            Auditorium = $"Auditorium: {auditorium}";
            Time = time;
        }
        public void SetPositionColumn(int index)
        {
            PositionInWeek = index;
        }
        public void SetPositionRow(int index)
        {
            PositionInDayStart = index;
        }
        public void SetRowSpan(int index)
        {
            PositionInDayEnd = index - PositionInDayStart;
        }
    }
}
