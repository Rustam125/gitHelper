using System.Configuration;
using System;
using NLog;
using System.IO;

namespace Configurations
{
    internal static class Handler
    {
        private const string _appSettingsFileName = "App.config";
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private static string ConfigurationPath { get => Path.Combine(Environment.CurrentDirectory, _appSettingsFileName); }

        /// <summary>
        /// Получение значение конфигурации по ключу <paramref name="key"/>.
        /// </summary>
        /// <typeparam name="T">Тип данных значения конфигурации.</typeparam>
        /// <param name="key">Ключ конфигурации.</param>
        /// <returns>Значение конфигурации в типе <typeparamref name="T"/>.</returns>
        public static T GetConfigurationValue<T>(string key) => ConvertTo<T>(GetAppSettingsValue(key));

        /// <summary>
        /// Установка значения <paramref name="value"/> в конфигурационный файл по ключу <paramref name="key"/>.
        /// </summary>
        /// <param name="key">Ключ конфигурации.</param>
        /// <param name="value">Значение конфигурации.</param>
        public static void SetConfigurationValue(string key, object value) => AddOrUpdateAppSettings(key, ConvertTo<string>(value));

        /// <summary>
        /// Приведение к типу.
        /// </summary>
        private static T ConvertTo<T>(object value)
        {
            if (value == null) return default;

            if (value is IConvertible)
            {
                Type type = typeof(T);
                Type underlyingType = Nullable.GetUnderlyingType(type);
                return (T)Convert.ChangeType(value, underlyingType ?? type);
            }

            return (T)value;
        }

        /// <summary>
        /// Получение значение конфигурации по ключу <paramref name="key"/>.
        /// </summary>
        /// <param name="key">Ключ конфигурации.</param>
        /// <returns>Значение конфигурации.</returns>
        private static string GetAppSettingsValue(string key)
        {
            try
            {
                var configFile = GetConfiguration();
                var settings = configFile.AppSettings.Settings;
                return settings[key]?.Value;
            }
            catch (ConfigurationErrorsException ex)
            {
                _logger.Error($"Error reading app settings. {ex.Message}. {ex.StackTrace}");
            }

            return null;
        }

        /// <summary>
        /// Сохранение настроек.
        /// </summary>
        /// <param name="key">Ключ конфигурации.</param>
        /// <param name="value">Значение конфигурации.</param>
        private static void AddOrUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = GetConfiguration();
                var settings = configFile.AppSettings.Settings;

                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }

                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException ex)
            {
                _logger.Error($"Error writing app settings. {ex.Message}. {ex.StackTrace}");
            }
        }

        private static Configuration GetConfiguration()
        {
            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap()
            {
                ExeConfigFilename = ConfigurationPath
            };

           return ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
        }
    }
}