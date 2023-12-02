// Part One
List<int> calibrationValues = new();

foreach (string line in File.ReadLines(@"input.txt"))
{
    char firstValue = char.MinValue;
    char secondValue = char.MinValue;

    foreach (char c in line)
    {
        if (char.IsDigit(c))
        {
            if (firstValue == char.MinValue)
            {
                firstValue = c;
            }
            else
            {
                secondValue = c;
            }
        }
    }

    string calibrationValueString =
        $"{firstValue}{(secondValue != char.MinValue ? secondValue : firstValue)}";

    calibrationValues.Add(Int32.Parse(calibrationValueString));
}

Console.WriteLine(calibrationValues.Sum());

// Part Two

List<int> newCalibrationValues = new();

foreach (string line in File.ReadLines(@"input.txt"))
{
    int firstValue = int.MinValue;
    int secondValue = int.MinValue;
    string temp = string.Empty;

    foreach (char c in line)
    {
        if (char.IsDigit(c))
        {
            if (firstValue == int.MinValue)
            {
                if (temp != string.Empty && TryGetDigit(temp, out int v))
                {
                    firstValue = v;
                    secondValue = int.Parse(c.ToString());
                }
                else
                {
                    firstValue = int.Parse(c.ToString());
                }
            }
            else
            {
                secondValue = int.Parse(c.ToString());
            }

            temp = string.Empty;
        }
        else
        {
            temp += c;

            if (TryGetDigit(temp, out int v))
            {
                if (firstValue == int.MinValue)
                {
                    firstValue = v;
                }
                else
                {
                    secondValue = v;
                }

                temp = string.Empty;
            }
        }
    }

    if (temp != string.Empty && TryGetDigit(temp, out int value))
    {
        secondValue = value;
    }

    string newCalibrationValueString =
        $"{firstValue}{(secondValue != int.MinValue ? secondValue : firstValue)}";

    newCalibrationValues.Add(Int32.Parse(newCalibrationValueString));
}

Console.WriteLine(newCalibrationValues.Sum());

bool TryGetDigit(string value, out int digit)
{
    digit = int.MinValue;

    if (value.Contains("one"))
    {
        digit = 1;
        return true;
    }
    else if (value.Contains("two"))
    {
        digit = 2;
        return true;
    }
    else if (value.Contains("three"))
    {
        digit = 3;
        return true;
    }
    else if (value.Contains("four"))
    {
        digit = 4;
        return true;
    }
    else if (value.Contains("five"))
    {
        digit = 5;
        return true;
    }
    else if (value.Contains("six"))
    {
        digit = 6;
        return true;
    }
    else if (value.Contains("seven"))
    {
        digit = 7;
        return true;
    }
    else if (value.Contains("eight"))
    {
        digit = 8;
        return true;
    }
    else if (value.Contains("nine"))
    {
        digit = 9;
        return true;
    }
    else if (value.Contains("zero"))
    {
        digit = 0;
        return true;
    }

    return false;
}
