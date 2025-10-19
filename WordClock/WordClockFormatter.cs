namespace WordClock
{
    public class WordClockFormatter
    {
        // Eigenschaften
        // Abstände
        public int HorizontalSpacing { get; set; } = 3;
        public int VerticalSpacing { get; set; } = 1;

        // Farben
        public ConsoleColor InActiveColor { get; set; } = ConsoleColor.Gray;
        public ConsoleColor ActiveColor { get; set; } = ConsoleColor.White;
    }
}
