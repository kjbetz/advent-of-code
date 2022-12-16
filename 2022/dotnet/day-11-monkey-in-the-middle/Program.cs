string[] lines = File.ReadAllLines("./input.txt");

List<Monkey> monkies = LoadMonkies(lines);

ulong mod = monkies.Select(m => m.Test.Value).Aggregate((a, v) => a * v);

PlayKeepAway(monkies, 10_000);

LogInspections();

ulong monkeyBusiness = monkies
    .OrderByDescending(m => m.Inspections)
    .Select(m => (ulong)m.Inspections)
    .Take(2)
    .Aggregate((a, i) => a * i);

Console.WriteLine();
Console.WriteLine($"Level of monkey business: {monkeyBusiness}");

void PlayKeepAway(List<Monkey> monkies, int rounds = 1)
{
    Console.WriteLine("Playing keep away!");

    for (int round = 0; round < rounds; round++)
    {
        foreach (Monkey monkey in monkies)
        {
            while (monkey.Items.Count > 0)
            {
                ulong item = monkey.Items.Dequeue();

                ulong operand = monkey.Operation.Operand == "old" ? item : ulong.Parse(monkey.Operation.Operand);

                item = monkey.Operation.Operator switch
                {
                    "+" => item + operand,
                    "*" => item * operand,
                    "-" => item - operand,
                    "/" => item / operand,
                    _ => item,
                };

                item %= mod;

                monkey.Inspections++;

                Monkey nextMonkey = 
                    item % monkey.Test.Value == 0
                        ? monkies.Find(m => m.Id == monkey.Test.TrueMonkey)
                        : monkies.Find(m => m.Id == monkey.Test.FalseMonkey);

                nextMonkey.Items.Enqueue(item);
            }
        }
    }
}

void LogInspections(int round = 0)
{
    foreach (Monkey monkey in monkies)
    {
        Console.WriteLine($"Monkey {monkey.Id} inspected items {monkey.Inspections} times.");
    }

    Console.WriteLine();
}

void LogMonkies(int round = 0)
{
    if (round > 0)
    {
        Console.WriteLine($"After round {round}, the monkeys are holding items with these worry levels:");
    }

    foreach (Monkey monkey in monkies)
    {
        Console.Write($"Monkey {monkey.Id}: ");

        foreach (ulong item in monkey.Items)
        {
            Console.Write($"{item}, ");
        }

        Console.Write("\n");
    }

    Console.WriteLine();
}

List<Monkey> LoadMonkies(string[] lines)
{
    List<Monkey> monkies = new();
    IEnumerable<string[]> chunks = lines.Chunk(7);

    foreach (string[] chunk in chunks)
    {
        int monkeyId = int.Parse(chunk[0].Substring(
                    chunk[0].IndexOf(' '),
                    chunk[0].IndexOf(':') - chunk[0].IndexOf(' ')));

        ulong[] items = chunk[1].Substring(chunk[1].IndexOf(':') + 1)
            .Split(", ")
            .Select(s => ulong.Parse(s.Trim()))
            .ToArray();

        char[] operands = new char[4] { '+', '-', '*', '/' };
        int operatorIndex = chunk[2].IndexOfAny(operands);

        Operation operation = new(
                chunk[2].Substring(operatorIndex, 1),
                chunk[2].Substring(operatorIndex + 1).Trim()
        );

        Test test = new(
                ulong.Parse(chunk[3].Trim().Split(" ")[3]),
                int.Parse(chunk[4].Trim().Split(" ")[5]),
                int.Parse(chunk[5].Trim().Split(" ")[5])
        );

        monkies.Add(new Monkey(monkeyId, items, operation, test));
    }

    return monkies;
}

class Monkey
{
    public int Id { get; set; }
    public Queue<ulong> Items { get; set; } = default!;
    public Queue<ulong> OriginalItems { get; set; } = default!;
    public Operation Operation { get; set; } = default!;
    public Test Test { get; set; } = default!;
    public int Inspections { get; set; } = 0;

    public Monkey(int monkeyId, ulong[] items, Operation operation, Test test)
    {
        this.Id = monkeyId;
        this.Items = new Queue<ulong>(items);
        this.OriginalItems = new Queue<ulong>(items);
        this.Operation = operation;
        this.Test = test;
    }
}

record Operation(string Operator, string Operand);
record Test(ulong Value, int TrueMonkey, int FalseMonkey);
