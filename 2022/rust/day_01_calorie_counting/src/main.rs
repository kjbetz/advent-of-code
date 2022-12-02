use std::fs::File;
use std::io::{self, BufRead};
use std::path::Path;

fn main() {
    let mut elf = 1;
    let mut calories = 0;
    let mut elf_with_most = 0;
    let mut most_calories = 0;

    if let Ok(lines) = read_lines("./input.txt") {
        for line in lines {
            if let Ok(cal) = line {
                if cal == "" {
                    if calories > most_calories {
                        most_calories = calories;
                        elf_with_most = elf;
                    }

                    elf += 1;
                    calories = 0;
                } else {
                    let cal: u32 = cal.trim().parse().expect("Not a number?!?");
                    calories += cal;
                }
            }
        }
    }

    println!("Elf {} has {} calories.", elf_with_most, most_calories);

    let new_lines = include_str!("./input.txt");

    let max_calories: u32 = new_lines.split("\n\n")
        .map(|l| l.split("\n")
            .flat_map(|num| num.parse::<u32>())
            .sum())
        .max()
        .unwrap();

    println!("Max calories: {:?}", max_calories);
}

fn read_lines<P>(filename: P) -> io::Result<io::Lines<io::BufReader<File>>>
where P: AsRef<Path>, {
    let file = File::open(filename)?;
    Ok(io::BufReader::new(file).lines())
}
