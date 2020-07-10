using System;
using System.ComponentModel;
using System.Linq;

namespace RIPE.CrossCutting.Extensions
{
    public static class EnumExtensions
    {
        public static string GetEnumDescription(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());

            var attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
            if (attributes != null && attributes.Any()) return attributes.First().Description;

            return value.ToString();
        }

        public static string GetEnumDisplayText(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());

            var attributes = fi.GetCustomAttributes(typeof(DisplayTextAttribute), false) as DisplayTextAttribute[];
            if (attributes != null && attributes.Any()) return attributes.First().DisplayText;

            return value.ToString();
        }
    }

    [AttributeUsage(AttributeTargets.All)]
    public class DisplayTextAttribute : Attribute
    {
        public DisplayTextAttribute(string displayText)
        {
            DisplayText = displayText;
        }

        public string DisplayText { get; }
    }
}
