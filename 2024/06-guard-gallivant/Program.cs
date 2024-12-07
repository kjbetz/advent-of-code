string[] map = File.ReadAllLines("input.txt");

Position startingPosition = new(0, 0);
HashSet<Position> obstructionPositions = new();

for (int i = 0; i < map.Length; i++)
for (int j = 0; j < map[i].Length; j++)
    if (map[i][j] == '^')
        startingPosition = new(i, j);

for (int y = 0; y < map.Length; y++)
{
    for (int x = 0; x < map[y].Length; x++)
    {
        Position currentObstruction = new(x, y);
        HashSet<Position> visitedPositions = new();
        Position currentPosition = startingPosition with { };

        while (
            currentPosition.X >= 0
            && currentPosition.X < map.Length
            && currentPosition.Y >= 0
            && currentPosition.Y < map[0].Length
        )
        {
            if (
                map[currentPosition.X][currentPosition.Y] == '#'
                || (
                    currentPosition.X == currentObstruction.X
                    && currentPosition.Y == currentObstruction.Y
                )
            )
                currentPosition = currentPosition.Direction switch
                {
                    Direction.North
                        => new(currentPosition.X + 1, currentPosition.Y, Direction.East),
                    Direction.East
                        => new(currentPosition.X, currentPosition.Y - 1, Direction.South),
                    Direction.South
                        => new(currentPosition.X - 1, currentPosition.Y, Direction.West),
                    Direction.West
                        => new(currentPosition.X, currentPosition.Y + 1, Direction.North),
                    _ => throw new InvalidOperationException()
                };

            if (visitedPositions.Contains(currentPosition))
            {
                obstructionPositions.Add(new Position(x, y));
                break;
            }

            visitedPositions.Add(currentPosition);

           currentPosition = currentPosition.Direction switch
            {
                Direction.North => new(currentPosition.X - 1, currentPosition.Y, Direction.North),
                Direction.East => new(currentPosition.X, currentPosition.Y + 1, Direction.East),
                Direction.South => new(currentPosition.X + 1, currentPosition.Y, Direction.South),
                Direction.West => new(currentPosition.X, currentPosition.Y - 1, Direction.West),
                _ => throw new InvalidOperationException()
            };
        }
    }
}

Console.WriteLine(obstructionPositions.Count);

record Position(int X, int Y, Direction Direction = Direction.North);

enum Direction
{
    North,
    East,
    South,
    West
}
