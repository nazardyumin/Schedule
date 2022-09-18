using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Schedule.Model
{
    public class Lesson
    {
        private string? Subject;
        private string? Teacher;
        private int AuditoriumNumber;
        private string? StartTime;
        private string? EndTime;
        private int PositionColumn;
        private int PositionRow;
        private int RowSpan;

        public Lesson (string? subject, string? teacher, int auditoriumNumber, string? startTime, string? endTime, int positionColumn, int positionRow, int rowSpan)
        {
            Subject = subject;
            Teacher = teacher;
            AuditoriumNumber = auditoriumNumber;
            StartTime = startTime;
            EndTime = endTime;
            PositionColumn = positionColumn;
            PositionRow = positionRow;
            RowSpan = rowSpan;
        }
    }
}
