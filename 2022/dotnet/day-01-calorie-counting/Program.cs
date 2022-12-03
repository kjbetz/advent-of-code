int elfWithMost = 0;
int mostCalories = 0;
int elf = 1;
int calories = 0;

string[] lines = System.IO.File.ReadAllLines(@"./input.txt");

List<Elf> elves = new();

for (int i = 0; i < lines.Length; i++)
{
    if (lines[i] == "")
    {
        elves.Add(new Elf(elf, calories));

        if (calories > mostCalories)
        {
            elfWithMost = elf;
            mostCalories = calories;
        }

        calories = 0;
        elf++;
    }
    else
    {
        calories += Int32.Parse(lines[i]);
    }
}

Console.WriteLine($"Elf {elfWithMost} has {mostCalories} calories.");

elves.OrderByDescending(e => e.Calories);

int top3Calories = elves
    .OrderByDescending(e => e.Calories)
    .Take(3)
    .Sum(e => e.Calories);

Console.WriteLine($"The top 3 elves carrying the most calories have {top3Calories} calories.");

public record Elf(int Id, int Calories);
