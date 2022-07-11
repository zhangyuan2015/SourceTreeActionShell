namespace SourceTreeActionShell
{
    [AttributeUsage(AttributeTargets.Field)]
    public class CommandTypeInfoAttribute : Attribute
    {
        public CommandTypeInfoAttribute(string command, string description)
        {
            Info = new CommandTypeInfo(command, description);
        }

        public CommandTypeInfoAttribute(string command, string description, CommandParam[] pparams)
        {
            Info = new CommandTypeInfo(command, description, pparams);
        }

        public CommandTypeInfo Info { get; set; }
    }

    public class CommandTypeInfo
    {
        public CommandTypeInfo(string command, string description)
        {
            this.Command = command;
            this.Description = description;
        }

        public CommandTypeInfo(string command, string description, CommandParam[] pparams)
        {
            this.Command = command;
            this.Description = description;
            this.Params = pparams;
        }

        public string Command { get; }

        public string Description { get; }

        public CommandParam[]? Params { get; }
    }
}