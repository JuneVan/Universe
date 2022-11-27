namespace Universe.Reflections
{
    public static class EnumHelper
    {
        /// <summary>
        /// 枚举转换成字典
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <returns></returns>
        public static Dictionary<string, int> ToDictionary<TEnum>()
            where TEnum : struct
        {
            FieldInfo[] fields = typeof(TEnum).GetFields(BindingFlags.Static | BindingFlags.Public);
            Dictionary<string, int> result = new Dictionary<string, int>();
            foreach (FieldInfo fieldInfo in fields)
            {
                DescriptionAttribute? attributes = fieldInfo.GetCustomAttribute<DescriptionAttribute>();
                if (attributes != null)
                    result.Add(attributes.Description, (int)(fieldInfo.GetValue(null) ?? -1));
                else
                    result.Add(fieldInfo.Name, (int)(fieldInfo.GetValue(null) ?? -1));
            }
            return result;
        }
    }
}
