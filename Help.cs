namespace SourceTreeActionShell
{
    public static class Help
    {
        public static void OutputHelpContent()
        {
            var commandInfos = EnumHelper.GetCommandInfos<EnumCommandType>();
            foreach (var commandInfo in commandInfos)
            {
                List<string> paramInfos = new List<string>();
                if (commandInfo.Params != null)
                {
                    foreach (var param in commandInfo.Params)
                    {
                        var info = param.GetCommandInfo();
                        paramInfos.Add($"{info.Command}={info.Description}");
                    }
                }
                Console.WriteLine($"{commandInfo.Command}    {commandInfo.Description} 参数：{string.Join("; ", paramInfos)}");
            }
        }
    }
}