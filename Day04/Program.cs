List<string> input = File.ReadAllLines("./input.txt").ToList();

List<Tuple<string, string>> pairs = new List<Tuple<string, string>>();
int insideCount = 0;
foreach (string item in input)
{
    string[] split = item.Split(",");
    Tuple<string, string> pair = new Tuple<string, string>(split[0], split[1]);
    pairs.Add(pair);
    bool inside = isInside(pair);
    if (inside)
    {
        insideCount++;
    }
    Console.WriteLine($"{PrintPair(pair)} added. {inside}");
}
Console.WriteLine($"Part One: {insideCount}");
Console.WriteLine($"Part Two: {SolvePartTwo(pairs)}");

//Solve part one
bool isInside(Tuple<string, string> pair)
{
    bool result = false;
    int lowestA = int.Parse(pair.Item1.Split("-")[0]);
    int highestA = int.Parse(pair.Item1.Split("-")[1]);
    int lowestB = int.Parse(pair.Item2.Split("-")[0]);
    int highestB = int.Parse(pair.Item2.Split("-")[1]);
    if(lowestB >= lowestA && highestB <= highestA)
    {
        return true;
    }else if(lowestA >= lowestB && highestA <= highestB)
    {
        return true;
    }

    return result;
}

string PrintPair(Tuple<string,string> pair)
{
    return $"{pair.Item1} - {pair.Item2}";
}

//Solve part two
int SolvePartTwo(List<Tuple<string,string>> parts)
{
    int result = 0;


    foreach (var item in parts)
    {
        List<int> listA = new List<int>();
        List<int> listB = new List<int>();
        int lowestA = int.Parse(item.Item1.Split("-")[0]);
        int highestA = int.Parse(item.Item1.Split("-")[1]);
        int lowestB = int.Parse(item.Item2.Split("-")[0]);
        int highestB = int.Parse(item.Item2.Split("-")[1]);
        for (int i = lowestA; i <= highestA; i++)
        {
            listA.Add(i);
        }
        for (int i = lowestB; i <= highestB; i++)
        {
            listB.Add(i);
        }
        foreach (int i in listA)
        {
            if (listB.Contains(i))
            {
                result++;
                break;
            }
        }
    }

    return result;
}