using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace World.Application.Behaviors
{
    public static class ObjectProperty
    {
        public static string GetAllProperties(this object? obj)
        {
            if(obj is null)
            {
                return "";
            }
            obj = new();
            return string.Join(" ", obj.GetType()
                                        .GetProperties()
                                        .Select(prop => prop.GetValue(obj)));
        }
    }
}
