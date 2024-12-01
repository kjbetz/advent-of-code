string[] lines = File.ReadAllLines("test1-input.txt");

Coordinate? startingCoordinate = null;

for (int y = 0; y < lines.Length; y++)
{
    for (int x = 0; x < lines[y].Length; x++)
    {
        if (lines[y][x] == 'S')
        {
            startingCoordinate = new Coordinate(x, y);
            break;
        }
    }

    if (startingCoordinate != null)
        break;
}
Dictionary<Coordinate, int> alreadyVisited = new();
Walk(startingCoordinate, alreadyVisited);

Console.WriteLine($"Starting coordinate: ({startingCoordinate?.X}, {startingCoordinate?.Y})");

void Walk(Coordinate currentCoordinate, Dictionary<Coordinate, int> alreadyVisited)
{
    if (
        currentCoordinate.X < 0
        || currentCoordinate.X > lines[0].Length
        || currentCoordinate.Y < 0
        || currentCoordinate.Y > lines.Length
    )
        return;

    
}

record Coordinate(int X, int Y);
