namespace SampleApp.Web.Infrastructure.Extensions
{
    using System;
    using System.ComponentModel;
    using System.Reflection;
     
    public static class EnumExtensions
    {
        public static string GetEnumTextRepresentation(this Enum enumValue)
        {
            FieldInfo enumFieldInfo = enumValue
                .GetType()
                .GetField(enumValue.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])enumFieldInfo
                .GetCustomAttributes(
                    attributeType: typeof(DescriptionAttribute),
                    inherit: false);

            if (attributes != null &&
                attributes.Length > 0)
            {
                return attributes[0].Description;
            } 
            else
            {
                return enumValue.ToString();
            }
        }
    }
}
