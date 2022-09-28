﻿using System.Windows.Controls;

namespace Schedule.Views.MenuItems
{
    public class MyMenuItem : MenuItem
    {
        private int _connectionDayIndex;
        private int _connectionLessonIndex;
        public void SetConnectionIndexes(int dayIndex, int lessonIndex)
        {
            _connectionDayIndex = dayIndex;
            _connectionLessonIndex = lessonIndex;
        }
        public (int dayIndex, int lessonIndex) GetConnectionIndexes()
        {
            return (_connectionDayIndex, _connectionLessonIndex);
        }
    }
}
