namespace SourceTreeActionShell
{
    public static class Help
    {
        public static void OutputHelpContent()
        {
            var commandInfos = CommandTypeEx.GetInfos();
            foreach (var commandInfo in commandInfos)
            {
                Console.WriteLine($"{commandInfo.Command}    {commandInfo.Description} 参数：{commandInfo.Params != null ? string.Join("", commandInfo.Params.Select(a->a.)):""}");
            }
        }
    }
}