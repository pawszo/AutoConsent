using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoConsent
{
    public class Constants
    {
        public enum Keys
        {
            Directory, Records
        }
        public static string FullBodyPattern = @"^Dane\:[\n\t\r]+([^\r\n\t]*)[\n\t\r]+Telefon\:\s+(\d{5,})[\n\t\r]+E-mail\:\s+([^\r\n\t]*)[\r\n\t]*(.*)";
        public static string ConsentsPattern = @"(Zgadzam się\:\s[^\n\r\t]*)";


    }
}
