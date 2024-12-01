string[] lines = File.ReadAllLines("input.txt");
List<string> listA = [];
List<string> listB = [];
int sum = 0;
int sum2 = 0;

foreach (string line in lines)
{
    string[] parts = line.Split("   ");
    listA.Add(parts[0]);
    listB.Add(parts[1]);
}

// Part One
listA.Sort();
listB.Sort();

for (int i = 0; i < listA.Count; i++)
    if (int.Parse(listA[i]) > int.Parse(listB[i]))
        sum += int.Parse(listA[i]) - int.Parse(listB[i]);
    else
        sum += int.Parse(listB[i]) - int.Parse(listA[i]);

Console.WriteLine(sum);


// Part Two
Dictionary<string, int> listBOccurrences = new();

foreach (string item in listB)
    if (listBOccurrences.ContainsKey(item))
        listBOccurrences[item]++;
    else
        listBOccurrences[item] = 1;

foreach (string item in listA)
    if (listBOccurrences.ContainsKey(item))
        sum2 += int.Parse(item) * listBOccurrences[item];

Console.WriteLine(sum2);
