using System.Reflection;

namespace SourceTreeActionShell
{
    public enum CommandType
    {
        [CommandTypeInfo("--Help", "帮助")]
        Help,
        [CommandTypeInfo("--merge-request", "发起一个合并请求", new[] { CommandParam.ProjectPath })]
        MergeRequest
    }

    public static class CommandTypeEx
    {
        public static CommandTypeInfo GetInfo<T>(this T enumValue)
        {
            string value = enumValue.ToString();
            FieldInfo? field = enumValue.GetType()?.GetField(value);
            if (field == null)
                throw new Exception("未设置 CommandTypeInfo");

            object[] objs = field.GetCustomAttributes(typeof(CommandTypeInfoAttribute), false);
            if (objs == null || objs.Length == 0)
                throw new Exception("未设置 CommandTypeInfo");

            return ((CommandTypeInfoAttribute)objs[0]).Info;
        }

        public static List<CommandTypeInfo> GetInfos()
        {
            List<CommandTypeInfo> resList = new List<CommandTypeInfo>();
            foreach (var e in Enum.GetValues(typeof(CommandType)))
            {
                resList.Add(GetInfo((CommandType)e));
            }
            return resList;
        }
    }
}