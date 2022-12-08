using Day08;

List<string> input = File.ReadAllLines("./input.txt").ToList();
//Loading the data into a 2D array
int xSize = input[0].Length;
int ySize = input.Count;
int[,] treeGrid = new int[xSize, ySize];
List<Vector2> VisibleTrees = new List<Vector2>();
for (int y = 0; y < ySize; y++)
{
	for (int x = 0; x < xSize; x++)
	{
		treeGrid[y, x] = int.Parse(input[y][x].ToString());
	}
}
//Find the ones visible from the left
for (int y = 0; y < treeGrid.GetLength(0); y++)
{
    int talestTree = int.MinValue;
    for (int x = 0; x < treeGrid.GetLength(1); x++)
    {
        if (treeGrid[y, x] > talestTree)
        {
            talestTree = treeGrid[y, x];
            if (!VisibleTrees.Contains(new Vector2(x, y)))
            {
                VisibleTrees.Add(new Vector2(x, y));
            }
        }
    }
}
//Find the ones visible from the right
for (int y = 0; y < treeGrid.GetLength(0); y++)
{
    int talestTree = int.MinValue;
    for (int x = treeGrid.GetLength(1) - 1; x > -1; x--)
    {
        if (treeGrid[y, x] > talestTree)
        {
            talestTree = treeGrid[y, x];
            if (!VisibleTrees.Contains(new Vector2(x, y)))
            {
                VisibleTrees.Add(new Vector2(x, y));
            }
        }
    }
}
//Find the ones visible from the top
for (int x = 0; x < treeGrid.GetLength(1); x++)
{
    int talestTree = int.MinValue;
    for (int y = 0; y < treeGrid.GetLength(0); y++)
    {
        if (treeGrid[y,x] > talestTree)
        {
            talestTree = treeGrid[y,x];
            if (!VisibleTrees.Contains(new Vector2(x, y)))
            {
                VisibleTrees.Add(new Vector2(x, y));
            }
        }
    }
}
//Find the ones visible from the bottom
for (int x = 0; x < treeGrid.GetLength(1); x++)
{
    int talestTree = int.MinValue;
    for (int y = treeGrid.GetLength(0) - 1; y >= 0; y--)
    {
        if (treeGrid[y, x] > talestTree)
        {
            talestTree = treeGrid[y, x];
            if (!VisibleTrees.Contains(new Vector2(x, y)))
            {
                VisibleTrees.Add(new Vector2(x, y));
            }
        }
    }
}
//Printing score for part one
Console.WriteLine($"Part one: {VisibleTrees.Count}");

Dictionary<Vector2, double> ScenicScores = new Dictionary<Vector2, double>();
for (int y = 0; y < treeGrid.GetLength(0); y++)
{
    for (int x = 0; x < treeGrid.GetLength(1); x++)
    {
        Vector2 pos = new Vector2(x, y);
        if(!ScenicScores.ContainsKey(pos))
        {
            ScenicScores.Add(pos, FindScenicScore(pos));
        }
    }
}
double highestScenicScore = double.MinValue;
Vector2 highestPos = new Vector2(0, 0);
foreach (KeyValuePair<Vector2, double> item in ScenicScores)
{
    if(item.Value > highestScenicScore)
    {
        highestScenicScore = item.Value;
        highestPos = item.Key;
    }
}
Console.WriteLine($"Part two: {highestScenicScore}");
//Printing the treegrid
PrintTreeArray();

//Functions
//Function to print the array, coloring the visible trees.
void PrintTreeArray()
{
    for (int y = 0; y < treeGrid.GetLength(0); y++)
    {
        for (int x = 0; x < treeGrid.GetLength(1); x++)
        {
			if(VisibleTrees.Contains(new Vector2(x, y)))
			{
				Console.ForegroundColor = ConsoleColor.Green;
			}
            if(highestPos.X == x && highestPos.Y == y)
            {
                Console.BackgroundColor = ConsoleColor.Red;
            }
            Console.Write(treeGrid[y, x].ToString());
			Console.ResetColor();
        }
        Console.WriteLine();
    }
}
//Calculating the scenic scores for each position, for part two.
double FindScenicScore(Vector2 pos)
{
    double score = 0;
    int up = 0;
    int down = 0;
    int left = 0;
    int right = 0;
    //Find scenic score up
    for (int y = pos.Y - 1; y >= 0; y--)
    {
        Vector2 testPos = new Vector2(pos.X, y);
        if (treeGrid[testPos.Y,testPos.X] >= treeGrid[pos.Y,pos.X])
        {
            up++;
            break;
        }
        else
        {
            up++;
        }
    }
    //Find scenic score down
    for (int y = pos.Y + 1; y < treeGrid.GetLength(0); y++)
    {
        Vector2 testPos = new Vector2(pos.X, y);
        if (treeGrid[testPos.Y, testPos.X] >= treeGrid[pos.Y,pos.X])
        {
            down++;
            break;
        }
        else
        {
            down++;
        }
    }
    //Find scenic score left
    for (int x = pos.X - 1; x >= 0; x--)
    {
        Vector2 testPos = new Vector2(x, pos.Y);
        if (treeGrid[testPos.Y, testPos.X] >= treeGrid[pos.Y,pos.X])
        {
            left++;
            break;
        }
        else
        {
            left++;
        }
    }
    //Find scenic score right
    for (int x = pos.X + 1; x < treeGrid.GetLength(1); x++)
    {
        Vector2 testPos = new Vector2(x, pos.Y);
        if (treeGrid[testPos.Y, testPos.X] >= treeGrid[pos.Y,pos.X])
        {
            right++;
            break;
        }
        else
        {
            right++;
        }
    }
    //Return the score
    //Console.WriteLine($"{pos}: U:{up} L:{left} D:{down} R:{right}");
    return up * down * left * right;
}