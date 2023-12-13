using Ardalis.SmartEnum;

string[] lines = System.IO.File.ReadAllLines(@"./input.txt");

// Console.WriteLine($"Your score is: {OriginalAttempt(lines)}");
Console.WriteLine($"Your score is: {SecondAttempt(lines)}");
Console.WriteLine($"Your score is: {ThirdAttempt(lines)}");

int ThirdAttempt(string[] lines)
{
    foreach (string line in lines)
    {
        (string hand, string outcome) = line.Split(" ");
        Console.WriteLine($"hand: {hand}, outcome: {outcome}");
    }

    return 0;
}

int SecondAttempt(string[] lines)
{
    Dictionary<string, int> lookup =
        new()
        {
            { "A X", 3 },
            { "A Y", 4 },
            { "A Z", 8 },
            { "B X", 1 },
            { "B Y", 5 },
            { "B Z", 9 },
            { "C X", 2 },
            { "C Y", 6 },
            { "C Z", 7 }
        };

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
            "A"
                => outcome switch
                {
                    "X" => 3,
                    "Y" => 4,
                    "Z" => 8,
                    _ => 0,
                },
            "B"
                => outcome switch
                {
                    "X" => 1,
                    "Y" => 5,
                    "Z" => 9,
                    _ => 0,
                },
            "C"
                => outcome switch
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

abstract class Hand : SmartEnum<Hand>
{
    public static readonly Hand Rock = new RockHand();
    public static readonly Hand Paper = new PaperHand();
    public static readonly Hand Scissors = new ScissorsHand();

    private Hand(string name, int value)
        : base(name, value) { }

    public abstract bool CanBeat(Hand next);

    private sealed class RockHand : Hand
    {
        public RockHand()
            : base("Rock", 1) { }

        public override bool CanBeat(Hand next) => next == Hand.Scissors;
    }

    private sealed class PaperHand : Hand
    {
        public PaperHand()
            : base("Paper", 2) { }

        public override bool CanBeat(Hand next) => next == Hand.Rock;
    }

    private sealed class ScissorsHand : Hand
    {
        public ScissorsHand()
            : base("Scissors", 3) { }

        public override bool CanBeat(Hand next) => next == Hand.Paper;
    }
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
