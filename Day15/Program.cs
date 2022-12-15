using System.Drawing;
using System.Numerics;

bool isDebug = false;

string inputPath = "./debug.txt";
bool debugImg = false;
if (isDebug)
{
    inputPath = "./debug.txt";
    debugImg = true;
}
List<string> input = File.ReadAllLines(inputPath).ToList();
List<Sensor> sensors = new List<Sensor>();
int maxX = 0;
int maxY = 0;
int minX = 0;
int minY = 0;

foreach (string s in input)
{
    //Sensor at x=2, y=18: closest beacon is at x=-2, y=15
    string edited = s.Replace("Sensor at x=", "").Replace(" y=", "").Replace(" closest beacon is at x=", "").Replace(" y=", "");
    string sensorString = edited.Split(":")[0];
    string beaconString = edited.Split(":")[1];
    Vector2 sensorPos = new Vector2(int.Parse(sensorString.Split(",")[0]), int.Parse(sensorString.Split(",")[1]));
    Vector2 beaconPos = new Vector2(int.Parse(beaconString.Split(",")[0]), int.Parse(beaconString.Split(",")[1]));
    Sensor sensor = new Sensor(sensorPos, beaconPos, ManhattanDistance(sensorPos, beaconPos));
    sensors.Add(sensor);
    if(sensorPos.X > maxX)
    {
        maxX = (int)sensorPos.X;
    }
    if(beaconPos.X > maxX)
    {
        maxX = (int)beaconPos.X;
    }
    if(sensorPos.Y > maxY)
    {
        maxY = (int)sensorPos.Y;
    }
    if(beaconPos.Y > maxY)
    {
        maxY = (int)beaconPos.Y;
    }
    if(sensorPos.X < minX)
    {
        minX = (int)sensorPos.X;
    }
    if(beaconPos.X < minX)
    {
        minX = (int)beaconPos.X;
    }
    if(sensorPos.Y < minY)
    {
        minY = (int)sensorPos.Y;
    }
    if(beaconPos.Y < minY)
    {
        minY = (int)beaconPos.Y;
    }
}

if (debugImg)
{
    Bitmap img = new Bitmap(maxX + Math.Abs(minX) + 1, maxY + Math.Abs(minY) + 1);
    for (int y = 0; y < img.Height; y++)
    {
        for (int x = 0; x < img.Width; x++)
        {
            img.SetPixel(x, y, Color.White);
        }
    }
    foreach (Sensor item in sensors)
    {
        Vector2 beaconPos = item.Beacon;
        Vector2 sensorPos = item.Position;
        beaconPos.X += Math.Abs(minX);
        beaconPos.Y += Math.Abs(minY);
        sensorPos.X += Math.Abs(minX);
        sensorPos.Y += Math.Abs(minY);
        //img.SetPixel((int)beaconPos.X, (int)beaconPos.Y, Color.Red);
        img.SetPixel((int)sensorPos.X, (int)sensorPos.Y, Color.Green);
        img.SetPixel((int)beaconPos.X, (int)beaconPos.Y, Color.Red);
    }
    img.Save("./test.bmp");
}

//Testing every position, to see if it is closer to a sensor then a beacon
for (int y = minY - 1; y < maxY + 1; y++)
{
    for (int x = minX - 1; x < maxX + 1; x++)
    {
        bool hasPrinted = false;
        foreach (Sensor item in sensors)
        {
            Vector2 current = new Vector2(x, y);
            if(current == item.Position)
            {
                Console.Write("S");
                hasPrinted = true;
            }
            else if(current == item.Beacon)
            {
                Console.Write("B");
                hasPrinted = true;
            }
        }
        if (!hasPrinted)
        {
            Console.Write(".");
        }
    }
    Console.WriteLine();
}

int ManhattanDistance(Vector2 vector1, Vector2 vector2)
{
    int distance = 0;
    distance += (int)Math.Abs(vector1.X - vector2.X);
    distance += (int)Math.Abs(vector1.Y - vector2.Y);
    return distance;
}


internal class Sensor
{
    public Sensor(Vector2 position, Vector2 beacon, int manhattenDistance)
    {
        Position = position;
        Beacon = beacon;
        ManhattenDistance = manhattenDistance;
    }

    public Vector2 Position { get; set; }
    public Vector2 Beacon { get; set; }
    public int ManhattenDistance { get; set; }
}