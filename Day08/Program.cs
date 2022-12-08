using Day08;

List<string> input = File.ReadAllLines("./debug.txt").ToList();
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
//Printing the array and the score for part one
PrintTreeArray();
Console.WriteLine($"Part one: {VisibleTrees.Count}");
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

    int x = pos.X;
    int y = pos.Y;

    return score;
}