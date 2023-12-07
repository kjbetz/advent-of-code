string[] lines = File.ReadAllLines(@"input.txt");
List<int> partNumbers = new();

HashSet<Position> positions = new();
Dictionary<Position, Gears> gears = new();

for (int y = 0; y < lines.Length; y++)
{
    string tempNumber = string.Empty;
    bool isValidPartNumber = false;

    for (int x = 0; x < lines[y].Length; x++)
    {
        char c = lines[y][x];

        if (char.IsDigit(c))
        {
            tempNumber += c;

            // Check position above
            if (y > 0)
            {
                if (!char.IsDigit(lines[y - 1][x]) && lines[y - 1][x] != '.')
                {
                    isValidPartNumber = true;
                }
            }

            // Check position below
            if (y < lines.Length - 1)
            {
                if (!char.IsDigit(lines[y + 1][x]) && lines[y + 1][x] != '.')
                {
                    isValidPartNumber = true;
                }
            }

            // Check position to the left
            if (x > 0)
            {
                if (!char.IsDigit(lines[y][x - 1]) && lines[y][x - 1] != '.')
                {
                    isValidPartNumber = true;
                }
            }

            // Check position to the right
            if (x < lines[y].Length - 1)
            {
                if (!char.IsDigit(lines[y][x + 1]) && lines[y][x + 1] != '.')
                {
                    isValidPartNumber = true;
                }
            }

            // Check position above and to the left
            if (y > 0 && x > 0)
            {
                if (!char.IsDigit(lines[y - 1][x - 1]) && lines[y - 1][x - 1] != '.')
                {
                    isValidPartNumber = true;
                }
            }

            // Check position above and to the right
            if (y > 0 && x < lines[y].Length - 1)
            {
                if (!char.IsDigit(lines[y - 1][x + 1]) && lines[y - 1][x + 1] != '.')
                {
                    isValidPartNumber = true;
                }
            }

            // Check position below and to the left
            if (y < lines.Length - 1 && x > 0)
            {
                if (!char.IsDigit(lines[y + 1][x - 1]) && lines[y + 1][x - 1] != '.')
                {
                    isValidPartNumber = true;
                }
            }

            // Check position below and to the right
            if (y < lines.Length - 1 && x < lines[y].Length - 1)
            {
                if (!char.IsDigit(lines[y + 1][x + 1]) && lines[y + 1][x + 1] != '.')
                {
                    isValidPartNumber = true;
                }
            }
        }
        else
        {
            if (tempNumber != string.Empty && isValidPartNumber)
            {
                partNumbers.Add(int.Parse(tempNumber));
            }

            tempNumber = string.Empty;
            isValidPartNumber = false;
        }
    }

    if (tempNumber != string.Empty && isValidPartNumber)
    {
        partNumbers.Add(int.Parse(tempNumber));
    }
}

/*
foreach (int partNumber in partNumbers)
{
    Console.WriteLine(partNumber);
}
*/

// Console.WriteLine($"Number of part numbers: {partNumbers.Count}");
Console.WriteLine($"Sum of part numbers: {partNumbers.Sum()}");

// Part 2

for (int y = 0; y < lines.Length; y++)
{
    string tempNumber = string.Empty;
    bool isGear = false;

    for (int x = 0; x < lines[y].Length; x++)
    {
        char c = lines[y][x];

        if (char.IsDigit(c))
        {
            tempNumber += c;

            // Check position above
            if (y > 0)
            {
                if (lines[y - 1][x] == '*')
                {
                    isGear = true;
                    if (!positions.Contains(new Position(x, y - 1)))
                    {
                        positions.Add(new Position(x, y - 1));
                    }
                }
            }

            // Check position below
            if (y < lines.Length - 1)
            {
                if (lines[y + 1][x] == '*')
                {
                    isGear = true;
                    if (!positions.Contains(new Position(x, y + 1)))
                    {
                        positions.Add(new Position(x, y + 1));
                    }
                }
            }

            // Check position to the left
            if (x > 0)
            {
                if (lines[y][x - 1] == '*')
                {
                    isGear = true;
                    if (!positions.Contains(new Position(x - 1, y)))
                    {
                        positions.Add(new Position(x - 1, y));
                    }
                }
            }

            // Check position to the right
            if (x < lines[y].Length - 1)
            {
                if (lines[y][x + 1] == '*')
                {
                    isGear = true;
                    if (!positions.Contains(new Position(x + 1, y)))
                    {
                        positions.Add(new Position(x + 1, y));
                    }
                }
            }

            // Check position above and to the left
            if (y > 0 && x > 0)
            {
                if (lines[y - 1][x - 1] == '*')
                {
                    isGear = true;
                    if (!positions.Contains(new Position(x - 1, y - 1)))
                    {
                        positions.Add(new Position(x - 1, y - 1));
                    }
                }
            }

            // Check position above and to the right
            if (y > 0 && x < lines[y].Length - 1)
            {
                if (lines[y - 1][x + 1] == '*')
                {
                    isGear = true;
                    if (!positions.Contains(new Position(x + 1, y - 1)))
                    {
                        positions.Add(new Position(x + 1, y - 1));
                    }
                }
            }

            // Check position below and to the left
            if (y < lines.Length - 1 && x > 0)
            {
                if (lines[y + 1][x - 1] == '*')
                {
                    isGear = true;
                    if (!positions.Contains(new Position(x - 1, y + 1)))
                    {
                        positions.Add(new Position(x - 1, y + 1));
                    }
                }
            }

            // Check position below and to the right
            if (y < lines.Length - 1 && x < lines[y].Length - 1)
            {
                if (lines[y + 1][x + 1] == '*')
                {
                    isGear = true;
                    if (!positions.Contains(new Position(x + 1, y + 1)))
                    {
                        positions.Add(new Position(x + 1, y + 1));
                    }
                }
            }
        }
        else
        {
            if (tempNumber != string.Empty && isGear)
            {
                foreach (Position position in positions)
                {
                    if (gears.ContainsKey(position))
                    {
                        gears[position] = new Gears(gears[position].Gear1, int.Parse(tempNumber));
                    }
                    else
                    {
                        gears.Add(position, new Gears(int.Parse(tempNumber), 0));
                    }
                }
            }

            tempNumber = string.Empty;
            isGear = false;
            positions.Clear();
        }
    }

    if (tempNumber != string.Empty && isGear)
    {
        foreach (Position position in positions)
        {
            if (gears.ContainsKey(position))
            {
                gears[position] = new Gears(gears[position].Gear1, int.Parse(tempNumber));
            }
            else
            {
                gears.Add(position, new Gears(int.Parse(tempNumber), 0));
            }
        }
    }
}

List<int> gearRatios = new();

foreach (Gears gear in gears.Values)
{
    gearRatios.Add(gear.Gear1 * gear.Gear2);
}

Console.WriteLine($"Sum of gear ratios: {gearRatios.Sum()}");

public record Position(int X, int Y);

public record Gears(int Gear1, int Gear2);
