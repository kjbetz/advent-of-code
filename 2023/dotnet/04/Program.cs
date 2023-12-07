string[] lines = File.ReadAllLines("input.txt");
List<int> points = new();

foreach (string line in lines)
{
    string[] game = line.Split(':');
    string[] numbers = game[1].Trim().Split('|');
    string[] winningNumbers = numbers[0].Trim().Split(' ');
    string[] yourNumbers = numbers[1].Trim().Split(' ');

    int[] winningNumbersInt = winningNumbers
        .Where(x => x.Trim().Length > 0)
        .Select(x => int.Parse(x.ToString().Trim()))
        .ToArray();
    int[] yourNumbersInt = yourNumbers
        .Where(x => x.Trim().Length > 0)
        .Select(x => int.Parse(x.ToString().Trim()))
        .ToArray();

    int matches = winningNumbersInt.Intersect(yourNumbersInt).Count();

    int pointsForGame = 0;
    string math = $"{pointsForGame}";

    for (int i = 0; i < matches; i++)
    {
        pointsForGame += pointsForGame == 0 ? 1 : pointsForGame;
    }

    if (pointsForGame > 0)
    {
        points.Add(pointsForGame);
    }
}

Console.WriteLine($"Part 1 points: {points.Sum()}");

// Part 2
Dictionary<int, Game> games = new();

foreach (string line in lines)
{
    string[] game = line.Split(':');
    string[] gameName = game[0].Trim().Split();
    int gameId = int.Parse(gameName[gameName.Length - 1].Trim());

    string[] numbers = game[1].Trim().Split('|');
    string[] winningNumbers = numbers[0].Trim().Split();
    string[] yourNumbers = numbers[1].Trim().Split();

    int[] winningNumbersInt = winningNumbers
        .Where(x => x.Trim().Length > 0)
        .Select(x => int.Parse(x.ToString().Trim()))
        .ToArray();

    int[] yourNumbersInt = yourNumbers
        .Where(x => x.Trim().Length > 0)
        .Select(x => int.Parse(x.ToString().Trim()))
        .ToArray();

    int matches = winningNumbersInt.Intersect(yourNumbersInt).Count();

    games.Add(
        gameId,
        new Game
        {
            Id = gameId,
            WinningNumbers = winningNumbersInt,
            YourNumbers = yourNumbersInt,
            Matches = matches
        }
    );
}

foreach (Game scratchcard in games.Values)
{
    if (scratchcard.Matches > 0)
    {
        for (int i = 0; i < scratchcard.Matches; i++)
        {
            games[scratchcard.Id + 1 + i].Count += 1 * scratchcard.Count;
        }
    }
}

Console.WriteLine($"Part 2 total scratchcards: {games.Values.Sum(g => g.Count)}");

public class Game
{
    public int Id { get; set; }
    public int[] WinningNumbers { get; set; }
    public int[] YourNumbers { get; set; }
    public int Matches { get; set; }
    public int Count { get; set; } = 1;
}
