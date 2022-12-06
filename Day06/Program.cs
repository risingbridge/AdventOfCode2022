List<string> input = File.ReadAllLines("./input.txt").ToList();

Queue<char> datastream = new Queue<char>();

//Solve part 1
foreach (char c in input[0])
{
    datastream.Enqueue(c);
}

bool foundMarker = false;
int counter = 0;
List<char> readDatastream = new List<char>();

while (!foundMarker && datastream.Count > 0)
{
    char c = datastream.Dequeue();
    readDatastream.Add(c);
    if(readDatastream.Count >= 4)
    {
        //Check if last 4 is not the same
        Stack<char> subList = new Stack<char>();
        for (int i = 4; i > 0; i--)
        {
            subList.Push(readDatastream[readDatastream.Count - i]);
        }
        bool isMarker = true;
        while(subList.Count > 0)
        {
            char z = subList.Pop();
            if (subList.Contains(z))
            {
                isMarker = false;
            }
        }
        if (isMarker)
        {
            foundMarker = true;
        }
    }
    counter++;
}

if (foundMarker)
{
    Console.WriteLine($"Part One: {counter}");
}

//Solve part 2
datastream.Clear();
foreach (char c in input[0])
{
    datastream.Enqueue(c);
}

foundMarker = false;
counter = 0;
readDatastream.Clear();

while (!foundMarker && datastream.Count > 0)
{
    char c = datastream.Dequeue();
    readDatastream.Add(c);
    if (readDatastream.Count >= 14)
    {
        //Check if last 4 is not the same
        Stack<char> subList = new Stack<char>();
        for (int i = 14; i > 0; i--)
        {
            subList.Push(readDatastream[readDatastream.Count - i]);
        }
        bool isMarker = true;
        while (subList.Count > 0)
        {
            char z = subList.Pop();
            if (subList.Contains(z))
            {
                isMarker = false;
            }
        }
        if (isMarker)
        {
            foundMarker = true;
        }
    }
    counter++;
}

if (foundMarker)
{
    Console.WriteLine($"Part Two: {counter}");
}