namespace SourceTreeActionShell
{
    public enum CommandParam
    {
        [CommandTypeInfo("ProjectPath", "项目路径")]
        ProjectPath,
        [CommandTypeInfo("TargetBranch", "目标分支")]
        TargetBranch
    }
}