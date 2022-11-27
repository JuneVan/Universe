namespace Universe.Domain
{
    public class EntityHelper
    {
        public static Type GetKeyType<TEntity>()
        {
            return GetKeyType(typeof(TEntity));
        }
        public static Type GetKeyType(Type entityType)
        {
            foreach (var interfaceType in entityType.GetTypeInfo().GetInterfaces())
            {
                if (interfaceType.GetTypeInfo().IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(IEntity<>))
                {
                    return interfaceType.GenericTypeArguments[0];
                }
            }
            throw new UniverseException($"无法找到实体类型`{entityType}`的主键类型。");
        }
    }
}
