﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace URL_App_Launcher_Console
{
    internal class Settings
    {
        const String SettingsFile = "appsettings.json";

        IConfiguration config;
        public Settings()
        {
            String fullPathToSettings = AppDomain.CurrentDomain.BaseDirectory + SettingsFile;
            if (!File.Exists(fullPathToSettings))
            {
                return;
            }
            config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(SettingsFile).Build();
        }
        public bool isDebug()
        {
            if (config == null) return false;
            return config.GetValue<bool>("isDebug");
        }

        public String grabKeyValueFromSettings(String keyToGet)
        {
            if (config == null) throw new ArgumentNullException("config null, Mostlikely due to missing or invalid appsettings.json");
            var valuesSection = config.GetSection("KeysToPath");
            foreach (IConfigurationSection section in valuesSection.GetChildren())
            {
                Console.WriteLine("key=" + section.GetValue<string>("key") + ", " + "path=" + section.GetValue<string>("path"));
                var key = section.GetValue<string>("key");
                if (keyToGet.Equals(key))
                {
                    return section.GetValue<string>("path");
                }
            }
            return "";
        }
    }
}
