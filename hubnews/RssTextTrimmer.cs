using System.Net;
using System.Windows.Data;
using System.Globalization;
using System.Text.RegularExpressions;

namespace hubnews
{
    public class RssTextTrimmer : IValueConverter
    {

        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            int maxLength = 100;
            int strLength = 0;
            string fixedString = "";

            //Remove HTML tags. 
            fixedString = Regex.Replace(value.ToString(), "<[^>]+>", string.Empty);

            //Remove newline characters.
            fixedString = fixedString.Replace("\r", "").Replace("\n", "");

            //Remove encoded HTML characters.
            fixedString = HttpUtility.HtmlDecode(fixedString);

            strLength = fixedString.ToString().Length;

            if (strLength == 0)
            {
                return null;
            }

            //Truncate the text if it is too long. 
            else if (strLength >= maxLength)
            {
                fixedString = fixedString.Substring(0, maxLength);
                fixedString = fixedString.Substring(0, fixedString.LastIndexOf(" "));
            }

            fixedString += "...";

            return fixedString;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
