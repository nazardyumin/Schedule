using Schedule.Models;

namespace Schedule.ViewModels.AddingSection
{
    public class AddingSectionDataBinding
    {
        public InputBinding? Subject { get; set; }
        public InputBinding? Teacher { get; set; }
        public InputBinding? Auditorium { get; set; }
        public ItemBinding? StartTimeSelectedItem { get; set; }
        public ItemBinding? EndTimeSelectedItem { get; set; }
        public ItemBinding? YearsFromSelectedItem { get; set; }
        public ItemBinding? MonthsFromSelectedItem { get; set; }
        public ItemBinding? DatesFromSelectedItem { get; set; }
        public ItemBinding? YearsToSelectedItem { get; set; }
        public ItemBinding? MonthsToSelectedItem { get; set; }
        public ItemBinding? DatesToSelectedItem { get; set; }
        public ItemBinding? CopyDays1SelectedItem { get; set; }
        public ItemBinding? CopyDays2SelectedItem { get; set; }
        public ItemBinding? CopyDays3SelectedItem { get; set; }

        public int GetYearFrom()
        {
            return YearsFromSelectedItem!.ValueToInt();
        }
        public int GetMonthFrom()
        {
            return MonthToInt(MonthsFromSelectedItem!.Value!);
        }
        public int GetDateFrom()
        {
            return DatesFromSelectedItem!.ValueToInt();
        }
        public int GetYearTo()
        {
            return YearsToSelectedItem!.ValueToInt();
        }
        public int GetMonthTo()
        {
            return MonthToInt(MonthsToSelectedItem!.Value!);
        }
        public int GetDateTo()
        {
            return DatesToSelectedItem!.ValueToInt();
        }
        public bool AllDatesFromAndToSelected()
        {
            return YearsFromSelectedItem!.IsOk && MonthsFromSelectedItem!.IsOk && DatesFromSelectedItem!.IsOk &&
                   YearsToSelectedItem!.IsOk && MonthsToSelectedItem!.IsOk && DatesToSelectedItem!.IsOk;
        }
        public bool AllDatesToSelected()
        {
            return YearsToSelectedItem!.IsOk && MonthsToSelectedItem!.IsOk && DatesToSelectedItem!.IsOk;
        }
        public bool YearFromEqualsYearTo()
        {
            return YearsFromSelectedItem!.Value == YearsToSelectedItem!.Value;
        }
        public bool MonthFromEqualsMonthTo()
        {
            return YearsFromSelectedItem!.Value == YearsToSelectedItem!.Value &&
                   MonthsFromSelectedItem!.Value == MonthsToSelectedItem!.Value;
        }
        public bool YearToOrMonthToNotSelected()
        {
            return !YearsToSelectedItem!.IsOk || !MonthsToSelectedItem!.IsOk;
        }
        public bool YearFromOrMonthFromNotSelected()
        {
            return !YearsFromSelectedItem!.IsOk || !MonthsFromSelectedItem!.IsOk;
        }
        public bool YearFromNotSelected()
        {
            return !YearsFromSelectedItem!.IsOk;
        }
        public bool YearToNotSelected()
        {
            return !YearsToSelectedItem!.IsOk;
        }
        public bool AnyDateFromNotSelected()
        {
            return !YearsFromSelectedItem!.IsOk || !MonthsFromSelectedItem!.IsOk || !DatesFromSelectedItem!.IsOk;
        }
        public bool AnyFieldIsEmpty()
        {
            return !Subject!.IsOk || !Teacher!.IsOk || !Auditorium!.IsOk ||
                !StartTimeSelectedItem!.IsOk || !EndTimeSelectedItem!.IsOk ||
                !YearsFromSelectedItem!.IsOk || !MonthsFromSelectedItem!.IsOk || !DatesFromSelectedItem!.IsOk ||
                !YearsToSelectedItem!.IsOk || !MonthsToSelectedItem!.IsOk || !DatesToSelectedItem!.IsOk;
        }
        public bool AnyFieldIsNotEmpty()
        {
            return Subject!.IsOk || Teacher!.IsOk || Auditorium!.IsOk || StartTimeSelectedItem!.IsOk ||
                YearsFromSelectedItem!.IsOk || YearsToSelectedItem!.IsOk || CopyDays1SelectedItem!.IsOk;
        }
        public bool AllFieldsSelectedAndPeriodNotSelected()
        {
            return Subject!.IsOk && Teacher!.IsOk && Auditorium!.IsOk &&
                    StartTimeSelectedItem!.IsOk && EndTimeSelectedItem!.IsOk &&
                    YearsFromSelectedItem!.IsOk && MonthsFromSelectedItem!.IsOk && DatesFromSelectedItem!.IsOk &&
                    !YearsToSelectedItem!.IsOk && !MonthsToSelectedItem!.IsOk && !DatesToSelectedItem!.IsOk;
        }
        public bool AllFieldsExceptPeriodSelected()
        {
            return Subject!.IsOk && Teacher!.IsOk && Auditorium!.IsOk &&
                    StartTimeSelectedItem!.IsOk && EndTimeSelectedItem!.IsOk &&
                    YearsFromSelectedItem!.IsOk && MonthsFromSelectedItem!.IsOk && DatesFromSelectedItem!.IsOk;
        }
        public bool IsPeriodSelected()
        {
            return YearsToSelectedItem!.IsOk && MonthsToSelectedItem!.IsOk && DatesToSelectedItem!.IsOk;
        }
        public string GetCopyDay1()
        {
            return CopyDays1SelectedItem!.Value!;
        }
        public string GetCopyDay2()
        {
            return CopyDays2SelectedItem!.Value!;
        }
        public int GetCopyDay1Index()
        {
            return CopyDayToIndex(CopyDays1SelectedItem!.Value!);
        }
        public int GetCopyDay2Index()
        {
            return CopyDayToIndex(CopyDays2SelectedItem!.Value!);
        }
        public int GetCopyDay3Index()
        {
            return CopyDayToIndex(CopyDays3SelectedItem!.Value!);
        }
        public (int year, int month, int day) GetFromValues()
        {
            return (GetYearFrom(), GetMonthFrom(), GetDateFrom());
        }
        public (int yearFrom, int monthFrom, int dayFrom, int yearTo, int monthTo, int dayTo, int copy1, int copy2, int copy3) GetFromAndToValues()
        {
            return (GetYearFrom(), GetMonthFrom(), GetDateFrom(),
                    GetYearTo(), GetMonthTo(), GetDateTo(),
                    GetCopyDay1Index(), GetCopyDay2Index(), GetCopyDay3Index());
        }
        public string GetSubject()
        {
            return Subject!.Value;
        }
        public string GetTeacher()
        {
            return Teacher!.Value;
        }
        public string GetAuditorium()
        {
            return Auditorium!.Value;
        }
        public int GetStartTimeIndex()
        {
            return StartTimeSelectedItem!.Index;
        }
        public int GetEndTimeIndex()
        {
            return EndTimeValueToIndex(EndTimeSelectedItem!.Value!);
        }
        public string GetDuration()
        {
            return $"{StartTimeSelectedItem!.Value} - {EndTimeSelectedItem!.Value}";
        }
        public void LoadLessonData(Lesson lesson)
        {
            Subject!.Value = lesson.Subject;
            Teacher!.Value = lesson.Teacher;
            Auditorium!.Value = lesson.Auditorium;
            StartTimeSelectedItem!.Value = lesson.GetStartTime();
            EndTimeSelectedItem!.Value = lesson.GetEndTime();
        }
        public void ClearInputs()
        {
            Subject!.Value = Teacher!.Value = Auditorium!.Value = string.Empty;
        }
        private int MonthToInt(string month)
        {
            return month switch
            {
                "January" => 1,
                "February" => 2,
                "March" => 3,
                "April" => 4,
                "May" => 5,
                "June" => 6,
                "July" => 7,
                "August" => 8,
                "September" => 9,
                "October" => 10,
                "November" => 11,
                "December" => 12,
                _ => -1,
            };
        }
        private int CopyDayToIndex(string dayOfTheWeek)
        {
            return dayOfTheWeek switch
            {
                "Monday" => 0,
                "Tuesday" => 1,
                "Wednesday" => 2,
                "Thursday" => 3,
                "Friday" => 4,
                "Saturday" => 5,
                "Sunday" => 6,
                _ => -1,
            };
        }
        private int EndTimeValueToIndex(string selectedValue)
        {
            return selectedValue switch
            {
                "08:00" => 0,
                "08:10" => 1,
                "08:20" => 2,
                "08:30" => 3,
                "08:40" => 4,
                "08:50" => 5,
                "09:00" => 6,
                "09:10" => 7,
                "09:20" => 8,
                "09:30" => 9,
                "09:40" => 10,
                "09:50" => 11,
                "10:00" => 12,
                "10:10" => 13,
                "10:20" => 14,
                "10:30" => 15,
                "10:40" => 16,
                "10:50" => 17,
                "11:00" => 18,
                "11:10" => 19,
                "11:20" => 20,
                "11:30" => 21,
                "11:40" => 22,
                "11:50" => 23,
                "12:00" => 24,
                "12:10" => 25,
                "12:20" => 26,
                "12:30" => 27,
                "12:40" => 28,
                "12:50" => 29,
                "13:00" => 30,
                "13:10" => 31,
                "13:20" => 32,
                "13:30" => 33,
                "13:40" => 34,
                "13:50" => 35,
                "14:00" => 36,
                "14:10" => 37,
                "14:20" => 38,
                "14:30" => 39,
                "14:40" => 40,
                "14:50" => 41,
                "15:00" => 42,
                "15:10" => 43,
                "15:20" => 44,
                "15:30" => 45,
                "15:40" => 46,
                "15:50" => 47,
                "16:00" => 48,
                "16:10" => 49,
                "16:20" => 50,
                "16:30" => 51,
                "16:40" => 52,
                "16:50" => 53,
                "17:00" => 54,
                "17:10" => 55,
                "17:20" => 56,
                "17:30" => 57,
                "17:40" => 58,
                "17:50" => 59,
                "18:00" => 60,
                "18:10" => 61,
                "18:20" => 62,
                "18:30" => 63,
                "18:40" => 64,
                "18:50" => 65,
                "19:00" => 66,
                "19:10" => 67,
                "19:20" => 68,
                "19:30" => 69,
                "19:40" => 70,
                "19:50" => 71,
                "20:00" => 72,
                _ => -1,
            };
        }
    }
}
