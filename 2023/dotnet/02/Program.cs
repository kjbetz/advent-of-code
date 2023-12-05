string[] lines = File.ReadAllLines("input.txt");
List<int> possibleGameIds = new();
List<int> powerOfMinimumCubes = new();

int RED_CUBES = 12;
int GREEN_CUBES = 13;
int BLUE_CUBES = 14;

foreach (string line in lines)
{
    string[] game = line.Split(":");
    int gameId = int.Parse(game[0].Split(" ")[1].Trim().ToString());
    string[] gameSets = game[1].Split(";");
    bool isImpossibleGame = false;
    int minRedCubes = 0;
    int minGreenCubes = 0;
    int minBlueCubes = 0;

    foreach (string set in gameSets)
    {
        string[] cubes = set.Trim().Split(",");

        foreach (string cube in cubes)
        {
            string[] cubeData = cube.Trim().Split(" ");
            int numberOfCubes = int.Parse(cubeData[0].Trim().ToString());
            string cubeColor = cubeData[1].Trim();

            if (cubeColor == "red")
            {
                minRedCubes = Math.Max(minRedCubes, numberOfCubes);

                if (numberOfCubes > RED_CUBES)
                {
                    isImpossibleGame = true;
                }
            }
            else if (cubeColor == "green")
            {
                minGreenCubes = Math.Max(minGreenCubes, numberOfCubes);

                if (numberOfCubes > GREEN_CUBES)
                {
                    isImpossibleGame = true;
                }
            }
            else if (cubeColor == "blue")
            {
                minBlueCubes = Math.Max(minBlueCubes, numberOfCubes);

                if (numberOfCubes > BLUE_CUBES)
                {
                    isImpossibleGame = true;
                }
            }
        }
    }

    powerOfMinimumCubes.Add(minRedCubes * minGreenCubes * minBlueCubes);

    if (isImpossibleGame == false)
    {
        possibleGameIds.Add(gameId);
    }
}

Console.WriteLine($"Possible game IDs Sum: {possibleGameIds.Sum()}");

Console.WriteLine($"Power of minimum cubes Sum: {powerOfMinimumCubes.Sum()}");
