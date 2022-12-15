using System.Drawing;
using System.Drawing.Text;
using System.Numerics;

List<string> input = File.ReadAllLines("./input.txt").ToList();
Vector2 sandEmitter = new Vector2(500, 0);
//List of positions with rocks. Will be used to populate the 2D-array later
List<Vector2> rockPositions = new List<Vector2>();
//Find the width and height of data-set to populate an 2D-array
int minHeight = 0;
int maxHeight = 0;
int minWidth = 0;
int maxWidth = 0;
foreach (var item in input)
{
    string[] strings = item.Split(" -> ");
    //Find max values
	foreach (var s in strings)
	{
		int x = int.Parse(s.Split(",")[0]);
        int y = int.Parse(s.Split(",")[1]);
        if(x > maxWidth)
        {
            maxWidth = x;
        }
        if(y > maxHeight)
        {
            maxHeight = y;
        }
    }
    //Find all points with rocks
    for (int i = 1; i < strings.Length; i++)
    {
        string startString = strings[i - 1];
        string endString = strings[i];
        int startX = int.Parse(startString.Split(",")[0]);
        int startY = int.Parse(startString.Split(",")[1]);
        int endX = int.Parse(endString.Split(",")[0]);
        int endY = int.Parse(endString.Split(",")[1]);
        Vector2 start = new Vector2(startX, startY);
        Vector2 end = new Vector2(endX, endY);
        //Console.WriteLine($"Line from {start} to {end}");
        bool isVertical = false;
        if(start.Y != end.Y)
        {
            isVertical = true;
        }
        if (isVertical)
        {
            if(end.Y < start.Y)
            {
                Vector2 temp = new Vector2(end.X, end.Y);
                end = start;
                start = temp;
            }
            for (int y = (int)start.Y; y <= end.Y; y++)
            {
                Vector2 rockPos = new Vector2(start.X, y);
                rockPositions.Add(rockPos);
            }
        }
        else
        {
            if(end.X < start.X)
            {
                Vector2 temp = end;
                end = start;
                start = temp;
            }
            for (int x = (int)start.X; x <= end.X; x++)
            {
                Vector2 rockPos = new Vector2(x, start.Y);
                rockPositions.Add(rockPos);
            }
        }
    }
}
//Console.WriteLine($"There are {rockPositions.Count} rocks");
MapNode[,] rockMap = new MapNode[maxHeight + 10, maxWidth + 10];
for (int y = 0; y < rockMap.GetLength(0); y++)
{
    for (int x = 0; x < rockMap.GetLength(1); x++)
    {
        rockMap[y, x] = new MapNode(NodeType.Air);
    }
}
foreach (var item in rockPositions)
{
    rockMap[(int)item.Y, (int)item.X] = new MapNode(NodeType.Rock);
}

CreateBitmap("CaveScan", rockMap.GetLength(1), rockMap.GetLength(0));



//Time to simulate the sand
///End-condition: One piece of sand is below the lowest rock - free falling
//A unit of sand always falls down one step if possible.
//If the tile immediately below is blocked (by rock or sand), the unit of sand attempts to instead
//move diagonally one step down and to the left. If that tile is blocked, the unit of sand attempts
//to instead move diagonally one step down and to the right.
//Sand keeps moving as long as it is able to do so, at each step trying to move down,
//then down-left, then down-right. If all three possible destinations are blocked,
//the unit of sand comes to rest and no longer moves,
//at which point the next unit of sand is created back at the source.

