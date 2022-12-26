using System.Numerics;

List<string> input = File.ReadAllLines("./debug.txt").ToList();
List<string> rockInput = File.ReadAllLines("./Rocks.txt").ToList();
List<List<string>> rockIngest = new List<List<string>>();
List<string> temp = new List<string>();
foreach (var item in rockInput)
{
    if (!string.IsNullOrWhiteSpace(item))
    {
        temp.Add(item);
    }
    else
    {
        rockIngest.Add(new List<string>(temp));
        temp.Clear();
    }
}
rockIngest.Add(new List<string>(temp));
temp.Clear();
foreach (var item in rockIngest)
{
    
}


internal class Rock
{
    public List<Vector2> Faces { get; set; }
}