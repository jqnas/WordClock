namespace WordClock
{
    public class WordClockFormatter
    {
        // Eigenschaften
        // Generale Einstellungen
        public bool UseAMPM { get; set; } = true;

        // Abstände
        public int HorizontalSpacing { get; set; } = 3;
        public int VerticalSpacing { get; set; } = 1;

        // Farben
        public ConsoleColor ActiveColor { get; set; } = ConsoleColor.White;
        public ConsoleColor InActiveColor { get; set; } = ConsoleColor.Gray;
    }
}
