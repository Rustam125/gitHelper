using System.Configuration;
using System.Collections.Specialized;

namespace Configurations
{
    public class Handler
    {
        public void Test()
        {
            string sAttr = ConfigurationManager.AppSettings.Get("Key0");
        }
    }
}
