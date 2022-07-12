using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace SourceTreeActionShell
{
    public static class MergeRequest
    {
        public static void OpenMergeRequest(string projectUrl, string source_branch, string target_branch, string gitScheme)
        {
            string url;
            if (gitScheme == EnumGitScheme.GitHub.ToString())
            {
                url = $"{projectUrl}/compare/{target_branch}...{source_branch}";
            }
            else
            {
                url = $"{projectUrl}/merge_requests/new?merge_request[source_branch]={source_branch}&merge_request[target_branch]={target_branch}";
            }
            Console.WriteLine("MRurl=" + url);
            OpenDefaultBrowserUrl(url);
        }

        public static void OpenDefaultBrowserUrl(string url)
        {
            //计算机\HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice
            //ChromeHTML
            var userChoiceProgid = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice\").GetValue("Progid").ToString();

            //计算机\HKEY_CLASSES_ROOT\ChromeHTML\shell\open\command
            //计算机\HKEY_CLASSES_ROOT\MSEdgeHTM\shell\open\command
            //计算机\HKEY_CLASSES_ROOT\IE.HTTP\shell\open\command
            RegistryKey? key = Registry.ClassesRoot.OpenSubKey(userChoiceProgid + @"\shell\open\command");
            //"C:\Program Files\Google\Chrome\Application\chrome.exe" --single-argument %1
            string commandStr = key?.GetValue("")?.ToString() ?? "";

            Regex reg = new Regex("\"([^\"]+)\"");
            MatchCollection matchs = reg.Matches(commandStr);
            string filename = "";
            if (matchs.Count > 0)
            {
                filename = matchs[0].Groups[1].Value;
                System.Diagnostics.Process.Start(filename, url);
            }
        }
    }
}