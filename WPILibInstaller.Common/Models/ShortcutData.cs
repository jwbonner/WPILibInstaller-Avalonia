namespace WPILibInstaller.Models
{
    public class ShortcutData
    {
        public bool IsAdmin { get; set; }
        public List<ShortcutInfo> DesktopShortcuts { get; set; } = new List<ShortcutInfo>();
        public List<NewEnvVariable> NewEnvironmentalVariables { get; set; } = new();
        public List<ShortcutInfo> StartMenuShortcuts { get; set; } = new List<ShortcutInfo>();
        public List<AddedPathVariable> AddToPath { get; set; } = new();
        public List<FileAssociationInfo> FileAssociations { get; set; } = new();
    }

    public class ShortcutInfo
    {
        public ShortcutInfo() { }

        public ShortcutInfo(string path, string name, string description, string iconLocation)
        {
            Path = path;
            Name = name;
            Description = description;
            IconLocation = iconLocation;
        }

        public string Path { get; set; } = "";
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string IconLocation { get; set; } = "";
    }

    public class NewEnvVariable
    {
        public string Name { get; set; } = "";
        public string Value { get; set; } = "";
    }

    public class AddedPathVariable
    {
        public string Path { get; set; } = "";
    }

    public class FileAssociationInfo
    {
        public FileAssociationInfo() { }

        public FileAssociationInfo(string extension, string progId, string name, string executablePath, string iconPath)
        {
            Extension = extension;
            ProgId = progId;
            Name = name;
            ExecutablePath = executablePath;
            IconPath = iconPath;
        }

        public string Extension { get; set; } = "";
        public string ProgId { get; set; } = "";
        public string Name { get; set; } = "";
        public string ExecutablePath { get; set; } = "";
        public string IconPath { get; set; } = "";
    }
}
