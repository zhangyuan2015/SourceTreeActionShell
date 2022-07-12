using System.Reflection;

namespace SourceTreeActionShell
{
    public enum EnumCommandType
    {
        [CommandInfo("--Help", "帮助")]
        Help,
        [CommandInfo("--merge-request", "发起一个合并请求", new[] { EnumCommandParam.ProjectPath, EnumCommandParam.TargetBranch, EnumCommandParam.GitScheme })]
        MergeRequest
    }
}