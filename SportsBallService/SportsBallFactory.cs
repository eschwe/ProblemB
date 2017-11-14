using System;
using System.Configuration;

namespace SportsBallService
{
    public static class SportsBallFactory
    {
        public static ISportsBallService GetProvider()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            AppSettingsSection appSettings = (AppSettingsSection)config.GetSection("appSettings");
            string type = appSettings.Settings["type"].Value;
            string dataSource = appSettings.Settings["dataSource"].Value;
           
            switch (type)
            {
                case "xml":
                    return new SportsBallXmlProvider(dataSource);
                default:
                    throw new ArgumentException("Could not determine the data source type");
            }
        }
    }
}
