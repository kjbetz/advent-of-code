string[] lines = File.ReadAllLines("./input-test.txt");
string[] alreadyVisited = lines;

Console.WriteLine($"alreadyVisited[0][0]: {alreadyVisited[0][0]}, lines[0][0]: {lines[0][0]}");

Position? startingPosition = null;
Position? endPosition = null;
int steps = 0;
Stack<Position> currentTrack = new();
List<Stack<Position>> successfulTracks = new();

for (int i = 0; i < lines.Length; ++i)
{
    for (int j = 0; j < lines[0].Length; ++j)
    {
        if (lines[i][j] == 'S')
        {
            startingPosition = new Position(i, j);
        }
        else if (lines[i][j] == 'E')
        {
            endPosition = new Position(i, j);
        }
    }
}

Console.WriteLine($"Starting position: ({startingPosition?.X}, {startingPosition?.Y})");
Console.WriteLine($"End positiong: ({endPosition?.X}, {endPosition?.Y})");

if (startingPosition != null && endPosition != null)
{
    Hike(startingPosition, endPosition);
}

int Hike(Position currentPosition, Position endPosition)
{
    // Off the map
    if (currentPosition.Y < 0 
            || currentPosition.Y > lines.Length 
            || currentPosition.X < 0 
            || currentPosition.X > lines[0].Length)
    {
        return 0;
    }

    return 0;
}

record Position(int Y, int X);
