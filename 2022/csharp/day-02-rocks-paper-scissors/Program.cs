string[] lines = System.IO.File.ReadAllLines(@"./input.txt");

// Console.WriteLine($"Your score is: {OriginalAttempt(lines)}");
Console.WriteLine($"Your score is: {SecondAttempt(lines)}");

int SecondAttempt(string[] lines)
{
    Dictionary<string, int> lookup = new();

    lookup.Add("A X", 3);
    lookup.Add("A Y", 4);
    lookup.Add("A Z", 8);
    lookup.Add("B X", 1);
    lookup.Add("B Y", 5);
    lookup.Add("B Z", 9);
    lookup.Add("C X", 2);
    lookup.Add("C Y", 6);
    lookup.Add("C Z", 7);

    int score = 0;

    foreach (string line in lines)
    {
        score += lookup[line];
    }

    return score;
}

int OriginalAttempt(string[] lines)
{
    int score = 0;

    foreach (string line in lines)
    {
        (string hand1, string outcome) = line.Split(" ");

        score += hand1 switch
        {
            "A" => outcome switch
            {
                "X" => 3,
                "Y" => 4,
                "Z" => 8,
                _ => 0,
            },
            "B" => outcome switch
            {
                "X" => 1,
                "Y" => 5,
                "Z" => 9,
                _ => 0,
            },
            "C" => outcome switch
            {
                "X" => 2,
                "Y" => 6,
                "Z" => 7,
                _ => 0,
            },
            _ => 0,
        };
    }

    return score;
}




public static class Extensions
{
    public static void Deconstruct(this string[] array, out string string1, out string string2)
    {
        string1 = array[0];
        string2 = array[1];
    }

    // Was starting to build dictionary with tuple of (string hand, string outcome)
    // Not needed, don't know what I was thinking. Of course I can just add
    // the line string "A Z"
    public static void Deconstruct(this string[] array, out (string, string) match)
    {
        match = (array[0], array[1]);
    }
}
