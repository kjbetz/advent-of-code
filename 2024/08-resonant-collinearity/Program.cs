string[] map = File.ReadAllLines("input.txt");

Dictionary<char, List<Point>> points = [];
HashSet<Point> collinearPoints = [];

for (int y = 0; y < map.Length; y++)
{
    for (int x = 0; x < map[y].Length; x++)
    {
        char c = map[y][x];

        if (c == '.')
            continue;

        if (!points.ContainsKey(c))
            points.Add(c, []);

        points[c].Add(new Point(x, y));
    }
}

foreach (KeyValuePair<char, List<Point>> pair in points)
{
    List<Point> currentPoints = pair.Value;

    for (int i = 0; i < currentPoints.Count - 1; i++)
    {
        for (int j = i + 1; j < currentPoints.Count; j++)
        {
            int xDiff = currentPoints[i].X - currentPoints[j].X;
            int yDiff = currentPoints[i].Y - currentPoints[j].Y;

            collinearPoints.Add(currentPoints[i]);
            collinearPoints.Add(currentPoints[j]);

            Point collinearPoint = new Point(
                currentPoints[i].X + xDiff,
                currentPoints[i].Y + yDiff
            );

            while (
                collinearPoint.X >= 0
                && collinearPoint.X < map[0].Length
                && collinearPoint.Y >= 0
                && collinearPoint.Y < map.Length
            )
            {
                collinearPoints.Add(collinearPoint);
                collinearPoint = new Point(collinearPoint.X + xDiff, collinearPoint.Y + yDiff);
            }

            collinearPoint = new Point(currentPoints[j].X - xDiff, currentPoints[j].Y - yDiff);

            while (
                collinearPoint.X >= 0
                && collinearPoint.X < map[0].Length
                && collinearPoint.Y >= 0
                && collinearPoint.Y < map.Length
            )
            {
                collinearPoints.Add(collinearPoint);
                collinearPoint = new Point(collinearPoint.X - xDiff, collinearPoint.Y - yDiff);
            }
        }
    }
}

Console.WriteLine(collinearPoints.Count);

record Point(int X, int Y);
