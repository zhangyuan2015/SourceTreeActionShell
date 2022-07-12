using Microsoft.Win32;
using System.Diagnostics;

namespace SourceTreeActionShell
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //args = new string[] { "--merge-request", "ProjectPath=D:\\Projects\\LmsCode\\Develop\\lms-api", "TargetBranch=test", "GitScheme=GitLab" };

            EnumCommandType.Help.GetCommandInfo();
            if (args == null || args.Length == 0 || (args.FirstOrDefault()?.Equals(EnumCommandType.Help.GetCommandInfo().Command, StringComparison.OrdinalIgnoreCase) ?? false))
            {
                Help.OutputHelpContent();
            }
            else if (args.FirstOrDefault()?.Equals(EnumCommandType.MergeRequest.GetCommandInfo().Command, StringComparison.OrdinalIgnoreCase) ?? false)
            {
                var targetBranch = GetArgsString(args, EnumCommandParam.TargetBranch.GetCommandInfo().Command);
                if (string.IsNullOrWhiteSpace(targetBranch))
                {
                    throw new Exception($"请传入参数:{EnumCommandParam.TargetBranch.ToString()}");
                }
                targetBranch = targetBranch.Trim();
                Console.WriteLine("TargetBranch=" + targetBranch);

                var projectPath = GetArgsString(args, EnumCommandParam.ProjectPath.GetCommandInfo().Command);
                if (string.IsNullOrWhiteSpace(projectPath))
                {
                    throw new Exception($"请传入参数:{EnumCommandParam.ProjectPath}");
                }
                projectPath = projectPath.Trim();
                Console.WriteLine("ProjectPath=" + projectPath);

                if (!Directory.Exists(projectPath))
                {
                    throw new Exception($"代码目录不存在：{projectPath}");
                }

                var gitScheme = GetArgsString(args, EnumCommandParam.GitScheme.GetCommandInfo().Command);
                gitScheme = string.IsNullOrWhiteSpace(EnumGitScheme.GitLab.ToString()) ? "" : gitScheme.Trim();
                Console.WriteLine("GitScheme=" + gitScheme);

                string currentBranch = GetCurrentBranch(projectPath);
                if (string.IsNullOrEmpty(currentBranch))
                {
                    throw new Exception($"未获取到当前分支：{currentBranch}");
                }
                Console.WriteLine("CurrentBranch=" + currentBranch);

                string tmpStr = "(push)";
                string remotes = new GitUtility(projectPath).ExcuteGitCommand("remote -v") ?? "";
                remotes = remotes.Replace("\n", "^").Split(new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault(a => a.Contains(tmpStr)) ?? "";
                remotes = remotes.Replace(tmpStr, "");
                remotes = remotes.Replace("\t", "^").Split(new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault(a => a.Contains("git@") || a.Contains("https://") || a.Contains("http://"))?.Trim() ?? "";
                if (string.IsNullOrEmpty(remotes))
                {
                    throw new Exception($"未配置Git远端：{remotes}");
                }
                if (remotes.IndexOf("git@") == 0)
                {
                    remotes = remotes.Replace(":", "/").Replace("git@", "https://").Replace(".git", "");
                }
                else
                {
                    remotes = remotes.Replace(".git", "");
                }
                Console.WriteLine("Remote=" + remotes);
                MergeRequest.OpenMergeRequest(remotes, currentBranch, targetBranch, gitScheme);
            }
            else
            {
                Console.WriteLine("未知命令");
            }
            //Console.ReadLine();
        }

        private static string GetCurrentBranch(string codeDirectory)
        {
            try
            {
                string currentBranch = new GitUtility(codeDirectory).ExcuteGitCommand("symbolic-ref --short -q HEAD");
                currentBranch = currentBranch.Replace("\n", "").Replace("\t", "").Replace("\r", "").Trim();
                return currentBranch;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private static string GetArgsString(string[] args, string paramName)
        {
            var argss = args.FirstOrDefault(a => a.Contains(paramName, StringComparison.CurrentCultureIgnoreCase))?.Split("=", StringSplitOptions.RemoveEmptyEntries);
            if (argss == null || argss.Length != 2 || string.IsNullOrWhiteSpace(argss[1]))
                return "";
            return argss[1].Trim();
        }
    }
}