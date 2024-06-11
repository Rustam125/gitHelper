namespace Configurations
{
    public static class AppSettings
    {
        private const string _pathToRepoKey = "PathToRepoKey";
        private const string _firstBranchNameKey = "FirstBranchNameKey";
        private const string _secondBranchNameKey = "SecondBranchNameKey";
        private const string _pathToReleaseCatalogsKey = "PathToReleaseCatalogsKey";
        private const string _releaseCatalogsKey = "ReleaseCatalogsKey";
        private const string _pathToRepositoryNetProjectBuildKey = "PathToRepositoryNetProjectBuildKey";
        private const string _pathToRepositoryWebProjectBuildKey = "PathToRepositoryWebProjectBuildKey";
        private const string _pathToRepositoryCustomBuildKey = "PathToRepositoryCustomBuildKey";
        private const string _customScriptArgumentsKey = "CustomScriptArgumentsKey";

        /// <summary>
        /// Путь к репозиторию.
        /// </summary>
        public static string PathToRepo
        {
            get => Handler.GetConfigurationValue<string>(_pathToRepoKey);
            set => Handler.SetConfigurationValue(_pathToRepoKey, value);
        }

        /// <summary>
        /// Наименование ветки 1.
        /// </summary>
        public static string FirstBranchName
        {
            get => Handler.GetConfigurationValue<string>(_firstBranchNameKey);
            set => Handler.SetConfigurationValue(_firstBranchNameKey, value);
        }

        /// <summary>
        /// Наименование ветки 2.
        /// </summary>
        public static string SecondBranchName
        {
            get => Handler.GetConfigurationValue<string>(_secondBranchNameKey);
            set => Handler.SetConfigurationValue(_secondBranchNameKey, value);
        }

        /// <summary>
        /// Путь для формирования каталогов релиза.
        /// </summary>
        public static string PathToReleaseCatalogs
        {
            get => Handler.GetConfigurationValue<string>(_pathToReleaseCatalogsKey);
            set => Handler.SetConfigurationValue(_pathToReleaseCatalogsKey, value);
        }

        /// <summary>
        /// Массив каталогов.
        /// </summary>
        public static string[] ReleaseCatalogs
        {
            get
            {
                string catalogs = Handler.GetConfigurationValue<string>(_releaseCatalogsKey);
                return string.IsNullOrEmpty(catalogs) ? new string[0] : catalogs.Split(',');
            }
        }

        /// <summary>
        /// Путь к проекту .NET.
        /// </summary>
        public static string PathToRepositoryNetProjectBuild
        {
            get => Handler.GetConfigurationValue<string>(_pathToRepositoryNetProjectBuildKey);
            set => Handler.SetConfigurationValue(_pathToRepositoryNetProjectBuildKey, value);
        }

        /// <summary>
        /// Путь к проекту web (npm build).
        /// </summary>
        public static string PathToRepositoryWebProjectBuild
        {
            get => Handler.GetConfigurationValue<string>(_pathToRepositoryWebProjectBuildKey);
            set => Handler.SetConfigurationValue(_pathToRepositoryWebProjectBuildKey, value);
        }

        /// <summary>
        /// Путь к кастомному скрипту.
        /// </summary>
        public static string PathToRepositoryCustomBuild
        {
            get => Handler.GetConfigurationValue<string>(_pathToRepositoryCustomBuildKey);
            set => Handler.SetConfigurationValue(_pathToRepositoryCustomBuildKey, value);
        }

        /// <summary>
        /// Аргументы кастомного скрипта.
        /// </summary>
        public static string CustomScriptArguments
        {
            get => Handler.GetConfigurationValue<string>(_customScriptArgumentsKey);
            set => Handler.SetConfigurationValue(_customScriptArgumentsKey, value);
        }
    }
}