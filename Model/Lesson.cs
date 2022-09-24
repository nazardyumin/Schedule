namespace Schedule.Model
{
    public class Lesson
    {
        public string Subject { get; set; }
        public string Teacher { get; set; }
        public string Auditorium { get; set; }
        public string Time { get; set; }
        public int PositionInWeek { get; set; }
        public int PositionInDayStart { get; set; }
        public int PositionInDayEnd { get; set; }
        public Lesson(string subject, string teacher, string auditorium, string time)
        {
            Subject = $"Subject: {subject}";
            Teacher = $"Teacher: {teacher}";
            Auditorium = $"Auditorium: {auditorium}";
            Time = time;
        }
        public void SetPositionInWeek(int index)
        {
            PositionInWeek = index;
        }
        public void SetPositionInDayStart(int startIndex)
        {
            PositionInDayStart = startIndex;
        }
        public void SetPositionInDayEnd(int endIndex)
        {
            PositionInDayEnd = endIndex - PositionInDayStart;
        }
    }
}
