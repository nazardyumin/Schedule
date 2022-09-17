using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Model
{
    public class Lesson
    {
        public string? Subject { get; set; }
        public string? Teacher { get; set; }
        public int AuditoriumNumber { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
    }
}
