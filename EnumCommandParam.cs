namespace SourceTreeActionShell
{
    public enum EnumCommandParam
    {
        [CommandInfo("ProjectPath", "项目路径")]
        ProjectPath,
        [CommandInfo("TargetBranch", "目标分支")]
        TargetBranch,
        [CommandInfo("GitScheme", "Git方案 [GitLab (默认) / GitHub")]
        GitScheme
    }
}