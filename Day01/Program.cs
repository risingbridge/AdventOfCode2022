using Day01;
using System.Security.Cryptography.X509Certificates;

List<string> input = File.ReadAllLines("./input.txt").ToList();
List<Elf> elfs = new List<Elf>();

Elf currentElf = new Elf();
List<int> calories = new List<int>();
int elfId = 0;
foreach (string item in input)
{
    if(item.Length > 0)
    {
        int food = int.Parse(item);
        calories.Add(food);
    }
    else
    {
        currentElf.Food = calories;
        currentElf.TotalCalories = CalculateTotalCalories(calories);
        currentElf.Id = elfId;
        elfs.Add(currentElf);
        currentElf = new Elf();
        calories.Clear();
        elfId++;
    }
}

//Find elf with highest total calories
int highest = int.MinValue;
int highestId = 0;
foreach (Elf elf in elfs)
{
    if(elf.TotalCalories > highest)
    {
        highestId = elf.Id;
        highest = elf.TotalCalories;
    }
}
Console.WriteLine($"The elf with the highest calories count is elf {highestId}, with count {highest}");

elfs = elfs.OrderBy(e => e.TotalCalories).ToList();

int totalTree = elfs[elfs.Count - 1].TotalCalories + elfs[elfs.Count - 2].TotalCalories + elfs[elfs.Count - 3].TotalCalories;
Console.WriteLine($"The three elfs with highest calorie number has {totalTree} combined calories");

int CalculateTotalCalories(List<int> food)
{
    int total = 0;
    foreach (int item in food)
    {
        total += item;
    }
    return total;
}