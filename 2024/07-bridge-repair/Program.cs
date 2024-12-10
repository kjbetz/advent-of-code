using System.Linq;

string[] lines = File.ReadAllLines("input.txt");
double calibration = 0;
Dictionary<int, List<string>> combinations = [];

foreach (string line in lines)
{
    string[] parts = line.Split(":");

    double total = double.Parse(parts[0]);

    int[] operands = parts[1].Split(" ").Where(x => x != "").Select(x => int.Parse(x)).ToArray();

    if (!combinations.ContainsKey(operands.Length))
    {
        List<string> newCombinations = [];
        GenerateCombinations("+*|", "", operands.Length - 1, newCombinations);
        combinations.Add(operands.Length, newCombinations);
    }

    List<string> operations = combinations[operands.Length];

    foreach (string operation in operations)
    {
        char[] operationChars = operation.ToCharArray();
        double runningTotal = operands[0];

        for (int i = 1; i < operands.Length; i++)
        {
            if (operationChars[i - 1] == '+')
                runningTotal += operands[i];
            else if (operationChars[i - 1] == '*')
                runningTotal *= operands[i];
            else if (operationChars[i - 1] == '|')
                runningTotal = double.Parse(runningTotal.ToString() + operands[i].ToString());
        }

        if (runningTotal == total)
        {
            calibration += total;
            break;
        }
    }
}

Console.WriteLine(calibration);

void GenerateCombinations(string chars, string prefix, int length, List<string> combinations)
{
    if (length == 0)
    {
        combinations.Add(prefix);
        return;
    }

    for (int i = 0; i < chars.Length; i++)
    {
        GenerateCombinations(chars, prefix + chars[i], length - 1, combinations);
    }
}
