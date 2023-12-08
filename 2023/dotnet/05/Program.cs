string[] lines = File.ReadAllLines("input.txt");

long[] seeds = lines[0].Split(":")[1].Trim().Split().Select(s => long.Parse(s)).ToArray();
List<Seed> seedRanges = new();

for (int i = 0; i < seeds.Length; i++)
{
    seedRanges.Add(new Seed(seeds[i], seeds[++i]));
}

List<Map> seedToSoilMaps = new();
List<Map> soilToFertilizerMaps = new();
List<Map> fertilizerToWaterMaps = new();
List<Map> waterToLightMaps = new();
List<Map> lightToTemperatureMaps = new();
List<Map> temperatureToHumidityMaps = new();
List<Map> humidityToLocationMaps = new();

string currentMap = "";
for (int i = 1; i < lines.Length; i++)
{
    if (lines[i] == "")
    {
        currentMap = "";
    }
    else if (lines[i] == "seed-to-soil map:")
    {
        currentMap = "seed-to-soil map:";
    }
    else if (currentMap == "seed-to-soil map:")
    {
        long[] mapRanges = lines[i].Split().Select(s => long.Parse(s)).ToArray();
        BuildMap(mapRanges, seedToSoilMaps);
    }
    else if (lines[i] == "soil-to-fertilizer map:")
    {
        currentMap = "soil-to-fertilizer map:";
    }
    else if (currentMap == "soil-to-fertilizer map:")
    {
        long[] mapRanges = lines[i].Split().Select(s => long.Parse(s)).ToArray();
        BuildMap(mapRanges, soilToFertilizerMaps);
    }
    else if (lines[i] == "fertilizer-to-water map:")
    {
        currentMap = "fertilizer-to-water map:";
    }
    else if (currentMap == "fertilizer-to-water map:")
    {
        long[] mapRanges = lines[i].Split().Select(s => long.Parse(s)).ToArray();
        BuildMap(mapRanges, fertilizerToWaterMaps);
    }
    else if (lines[i] == "water-to-light map:")
    {
        currentMap = "water-to-light map:";
    }
    else if (currentMap == "water-to-light map:")
    {
        long[] mapRanges = lines[i].Split().Select(s => long.Parse(s)).ToArray();
        BuildMap(mapRanges, waterToLightMaps);
    }
    else if (lines[i] == "light-to-temperature map:")
    {
        currentMap = "light-to-temperature map:";
    }
    else if (currentMap == "light-to-temperature map:")
    {
        long[] mapRanges = lines[i].Split().Select(s => long.Parse(s)).ToArray();
        BuildMap(mapRanges, lightToTemperatureMaps);
    }
    else if (lines[i] == "temperature-to-humidity map:")
    {
        currentMap = "temperature-to-humidity map:";
    }
    else if (currentMap == "temperature-to-humidity map:")
    {
        long[] mapRanges = lines[i].Split().Select(s => long.Parse(s)).ToArray();
        BuildMap(mapRanges, temperatureToHumidityMaps);
    }
    else if (lines[i] == "humidity-to-location map:")
    {
        currentMap = "humidity-to-location map:";
    }
    else if (currentMap == "humidity-to-location map:")
    {
        long[] mapRanges = lines[i].Split().Select(s => long.Parse(s)).ToArray();
        BuildMap(mapRanges, humidityToLocationMaps);
    }
}

/*
Console.WriteLine("Fertilizer to water maps:");
foreach (Map map in fertilizerToWaterMaps)
{
    Console.WriteLine($"{map.DestinationMin}-{map.DestinationMax}, {map.SourceMin}-{map.SourceMax}");
}
*/

List<long> seedLocations = new();

foreach (Seed seed in seedRanges)
{
    for (int i = 0; i < seed.Count; i++)
    {
        long seedLocation = Convert(
            Convert(
                Convert(
                    Convert(
                        Convert(
                            Convert(Convert(seed.Start + i, seedToSoilMaps), soilToFertilizerMaps),
                            fertilizerToWaterMaps
                        ),
                        waterToLightMaps
                    ),
                    lightToTemperatureMaps
                ),
                temperatureToHumidityMaps
            ),
            humidityToLocationMaps
        );

        // Console.WriteLine();
        // Console.WriteLine($"Seed {seed} is at location {seedLocation}");
        seedLocations.Add(seedLocation);
    }
}

Console.WriteLine($"Minimum seed location: {seedLocations.Min()}");

void BuildMap(long[] mapRanges, List<Map> maps)
{
    maps.Add(
        new Map(
            mapRanges[0],
            mapRanges[0] + mapRanges[2],
            mapRanges[1],
            mapRanges[1] + mapRanges[2]
        )
    );
}

long Convert(long startingValue, List<Map> maps)
{
    long result = startingValue;

    foreach (Map map in maps)
    {
        if (result >= map.SourceMin && result < map.SourceMax)
        {
            // Console.WriteLine($"result = map.DestinationMin + (result - map.SourceMin) = {map.DestinationMin} + ({result} - {map.SourceMin}) = {map.DestinationMin + (result - map.SourceMin)}");
            result = map.DestinationMin + (result - map.SourceMin);
            break;
        }
    }

    // Console.Write($"{result}, ");

    return result;
}

public record Map(long DestinationMin, long DestinationMax, long SourceMin, long SourceMax);

public record Seed(long Start, long Count);
