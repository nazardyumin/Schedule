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
        public int ConnectionDayIndex { get; set; }
        public int ConnectionLessonIndex { get; set; }
        public Lesson(string subject, string teacher, string auditorium, string time)
        {
            Subject = subject;
            Teacher = teacher;
            Auditorium = auditorium;
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
        public Lesson GetCopy()
        {
            var newLesson = new Lesson(Subject, Teacher,Auditorium, Time);
            newLesson.PositionInWeek = PositionInWeek;
            newLesson.PositionInDayStart = PositionInDayStart;
            newLesson.PositionInDayEnd = PositionInDayEnd;
            newLesson.ConnectionDayIndex = ConnectionDayIndex;
            newLesson.ConnectionLessonIndex = ConnectionLessonIndex;
            return newLesson;
        }
        public void SetConnectionIndexes(int dayIndex, int lessonIndex)
        {
            ConnectionDayIndex = dayIndex;
            ConnectionLessonIndex = lessonIndex;
        }
    }
}
