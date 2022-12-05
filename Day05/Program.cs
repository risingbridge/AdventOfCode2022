using Day05;

List<string> input = File.ReadAllLines("./input.txt").ToList();
List<string> unfilteredCrates = new List<string>();
List<string> unfilteredInstructions = new List<string>();
List<Stack<char>> CrateStacks = new List<Stack<char>>();
Queue<BasicInstruction> InstructionQueue = new Queue<BasicInstruction>();


IngestInput();
PopulateCrateStack();
PopulateInstructionQueue();

void IngestInput()
{
    unfilteredCrates.Clear();
    unfilteredInstructions.Clear();
    bool isCrates = true;
    foreach (string inputLine in input)
    {
        if (string.IsNullOrWhiteSpace(inputLine))
        {
            isCrates = false;
        }
        if (!string.IsNullOrWhiteSpace(inputLine))
        {
            if (isCrates)
            {
                unfilteredCrates.Add(inputLine);
            }
            else
            {
                unfilteredInstructions.Add(inputLine);
            }
        }
    }
}
void PopulateCrateStack()
{
    CrateStacks.Clear();
    int numberOfStacks = int.Parse($"{unfilteredCrates.Last()[unfilteredCrates.Last().Length - 2]}");
    string numberString = unfilteredCrates.Last().ToString();
    unfilteredCrates.RemoveAt(unfilteredCrates.Count - 1);
    //Console.WriteLine($"We have {unfilteredCrates.Count} crate-lines and {unfilteredInstructions.Count} instruction-lines");
    //Console.WriteLine($"Number of crate stacks: {numberOfStacks}");

    for (int i = 0; i < numberOfStacks; i++)
    {
        CrateStacks.Add(new Stack<char>());
    }

    for (int i = 0; i < numberString.Length; i++)
    {
        if (!string.IsNullOrWhiteSpace(numberString[i].ToString()))
        {
            for (int z = unfilteredCrates.Count - 1; z >= 0; z--)
            {
                int stackId = int.Parse(numberString[i].ToString()) - 1;
                if (!string.IsNullOrWhiteSpace(unfilteredCrates[z][i].ToString()))
                {
                    CrateStacks[stackId].Push(unfilteredCrates[z][i]);
                }
            }
        }
    }

    //Console.WriteLine("Crate Stacks populated");
    foreach (var item in CrateStacks)
    {
        //Console.WriteLine(item.Peek());
    }
}

void PopulateInstructionQueue()
{
    InstructionQueue.Clear();
    foreach (var item in unfilteredInstructions)
    {
        string[] tmpString = item.ToString().Replace("move ", "").Replace(" from ", ",").Replace(" to ", ",").Split(",");
        int cratesToMove = int.Parse(tmpString[0]);
        int moveFrom = int.Parse(tmpString[1]);
        int moveTo = int.Parse(tmpString[2]);
        BasicInstruction instruction = new BasicInstruction(cratesToMove, moveFrom, moveTo);
        InstructionQueue.Enqueue(instruction);
    }
    //Console.WriteLine("Instruction Queue populated.");
    //Console.WriteLine(InstructionQueue.Peek());
}

//Solve part 1
while(InstructionQueue.Count > 0)
{
    BasicInstruction currentInstruction = InstructionQueue.Dequeue();
    for (int i = 0; i < currentInstruction.NumberOfCratesToMove; i++)
    {
        char temp = CrateStacks[currentInstruction.From - 1].Pop();
        CrateStacks[currentInstruction.To - 1].Push(temp);
    }
}
Console.WriteLine($"Part One: ");
foreach (var item in CrateStacks)
{
    Console.Write(item.Peek());
}
Console.WriteLine();

//Solve Part 2
IngestInput();
PopulateCrateStack();
PopulateInstructionQueue();

while (InstructionQueue.Count > 0)
{
    BasicInstruction currentInstruction = InstructionQueue.Dequeue();
    Stack<char> tempStack = new Stack<char>();
    for (int i = 0; i < currentInstruction.NumberOfCratesToMove; i++)
    {
        tempStack.Push(CrateStacks[currentInstruction.From - 1].Pop());
        //CrateStacks[currentInstruction.To - 1].Push(temp);
    }
    while(tempStack.Count > 0)
    {
        CrateStacks[currentInstruction.To - 1].Push(tempStack.Pop());
    }
}
Console.WriteLine($"Part Two: ");
foreach (var item in CrateStacks)
{
    Console.Write(item.Peek());
}
Console.WriteLine();