namespace Day07;
public class BasicDirectory
{
    public BasicDirectory(BasicDirectory parentDirectory, string name)
    {
        ParentDirectory = parentDirectory;
        Name = name;
    }

    public BasicDirectory? ParentDirectory { get; set; }
    public string Name { get; set; }

    public override string ToString()
    {
        string parentName = string.Empty;
        if(ParentDirectory != null)
        {
            parentName = ParentDirectory.Name;
        }
        BasicDirectory parent = ParentDirectory;
        while(parent is not null)
        {
            string tmp = parentName;
            parentName = $"{parent.Name}/{tmp}";
            parent = parent.ParentDirectory;
        }

        return parentName + "/" + Name;
    }

}
