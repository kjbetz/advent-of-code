use std::collections::HashMap;

fn main() {
    let lines = include_str!("./input.txt");

    let lines: Vec<&str> = lines.lines().collect();

    let lookup_1 = HashMap::from([
        ("A X", 4),
        ("A Y", 8),
        ("A Z", 3),
        ("B X", 1),
        ("B Y", 5),
        ("B Z", 9),
        ("C X", 7),
        ("C Y", 2),
        ("C Z", 6),
    ]);

    let lookup_2 = HashMap::from([
        ("A X", 3),
        ("A Y", 4),
        ("A Z", 8),
        ("B X", 1),
        ("B Y", 5),
        ("B Z", 9),
        ("C X", 2),
        ("C Y", 6),
        ("C Z", 7),
    ]);

    let mut score = 0;

    for &line in &lines {
        let value = lookup_1.get(&line);

        match value {
            Some(v) => score += v,
            None => println!("Oops?!"),
        }
    }

    let mut score_2 = 0;

    for &line in &lines {
        let value = lookup_2.get(&line);

        match value {
            Some(v) => score_2 += v,
            None => println!("Oops?!"),
        }
    }

    println!("Answer 1: {score}");
    println!("Answer 2: {score_2}");
}
