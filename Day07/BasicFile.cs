namespace Day07;
public class BasicFile
{
    public BasicFile(string filename, int size, string parentDirectory)
    {
        Filename = filename;
        Size = size;
        ParentDirectory = parentDirectory;
    }

    public string Filename { get; set; }
    public int Size { get; set; }
    public string ParentDirectory { get; set; }
}
