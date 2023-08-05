using Penna.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Penna.Core.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            string displayName;
            displayName = enumValue.GetType()
                .GetMember(enumValue.ToString())
                .FirstOrDefault()
                .GetCustomAttribute<DisplayAttribute>()?
                .GetName();

            if (String.IsNullOrEmpty(displayName))
            {
                displayName = enumValue.ToString();
            }

            return displayName;
        }

        public static string ToName(this Enum value)
        {
            return value.GetDisplayName();
        }


        public static List<EnumListItem> GetAttributeList(this Type enumType)
        {
            var result = new List<EnumListItem>();
            foreach (Enum enumItem in Enum.GetValues(enumType))
            {
                result.Add(new EnumListItem { Value = enumItem.ToString(), Text = GetDisplayName(enumItem) });
            }

            return result;
        }
    }

    
}
