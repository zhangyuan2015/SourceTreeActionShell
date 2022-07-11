using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceTreeActionShell
{
    public static class MergeRequest
    {
        public static void OpenMergeRequest(string projectUrl, string source_branch, string target_branch)
        {
            string url = $"{projectUrl}/-/merge_requests/new?merge_request%5Bsource_branch%5D={source_branch}&merge_request%5Btarget_branch%5D={target_branch}";
            OpenDefaultBrowserUrl(url);
        }

        public static void OpenDefaultBrowserUrl(string url)
        {
            System.Diagnostics.Process.Start("explorer.exe", url);
        }
    }
}