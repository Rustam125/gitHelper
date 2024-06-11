using GitHelper.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace GitHelper.Helpers
{
    public static class CommandHandler
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Выполнение скриптов в <see cref="PowerShell"/>.
        /// </summary>
        /// <param name="commands">Список команд.</param>
        /// <returns>Результат выполнения скриптов в формате <see cref="CommandHandlerResultModel"/>.</returns>
        public static CommandHandlerResultModel PowerShellInvoke(List<string> commands)
        {
            List<string> result = new List<string>();
            string errors = string.Empty;

            try
            {
                if (commands == null ||
                    commands.Count == 0)
                {
                    throw new ArgumentNullException("Не удалось определить команды к исполнению.");
                }

                using PowerShell powershell = PowerShell.Create();

                foreach (string command in commands)
                {
                    powershell.AddScript(command);
                }

                if (powershell != null)
                {
                    Collection<PSObject> psResults = powershell.Invoke();

                    if (powershell.Streams.Error.Any())
                    {
                        throw new Exception($"Список ошибок:{Environment.NewLine}" +
                            string.Join(Environment.NewLine, powershell.Streams.Error));
                    }

                    if (psResults != null &&
                        psResults.Count > 0)
                    {
                        result.AddRange(psResults.Select(psResult => psResult.BaseObject.ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                errors = ex.Message;
            }

            return new CommandHandlerResultModel(result, errors);
        }

        /// <summary>
        /// Выполнение скриптов в командной строке cmd.exe.
        /// </summary>
        /// <param name="commands">Список команд.</param>
        /// <param name="invokeByPowerShell">Запуск из <see cref="PowerShell"/>.</param>
        /// <returns>Результат выполнения скриптов в формате <see cref="CommandHandlerResultModel"/>.</returns>
        public static CommandHandlerResultModel CmdInvoke(List<string> commands, bool invokeByPowerShell = false)
        {
            List<string> result = new List<string>();
            StringBuilder errors = new StringBuilder();

            try
            {
                if (commands == null ||
                    commands.Count == 0)
                {
                    throw new ArgumentNullException("Не удалось определить команды к исполнению.");
                }

                using Process process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = invokeByPowerShell ? "powershell.exe" : "cmd.exe",
                        Arguments = $"/c {string.Join("&", commands)}",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        StandardOutputEncoding = Encoding.UTF8,
                        StandardErrorEncoding = Encoding.UTF8
                    }
                };

                process.OutputDataReceived += (sender, args) => result.Add(args.Data);
                process.ErrorDataReceived += (sender, args) =>
                {
                    if (string.IsNullOrEmpty(args.Data) == false)
                    {
                        errors.AppendLine(args.Data);
                    }
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();
                process.Close();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                errors.AppendLine(ex.Message);
            }

            return new CommandHandlerResultModel(result, errors.ToString());
        }
    }
}