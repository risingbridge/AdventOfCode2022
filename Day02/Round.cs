namespace Day02;
public class Round
{
    public Round(char opponent, char me)
    {
        Opponent = opponent;
        Me = me;
    }
    public char Opponent { get; set; }
    public char Me { get; set; }
}
