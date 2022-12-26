List<string> input = File.ReadAllLines("./input.txt").ToList();
Dictionary<string, ValveRoom> rooms = new();
string currentRoom = string.Empty;
//Creates the rooms based on the input
foreach (string line in input)
{
    string name = line.Split(" ")[1];
    if (string.IsNullOrEmpty(currentRoom)){
        currentRoom = name;
    };
    int flowRate = int.Parse(line.Split(" ")[4].Replace("rate=", "").Replace(";", ""));
    string leadsString = line.Split(";")[1];
    ValveRoom room = new ValveRoom(name, flowRate, leadsString);
    rooms.Add(name, room);
    //Console.WriteLine($"Created new room with name: {name}");
}

//Connects the room based on the leads-to-string
foreach (KeyValuePair<string, ValveRoom> item in rooms)
{
    string leadsToString = item.Value.LeadsTo.Replace(" tunnels lead to valves ", "").Replace(" tunnel leads to valve ", "").Replace(" ", "");
    if (leadsToString.Length == 2)
    {
        rooms[item.Key].Childs.Add(leadsToString);
        //Console.WriteLine($"Connection from {item.Key} to {leadsToString} established");
    }
    else
    {
        string[] leadstonames = leadsToString.Split(",");
        foreach (var s in leadstonames)
        {
            ValveRoom room = rooms[item.Key];
            room.Childs.Add(s);
            rooms[item.Key] = room;
            //Console.WriteLine($"Connection from {item.Key} to {s} established");
        }
    }
}

//Calculate the best route
int minuteCounter = 1;
List<string> openValves = new();
int releasedPressure = 0;
while(minuteCounter <= 30)
{
    Console.WriteLine($"== Minute {minuteCounter} ==");
    Console.WriteLine($"{OpenValvesString(openValves)}");


    Console.WriteLine();
    minuteCounter++;
}

string OpenValvesString(List<string> valves)
{
    string returnString = "No valves are open.";
    if (valves.Count <= 0)
    {
        return returnString;
    }
    else return "lol";
}

//Room-class
internal class ValveRoom
{
    public ValveRoom(string name, int flowRate, string? leadsTo)
    {
        Name = name;
        FlowRate = flowRate;
        LeadsTo = leadsTo;
    }

    public string Name { get; set; }
    public int FlowRate { get; set; }
    public List<string> Childs { get; set; } = new List<string>();
    public string? LeadsTo { get; set; }
}