bool isFreefalling = false;
int sandCounter = 0;
int stepCounter = 0;
while (!isFreefalling)
{
    Vector2 sandPosition = sandEmitter;
    sandCounter++;
    bool isSandStopped = false;
    while (!isSandStopped)
    {
        stepCounter++;
        //Check if sand can move down
        MapNode straightDown = rockMap[(int)sandPosition.Y + 1, (int)sandPosition.X];
        MapNode downLeft = rockMap[(int)sandPosition.Y + 1, (int)sandPosition.X - 1];
        MapNode downRight = rockMap[(int)sandPosition.Y + 1, (int)sandPosition.X + 1];
        if(straightDown.Type == NodeType.Air)
        {
            sandPosition.Y++;
        }
        else if(downLeft.Type == NodeType.Air)
        {
            sandPosition.Y++;
            sandPosition.X--;
        }else if(downRight.Type == NodeType.Air)
        {
            sandPosition.Y++;
            sandPosition.X++;
        }
        else
        {
            isSandStopped = true;
        }
        if (sandPosition.Y > maxHeight)
        {
            isFreefalling = true;
            break;
        }
    }
    rockMap[(int)sandPosition.Y, (int)sandPosition.X].Type = NodeType.Sand;
    //CreateBitmap(sandCounter.ToString());
}


Console.WriteLine($"Part one: {sandCounter - 1}");



//PART TWO
//Resets and expands rockmap
rockMap = new MapNode[maxHeight + 500, maxWidth + 500];
for (int y = 0; y < rockMap.GetLength(0); y++)
{
    for (int x = 0; x < rockMap.GetLength(1); x++)
    {
        rockMap[y, x] = new MapNode(NodeType.Air);
    }
}
foreach (var item in rockPositions)
{
    rockMap[(int)item.Y, (int)item.X] = new MapNode(NodeType.Rock);
}

for (int x = 0; x < rockMap.GetLength(1); x++)
{
    int y = maxHeight + 2;
    rockMap[y,x].Type = NodeType.Rock;
}

CreateBitmap("NewCaveScan", rockMap.GetLength(1), rockMap.GetLength(0));

bool isBlocked = false;
sandCounter = 0;
stepCounter = 0;
while (!isBlocked)
{
    Vector2 sandPosition = sandEmitter;
    sandCounter++;
    bool isSandStopped = false;
    while (!isSandStopped)
    {
        stepCounter++;
        //Check if sand can move down
        MapNode straightDown = rockMap[(int)sandPosition.Y + 1, (int)sandPosition.X];
        MapNode downLeft = rockMap[(int)sandPosition.Y + 1, (int)sandPosition.X - 1];
        MapNode downRight = rockMap[(int)sandPosition.Y + 1, (int)sandPosition.X + 1];
        if (straightDown.Type == NodeType.Air)
        {
            sandPosition.Y++;
        }
        else if (downLeft.Type == NodeType.Air)
        {
            sandPosition.Y++;
            sandPosition.X--;
        }
        else if (downRight.Type == NodeType.Air)
        {
            sandPosition.Y++;
            sandPosition.X++;
        }
        else
        {
            isSandStopped = true;
        }
        if(isSandStopped && sandPosition.X == 500 && sandPosition.Y == 0)
        {
            isBlocked = true;
            break;
        }
    }
    rockMap[(int)sandPosition.Y, (int)sandPosition.X].Type = NodeType.Sand;
    if(sandCounter % 100 == 0)
    {
        //CreateBitmap(sandCounter.ToString(), rockMap.GetLength(1), rockMap.GetLength(0));
    }
}

CreateBitmap(sandCounter.ToString(), rockMap.GetLength(1), rockMap.GetLength(0));
Console.WriteLine($"Part two: {sandCounter}");



void CreateBitmap(string name, int xSize, int ySize)
{
    Bitmap img = new Bitmap(xSize,ySize);
    for (int y = 0; y < rockMap.GetLength(0); y++)
    {
        for (int x = 0; x < rockMap.GetLength(1); x++)
        {
            if (rockMap[y, x].Type == NodeType.Rock)
            {
                img.SetPixel(x, y, Color.Black);
            }else if (rockMap[y,x].Type == NodeType.Sand)
            {
                img.SetPixel(x, y, Color.Green);
            }
            else
            {
                img.SetPixel(x, y, Color.White);
            }
        }
    }
    img.SetPixel((int)sandEmitter.X, (int)sandEmitter.Y, Color.Pink);
    img.Save($"./{name}.bmp");
}

internal class MapNode
{
    public MapNode(NodeType type)
    {
        Type = type;
    }

    public NodeType Type { get; set; }
}

internal enum NodeType
{
    Rock,
    Air,
    Sand
}