//Time to ingest the input data and parse it into monkeys
using Day11;
using System.Numerics;
using System.Text.RegularExpressions;

List<string> input = File.ReadAllLines("./debug.txt").ToList();
Dictionary<int, Monkey> monkeyDictionary = new Dictionary<int, Monkey>();

int monkeyCounter = 0;
List<List<string>> unparsedMonkeys = new List<List<string>>();
List<string> monkeyStrings = new List<string>();
foreach (string line in input)
{
    monkeyStrings.Add(line);
    if (string.IsNullOrWhiteSpace(line))
    {
        unparsedMonkeys.Add(new List<string>(monkeyStrings));
        monkeyStrings.Clear();
        monkeyCounter++;
    }
}
unparsedMonkeys.Add(new List<string>(monkeyStrings));
monkeyStrings.Clear();

foreach (List<string> item in unparsedMonkeys)
{
    //Parsing the starting items
    List<BigInteger> startingItems = new List<BigInteger>();
    string[] startingStrings = item[1].Replace("  Starting items: ", "").Split(", ");
    foreach (var st in startingStrings)
    {
        startingItems.Add(int.Parse(st));
    }
    //Finding the monkey ID
    int monkeyID = int.Parse(item[0].Replace("Monkey ", "").Replace(":",""));
    //Finding the operation
    string operation = item[2].Replace("  Operation: new = ", "");
    //Finding the test
    string test = item[3].Replace("  Test: divisible by ", "");
    //Find the true
    string istrue = item[4].Replace("    If true: throw to monkey ", "");
    //Finding the false
    string isfalse = item[5].Replace("    If false: throw to monkey ", "");

    Monkey monkey = new Monkey(monkeyID, startingItems, operation, int.Parse(test), int.Parse(istrue), int.Parse(isfalse));
    monkeyDictionary.Add(monkeyID, monkey);
}
Console.WriteLine($"Parsed {monkeyDictionary.Count} monkeys");

//Monkey inspects an item
    //Worry level is increased by the operation
        //Monkey gets bored with the item, worry level divided by 3, rounded down to int
            //Test is ran
                //Thrown to new monkey, and added to the end of their list, depending on test

int numberOfRounds = 20;
int roundCounter = 0;
while(roundCounter < numberOfRounds)
{
    //Do stuff
    Console.WriteLine($"Running round {roundCounter + 1}");
    foreach (KeyValuePair<int, Monkey> monkey in monkeyDictionary)
    {
        int numberOfInspectedItems = 0;
        Console.WriteLine($"Its Monkey {monkey.Key}'s turn!");
        for (int i = 0; i < monkey.Value.Items.Count; i++)
        {
            numberOfInspectedItems++;
            //Inspects item
            BigInteger worryLevel = monkey.Value.Items[i];
            BigInteger newWorryLevel = worryLevel;
            string opA = monkey.Value.Operation.Split(" ")[0];
            string opB = monkey.Value.Operation.Split(" ")[1];
            string opC = monkey.Value.Operation.Split(" ")[2];
            switch (opB)
            {
                case "+":
                    if (int.TryParse(opC, out int a)){
                        newWorryLevel = worryLevel + a;
                    }
                    else
                    {
                        newWorryLevel = worryLevel + worryLevel;
                    }
                    break;
                case "-":
                    if (int.TryParse(opC, out int b))
                    {
                        newWorryLevel = worryLevel - b;
                    }
                    else
                    {
                        newWorryLevel = worryLevel - worryLevel;
                    }
                    break;
                case "*":
                    if (int.TryParse(opC, out int c))
                    {
                        newWorryLevel = worryLevel * c;
                    }
                    else
                    {
                        newWorryLevel = worryLevel * worryLevel;
                    }
                    break;
                default:
                    Console.WriteLine("ERROR in operation!");
                    break;
            }
            Console.WriteLine($"Inspects item {worryLevel}, new worry level is {newWorryLevel}");
            worryLevel = newWorryLevel;

            //Gets bored of item
            worryLevel = (int)Math.Floor((float)worryLevel / 3);
            Console.WriteLine($"Gets bored of item, new worry level is {worryLevel}");

            if(worryLevel % monkey.Value.Test == 0)
            {
                Console.WriteLine($"Is true: Throw to monkey {monkey.Value.IsTrue}");
                monkeyDictionary[monkey.Value.IsTrue].Items.Add(worryLevel);
            }
            else
            {
                Console.WriteLine($"Is false: Throw to monkey {monkey.Value.IsFalse}");
                monkeyDictionary[monkey.Value.IsFalse].Items.Add(worryLevel);
            }
        }
        monkeyDictionary[monkey.Key].Items.Clear();
        monkeyDictionary[monkey.Key].NumberOfInspectedItems += numberOfInspectedItems;
    }
    roundCounter++;
    Console.WriteLine();
}

//Find the two monkeys with most inspected items
List<int> inspections = new List<int>();
foreach (KeyValuePair<int, Monkey> item in monkeyDictionary)
{
    Console.WriteLine($"Monkey ID: {item.Value.MonkeyID}: Inspected {item.Value.NumberOfInspectedItems} items");
    inspections.Add(item.Value.NumberOfInspectedItems);
}
int MaxA = inspections.Max();
inspections.Remove(inspections.Max());
int MaxB = inspections.Max();
Console.WriteLine($"Monkey Business after {numberOfRounds} is {MaxA * MaxB}");