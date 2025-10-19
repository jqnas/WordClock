using System.Globalization;

namespace WordClock
{
    public class WordClockCulture
    {
        // Eigenschaften
        // Sprachinformation
        public string Culture { get; }

        // Konstruktor
        public WordClockCulture(string culture)
        {
            // Überprüft, ob die Kultur existiert
            if (!WordClockGrids.IsCultureSupported(culture))
            {
                // Wirft eine Fehler, wenn die Kultur nicht gefunden wird
                throw new CultureNotFoundException($"Culture '{culture}' not found.");
            }

            // Initialisiert die CultureInfo
            Culture = culture;
        }

        // Methoden
        public WordClockGrids.WordClockLayout GetLayout()
        {
            // Versucht, das Grid für die angegebene Kultur zu holen
            if (!WordClockGrids.TryGetGridForCulture(Culture, out var layout))
            {
                // Wirft eine Fehler, wenn die Kultur nicht unterstützt wird
                throw new NotSupportedException($"Culture '{Culture}' is not supported.");
            }

            return layout;
        }

        // Statische Methoden zur Erstellung von WordClockCulture-Instanzen
        public static WordClockCulture FromCultureName(string cultureName)
        {
            // Überprüft, ob die Kultur existiert
            if (!WordClockGrids.IsCultureSupported(cultureName))
            {
                // Wirft eine Fehler, wenn die Kultur nicht gefunden wird
                throw new CultureNotFoundException($"Culture '{cultureName}' not found.");
            }

            return new WordClockCulture(cultureName);
        }
    }
}
