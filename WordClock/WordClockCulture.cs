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
            if (!WordClockGrids.ContainsLayoutForCulture(culture))
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
            if (!WordClockGrids.TryGetLayoutForCulture(Culture, out var layout))
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
            if (!WordClockGrids.ContainsLayoutForCulture(cultureName))
            {
                // Wirft eine Fehler, wenn die Kultur nicht gefunden wird
                throw new CultureNotFoundException($"Culture '{cultureName}' not found.");
            }

            return new WordClockCulture(cultureName);
        }

        // Überprüft, ob die angegebene Kultur unterstützt wird
        public static bool IsCultureSupported(string cultureName) => WordClockGrids.ContainsLayoutForCulture(cultureName);

        // Gibt eine Liste der unterstützten Kulturen zurück
        public static IEnumerable<string> GetSupportedCultures() => WordClockGrids.GetSupportedLayouts();
    }
}
