using WordClock;

WordClock.WordClock wordClock = new(WordClockCulture.FromCultureName("de-de"), new WordClockFormatter() { Use5ToPastNotation = true, ActiveColor = ConsoleColor.Green });

while (true)
{
    Console.Write("Enter a time (HH:MM) or press Return to use the current time: ");
    string? input = Console.ReadLine();
    Console.Clear();
    TimeOnly time;

    try
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            time = TimeOnly.FromDateTime(DateTime.Now);
        }
        else
        {
            time = TimeOnly.Parse(input!);
        }
    }
    catch { continue; }

    Console.WriteLine("Time: " + time);
    Console.WriteLine(wordClock.GetTimeInWords(time));
}