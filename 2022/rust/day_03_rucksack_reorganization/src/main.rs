use std::collections::HashMap;

fn main() {
    static INPUT: &str = std::include_str!("./input.txt");
    static VALS: &str = "0abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    let priority: HashMap<char, usize> = VALS.chars().enumerate().map(|(i, v)| (v, i)).collect();

    let total: usize = INPUT
        .lines()
        .map(|line| {
            line[..line.len() / 2]
                .chars()
                .into_iter()
                .filter(|c| line[line.len() / 2..].contains(*c))
                .next()
                .unwrap_or('0')
        })
        .map(|c| priority.get(&c).unwrap_or(&0))
        .sum();

    println!("The priority is {}", total);

    let total2: usize = INPUT
        .lines()
        .collect::<Vec<&str>>()
        .chunks(3)
        .filter(|v| v.len() == 3)
        .map(|v| {
            v[0].
        
}
