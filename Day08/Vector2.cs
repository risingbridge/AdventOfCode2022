namespace Day08;
public class Vector2 : IEquatable<Vector2>
{
    public Vector2(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; set; }
    public int Y { get; set; }

    public bool Equals(Vector2? other)
    {
        if(other is null)
        {
            return false;
        }
        if(other.X == this.X && other.Y == this.Y)
        {
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        return $"{X}:{Y}";
    }
}
