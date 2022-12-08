string[] lines = File.ReadAllLines("./input.txt");

int treesVisible = (lines[0].Length * 2) + ((lines.Length - 2) * 2);
int highestScenicScore = 0;

for (int y = 1; y < lines.Length - 1; y++)
{
    for (int x = 1; x < lines[0].Length - 1; x++)
    {
        int height = int.Parse(lines[y][x].ToString());

        treesVisible += IsVisible(new Position(y, x), height, Direction.Top)
            || IsVisible(new Position(y, x), height, Direction.Right)
            || IsVisible(new Position(y, x), height, Direction.Bottom)
            || IsVisible(new Position(y, x), height, Direction.Left) ? 1 : 0;
    }
}

for (int y = 0; y < lines.Length; y++)
{
    for (int x = 0; x < lines[0].Length; x++)
    {
        int height = int.Parse(lines[y][x].ToString());
                        
        int scenicScore = TreesVisible(new Position(y, x), height, Direction.Top)
            * TreesVisible(new Position(y, x), height, Direction.Right)
            * TreesVisible(new Position(y, x), height, Direction.Bottom)
            * TreesVisible(new Position(y, x), height, Direction.Left);
           

        highestScenicScore = Math.Max(scenicScore, highestScenicScore);
    }
}

Console.WriteLine($"Trees visible: { treesVisible }");
Console.WriteLine($"Highest scenic score: { highestScenicScore }");

bool IsVisible(Position position, int height, Direction direction, bool currentDetermination = false)
{
    Position newPosition = GetNextPosition(position, direction);

    if (newPosition.y < 0 || newPosition.y >= lines.Length) return currentDetermination;
    if (newPosition.x < 0 || newPosition.x >= lines[0].Length) return currentDetermination;

    if (height <= int.Parse(lines[newPosition.y][newPosition.x].ToString())) return false;
    
    return IsVisible(newPosition, height, direction, true);
}

int TreesVisible(Position position, int height, Direction direction, int currentTreesVisible = 0)
{
    Position newPosition = GetNextPosition(position, direction);

    if (newPosition.y < 0 || newPosition.y >= lines.Length) return currentTreesVisible;
    if (newPosition.x < 0 || newPosition.x >= lines[0].Length) return currentTreesVisible;

    if (height <= int.Parse(lines[newPosition.y][newPosition.x].ToString())) return ++currentTreesVisible;

    return TreesVisible(newPosition, height, direction, ++currentTreesVisible);
}

Position GetNextPosition(Position position, Direction direction) =>
    direction switch
    {
        Direction.Top => new Position(position.y - 1, position.x),
        Direction.Right => new Position(position.y, position.x + 1),
        Direction.Bottom => new Position(position.y + 1, position.x),
        Direction.Left => new Position(position.y, position.x - 1),
        _ => position,
    };

record Position(int y, int x);

enum Direction
{
    Top,
    Right,
    Bottom,
    Left,
}
