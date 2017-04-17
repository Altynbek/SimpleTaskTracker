﻿using System.ComponentModel;
using System.Reflection;

namespace TaskTracker.Classes.Helpers
{
    public static class AttributeHelper
    {
        public static string DescriptionAttr<T>(this T source)
        {
            if (source == null)
            {
                return null;
            }
            FieldInfo fi = source.GetType().GetField(source.ToString());
            if (fi == null)
            {
                return null;
            }
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            return source.ToString();
        }
    }
}