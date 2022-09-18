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
        private string? AuditoriumNumber;
        private int PositionColumn;
        private int PositionRow;
        private int RowSpan;

        public Lesson (string? subject, string? teacher, string auditoriumNumber)
        {
            Subject = subject;
            Teacher = teacher;
            AuditoriumNumber = auditoriumNumber;
        }

        public Card ConvertToCard()
        {
            return null;
        }
    }
}
