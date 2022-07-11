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
                MergeRequest.OpenMergeRequest();
            }
            else
            {
                Console.WriteLine("未知命令");
            }
            Console.ReadLine();
        }
    }
}