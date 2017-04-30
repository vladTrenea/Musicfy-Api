using System.Configuration;

namespace Musicfy.Infrastructure.Configs
{
    public static class Config
    {
        public static string LogFilePath
        {
            get { return ConfigurationManager.AppSettings["LogFilePath"]; }
        }

        public static string GraphDbUrl
        {
            get { return ConfigurationManager.AppSettings["GraphDbUrl"]; }
        }

        public static string GraphDbUser
        {
            get { return ConfigurationManager.AppSettings["GraphDbUser"]; }
        }

        public static string GraphDbPassword
        {
            get { return ConfigurationManager.AppSettings["GraphDbPassword"]; }
        }
    }
}