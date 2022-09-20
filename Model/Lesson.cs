using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Schedule.Model
{
    public class Lesson
    {
        private readonly string? _subject;
        private readonly string? _teacher;
        private readonly string? _auditorium;
        private readonly string? _time;
        private int PositionColumn;
        private int PositionRow;
        private int RowSpan;

        public Lesson (string subject, string teacher, string auditorium, string time)
        {
            _subject = $"Subject: {subject}";
            _teacher = $"Teacher: {teacher}";
            _auditorium = $"Auditorium: {auditorium}";
            _time = time;
        }

        public void SetPositionColumn(int index)
        {
            PositionColumn = index;
        }
        public void SetPositionRow(int index)
        {
            PositionRow = index;
        }
        public void SetRowSpan(int index)
        {
            RowSpan = index - PositionRow;
        }
        public Card ConvertToCard()  //move to view
        {
            var card = new Card();
            if (RowSpan<7)
            {
                card.Content = $"{_subject}\n{_teacher}";
                card.FontSize = 11; 
            }
            else
            {
                card.Content = $"{_subject}\n{_teacher}\n{_auditorium}\n{_time}";
            }           
            card.HorizontalContentAlignment = HorizontalAlignment.Center;
            card.VerticalContentAlignment = VerticalAlignment.Center;
            Grid.SetColumn(card, PositionColumn);
            Grid.SetRow(card, PositionRow);
            Grid.SetRowSpan(card, RowSpan);
            return card;
        }
    }
}
