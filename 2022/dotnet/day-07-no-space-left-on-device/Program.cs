string[] lines = System.IO.File.ReadAllLines("./input.txt");

DirectoryNode headDirectoryNode = new DirectoryNode("/");
DirectoryNode? currentDirectory = headDirectoryNode;

int i = 0;
while (i < lines.Length)
{
    string[] output = lines[i].Split(" ");

    if (output[0] == "$")
    {
        switch (output[1])
        {
            case "cd":
                currentDirectory = output[2] switch
                {
                    ".." => currentDirectory?.ParentDirectory,
                    "/" => headDirectoryNode,
                    _ => currentDirectory?.SubDirectories.Find(sd => sd.Name == output[2]),
                };
                
                i++;

                break;
            case "ls":
                i++;
                output = lines[i].Split(" ");

                if (i < lines.Length)
                {
                    while (output[0] != "$")
                    {
                        if (output[0] == "dir")
                        {
                            currentDirectory?.SubDirectories.Add(new DirectoryNode(output[1], currentDirectory));
                        } else {
                            if (int.TryParse(output[0], out int fileSize))
                            {
                                currentDirectory?.Files.Add(new File(output[1], fileSize));
                            }
                        }

                        i++;
                        if (i < lines.Length)
                        {
                            output = lines[i].Split(" ");
                        } else {
                            break;
                        }
                    };
                
                    continue;
                } else {
                    break;
                }
            default:
                break;
        };
    }
}

int totalSize = CalculateDirectorySize(headDirectoryNode);
Console.WriteLine($"The total size of / is: {totalSize}");

int sizeUnder100K = GetDirectoriesUnderSize(headDirectoryNode, 100_000);
Console.WriteLine($"The total size of directories <= 100,000 is: {sizeUnder100K}");

int unusedSpace = 70_000_000 - totalSize;
int targetSize = 30_000_000 - unusedSpace;
Console.WriteLine($"The unused space is: {unusedSpace}");
Console.WriteLine($"The target size to delete is: {targetSize}");

DirectoryNode directoryToDelete = FindSmallestDirectoryToDelete(headDirectoryNode, targetSize);
Console.WriteLine($"The smallest directory to delete is {directoryToDelete.Name} ({directoryToDelete.Size})");

int CalculateDirectorySize(DirectoryNode directory)
{
    foreach (DirectoryNode subDirectory in directory.SubDirectories)
    {
        directory.Size += CalculateDirectorySize(subDirectory);
    }

    foreach (File file in directory.Files)
    {
        directory.Size += file.Size;
    }

    return directory.Size;
}

int GetDirectoriesUnderSize(DirectoryNode directory, int targetSize)
{
    int totalSize = 0;

    foreach (DirectoryNode subDirectory in directory.SubDirectories)
    {
        totalSize += GetDirectoriesUnderSize(subDirectory, targetSize);
    }

    return totalSize += directory.Size <= targetSize ? directory.Size : 0;
}

DirectoryNode FindSmallestDirectoryToDelete(DirectoryNode directory, int targetSize)
{
    DirectoryNode smallestDirectory = directory;

    foreach (DirectoryNode subDirectory in directory.SubDirectories)
    {
        var d = FindSmallestDirectoryToDelete(subDirectory, targetSize);

        if (d.Size >= targetSize && d.Size < smallestDirectory.Size)
        {
            smallestDirectory = d;
        }
    }

    return smallestDirectory;
}

public class DirectoryNode
{
    public string Name { get; set; }
    public DirectoryNode? ParentDirectory { get; set; }
    public int Size { get; set; }
    public List<DirectoryNode> SubDirectories { get; set; }
    public List<File> Files { get; set; }

    public DirectoryNode(string name, DirectoryNode? parentDirectory = null)
    {
        Name = name;
        Size = 0;
        ParentDirectory = parentDirectory;
        SubDirectories = new List<DirectoryNode>();
        Files = new List<File>();
    }
}

public class File
{
    public string Name { get; set; }
    public int Size { get; set; }

    public File(string name, int size)
    {
        Name = name;
        Size = size;
    }
}

