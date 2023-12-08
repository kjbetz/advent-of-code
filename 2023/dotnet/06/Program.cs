string[] lines = File.ReadAllLines("input.txt");

int[] times = lines[0]
    .Split(':')[1]
    .Trim()
    .Split()
    .Where(s => s.Trim() != "")
    .Select(n => int.Parse(n.Trim()))
    .ToArray();

int[] distances = lines[1]
    .Split(':')[1]
    .Trim()
    .Split()
    .Where(s => s.Trim() != "")
    .Select(n => int.Parse(n.Trim()))
    .ToArray();

List<int> wins = new();

for (int i = 0; i < times.Length; i++)
{
    int l = 0;
    int r = times[i];

    for (int j = 0; j < times[i]; j++)
    {
        if ((times[i] - j) * j > distances[i])
        {
            l = j;
            break;
        }
    }

    for (int j = times[i]; j > 0; j--)
    {
        if ((times[i] - j) * j > distances[i])
        {
            r = j;
            break;
        }
    }

    wins.Add((r + 1) - l);
}

Console.WriteLine($"Part 1 - Number of wins multiplied: {wins.Aggregate((a, b) => a * b)}");

// Part 2

string[] timeDigits = lines[0]
    .Split(':')[1]
    .Trim()
    .Split()
    .Where(s => s.Trim() != "")
    .ToArray();

string[] distanceDigits = lines[1]
    .Split(':')[1]
    .Trim()
    .Split()
    .Where(s => s.Trim() != "")
    .ToArray();

long time = long.Parse(string.Join("", timeDigits));
long distance = long.Parse(string.Join("", distanceDigits));

long l2 = 0;
long r2 = time;

for (long i = 0; i < time; i++)
{
    if ((time - i) * i > distance)
    {
        l2 = i;
        break;
    }
}

for (long i = time; i > 0; i--)
{
    if ((time - i) * i > distance)
    {
        r2 = i;
        break;
    }
}

Console.WriteLine($"Part 2 - Number of wins: {(r2 + 1) - l2}");
