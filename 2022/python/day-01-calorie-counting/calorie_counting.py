fhand = open('input.txt')

elf = 1;
calories = 0;
most_calories = 0;
elf_with_most = 0;

for line in fhand:
    line = line.rstrip()

    if line == '':
        if calories > most_calories:
            most_calories = calories
            elf_with_most = elf
        
        elf += 1
        calories = 0
    else:
        calories += int(line)

print('Elf', elf_with_most, 'has', most_calories, 'calories.')
