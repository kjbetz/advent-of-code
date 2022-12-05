string[] lines = System.IO.File.ReadAllLines(@"./input.txt");

Console.WriteLine($"Part 1 Value: {Part01(lines)}");
Console.WriteLine($"Part 2 Value: {Part02(lines)}");

int Part01(string[] lines)
{
    int sum = 0;

    foreach (string line in lines)
    {
        int mid = line.Length / 2;
        HashSet<char> sack1 = new();

        for (int i = 0; i < mid; i++)
        {
            sack1.Add(line[i]);
        }

        for (int i = mid; i < line.Length; i++)
        {
            if (sack1.Contains(line[i]))
            {
                sum += (int)line[i] > 90 ? (int)line[i] - 96 : (int)line[i] - 38;
                break;
            }
        }
    }

    return sum;
}

int Part02(string[] lines)
{
    int sum = 0;
    int sack = 1;
    HashSet<char> sack1 = new();
    HashSet<char> sack2 = new();

    for (int i = 0; i < lines.Length; i++)
    {
        if (sack == 1)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                sack1.Add(lines[i][j]);
            }

            sack = 2;
        }
        else if (sack == 2)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                sack2.Add(lines[i][j]);
            }

            sack = 3;
        }
        else
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                if (sack1.Contains(lines[i][j]) && sack2.Contains(lines[i][j]))
                {
                    sum += (int)lines[i][j] > 90 ? (int)lines[i][j] - 96 : (int)lines[i][j] - 38;
                    break;
                }
            }

            sack = 1;
            sack1.Clear();
            sack2.Clear();
        }
    }

    return sum;
}

int Part02Refactored(string[] lines)
{
    int sum = 0;

    HashSet<char> sack1 = new();
    HashSet<char> sack2 = new();

    return sum;
}
