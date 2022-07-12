namespace SourceTreeActionShell
{
    [AttributeUsage(AttributeTargets.Field)]
    public class CommandInfoAttribute : Attribute
    {
        public CommandInfoAttribute(string command, string description)
        {
            Info = new CommandInfo(command, description);
        }

        public CommandInfoAttribute(string command, string description, EnumCommandParam[] pparams)
        {
            Info = new CommandInfo(command, description, pparams);
        }

        public CommandInfo Info { get; set; }
    }

    public class CommandInfo
    {
        public CommandInfo(string command, string description)
        {
            this.Command = command;
            this.Description = description;
        }

        public CommandInfo(string command, string description, EnumCommandParam[] pparams)
        {
            this.Command = command;
            this.Description = description;
            this.Params = pparams;
        }

        public string Command { get; }

        public string Description { get; }

        public EnumCommandParam[]? Params { get; }
    }
}