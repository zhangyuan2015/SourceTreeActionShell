using System.Reflection;

namespace SourceTreeActionShell
{
    public static class EnumHelper
    {
        public static CommandInfo GetCommandInfo<T>(this T enumValue)
        {
            string value = enumValue.ToString();
            FieldInfo? field = enumValue.GetType()?.GetField(value);
            if (field == null)
                throw new Exception("未设置 CommandTypeInfo");

            object[] objs = field.GetCustomAttributes(typeof(CommandInfoAttribute), false);
            if (objs == null || objs.Length == 0)
                throw new Exception("未设置 CommandTypeInfo");

            return ((CommandInfoAttribute)objs[0]).Info;
        }

        public static List<CommandInfo> GetCommandInfos<T>()
        {
            List<CommandInfo> resList = new List<CommandInfo>();
            foreach (var e in Enum.GetValues(typeof(T)))
            {
                resList.Add(GetCommandInfo((T)e));
            }
            return resList;
        }
    }
}