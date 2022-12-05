string[] lines = File.ReadAllLines("./input.txt");
List<List<int>> areas = new List<List<int>>();

int inclusives = 0;

foreach (string line in lines)
{
    string[] split = line.Split(",");

    string[] range1 = split[0].Split("-");
    List<int> area1 = new();

    string[] range2 = split[1].Split("-");
    List<int> area2 = new();

    for (int i = Int32.Parse(range1[0]); i < Int32.Parse(range1[1]) + 1; i++)
    {
        area1.Add(i);
    }

    for (int i = Int32.Parse(range2[0]); i < Int32.Parse(range2[1]) + 1; i++)
    {
        area2.Add(i);
    }

    if (ListHelper<int>.ContainsAnyItems(area1, area2) || ListHelper<int>.ContainsAnyItems(area2, area1))
    {
        inclusives++;
    }

}

Console.WriteLine($"There are {inclusives} inclusive assignments");

class ListHelper<T>
{
    public static bool ContainsAllItems(List<T> a, List<T> b)
        => !b.Except(a).Any();

    public static bool ContainsAnyItems(List<T> a, List<T> b)
        => a.Select(a => a).Intersect(b).Any();
    
}
