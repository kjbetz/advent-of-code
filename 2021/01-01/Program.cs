// See https://aka.ms/new-console-template for more information
int counter = 0;
int hold = 0;

foreach (string line in File.ReadLines(@"input.txt"))
{
    var depth = Int32.Parse(line.Trim());

    if (hold != 0)
    {
        if (depth > hold)
        {
            counter++;
        }

        hold = depth;
    }
    else
    {
        hold = depth;
    }
}

Console.WriteLine(counter);
