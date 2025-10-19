# WordClock

A simple word clock application built with C# .NET.

The word clock displays the current time using words instead of numbers. It highlights the relevant words to indicate the time in a visually appealing way.

## Using the WordClock Library

To use the WordClock library in your project, follow these steps:
- Add a reference to the WordClock library in your project.
- Create an instance of the `WordClock` class.
    - Provide the desired culture using the `WordClockCulture.FromCultureName` method or an instance of the `WordClockCulture` class.
    - Provide an instance of the `WordClockFormatter` class to handle the formatting of the time display.
- Use the `GetTimeInWords()` method of the `WordClock` instance to retrieve the given `TimeOnly` instance as a formatted string.
- Optional: Use the `GetActivePositionsGrid()` method of the `WordClock` instance to retrieve a grid indicating which positions are active (`true/false`) for the given `TimeOnly` instance.

### Example Code
```csharp
WordClock wordClock = new(WordClockCulture.FromCultureName("de"), new WordClockFormatter());
````

---

## Formatting

You can customize the formatting of the time display by changing the properties of your `WordClockFormatter` instance.

Available Formatting Options:
- `UseAMPM`: Specifies whether to use AM/PM notation (if the language supports it).
- `HorizontalSpacing`: Specifies the number of spaces between characters in the horizontal direction.
- `VerticalSpacing`: Specifies the number of empty lines between characters in the vertical direction.
- `ActiveColor`: Specifies the color used for active characters/words.
- `InactiveColor`: Specifies the color used for inactive characters/words.

---

## Languages

The WordClock Libary supports the different displaying languages defined in the `WordClockGrids.cs` file. Currently supported languages are:
- German (de)
- English (en)

Every language has its own layout defenitions:
- Name of the language/culture (e.g. "en" for English) to reference the layout when using the library
- Character-Grid (typically 10x11, but any size is possible) that contains all the characters/words needed to display the time
- Postion definitions for every word needed to display the time. Each position definition contains:
  - Row index of the first character of the word
  - Column index of the first character of the word
  - Length of the word

### Example for the English layout:
```csharp
"en", // Name
new(new char[10, 11] // Character Grid
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
[ // Position Definitions
    (0, 0, 2), // 0   IT
    (0, 3, 2), // 1   IS
    (0, 7, 2), // 2   AM
    (0, 9, 2), // 3   PM
    (2, 6, 4), // 4   FIVE (Minutes)
    (3, 5, 3), // 5   TEN (Minutes)
    (2, 0, 6), // 6   TWENTY (Minutes)
    (0, 0, 0), // 7   not in English
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
])        
```

---