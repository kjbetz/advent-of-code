string[] lines = File.ReadAllLines("./input.txt");

foreach (string line in lines)
{
    Console.WriteLine($"{FindSequenceNumber(line)}");
    Console.WriteLine($"{SecondIdeaStolenFromGnoat(line)}");
    Console.WriteLine($"{SecondIdeaStolenFromGnoat(line, 14)}");
}


int SecondIdeaStolenFromGnoat(string line, int windowSize = 4)
{
    for (int i = 0; i < line.Length; i++)
    {
        if (i + windowSize < line.Length)
        {
            string s = line.Substring(i, windowSize);
            HashSet<char> c = new(s);

            if (s.Length == c.Count)
            {
                return i + windowSize;
            }
        }
    }

    return 0;
}

int FindSequenceNumber(string line)
{
    int left = 0;
    int right = 0;
    Dictionary<char, int> code = new();

    while (left < line.Length && right < line.Length)
    {
        if (!code.TryAdd(line[right], right))
        {
            code[line[right]] = right;

            while (true)
            {
                if (line[left] == line[right])
                {
                    left++;
                    break;
                } else {
                    code.Remove(line[left]);
                    left++;
                }
            }
        }

        if (right - left == 13)
        {
            return right + 1;
        }

        right++;
    }

    return 0;
}

