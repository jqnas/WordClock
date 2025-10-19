using System.Diagnostics.CodeAnalysis;

namespace WordClock
{
    public static class WordClockGrids
    {
        // Offset für die Stundenindizes im Positionsarray
        public const int HourIndexOffset = 12;

        // Dictionary, das die Grids für verschiedene Sprachen/Varianten enthält
        private static readonly Dictionary<string, WordClockLayout> Layouts = new()
        {
            // Deutsches Wortuhr-Layout
            {
                "de",
                new(new char[10, 11]
                {
                    //        0    1    2    3    4    5    6    7    8    9    10
                    /* 0*/ { 'E', 'S', 'K', 'I', 'S', 'T', 'A', 'F', 'Ü', 'N', 'F' },
                    /* 1*/ { 'Z', 'E', 'H', 'N', 'Z', 'W', 'A', 'N', 'Z', 'I', 'G' },
                    /* 2*/ { 'D', 'R', 'E', 'I', 'V', 'I', 'E', 'R', 'T', 'E', 'L' },
                    /* 3*/ { 'V', 'O', 'R', 'F', 'U', 'N', 'K', 'N', 'A', 'C', 'H' },
                    /* 4*/ { 'H', 'A', 'L', 'B', 'A', 'E', 'L', 'F', 'Ü', 'N', 'F' },
                    /* 5*/ { 'E', 'I', 'N', 'S', 'X', 'A', 'M', 'Z', 'W', 'E', 'I' },
                    /* 6*/ { 'D', 'R', 'E', 'I', 'P', 'M', 'J', 'V', 'I', 'E', 'R' },
                    /* 7*/ { 'S', 'E', 'C', 'H', 'S', 'N', 'L', 'A', 'C', 'H', 'T' },
                    /* 8*/ { 'S', 'I', 'E', 'B', 'E', 'N', 'Z', 'W', 'Ö', 'L', 'F' },
                    /* 9*/ { 'Z', 'E', 'H', 'N', 'E', 'U', 'N', 'K', 'U', 'H', 'R' },
                },
                [
                    (0, 0, 2), // 0   ES
                    (0, 3, 3), // 1   IST
                    (0, 0, 0), // 2   AM (nicht vorhanden in Deutsch)
                    (0, 0, 0), // 3   PM (nicht vorhanden in Deutsch)
                    (0, 7, 4), // 4   FÜNF (Minuten)
                    (1, 0, 4), // 5   ZEHN (Minuten)
                    (1, 4, 7), // 6   ZWANZIG (Minuten)
                    (2, 0, 4), // 7   DREI
                    (2, 4, 7), // 8   VIERTEL
                    (3, 0, 3), // 9   VOR
                    (3, 7, 4), // 10  NACH
                    (4, 0, 4), // 11  HALB
                    (8, 6, 5), // 12  ZWÖLF (Stunden) -> Mitternacht
                    (5, 0, 4), // 13  EINS (Stunden)
                    (5, 7, 4), // 14  ZWEI (Stunden)
                    (6, 0, 4), // 15  DREI (Stunden)
                    (6, 7, 4), // 16  VIER (Stunden)
                    (4, 7, 4), // 17  FÜNF (Stunden)
                    (7, 0, 5), // 18  SECHS (Stunden)
                    (8, 0, 6), // 19  SIEBEN (Stunden)
                    (7, 7, 4), // 20  ACHT (Stunden)
                    (9, 3, 4), // 21  NEUN (Stunden)
                    (9, 0, 4), // 22  ZEHN (Stunden)
                    (4, 5, 3), // 23  ELF (Stunden)
                    (8, 6, 5), // 24  ZWÖLF (Stunden)
                    (9, 8, 3), // 25  UHR
                ]
                )
            },
            // Englishes Wortuhr-Layout
            {
                "en",
                new(new char[10, 11]
                {
                    //        0    1    2    3    4    5    6    7    8    9    10
                    /* 0*/ { 'I', 'T', 'L', 'I', 'S', 'A', 'S', 'A', 'M', 'P', 'M' },
                    /* 1*/ { 'A', 'C', 'Q', 'U', 'A', 'R', 'T', 'E', 'R', 'D', 'C' },
                    /* 2*/ { 'T', 'W', 'E', 'N', 'T', 'Y', 'F', 'I', 'V', 'E', 'X' },
                    /* 3*/ { 'H', 'A', 'L', 'F', 'S', 'T', 'E', 'N', 'F', 'T', 'O' },
                    /* 4*/ { 'P', 'A', 'S', 'T', 'E', 'R', 'U', 'N', 'I', 'N', 'E' },
                    /* 5*/ { 'O', 'N', 'E', 'S', 'I', 'X', 'T', 'H', 'R', 'E', 'E' },
                    /* 6*/ { 'F', 'O', 'U', 'R', 'F', 'I', 'V', 'E', 'T', 'W', 'O' },
                    /* 7*/ { 'E', 'I', 'G', 'H', 'T', 'E', 'L', 'E', 'V', 'E', 'N' },
                    /* 8*/ { 'S', 'E', 'V', 'E', 'N', 'T', 'W', 'E', 'L', 'W', 'E' },
                    /* 9*/ { 'T', 'E', 'N', 'S', 'E', 'O', 'C', 'L', 'O', 'C', 'K' },
                },
                [
                    (0, 0, 2), // 0   IT
                    (0, 3, 2), // 1   IS
                    (0, 7, 2), // 2   AM
                    (0, 9, 2), // 3   PM
                    (2, 6, 4), // 4   FIVE (Minutes)
                    (3, 5, 3), // 5   TEN (Minutes)
                    (2, 0, 6), // 6   TWENTY (Minutes)
                        (2, 0, 4), // 7   DREI
                    (1, 2, 7), // 8   QUARTER
                    (3, 9, 2), // 9   TO
                    (4, 0, 4), // 10  PAST
                    (3, 0, 4), // 11  HALF
                    (8, 5, 6), // 12  TWELVE (Hour) -> Midnight
                    (5, 0, 3), // 13  ONE (Hour)
                    (6, 8, 3), // 14  TWO (Hour)
                    (5, 6, 5), // 15  THREE (Hour)
                    (6, 0, 4), // 16  FOUR (Hour)
                    (6, 4, 4), // 17  FIVE (Hour)
                    (5, 3, 3), // 18  SIX (Hour)
                    (8, 0, 5), // 19  SEVEN (Hour)
                    (7, 0, 5), // 20  EIGHT (Hour)
                    (4, 7, 4), // 21  NINE (Hour)
                    (9, 0, 3), // 22  TEN (Hour)
                    (7, 5, 6), // 23  ELEVEN (Hour)
                    (8, 5, 6), // 24  TWELVE (Hour)
                    (9, 5, 6), // 25  OCLOCK
                ]
                )
            },
        };


        // Versucht, das Grid für die angegebene Kultur zu finden
        public static bool TryGetGridForCulture(string cultureName, [NotNullWhen(true)] out WordClockLayout layout)
        {
            // Versucht, das Grid aus dem Dictionary zu holen
            if (!Layouts.TryGetValue(cultureName, out layout))
            {
                return false;
            }

            return true;
        }

        // Überprüft, ob die angegebene Kultur unterstützt wird
        public static bool IsCultureSupported(string cultureName)
        {
            return Layouts.ContainsKey(cultureName);
        }

        // Gibt eine Liste der unterstützten Kulturen zurück
        public static IEnumerable<string> GetSupportedCultures()
        {
            return Layouts.Keys;
        }


        // Struktur zur Darstellung des Wortuhr-Layouts
        public struct WordClockLayout
        {
            // Eigenschaften
            // Das Grid der Wortuhr
            public char[,] Grid { get; init; }

            // Die Positionen der Wörter im Grid
            public (int row, int col, int length)[] Positions { get; init; }

            // Konstruktor
            public WordClockLayout(char[,] grid, (int row, int col, int length)[] positions)
            {
                Grid = grid;
                Positions = positions;
            }
        }
    }
}
