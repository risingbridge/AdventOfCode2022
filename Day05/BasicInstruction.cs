namespace Day05;
public class BasicInstruction
{
    public BasicInstruction(int numberOfCratesToMove, int from, int to)
    {
        NumberOfCratesToMove = numberOfCratesToMove;
        From = from;
        To = to;
    }

    public int NumberOfCratesToMove { get; set; }
    public int From { get; set; }
    public int To { get; set; }

    public override string ToString()
    {
        string returnString = $"move {NumberOfCratesToMove} from {From} to {To}";
        return returnString;
    }
}
