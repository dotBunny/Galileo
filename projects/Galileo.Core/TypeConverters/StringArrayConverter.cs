using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Galileo.Core.TypeConverters
{
    /// <summary>
    /// Custom String Array Converter used with PropertyGrids
    /// </summary>
    class StringArrayConverter : ArrayConverter
    {
        /// <summary>
        /// Convert to desired destination type
        /// </summary>
        /// <param name="context">Conversion context.</param>
        /// <param name="culture">Conversion culture.</param>
        /// <param name="value">Source object.</param>
        /// <param name="destinationType">Requested destination type.</param>
        /// <returns>The requested converted object.</returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        { 
            // If we're looking for a string (presumably the shown string on the Property grid, make a flat list)
            if ( destinationType == typeof(string))
            {
                string[] list = (string[])value;
                StringBuilder titleBuilder = new StringBuilder();
                foreach(string s in list)
                {
                    titleBuilder.Append(s);
                    titleBuilder.Append(", ");
                }
                return titleBuilder.ToString().TrimEnd(new char[] { ' ', ','});
            }

            // Return default convertion
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
