using Day07;

List<string> inputList = File.ReadAllLines("./debug.txt").ToList();
Queue<string> inputQueue = new Queue<string>();
List<BasicFile> fileList = new List<BasicFile>();
foreach (var item in inputList)
{
    inputQueue.Enqueue(item);
}

string workingDirectory = string.Empty;
while(inputQueue.Count > 0)
{
    string input = inputQueue.Dequeue();
    if (input[0] == '$')
    {
        //Console.WriteLine($"Running command: {input}");
        input = input.Replace("$ ", "");
        if(input.Substring(0,2) == "cd")
        {
            input = input.Replace("cd ", "");
            //Console.WriteLine($"Changing directory to {input}");
            if(input == "..")
            {
                string[] directoryArray = workingDirectory.Split("/");
                if(directoryArray.Length > 1)
                {
                    workingDirectory = string.Empty;
                    for (int i = 0; i < directoryArray.Length - 1; i++)
                    {
                        if (directoryArray[i] == "root")
                        {
                            workingDirectory += "root";
                        }
                        else
                        {
                            workingDirectory += "/" + directoryArray[i];
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Cant go past root!");
                }
            }
            else if(input == "/")
            {
                workingDirectory += "root";
            }
            else
            {
                workingDirectory += $"/{input}";
            }
            Console.WriteLine($"Working directory: {workingDirectory}");
        }else if(input.Substring(0,2) == "ls")
        {
            while(inputQueue.Peek()[0] != '$')
            {
                string output = inputQueue.Dequeue();
                int fileSize = int.Parse(output.Split(" ")[0]);
                string filename = output.Split(" ")[1];
                BasicFile file = new BasicFile(filename, fileSize, workingDirectory);
                Console.WriteLine($"OUTPUT: {output}");
                if(inputQueue.Count == 0)
                {
                    break;
                }
            }
        }
        Console.WriteLine();
    }
}