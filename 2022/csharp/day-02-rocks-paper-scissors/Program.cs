string[] lines = System.IO.File.ReadAllLines(@"./input.txt");

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

Console.WriteLine($"Your score is: {score}");

public static class Extensions
{
    public static void Deconstruct(this string[] array, out string string1, out string string2)
    {
        string1 = array[0];
        string2 = array[1];
    }
}
