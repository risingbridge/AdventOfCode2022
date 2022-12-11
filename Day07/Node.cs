namespace Day07;
public class Node
{
    public string Name { get; set; }
    public Node Parent { get; set; }
    public List<Node> Children { get; set; }
    public int Size { get; set; }
}
