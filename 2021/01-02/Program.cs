// See https://aka.ms/new-console-template for more information
int counter = 0;
List<int> depths = new();

foreach (string line in File.ReadLines(@"input.txt"))
{
    int depth = Int32.Parse(line);

    depths.Add(depth);

    if (depths.Count() > 4) depths.RemoveAt(0);

    if (depths.Count() == 4)
    {
        if (depths.GetRange(1, 3).Sum() > depths.GetRange(0, 3).Sum())
        {
            counter++;
        }
    }
}

Console.WriteLine(counter);
