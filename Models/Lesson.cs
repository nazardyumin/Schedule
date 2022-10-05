using System;

namespace Schedule.Models
{
    public class Lesson
    {
        public string Subject { get; set; }
        public string Teacher { get; set; }
        public string Auditorium { get; set; }
        public DateTime Date { get; set; }
        public string Duration { get; set; }
        public int PositionInWeek { get; set; }
        public int PositionInDayStart { get; set; }
        public int PositionInDayEnd { get; set; }
        public int ConnectionDayIndex { get; set; }
        public int ConnectionLessonIndex { get; set; }

        public Lesson(string subject, string teacher, string auditorium, string duration)
        {
            Subject = subject;
            Teacher = teacher;
            Auditorium = auditorium;
            Duration = duration;
        }

        public void SetDate(DateTime date)
        {
            Date = date;
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
            var newLesson = new Lesson(Subject, Teacher, Auditorium, Duration)
            {
                PositionInWeek = PositionInWeek,
                PositionInDayStart = PositionInDayStart,
                PositionInDayEnd = PositionInDayEnd,
                ConnectionDayIndex = ConnectionDayIndex,
                ConnectionLessonIndex = ConnectionLessonIndex
            };
            return newLesson;
        }

        public void SetConnectionIndexes(int dayIndex, int lessonIndex)
        {
            ConnectionDayIndex = dayIndex;
            ConnectionLessonIndex = lessonIndex;
        }

        public void Edit(Lesson lesson)
        {
            Subject = lesson.Subject;
            Teacher = lesson.Teacher;
            Auditorium = lesson.Auditorium;
            Duration = lesson.Duration;
            PositionInDayStart = lesson.PositionInDayStart;
            PositionInDayEnd = lesson.PositionInDayEnd;
        }
    }
}