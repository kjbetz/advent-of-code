string[] lines = File.ReadAllLines("input.txt");
List<int> predictedValues = new();
List<int> predictedFirstValues = new();

foreach (string line in lines)
{
    List<List<int>> values = new();

    values.Add(line.Split(' ').Select(c => int.Parse(c)).ToList());

    while (
        !(
            values.Last().Select(n => n).Distinct().Count() == 1
            && values.Last().Select(n => n).Distinct().First() == 0
        )
    )
    {
        List<int> nextDiffs = new();
        for (int i = 1; i < values.Last().Count; i++)
        {
            nextDiffs.Add(values.Last()[i] - values.Last()[i - 1]);
        }

        values.Add(nextDiffs);
    }
/*
    foreach (List<int> value in values)
    {
        Console.WriteLine(string.Join(" ", value));
    }
*/
    int predictedValue = values.Select(v => v.Last()).Sum();
    predictedValues.Add(predictedValue);

    List<int> firstValues = values.Select(v => v.First()).ToList();
    int firstValue = 0;
    for (int i = firstValues.Count - 2; i >= 0; i--)
    {
        // Console.WriteLine($"{firstValues[i]} - {firstValue} = {firstValues[i] - firstValue}");
        firstValue = firstValues[i] - firstValue;
    }

    predictedFirstValues.Add(firstValue);
}

Console.WriteLine($"Sum of predicted values: {predictedValues.Sum()}");
Console.WriteLine($"Sum of first values: {predictedFirstValues.Sum()}");
