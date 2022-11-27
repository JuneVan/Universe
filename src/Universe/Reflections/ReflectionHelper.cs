namespace Universe.Reflections
{
    public static class ReflectionHelper
    {
        public static TAttribute GetAttributeOrDefault<TAttribute>(MemberInfo? memberInfo, TAttribute defaultValue)
          where TAttribute : class => memberInfo?.GetCustomAttributes(true).OfType<TAttribute>()?.FirstOrDefault()
                   ?? memberInfo?.ReflectedType?.GetTypeInfo().GetCustomAttributes(true).OfType<TAttribute>().FirstOrDefault()
                   ?? defaultValue;

        public static TAttribute? GetAttribute<TAttribute>(PropertyInfo propertyInfo)
          where TAttribute : class
        {
            return propertyInfo.GetCustomAttributes(true).OfType<TAttribute>().FirstOrDefault();
        }

        /// <summary>
        /// 检查指定类型是否为泛型类型的继承类型
        /// </summary>
        /// <param name="givenType">指定类型</param>
        /// <param name="genericType">泛型类型</param>
        /// <returns></returns>
        public static bool IsAssignableToGenericType(Type givenType, Type genericType)
        {
            var givenTypeInfo = givenType.GetTypeInfo();

            if (givenTypeInfo.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
            {
                return true;
            }

            foreach (var interfaceType in givenType.GetInterfaces())
            {
                if (interfaceType.GetTypeInfo().IsGenericType && interfaceType.GetGenericTypeDefinition() == genericType)
                {
                    return true;
                }
            }

            if (givenTypeInfo.BaseType == null)
            {
                return false;
            }

            return IsAssignableToGenericType(givenTypeInfo.BaseType, genericType);
        }
    }
}
