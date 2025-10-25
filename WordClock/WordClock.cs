using System.Text;

namespace WordClock
{
    public class WordClock
    {
        // Konstanten
        public const string Version = "1.0.0";

        // ANSI Farbcode Konstanten
        private static readonly Dictionary<ConsoleColor, string> ConsoleColors = new()
        {
            { ConsoleColor.Black,           "\x1b[30m"},
            { ConsoleColor.DarkRed,         "\x1b[31m"},
            { ConsoleColor.DarkGreen,       "\x1b[32m"},
            { ConsoleColor.DarkYellow,      "\x1b[33m"},
            { ConsoleColor.DarkBlue,        "\x1b[34m"},
            { ConsoleColor.DarkMagenta,     "\x1b[35m"},
            { ConsoleColor.DarkCyan,        "\x1b[36m"},
            { ConsoleColor.DarkGray,        "\x1b[37m"},
            { ConsoleColor.Gray,            "\x1b[90m"},
            { ConsoleColor.Red,             "\x1b[91m"},
            { ConsoleColor.Green,           "\x1b[92m"},
            { ConsoleColor.Yellow,          "\x1b[93m"},
            { ConsoleColor.Blue,            "\x1b[94m"},
            { ConsoleColor.Magenta,         "\x1b[95m"},
            { ConsoleColor.Cyan,            "\x1b[96m"},
            { ConsoleColor.White,           "\x1b[97m"} ,
        };

        // Eigenschaften
        // Sprachinformationen der Wortuhr
        public WordClockCulture ClockCulture { get; }

        // Formatierer für die Wortuhr
        public WordClockFormatter ClockFormatter { get; set; }

        // Konstruktor
        public WordClock(WordClockCulture clockCulture, WordClockFormatter formatter)
        {
            ClockCulture = clockCulture;
            ClockFormatter = formatter;
        }

        // Methoden
        // Gibt die aktuelle Uhrzeit in Worten zurück
        public string GetTimeInWords(TimeOnly time)
        {
            // Gird der Wortuhr
            WordClockGrids.WordClockLayout layout = ClockCulture.GetLayout();

            // Aktiven Positionen im Grid
            bool[,] activePositions = GetActivePositionsGrid(time, ref layout);

            // Gibt das Grid als String mit mehreren Zeilen zurück
            return BuildStringFromGrid(ref layout, ref activePositions);
        }

        // Bestimmt die aktiven Positionen im Grid basierend auf der aktuellen Zeit
        public bool[,] GetActivePositionsGrid(TimeOnly time, ref WordClockGrids.WordClockLayout layout)
        {
            // Aktiven Positionen im Grid
            bool[,] activePositions = new bool[layout.Grid.GetLength(0), layout.Grid.GetLength(1)];

            // Markiert die aktiven Positionen basierend auf der aktuellen Zeit
            SetActivePositions(ref activePositions, layout.Positions[0]); // "ES"
            SetActivePositions(ref activePositions, layout.Positions[1]); // "IST"

            if (ClockFormatter.UseAMPM)
            {
                // Entsscheidung über vormittag oder nachmittag (AM/PM)
                SetActivePositions(ref activePositions, time.Hour < 12 ? layout.Positions[2] : layout.Positions[3]);
            }

            // Volle Stunden
            if (time.Minute < 5)
            {
                SetActivePositions(ref activePositions, layout.Positions[25]); // "UHR"
                SetActivePositions(ref activePositions, layout.Positions[time.Hour % 12 + WordClockGrids.HourIndexOffset]); // Stunde
            }
            // Sonderfall für "FÜNF VOR HALB"
            else if (ClockFormatter.Use5ToPastNotation && time.Minute >= 25 && time.Minute < 30)
            {
                SetActivePositions(ref activePositions, layout.Positions[4]); // "FÜNF" (Minuten)
                SetActivePositions(ref activePositions, layout.Positions[9]);
                SetActivePositions(ref activePositions, layout.Positions[11]); // "HALB"
                SetActivePositions(ref activePositions, layout.Positions[(time.Hour + 1) % 12 + WordClockGrids.HourIndexOffset]); // Stunde
            }
            // Halbe Stunden
            else if (time.Minute >= 30 && time.Minute < 35)
            {
                SetActivePositions(ref activePositions, layout.Positions[11]); // "HALB"
                SetActivePositions(ref activePositions, layout.Positions[(time.Hour + 1) % 12 + WordClockGrids.HourIndexOffset]); // Stunde
            }
            // Sonderfall für "FÜNF NACH HALB"
            else if (ClockFormatter.Use5ToPastNotation && time.Minute >= 35 && time.Minute < 40)
            {
                SetActivePositions(ref activePositions, layout.Positions[4]); // "FÜNF" (Minuten)
                SetActivePositions(ref activePositions, layout.Positions[10]);
                SetActivePositions(ref activePositions, layout.Positions[11]); // "HALB"
                SetActivePositions(ref activePositions, layout.Positions[(time.Hour + 1) % 12 + WordClockGrids.HourIndexOffset]); // Stunde
            }
            // Sonstige Minutenangaben
            else
            {
                // Bestimmt, ob es vor oder nach der halben Stunde ist
                bool under30 = time.Minute < 30;

                // Entscheidet zwischen "NACH" und "VOR"
                SetActivePositions(ref activePositions, under30 ? layout.Positions[10] : layout.Positions[9]);

                // Markiert die Stundenposition
                SetActivePositions(ref activePositions, layout.Positions[(under30 ? time.Hour % 12 : (time.Hour + 1) % 12) + WordClockGrids.HourIndexOffset]); // Stunde

                // Markiert die Minutenpositionen
                if (time.Minute < 10 || time.Minute > 50)
                {
                    SetActivePositions(ref activePositions, layout.Positions[4]); // "FÜNF" (Minuten)
                }
                else if (time.Minute < 15 || time.Minute > 45)
                {
                    SetActivePositions(ref activePositions, layout.Positions[5]); // "ZEHN" (Minuten)
                }
                else if (time.Minute < 20 || time.Minute > 40)
                {
                    SetActivePositions(ref activePositions, layout.Positions[8]); // "VIERTEL" (Minuten)
                }
                else if(time.Minute < 25 || time.Minute > 35)
                {
                    SetActivePositions(ref activePositions, layout.Positions[6]); // "ZWANZIG" (Minuten)
                }
                else if (time.Minute < 30 || time.Minute > 30)
                {
                    SetActivePositions(ref activePositions, layout.Positions[4]);
                    SetActivePositions(ref activePositions, layout.Positions[6]);
                }
            }

            // Detaillierte Minutenanzeige (Punkte)
            int detailedMinutes = time.Minute % 5;
            for (int i = 0; i < detailedMinutes; i++)
            {
                // Punkte für Minutenanzeige
                SetActivePositions(ref activePositions, layout.Positions[26 + i]);
            }

            return activePositions;
        }

