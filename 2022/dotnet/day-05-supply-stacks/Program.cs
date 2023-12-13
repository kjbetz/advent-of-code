string[] moves = File.ReadAllLines("./input.txt");

Stack<string>[] testCrates = new Stack<string>[]
{
    new Stack<string>(new string[] { "Z", "N" }),
    new Stack<string>(new string [] { "M", "C", "D" }),
    new Stack<string>(new string [] { "P" })
};

Stack<string>[] crates = new Stack<string>[]
{
    new Stack<string>(new string[] { "T", "P", "Z", "C", "S", "L", "Q", "N" }),
    new Stack<string>(new string[] { "L", "P", "T", "V", "H", "C", "G" }),
    new Stack<string>(new string[] { "D", "C", "Z", "F" }),
    new Stack<string>(new string[] { "G", "W", "T", "D", "L", "M", "V", "C" }),
    new Stack<string>(new string[] { "P", "W", "C" }),
    new Stack<string>(new string[] { "P", "F", "J", "D", "C", "T", "S", "Z" }),
    new Stack<string>(new string[] { "V", "W", "G", "B", "D" }),
    new Stack<string>(new string[] { "N", "J", "S", "Q", "H", "W" }),
    new Stack<string>(new string[] { "R", "C", "Q", "F", "S", "L", "V" }),
};

foreach (string move in moves)
{
    string[] moveSplit = move.Split(" ");
    int numberOfMoves = int.Parse(moveSplit[1]);
    int fromStack = int.Parse(moveSplit[3]) - 1;
    int toStack = int.Parse(moveSplit[5]) - 1;
    Stack<string> tempStack = new();

    for (int i = 0; i < numberOfMoves; i++)
    {
        string popped = crates[fromStack].Pop();
        tempStack.Push(popped);
    }

    for (int i = 0; i < numberOfMoves; i++)
    {
        string popped = tempStack.Pop();
        crates[toStack].Push(popped);
    }
}

string tops = "";

foreach (Stack<string> crate in crates)
{
    tops += crate.Peek();
}

Console.WriteLine($"{tops}");
