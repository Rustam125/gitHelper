using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using GitHelper.Models;
using GitHelper.Helpers;

namespace GitHelper
{
    public class GitFiles
    {
        #region Fields

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Public methods

        public static List<string> ShowChangesBetweenBranches(
            string pathToRepo,
            string forkName_1,
            string forkName_2,
            WayToAccessGitEnum wayToAccessGit)
        {
            List<string> result = new List<string>();

            if (CheckRepoPath(pathToRepo) == false ||
                CheckForkName(forkName_1, 1) == false ||
                CheckForkName(forkName_2, 2) == false)
            {
                return result;
            }

            if (wayToAccessGit == WayToAccessGitEnum.CMD)
            {
                result = ShowChangesBetweenBranchesCmd(pathToRepo, forkName_1, forkName_2);
            }
            else
            {
                result = ShowChangesBetweenBranchesPowerShell(pathToRepo, forkName_1, forkName_2);
            }

            return result;
        }

        public static void CopyModifiedFilesToDirectory(
            List<string> paths,
            string repoPath)
        {
            string path = SelectFolderPath();
            List<string> errors = new List<string>();

            if (CheckRepoPath(path) == false ||
                string.IsNullOrEmpty(repoPath) ||
                paths == null ||
                paths.Any() == false)
            {
                return;
            }

            DialogResult dialogResult = MessageBox.Show(
                "Очистить каталог перед копированием файлов?",
                "Очистка каталога",
                MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                Directory.Delete(path, true);
                Directory.CreateDirectory(path);
            }

            foreach (string filePath in paths)
            {
                try
                {
                    string repoFilePath = Path.Combine(repoPath, filePath);
                    string targetFilePath = Path.Combine(path, filePath);
                    DirectoryInfo repoDirectoryInfo = new DirectoryInfo(Path.GetDirectoryName(repoFilePath));
                    DirectoryInfo targetDirectoryInfo = new DirectoryInfo(Path.GetDirectoryName(targetFilePath));

                    if (repoDirectoryInfo.Exists == false)
                    {
                        _logger.Warn($"Копирование файлов. Отсуствует директория {repoDirectoryInfo.FullName}.");
                        continue;
                    }

                    if (File.Exists(repoFilePath) == false)
                    {
                        _logger.Warn($"Копирование файлов. Отсуствует файл {repoFilePath}.");
                        continue;
                    }

                    if (targetDirectoryInfo.Exists == false)
                    {
                        _logger.Info($"Копирование файлов. Создание каталога {targetDirectoryInfo.FullName}");
                        targetDirectoryInfo.Create();
                    }

                    File.Copy(repoFilePath, targetFilePath, true);
                }
                catch (Exception ex)
                {
                    errors.Add(ex.Message);
                    continue;
                }
            }

            if (errors.Any())
            {
                ErrorMessage($"Копирование файлов завнршено с ошибками: {Environment.NewLine}{string.Join(Environment.NewLine, errors)}");
            }
            else
            {
                ShowMessageHelper.ShowInfo("Копирование файлов завершено!");
            }
        }

        public static string SelectFolderPath()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    return fbd.SelectedPath;
                }
            }

            return string.Empty;
        }

        public static void ErrorMessage(string text)
        {
            _logger.Error(text);
            ShowMessageHelper.ShowError(text);
        }

        #endregion

        #region Private methods

        private static bool CheckRepoPath(string pathToRepo)
        {
            bool value = string.IsNullOrEmpty(pathToRepo) == false && Directory.Exists(pathToRepo);

            if (value == false)
            {
                ErrorMessage("Указан некорректный путь к репозиторию!");
            }

            return value;
        }

        private static bool CheckForkName(string forkName, int forkNumber)
        {
            bool value = string.IsNullOrEmpty(forkName) == false;

            if (value == false)
            {
                ErrorMessage($"Некорректно указано наименование ветки #{forkNumber}");
            }

            return value;
        }

        private static List<string> ShowChangesBetweenBranchesPowerShell(
            string pathToRepo,
            string forkName_1,
            string forkName_2)
        {
            CommandHandlerResultModel commandHandlerResult = CommandHandler.PowerShellInvoke(
                new List<string>()
                {
                    $"cd {pathToRepo}",
                    $"git diff --name-only --encoding=utf8 {forkName_1}..{forkName_2}"
                });

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

        private static List<string> ShowChangesBetweenBranchesCmd(
            string pathToRepo,
            string forkName_1,
            string forkName_2)
        {
            CommandHandlerResultModel commandHandlerResult = CommandHandler.CmdInvoke(
                new List<string>()
                {
                    $"cd {pathToRepo}",
                    $"git diff --name-only --encoding=utf8 {forkName_1}..{forkName_2}"
                });

            if (commandHandlerResult.HasErrors)
            {
                ShowMessageHelper.ShowError(commandHandlerResult.Errors);
                return new List<string>();
            }

            return commandHandlerResult.Result;
        }

        #endregion
    }
}