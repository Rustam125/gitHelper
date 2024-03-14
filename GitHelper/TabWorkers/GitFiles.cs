using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Windows.Forms;
using WinFormsApp.Models;

namespace WinFormsApp
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
                MessageBox.Show(
                    "Копирование файлов завершено!",
                    "Инфо",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
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
            MessageBox.Show(
                    text,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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
            List<string> result = new List<string>();

            try
            {
                using (PowerShell powershell = PowerShell.Create())
                {
                    powershell.AddScript($"cd {pathToRepo}");
                    powershell.AddScript($"git diff --name-only --encoding=utf8 {forkName_1}..{forkName_2}");

                    if (powershell != null)
                    {
                        Collection<PSObject> psResults = powershell.Invoke();

                        if (powershell.Streams.Error.Any())
                        {
                            string errors = $"Список ошибок:{Environment.NewLine}";
                            errors += string.Join(Environment.NewLine, powershell.Streams.Error);
                            ErrorMessage(errors);

                            return result;
                        }

                        if (psResults.Any())
                        {
                            result.AddRange(psResults.Select(psResult => psResult.BaseObject.ToString()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage($"Произошла ошибка выполнения скрипта:{Environment.NewLine}{ex.Message}");
            }

            return result;
        }

        private static List<string> ShowChangesBetweenBranchesCmd(
            string pathToRepo,
            string forkName_1,
            string forkName_2)
        {
            List<string> result = new List<string>();

            try
            {
                string currentDirectory = Environment.CurrentDirectory;
                string diffPath = Path.Combine(currentDirectory, "git_diff.diff");
                string cmd_1 = $"cd {pathToRepo}";
                string cmd_2 = $"git diff --name-only --encoding=utf8 {forkName_1}..{forkName_2} > \"{diffPath}\"";

                ProcessStartInfo startInfo = new ProcessStartInfo()
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c {cmd_1}&{cmd_2}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                };

                using Process process = new Process() { StartInfo = startInfo };
                process.Start();
                string error = process.StandardError.ReadToEnd();

                if (!string.IsNullOrEmpty(error))
                {
                    return result;
                }

                process.WaitForExit();
                process.Close();

                using (var reader = new StreamReader(diffPath))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        result.Add(line);
                    }
                }

                if (File.Exists(diffPath))
                {
                    File.Delete(diffPath);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage($"Произошла ошибка выполнения скрипта:{Environment.NewLine}{ex.Message}");
            }

            return result;
        }

        #endregion
    }
}