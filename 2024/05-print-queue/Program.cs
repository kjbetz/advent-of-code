string[] lines = File.ReadAllLines("input.txt");

// Part One
bool firstHalf = true;
Dictionary<int, List<int>> rules = [];
int validTotal = 0;
int updatedTotal = 0;

foreach (string line in lines)
{
    if (line == "")
    {
        firstHalf = false;
        continue;
    }

    if (firstHalf)
    {
        string[] parts = line.Split("|");
        if (rules.ContainsKey(int.Parse(parts[0])))
        {
            rules[int.Parse(parts[0])].Add(int.Parse(parts[1]));
        }
        else
        {
            rules.Add(int.Parse(parts[0]), [int.Parse(parts[1])]);
        }
    }
    else
    {
        HashSet<int> pagesAlreadyDone = new();
        List<string> pages = line.Split(",").ToList();

        bool isUpdateValid = true;

        foreach (string page in pages)
        {
            bool isPageValid = true;

            if (rules.ContainsKey(int.Parse(page)))
            {
                foreach (int rule in rules[int.Parse(page)])
                {
                    if (pagesAlreadyDone.Contains(rule))
                    {
                        isPageValid = false;
                        break;
                    }
                }
            }

            if (isPageValid)
            {
                pagesAlreadyDone.Add(int.Parse(page));
            }
            else
            {
                isUpdateValid = false;
                break;
            }
        }

        if (isUpdateValid)
        {
            validTotal += int.Parse(pages[pages.Count / 2]);
        }
        else
        {
            for (int i = 0; i < pages.Count; i++)
            {
                if (rules.ContainsKey(int.Parse(pages[i])))
                {
                    foreach (int rule in rules[int.Parse(pages[i])])
                    {
                        int foundIndex = pages.FindIndex(page => page == rule.ToString());

                        if (foundIndex > -1 && foundIndex < i)
                        {
                            pages.Insert(foundIndex, pages[i]);

                            pages.RemoveAt(i + 1);

                            i = foundIndex;
                        }
                    }
                }
            }

            updatedTotal += int.Parse(pages[pages.Count / 2]);
        }
    }
}

Console.WriteLine($"Valid Total: {validTotal}");
Console.WriteLine($"Updated Total: {updatedTotal}");
