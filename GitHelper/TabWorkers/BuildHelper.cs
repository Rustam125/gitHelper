using GitHelper.Helpers;
using GitHelper.Models;
using System.Collections.Generic;
using System.IO;

namespace GitHelper.TabWorkers
{
    public static class BuildHelper
    {
        /// <summary>
        /// Сборка проекта .NET.
        /// </summary>
        /// <param name="path">Путь к проекту.</param>
        public static List<string> BuildNetProject(string path) =>
            ExecuteCommands(
                new List<string>()
                {
                    $"cd {path}",
                    $"dotnet build"
                });

        /// <summary>
        /// Сборка проекта npm.
        /// </summary>
        /// <param name="path">Путь к проекту.</param>
        public static List<string> BuildNpmProject(string path) =>
            ExecuteCommands(
                new List<string>()
                {
                    $"cd {path}",
                    $"npm run build"
                });

        /// <summary>
        /// Выполнение кастомного скрипта.
        /// </summary>
        /// <param name="path">Путь к проекту.</param>
        /// <param name="scriptArguments">Аргументы скрипта.</param>
        public static List<string> BuildCustomScript(string path, string scriptArguments)
        {
            if (string.IsNullOrEmpty(path))
            {
                ShowMessageHelper.ShowError("Не удалось определить путь до исполняемого файла.");
                return new List<string>();
            }

            string ext = Path.GetExtension(path);
            ext = string.IsNullOrEmpty(ext) ? string.Empty : ext.ToLower();
            path += string.IsNullOrEmpty(scriptArguments) ? string.Empty : $" {scriptArguments}";

            if (ext == ".bat")
            {
                return ExecuteCommands(new List<string>() { path });
            }
            else if (ext == ".ps1")
            {
                return ExecuteCommands(new List<string>() { path }, true);
            }
            else
            {
                ShowMessageHelper.ShowError("Не удалось определить расширение исполняемого файла.");
                return new List<string>();
            }
        }

        private static List<string> ExecuteCommands(List<string> commands, bool invokeByPowerShell = false)
        {
            CommandHandlerResultModel commandHandlerResult = CommandHandler.CmdInvoke(commands, invokeByPowerShell);

            if (commandHandlerResult.HasErrors)
            {
                ShowMessageHelper.ShowError(commandHandlerResult.Errors);
                return new List<string>();
            }
            else
            {
                return commandHandlerResult.Result;
            }
        }
    }
}
