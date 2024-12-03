using System.Text.RegularExpressions;

string file = File.ReadAllText("input.txt");

// Part One
string pattern = @"mul\(\d{1,3},\d{1,3}\)";

MatchCollection matches = Regex.Matches(file, pattern);

int sum = 0;

foreach (Match match in matches)
    sum += MultiplyMatch(match);

Console.WriteLine(sum);

// Part Two
string pattern2 = @"mul\(\d{1,3},\d{1,3}\)|do\(\)|don\'t\(\)";

MatchCollection matches2 = Regex.Matches(file, pattern2);

int sum2 = 0;
bool isEnabled = true;

foreach (Match match in matches2)
{
    if (match.ToString() == "do()")
        isEnabled = true;
    else if (match.ToString() == "don't()")
        isEnabled = false;
    else if (isEnabled)
        sum2 += MultiplyMatch(match);
}

Console.WriteLine(sum2);

int MultiplyMatch(Match match)
{
    int indexOfComma = match.ToString().IndexOf(",");
    int indexOfClosingParenthesis = match.ToString().IndexOf(")");
    int firstNumber = int.Parse(match.ToString().Substring(4, indexOfComma - 4));
    int secondNumber = int.Parse(
        match.ToString().Substring(indexOfComma + 1, indexOfClosingParenthesis - indexOfComma - 1)
    );

    return firstNumber * secondNumber;
}
