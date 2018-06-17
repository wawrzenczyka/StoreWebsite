using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace StoreWebsite
{
    public static class EnumExtender
    {
        public static string GetEnumDescription(this Enum value)
        {
            var enumType = value.GetType();

            var field = enumType.GetField(value.ToString());

            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length == 0 ? value.ToString() : ((DescriptionAttribute)attributes[0]).Description;
        }
    }
}
