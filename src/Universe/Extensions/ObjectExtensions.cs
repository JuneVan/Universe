namespace Universe.Extensions
{
    public static class ObjectExtensions
    {
        public static T As<T>(this object obj)
            where T : class
        {
            return (T)obj;
        }

        public static T? To<T>(this object obj)
            where T : struct
        {
            if (obj == null)
                return null;
            if (typeof(T) == typeof(Guid) || typeof(T) == typeof(TimeSpan))
            {
#pragma warning disable CS8604 // 引用类型参数可能为 null。
                return (T?)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(obj.ToString());
#pragma warning restore CS8604 
            }
            if (typeof(T).IsEnum)
            {
                if (Enum.IsDefined(typeof(T), obj))
                {
#pragma warning disable CS8604 // 引用类型参数可能为 null。
                    return (T)Enum.Parse(typeof(T), obj.ToString());
#pragma warning restore CS8604 
                }
                else
                {
                    throw new ArgumentException($"Enum type undefined '{obj}'.");
                }
            }
            return (T)Convert.ChangeType(obj, typeof(T), CultureInfo.InvariantCulture);
        }

        public static bool IsIn<T>(this T item, params T[] list)
        {
            return list.Contains(item);
        }
    }
}
