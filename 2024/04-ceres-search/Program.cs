string[] lines = File.ReadAllLines("input.txt");

// Part One
int xmasCount = 0;

for (int y = 0; y < lines.Length; y++)
{
    for (int x = 0; x < lines[y].Length; x++)
    {
        if (lines[y][x] == 'X')
        {
            if (SearchForXmas(x, y, Direction.Up))
                xmasCount++;

            if (SearchForXmas(x, y, Direction.UpRight))
                xmasCount++;

            if (SearchForXmas(x, y, Direction.Right))
                xmasCount++;

            if (SearchForXmas(x, y, Direction.DownRight))
                xmasCount++;

            if (SearchForXmas(x, y, Direction.Down))
                xmasCount++;

            if (SearchForXmas(x, y, Direction.DownLeft))
                xmasCount++;

            if (SearchForXmas(x, y, Direction.Left))
                xmasCount++;

            if (SearchForXmas(x, y, Direction.UpLeft))
                xmasCount++;
        }
    }
}

Console.WriteLine(xmasCount);

// Part Two

xmasCount = 0;

for (int y = 0; y < lines.Length; y++)
{
    for (int x = 0; x < lines[y].Length; x++)
    {
        if (lines[y][x] == 'A')
        {
            if (SearchForXmas2(x, y))
                xmasCount++;
        }
    }
}

Console.WriteLine(xmasCount);

bool SearchForXmas(int x, int y, Direction direction, char? lastLetter = null)
{
    if (x < 0 || x >= lines[0].Length || y < 0 || y >= lines.Length)
        return false;

    if (lines[y][x] == 'X' && lastLetter == null)
    {
        (y, x) = NextDirection(x, y, direction);
        return SearchForXmas(x, y, direction, 'X');
    }

    if ((lastLetter == 'X' && lines[y][x] == 'M') || (lastLetter == 'M' && lines[y][x] == 'A'))
    {
        lastLetter = lines[y][x];
        (y, x) = NextDirection(x, y, direction);
        return SearchForXmas(x, y, direction, lastLetter);
    }
    else if (lastLetter == 'A' && lines[y][x] == 'S')
    {
        return true;
    }

    return false;
}

bool SearchForXmas2(int x, int y)
{
    if (
        x + 1 < 0
        || x + 1 >= lines[0].Length
        || x - 1 < 0
        || x - 1 >= lines[0].Length
        || y + 1 < 0
        || y + 1 >= lines.Length
        || y - 1 < 0
        || y - 1 >= lines.Length
    )
        return false;

    if (
        (
            lines[y - 1][x - 1] == 'M'
            && lines[y + 1][x + 1] == 'S'
            && lines[y - 1][x + 1] == 'M'
            && lines[y + 1][x - 1] == 'S'
        )
        || (
            lines[y - 1][x - 1] == 'M'
            && lines[y + 1][x + 1] == 'S'
            && lines[y - 1][x + 1] == 'S'
            && lines[y + 1][x - 1] == 'M'
        )
        || (
            lines[y - 1][x - 1] == 'S'
            && lines[y + 1][x + 1] == 'M'
            && lines[y - 1][x + 1] == 'M'
            && lines[y + 1][x - 1] == 'S'
        )
        || (
            lines[y - 1][x - 1] == 'S'
            && lines[y + 1][x + 1] == 'M'
            && lines[y - 1][x + 1] == 'S'
            && lines[y + 1][x - 1] == 'M'
        )
    )
        return true;

    return false;
}

(int, int) NextDirection(int x, int y, Direction direction) =>
    direction switch
    {
        Direction.Up => (y - 1, x),
        Direction.UpRight => (y - 1, x + 1),
        Direction.Right => (y, x + 1),
        Direction.DownRight => (y + 1, x + 1),
        Direction.Down => (y + 1, x),
        Direction.DownLeft => (y + 1, x - 1),
        Direction.Left => (y, x - 1),
        Direction.UpLeft => (y - 1, x - 1),
        _ => throw new ArgumentException("Invalid direction")
    };

enum Direction
{
    Up,
    UpRight,
    Right,
    DownRight,
    Down,
    DownLeft,
    Left,
    UpLeft
}
