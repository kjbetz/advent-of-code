string[] lines = File.ReadAllLines("input.txt");

Dictionary<string, Coordinate> map = new();
string moves = lines[0];
int movesCount = 0;

for (int i = 2; i < lines.Length; i++)
{
    string[] parts = lines[i].Split(" = ");
    string point = parts[0];
    string[] coordinates = parts[1].Substring(1, parts[1].Length - 2).Split(", ");

    map.Add(point, new Coordinate(coordinates[0], coordinates[1]));
}

// Part 1
string currentPoint = "AAA";
int currentMove = 0;

while (currentPoint != "ZZZ")
{
    Coordinate currentCoordinate = map[currentPoint];

    currentPoint = moves[currentMove] == 'R' ? currentCoordinate.R : currentCoordinate.L;

    currentMove = currentMove == moves.Length - 1 ? 0 : currentMove + 1;

    movesCount++;
}

Console.WriteLine($"Part 1: Number of moves for AAA -> ZZZ: {movesCount}");

// Part 2
List<Point> startingPoints = new();

foreach (KeyValuePair<string, Coordinate> entry in map)
{
    if (entry.Key.Substring(2) == "A")
    {
        startingPoints.Add(new Point(entry.Key));
        Console.WriteLine($"Starting point: {entry.Key}");
    }
}

foreach (Point point in startingPoints)
{
    int currentMove = 0;
    ulong movesCount = 0;
    string currentPoint = point.Start;

    while (currentPoint.Substring(2) != "Z")
    {
        Coordinate currentCoordinate = map[currentPoint];

        currentPoint = moves[currentMove] == 'R' ? currentCoordinate.R : currentCoordinate.L;

        currentMove = currentMove == moves.Length - 1 ? 0 : currentMove + 1;

        movesCount++;
    }

    point.Moves = movesCount;
}

ulong lcm = Lcm2(startingPoints.Select(p => p.Moves).ToArray());

Console.WriteLine($"Part 2: Number of moves for all A's -> Z's: {lcm}");

static ulong Gcd(ulong a, ulong b)
{
    while (b != 0)
    {
        ulong temp = b;
        b = a % b;
        a = temp;
    }

    return a;
}

static ulong Lcm(ulong a, ulong b)
{
    return (a / Gcd(a, b)) * b;
}

static ulong Lcm2(ulong[] numbers)
{
    ulong result = numbers[0];

    for (int i = 1; i < numbers.Length; i++)
    {
        result = Lcm(result, numbers[i]);
    }

    return result;
}

record Coordinate(string L, string R);

class Point
{
    public string Start { get; set; }
    public ulong Moves { get; set; } = 0;
    public List<ulong> Multiples { get; set; } = new();

    public Point(string start)
    {
        Start = start;
    }
}
