using System.Linq;

string[] lines = File.ReadAllLines("input.txt");
int safeReports = 0;

foreach (string line in lines)
{
    bool isSafe = true;
    string[] parts = line.Split(" ");

    bool isAscending = int.Parse(parts[1]) > int.Parse(parts[0]) ? true : false;

    isSafe = CheckIfSafe(parts, isAscending);

    if (!isSafe)
    {

        isAscending = int.Parse(parts[1]) < int.Parse(parts[0]) ? true : false;
        isSafe = CheckIfSafe(parts, isAscending);

        for (int i = 0; i < parts.Length; i++)
        {
            string[] parts2 = parts.Where((x, index) => index != i).ToArray();

            isAscending = int.Parse(parts2[1]) > int.Parse(parts2[0]) ? true : false;

            isSafe = CheckIfSafe(parts2, isAscending);

            if (isSafe)
                break;
        }
    }

    if (isSafe)
        safeReports++;
}

Console.WriteLine(safeReports);

bool CheckIfSafe(string[] parts, bool isAscending)
{
    for (int i = 1; i < parts.Length; i++)
    {
        if (isAscending && int.Parse(parts[i]) < int.Parse(parts[i - 1]))
            return false;
        else if (!isAscending && int.Parse(parts[i]) > int.Parse(parts[i - 1]))
            return false;

        if (
            Math.Abs(int.Parse(parts[i]) - int.Parse(parts[i - 1])) < 1
            || Math.Abs(int.Parse(parts[i]) - int.Parse(parts[i - 1])) > 3
        )
            return false;
    }

    return true;
}
