using System.Numerics;

List<string> input = File.ReadAllLines("./input.txt").ToList();
int mapHeight = input.Count;
int mapWidth = input[0].Length;

char[,] Map = new char[mapHeight,mapWidth];
Vector2 startingPos = new Vector2(0, 0);
Vector2 endPos = new Vector2(0, 0);
for (int y = 0; y < Map.GetLength(0); y++)
{
	for (int x = 0; x < Map.GetLength(1); x++)
	{
		char c = input[y][x];
		if(c == 'S')
		{
			startingPos = new Vector2(x, y);
			Console.ForegroundColor = ConsoleColor.Green;
		}else if(c == 'E')
		{
			endPos = new Vector2(x, y);
			Console.ForegroundColor = ConsoleColor.Red;
		}
		Map[y, x] = c;
		Console.Write(c);
		Console.ResetColor();
	}
	Console.WriteLine();
}