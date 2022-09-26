using MaterialDesignThemes.Wpf;

namespace Schedule.View
{
    public class MyCard : Card
    {
        private int _dayIndex;
        private int _lessonIndex;
        public void SetConnectionIndexes(int dayIndex, int lessonIndex)
        {
            _dayIndex=dayIndex;
            _lessonIndex=lessonIndex;
        }
        public (int dayIndex, int lessonIndex) GetConnectionIndexes()
        {
            return (_dayIndex, _lessonIndex);
        }
    }
}