        // Baut einen String aus einem 2D-Char-Array (Grid) der Wortuhr
        private string BuildStringFromGrid(ref WordClockGrids.WordClockLayout layout, ref bool[,] activePositions)
        {
            StringBuilder s = new();

            // Definiert den horizontalen und vertikalen Abstand basierend auf dem Formatter
            string horizontalSpacing = new(' ', ClockFormatter.HorizontalSpacing);
            string verticalSpacing = string.Concat(Enumerable.Repeat(Environment.NewLine, ClockFormatter.VerticalSpacing + 1));

            // Definiert eine Aktion zum Anhängen eines Zeichens mit der entsprechenden Farbe
            Action<char, bool> AppendSection = ClockFormatter.DisableColor ? (c, _) => s.Append(c) : (c, active) =>
            {
                if (active)
                {
                    s.Append(ConsoleColors[ClockFormatter.ActiveColor]);
                }
                else
                {
                    s.Append(ConsoleColors[ClockFormatter.InActiveColor]);
                }
                s.Append(c);
                s.Append("\x1b[0m"); // Reset color
            };


            // Durchläuft alle Zeilen des Grids
            int rows = layout.Grid.GetLength(0);
            for (int row = 0; row < rows; row++)
            {
                // Baut jede Zeile des Grids zusammen
                int cols = layout.Grid.GetLength(1);
                for (int col = 0; col < cols; col++)
                {
                    // Fügt das aktuelle Zeichen hinzu
                    if (activePositions[row, col])
                    {
                        AppendSection(layout.Grid[row, col], true);
                    }
                    else if (!ClockFormatter.DontShowInActivePositions)
                    {
                        AppendSection(layout.Grid[row, col], false);
                    }
                    else
                    {
                        AppendSection(' ', false);
                    }

                    // Fügt horizontalen Abstand hinzu
                    s.Append(horizontalSpacing);
                }

                // Neue Zeile nach jeder Zeile im Grid mit vertikalem Abstand
                s.Append(verticalSpacing);
            }

            // Entfernt den letzten vertikalen Abstand
            s.Remove(s.Length - verticalSpacing.Length, verticalSpacing.Length);

            // Gibt den zusammengebauten String zurück
            return s.ToString();
        }

        // Markiert die aktiven Positionen im Grid basierend auf der angegebenen Position
        private static void SetActivePositions(ref bool[,] activePositions, (int row, int col, int length) position)
        {
            for (int i = 0; i < position.length; i++)
            {
                activePositions[position.row, position.col + i] = true;
            }
        }
    }
}
