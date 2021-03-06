﻿using System.Configuration;

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

        public static int ArtistsPageCount
        {
            get { return int.Parse(ConfigurationManager.AppSettings["ArtistsPageCount"]); }
        }

        public static int SongsPageCount
        {
            get { return int.Parse(ConfigurationManager.AppSettings["SongsPageCount"]); }
        }

        public static int InstrumentRecommendationImportance
        {
            get { return int.Parse(ConfigurationManager.AppSettings["InstrumentRecommendationImportance"]); }
        }

        public static int TagRecommendationImportance
        {
            get { return int.Parse(ConfigurationManager.AppSettings["TagRecommendationImportance"]); }
        }

        public static int LikeRecommendationImportance
        {
            get { return int.Parse(ConfigurationManager.AppSettings["LikeRecommendationImportance"]); }
        }
    }
}