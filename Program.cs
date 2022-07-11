using Microsoft.Win32;
using System.Diagnostics;

namespace SourceTreeActionShell
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CommandType.Help.GetInfo();
            if (args == null || args.Length == 0 || (args.FirstOrDefault()?.Equals(CommandType.Help.GetInfo().Command, StringComparison.OrdinalIgnoreCase) ?? false))
            {
                Help.OutputHelpContent();
            }
            else if (args.FirstOrDefault()?.Equals(CommandType.MergeRequest.GetInfo().Command, StringComparison.OrdinalIgnoreCase) ?? false)
            {
                var projectPath = getArgsString(args, CommandParam.ProjectPath.GetInfo().Command);
                //MergeRequest.OpenMergeRequest("", "", "");
            }
            else
            {
                Console.WriteLine("未知命令");
            }
            Console.ReadLine();
        }

        private static string getArgsString(string[] args, string paramName)
        {
            var argss = args.FirstOrDefault(a => a.Contains(paramName)).Split("=", StringSplitOptions.RemoveEmptyEntries);
            if (argss.Length != 2)
                throw new Exception($"请传入参数：{paramName}");
            return argss[1];
        }
    }
}