﻿string[] lines = File.ReadAllLines("./input.txt");

int cycle = 0;
int value = 1;
List<int> signalStrengths = new();

PartOne();

cycle = 0;
value = 1;

PartTwo();

void PartTwo()
{
    foreach (string line in lines)
    {
        string[] instruction = line.Split(" ");

        if (instruction[0] == "noop")
        {
            DrawCycle();
        }
        else if (instruction[0] == "addx")
        {
            DrawCycle(2);

            value += int.Parse(instruction[1]);
        }
    }
}

void DrawCycle(int cycles = 1)
{
    for (int i = 0; i < cycles; i++)
    {
        int position = cycle - (40 * ((cycle) / 40));
        // Console.WriteLine($"value: {value}, cycle: {cycle}, position: {position}");

        if (position >= value - 1 && position <= value + 1)
        {
            Console.Write("#");
        } 
        else
        {
            Console.Write(".");
        }

        if ((cycle + 1) % 40 == 0)
        {
            Console.Write("\n");
        }

        cycle++;
    }
}

void PartOne()
{
    foreach (string line in lines)
    {
        string[] instruction = line.Split(" ");

        if (instruction[0] == "noop")
        {
            cycle++;
            LogCycle();
        } 
        else if (instruction[0] == "addx")
        {
            cycle++;
            LogCycle();

            cycle++;
            LogCycle();
            value += int.Parse(instruction[1]);
        }
    }

    Console.WriteLine($"Sum of signal strengths: {signalStrengths.Sum()}\n");
}

void LogCycle()
{
    if(cycle == 20 || (cycle <= 220 && (cycle - 20) % 40 == 0))
    {
        signalStrengths.Add(cycle * value);
    }
}
