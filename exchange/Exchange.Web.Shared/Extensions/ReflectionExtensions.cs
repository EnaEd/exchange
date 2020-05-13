using System;
using System.Reflection;

namespace Exchange.Web.Shared.Extensions
{
    public static class ReflectionExtensions
    {
        public static TAttribute GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            return Attribute.GetCustomAttribute(field, typeof(TAttribute)) as TAttribute;
        }
    }
}
