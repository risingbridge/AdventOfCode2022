using Day03;

List<string> input = File.ReadAllLines("./input.txt").ToList();
Dictionary<char, int> LookupTable = new Dictionary<char, int>();
AddLookupTable();

List<Rucksack> rucksacks = new List<Rucksack>();
foreach (string inputLine in input)
{
    int stringLength = inputLine.Length;
    string firstHalf = inputLine.Substring(0, stringLength / 2);
    string lastHalf = inputLine.Substring((int)stringLength / 2, stringLength / 2);
    Rucksack sack = new Rucksack(firstHalf, lastHalf);
    sack.DoubleItems = FindDoubles(sack);
    rucksacks.Add(sack);
    //PrintSack(sack);
}

int partOneResult = SolvePartOne(rucksacks);
Console.WriteLine($"Part One: {partOneResult}");

FindBadges();
int partTwoResult = SolvePartTwo(rucksacks);
Console.WriteLine($"Part two: {partTwoResult}");

List<char> FindDoubles(Rucksack sack)
{
    List<char> result = new List<char>();

    foreach (char c in sack.CompartmentA)
    {
        if (sack.CompartmentB.Contains(c))
        {
            if (!result.Contains(c))
            {
                result.Add(c);
            }
        }
    }

    return result;
}

void PrintSack(Rucksack sack)
{
    Console.WriteLine(sack.ToString());
    foreach (char c in sack.DoubleItems)
    {
        int value = LookupTable[c];
        Console.WriteLine($"{c} - {value}");
    }
    Console.WriteLine();
}

void AddLookupTable()
{
    string lookupString = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    int count = 1;
    foreach (char c in lookupString)
    {
        LookupTable.Add(c, count);
        count++;
    }
}

//Solve part 1
int SolvePartOne(List<Rucksack> sacks)
{
    int result = 0;
    foreach (Rucksack item in sacks)
    {
        foreach (char c in item.DoubleItems)
        {
            int value = LookupTable[c];
            result += value;
        }
    }


    return result;
}

void FindBadges()
{
    //Solve part 2
    for (int i = 0; i < rucksacks.Count; i+= 3)
    {
        Rucksack a = rucksacks[i];
        Rucksack b = rucksacks[i+1];
        Rucksack c = rucksacks[i+2];

        foreach (char item in a.FullString)
        {
            if(b.FullString.Contains(item) && c.FullString.Contains(item))
            {
                a.SecurityBadge = item;
                b.SecurityBadge = item;
                c.SecurityBadge = item;
                break;
            }
        }
    }
}

int SolvePartTwo(List<Rucksack> sacks)
{
    int result = 0;

    for (int i = 0; i < sacks.Count; i+= 3)
    {
        result += LookupTable[sacks[i].SecurityBadge];
    }

    return result;
}