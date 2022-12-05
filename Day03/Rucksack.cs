namespace Day03;
public class Rucksack
{
    public Rucksack(string compartmentA, string compartmentB)
    {
        CompartmentA = compartmentA;
        CompartmentB = compartmentB;
    }

    public string CompartmentA { get; set; }
    public string CompartmentB { get; set; }
    public List<char> DoubleItems { get; set; }
    public string FullString { get {
            return $"{CompartmentA}{CompartmentB}";
        } }
    public char SecurityBadge { get; set; }

    public override string ToString()
    {
        return $"{CompartmentA} {CompartmentB}";
    }
}
