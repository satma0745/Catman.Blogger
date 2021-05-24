namespace Catman.Blogger.Persistence.Migrator.Extensions
{
    using System;
    using System.Linq;

    internal static class ReflectionExtensions
    {
        public static bool Implements<TInterface>(this Type type) =>
            type.GetInterfaces().Contains(typeof(TInterface));

        public static bool HasAttribute<TAttribute>(this Type type)
            where TAttribute : Attribute =>
            Attribute.IsDefined(type, typeof(TAttribute));

        public static TAttribute GetAttribute<TAttribute>(this Type type)
            where TAttribute : Attribute =>
            (TAttribute) Attribute.GetCustomAttribute(type, typeof(TAttribute));

        public static T Instantiate<T>(this Type type) =>
            (T) Activator.CreateInstance(type);
    }
}
