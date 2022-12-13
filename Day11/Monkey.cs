using System.Numerics;

namespace Day11;
public class Monkey
{
    public Monkey(int monkeyID, List<BigInteger> items, string operation, int test, int isTrue, int isFalse)
    {
        MonkeyID = monkeyID;
        Items = items;
        Operation = operation;
        Test = test;
        IsTrue = isTrue;
        IsFalse = isFalse;
    }

    public int MonkeyID { get; set; }
    public List<BigInteger> Items { get; set; }
    public string Operation { get; set; }
    public int Test { get; set; }
    public int IsTrue { get; set; }
    public int IsFalse { get; set; }
    public int NumberOfInspectedItems { get; set; } = 0;
}
