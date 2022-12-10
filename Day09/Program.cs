using System.Numerics;

List<string> input = File.ReadAllLines("./debug.txt").ToList();

Queue<Tuple<Direction, int>> Instructions = new Queue<Tuple<Direction, int>>();
foreach (var item in input)
{
    Direction dir = Direction.up;
    switch(item.Split(" ")[0])
    {
        case "U":
            dir = Direction.up;
            break;
        case "R":
            dir = Direction.right;
            break;
        case "D":
            dir = Direction.down;
            break;
        case "L":
            dir = Direction.left;
            break;
        default:
            dir = Direction.up;
            break;
    }
    int distance = int.Parse(item.Split(" ")[1]);
    Tuple<Direction, int> instruction = new Tuple<Direction, int>(dir, distance);
    Instructions.Enqueue(instruction);
}
Console.WriteLine($"{Instructions.Count} instructions loaded");

Vector2 headPosition = new Vector2(0, 0);
Vector2 previousHeadPosition = headPosition;
Vector2 tailPosition = new Vector2(0, 0);
List<Vector2> tailPositions = new List<Vector2>();

while(Instructions.Count > 0)
{
    headPosition = MoveRopeHead(Instructions.Dequeue(), headPosition);
}

//Print movements
int minX = int.MaxValue;
int minY = int.MaxValue;
int maxX = int.MinValue;
int maxY = int.MinValue;
foreach (var item in tailPositions)
{
    if(item.X < minX)
    {
        minX = (int)item.X;
    }
    if(item.Y < minY)
    {
        minY = (int)item.Y;
    }
    if(item.X > maxX)
    {
        maxX = (int)item.X;
    }
    if(item.Y > maxY)
    {
        maxY = (int)item.Y;
    }
}
Console.WriteLine($"Will print movements now");
for (int y = minY; y <= maxY; y++)
{
    for (int x = minX; x <= maxX; x++)
    {
        if (tailPositions.Contains(new Vector2(x, y)))
        {
            Console.Write("#");
        }
        else
        {
            Console.Write(".");
        }
    }
    Console.WriteLine();
}


Vector2 MoveRopeHead(Tuple<Direction, int> input, Vector2 current)
{
    Vector2 result = new Vector2(0, 0);
    previousHeadPosition = current;

    for (int i = 0; i < input.Item2; i++)
    {
        switch (input.Item1)
        {
            case Direction.up:
                result.Y++;
                break;
            case Direction.right:
                result.X++;
                break;
            case Direction.down:
                result.Y--;
                break;
            case Direction.left:
                result.X--;
                break;
        }
        tailPosition = MoveRoapTail(headPosition, tailPosition, previousHeadPosition);
    }

    Vector2 newPosition = new Vector2(current.X + result.X, current.Y + result.Y);
    previousHeadPosition = newPosition;
    return newPosition;
}

Vector2 MoveRoapTail(Vector2 headPosition, Vector2 current, Vector2 prevHead)
{
    Vector2 result = new Vector2(0, 0);

    bool isTouchingHead = IsTailTouchingHead(headPosition, current);
    if (!isTouchingHead)
    {
        Console.WriteLine($"Tail moves to {prevHead}");
        tailPositions.Add(prevHead);
        return prevHead;
    }

    return result;
}

bool IsTailTouchingHead(Vector2 headPosition, Vector2 tailPosition)
{
    if(headPosition == tailPosition)
    {
        return true;
    }

    if(headPosition.X == tailPosition.X)
    {
        if(headPosition.Y == tailPosition.Y + 1 || headPosition.Y == tailPosition.Y - 1)
        {
            return true;
        }
    }
    if(headPosition.Y == tailPosition.Y)
    {
        if (headPosition.X == tailPosition.X + 1 || headPosition.X == tailPosition.X - 1)
        {
            return true;
        }
    }
    return false;
}


enum Direction
{
    up,
    right,
    down,
    left
}