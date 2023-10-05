using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace World.Unit.Test.Helper
{
    public static class UtilityTestMethods
    {
        public static Dictionary<string, object?> GetAllGetterProperty<T>(this T obj)
        {
            var qry = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(p => p.CanRead);

            Dictionary<string, object?> properties = new();
            foreach (var property in qry)
            {
                string propertyName = property.Name;
                object? value =  typeof(T).GetProperty(propertyName).GetValue(obj, null);
                properties[propertyName] = value;
            }
            return properties;
        }
    }
}
