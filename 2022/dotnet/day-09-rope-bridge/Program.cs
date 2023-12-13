string[] lines = File.ReadAllLines("./input.txt");

HashSet<string> tailVisits = new();
List<Position> knots = new List<Position>
{
    new Position(0,0),
    new Position(0,0),
    new Position(0,0),
    new Position(0,0),
    new Position(0,0),
    new Position(0,0),
    new Position(0,0),
    new Position(0,0),
    new Position(0,0),
    new Position(0,0)
};

tailVisits.Add("(0,0)");

foreach (string line in lines)
{
    string[] move = line.Split(" ");

    string direction = move[0];
    int moves = int.Parse(move[1]);

    Position directionMove = direction switch
        {
            "R" => new Position(1, 0),
            "U" => new Position(0, 1),
            "L" => new Position(-1, 0),
            "D" => new Position(0, -1),
            _ => new Position(0, 0),
        };

    for (int i = 0; i < moves; i++)
    {
        knots[0].X += directionMove.X;
        knots[0].Y += directionMove.Y;

        for (int j = 1; j < knots.Count; j++)
        {
            bool moved = false;

            if ((Math.Abs(knots[j-1].X - knots[j].X) > 1 && knots[j-1].Y != knots[j].Y)
                || (knots[j-1].X != knots[j].X && Math.Abs(knots[j-1].Y - knots[j].Y) > 1))
            {
                knots[j].X += knots[j-1].X > knots[j].X ? 1 : -1;
                knots[j].Y += knots[j-1].Y > knots[j].Y ? 1 : -1;

                moved = true;
            } else if(Math.Abs(knots[j-1].X - knots[j].X) > 1
                    && knots[j-1].Y == knots[j].Y)
            {
                knots[j].X += knots[j-1].X > knots[j].X ? 1 : -1;

                moved = true;
            } else if(knots[j-1].X == knots[j].X
                    && Math.Abs(knots[j-1].Y - knots[j].Y) > 1)
            {
                knots[j].Y += knots[j-1].Y > knots[j].Y ? 1 : -1;

                moved = true;
            }

            if (moved == true && j == (knots.Count - 1))
            {
                tailVisits.Add($"({knots[j].X},{knots[j].Y})");
            }
        }
    }
}

Console.WriteLine($"Number of tail visits: {tailVisits.Count}");

public class Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }
}
