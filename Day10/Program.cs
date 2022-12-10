List<string> input = File.ReadAllLines("./input.txt").ToList();

int cycleCounter = 0;
int regX = 1;
int signalStrength = 0;
int pixelPosition = 0;
List<char> pixels = new List<char>();


foreach (var item in input)
{
    if(item == "noop")
    {
        cycleCounter++;
        PrintAndCalculate();
        CalculatePixel();
    }
    else
    {
        cycleCounter++;
        PrintAndCalculate();
        CalculatePixel();
        cycleCounter++;
        PrintAndCalculate();
        CalculatePixel();
        regX += int.Parse(item.Split(" ")[1]);
    }
}

Console.WriteLine($"Part one: {signalStrength}");
PrintScreen();

void PrintAndCalculate()
{
    if(cycleCounter == 20 || cycleCounter == 60 || cycleCounter == 100 || cycleCounter == 140 || cycleCounter == 180 || cycleCounter == 220)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        signalStrength += (cycleCounter * regX);
    }
    Console.WriteLine($"Cycle: {cycleCounter}\n" +
        $"X: {regX} \t Signal strength: {cycleCounter * regX}\n");
    Console.ResetColor();
}

void CalculatePixel()
{
    if(pixelPosition > 39)
    {
        pixelPosition = cycleCounter % 39 - 1;
    }
    int xValue = regX;
    if(xValue == pixelPosition || xValue - 1 == pixelPosition || xValue + 1 == pixelPosition)
    {
        pixels.Add('#');
    }
    else
    {
        pixels.Add('.');
    }
    pixelPosition++;
    if(pixelPosition >= 40)
    {
        pixelPosition = 0;
    }
}

void PrintScreen()
{
    int pixelCount = 0;
    foreach (var item in pixels)
    {
        if(item == '#')
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }
        Console.Write(item);
        Console.ResetColor();
        if(pixelCount == 39)
        {
            Console.WriteLine();
            pixelCount = 0;
        }
        else
        {
            pixelCount++;
        }
    }
}