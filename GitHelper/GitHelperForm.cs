using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using GitHelper.Helpers;
using GitHelper.Models;
using GitHelper.TabWorkers;

namespace GitHelper
{
    public partial class GitHelperForm : Form
    {
        #region Fields

        private readonly TextBoxContentModel _pathToRepository;
        private readonly TextBoxContentModel _gitForkName_1;
        private readonly TextBoxContentModel _gitForkName_2;
        private readonly TextBoxContentModel _changesBetweenBranchesText;
        private readonly TextBoxContentModel _pathToReleaseCatalogs;
        private readonly TextBoxContentModel _pathToRepositoryNetProjectBuild;
        private readonly TextBoxContentModel _pathToRepositoryWebProjectBuild;
        private readonly TextBoxContentModel _pathToRepositoryCustomBuild;
        private readonly TextBoxContentModel _resultBuild;
        private readonly TextBoxContentModel _customScriptArguments;
        private List<string> _changesBetweenBranches;
        private WayToAccessGitEnum _wayToAccessGit;
        private BackgroundWorker _backgroundWorkerBuild;

        #endregion

        #region Constructor

        public GitHelperForm()
        {
            InitializeComponent();

            // fields
            _pathToRepository = new TextBoxContentModel(Configurations.AppSettings.PathToRepo);
            _gitForkName_1 = new TextBoxContentModel(Configurations.AppSettings.FirstBranchName);
            _gitForkName_2 = new TextBoxContentModel(Configurations.AppSettings.SecondBranchName);
            _changesBetweenBranchesText = new TextBoxContentModel();
            _pathToReleaseCatalogs = new TextBoxContentModel(Configurations.AppSettings.PathToReleaseCatalogs);
            _pathToRepositoryNetProjectBuild = new TextBoxContentModel(Configurations.AppSettings.PathToRepositoryNetProjectBuild);
            _pathToRepositoryWebProjectBuild = new TextBoxContentModel(Configurations.AppSettings.PathToRepositoryWebProjectBuild);
            _pathToRepositoryCustomBuild = new TextBoxContentModel(Configurations.AppSettings.PathToRepositoryCustomBuild);
            _customScriptArguments = new TextBoxContentModel(Configurations.AppSettings.CustomScriptArguments);
            _resultBuild = new TextBoxContentModel();

            SetBindings();
            RadioButtons_CheckedChanged(null, null);
        }

        #endregion

        #region Buttons click

        #region Main page

        private void Button1_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = ReadTaskInfo(richTextBox1.Text);
        }

        private void CreateCatalogs(object sender, EventArgs e)
        {
            string path = _pathToReleaseCatalogs.Text;
            string releaseNumber = string.IsNullOrEmpty(textBox2.Text) ? "00" : textBox2.Text;

            if (string.IsNullOrEmpty(path))
            {
                ShowMessageHelper.ShowError("Указан пустой путь");
                return;
            }

            string[] releaseCatalogs = Configurations.AppSettings.ReleaseCatalogs;

            if (releaseCatalogs == null || releaseCatalogs.Length == 0)
            {
                ShowMessageHelper.ShowError("Не удалось получить каталоги для создания. Проверьте настройки App.config.");
                return;
            }

            path = Path.Combine(path, $"release {releaseNumber}.{DateTime.Now.Year}");
            DirectoryInfo dirInfo = new DirectoryInfo(path);

            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            foreach (string catalog in releaseCatalogs)
            {
                dirInfo.CreateSubdirectory(catalog);
            };

            CopyFixesFiles(releaseCatalogs, dirInfo);
            ShowMessageHelper.ShowInfo("Каталоги успешно созданы!");
        }

        private void CopyFixesFiles(string[] releaseCatalogs, DirectoryInfo dirInfo)
        {
            string fixesCatalogName = "Fixes";

            if (releaseCatalogs.Contains(fixesCatalogName) == false)
            {
                return;
            }

            string sourceDirectory = Path.Combine(Environment.CurrentDirectory, "resources", fixesCatalogName);
            string destinationDirectory = Path.Combine(dirInfo.FullName, fixesCatalogName);
            string constraintsOffFileName = "ConstraintsOff.pg.sql";
            string constraintsOnFileName = "ConstraintsOn.pg.sql";

            File.Copy(Path.Combine(sourceDirectory, constraintsOffFileName), Path.Combine(destinationDirectory, constraintsOffFileName));
            File.Copy(Path.Combine(sourceDirectory, constraintsOnFileName), Path.Combine(destinationDirectory, constraintsOnFileName));
        }

        private void SelectPathToReleaseCatalogsButton_Click(object sender, EventArgs e)
        {
            string path = GitFiles.SelectFolderPath();

            if (string.IsNullOrEmpty(path) == false)
            {
                _pathToReleaseCatalogs.Text = path;
            }
        }

        #endregion

        #region Git files page

        private void ShowChangesBetweenBranchesButton_Click(object sender, EventArgs e)
        {
            _changesBetweenBranches = GitFiles.ShowChangesBetweenBranches(_pathToRepository.Text, _gitForkName_1.Text, _gitForkName_2.Text, _wayToAccessGit);
            SetTextBoxContentModelFromList(_changesBetweenBranches, _changesBetweenBranchesText);
        }

        private void CopyModifiedFilesToDirectoryButton_Click(object sender, EventArgs e)
        {
            GitFiles.CopyModifiedFilesToDirectory(_changesBetweenBranches, _pathToRepository.Text);
        }

        private void SelectPathToRepositoryButton_Click(object sender, EventArgs e) =>
            SelectPathToTextBoxContentModel(_pathToRepository);

        private void RadioButtons_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                _wayToAccessGit = WayToAccessGitEnum.CMD;
            }
            else if (radioButton2.Checked)
            {
                _wayToAccessGit = WayToAccessGitEnum.PowerSHell;
            }
        }

        #endregion

        #region Build page

        private void SelectPathToNetProjectBuildButton_Click(object sender, EventArgs e) =>
            SelectPathToTextBoxContentModel(_pathToRepositoryNetProjectBuild);

        private void SelectPathToWebProjectBuildButton_Click(object sender, EventArgs e) =>
            SelectPathToTextBoxContentModel(_pathToRepositoryWebProjectBuild);

        private void SelectPathToCustomScriptBuildButton_Click(object sender, EventArgs e)
        {
            string fileName = FilesHelper.SelectScriptFullFileName();

            if (_pathToRepositoryCustomBuild != null &&
                string.IsNullOrEmpty(fileName) == false)
            {
                _pathToRepositoryCustomBuild.Text = fileName;
            }
        }

        private void BuildNetButton_Click(object sender, EventArgs e) =>
            InitializeBackgroundWorkerBuild(
                (sender, e) =>
                {
                    _backgroundWorkerBuild.ReportProgress(0);
                    e.Result = BuildHelper.BuildNetProject(_pathToRepositoryNetProjectBuild?.Text);
                });

        private void BuildWebButton_Click(object sender, EventArgs e) =>
            InitializeBackgroundWorkerBuild(
                (sender, e) =>
                {
                    _backgroundWorkerBuild.ReportProgress(0);
                    e.Result = BuildHelper.BuildNpmProject(_pathToRepositoryWebProjectBuild?.Text);
                });

        private void BuildAllButton_Click(object sender, EventArgs e) =>
            InitializeBackgroundWorkerBuild(
                (sender, e) =>
                {
                    _backgroundWorkerBuild.ReportProgress(0);
                    var netResults = BuildHelper.BuildNetProject(_pathToRepositoryNetProjectBuild?.Text);
                    var npmResults = BuildHelper.BuildNpmProject(_pathToRepositoryWebProjectBuild?.Text);
                    netResults.AddRange(npmResults);
                    e.Result = netResults;
                });

        private void BuildCustomScriptButton_Click(object sender, EventArgs e)
        {
            InitializeBackgroundWorkerBuild(
                (sender, e) =>
                {
                    _backgroundWorkerBuild.ReportProgress(0);
                    e.Result = BuildHelper.BuildCustomScript(_pathToRepositoryCustomBuild?.Text, _customScriptArguments?.Text);
                });
        }

        #endregion

        #endregion

        #region Private methods

        private void SetBindings()
        {
            // Путь к репозиторию
            PathToRepositoryTextBox.DataBindings.Add(new Binding("Text", _pathToRepository, "Text"));
            PathToRepositoryBuildTextBox.DataBindings.Add(new Binding("Text", _pathToRepository, "Text"));

            // Ветки
            gitForkNameTextBox_1.DataBindings.Add(new Binding("Text", _gitForkName_1, "Text"));
            gitForkNameTextBox_2.DataBindings.Add(new Binding("Text", _gitForkName_2, "Text"));

            // Список изменений
            ChangesBetweenBranchesRichTextBox.DataBindings.Add(new Binding("Text", _changesBetweenBranchesText, "Text"));

            // Способ обращения к git
            radioButton1.CheckedChanged += new EventHandler(RadioButtons_CheckedChanged);
            radioButton2.CheckedChanged += new EventHandler(RadioButtons_CheckedChanged);

            // Путь для формирования каталогов релиза
            PathToReleaseCatalogsTextBox.DataBindings.Add(new Binding("Text", _pathToReleaseCatalogs, "Text"));

            // Кол-во строк списка изменений
            ChangesCounterLabel.DataBindings.Add(new Binding("Text", _changesBetweenBranchesText, "LinesCountText"));

            // Пути сборки решения
            PathToRepositoryNetProjectBuildTextBox.DataBindings.Add(new Binding("Text", _pathToRepositoryNetProjectBuild, "Text"));
            PathToRepositoryWebProjectBuildTextBox.DataBindings.Add(new Binding("Text", _pathToRepositoryWebProjectBuild, "Text"));
            PathToRepositoryCustomBuildTextBox.DataBindings.Add(new Binding("Text", _pathToRepositoryCustomBuild, "Text"));
            CustomScriptArgumentsTextBox.DataBindings.Add(new Binding("Text", _customScriptArguments, "Text"));

            // Результат сборки
            ResultBuildRichTextBox.DataBindings.Add(new Binding("Text", _resultBuild, "Text"));
        }

        private string ReadTaskInfo(string text)
        {
            string result = string.Empty;
            string specialSymbol = "#";

            if (string.IsNullOrEmpty(textBox3.Text) == false)
            {
                specialSymbol = textBox3.Text;
            }

            if (string.IsNullOrEmpty(text))
            {
                return result;
            }

            bool tempBool = false;

            foreach (char symbol in text)
            {
                if (symbol.ToString() == specialSymbol)
                {
                    tempBool = true;
                    result += Environment.NewLine + specialSymbol;
                    continue;
                }

                if (tempBool)
                {
                    if (int.TryParse(symbol.ToString(), out int digit) == false)
                    {
                        tempBool = false;
                        continue;
                    }

                    result += digit;
                }
            }

            return result;
        }

        private void SetTextBoxContentModelFromList(
            List<string> collection,
            TextBoxContentModel textBoxContentModel)
        {
            string text = collection != null ?
                string.Join(Environment.NewLine, collection) :
                string.Empty;

            try
            {
                text = Decoder.Decoder.DecodeText(text);

                if (textBoxContentModel == null)
                {
                    throw new Exception("Не удалось определить текстовое поле для записи результата.");
                }
            }
            catch (Exception ex)
            {
                ShowMessageHelper.ShowError($"Ошибка кодирования:{Environment.NewLine}{ex.Message}");
                return;
            }

            textBoxContentModel.Text = text;
        }

        /// <summary>
        /// Сохранение конфигурации.
        /// </summary>
        private void SaveConfiguration(object sender, EventArgs e)
        {
            Configurations.AppSettings.FirstBranchName = _gitForkName_1?.Text;
            Configurations.AppSettings.SecondBranchName = _gitForkName_2?.Text;
            Configurations.AppSettings.PathToRepo = _pathToRepository?.Text;
            Configurations.AppSettings.PathToReleaseCatalogs = _pathToReleaseCatalogs?.Text;
            Configurations.AppSettings.PathToRepositoryNetProjectBuild = _pathToRepositoryNetProjectBuild?.Text;
            Configurations.AppSettings.PathToRepositoryWebProjectBuild = _pathToRepositoryWebProjectBuild?.Text;
            Configurations.AppSettings.PathToRepositoryCustomBuild = _pathToRepositoryCustomBuild?.Text;
            Configurations.AppSettings.CustomScriptArguments = _customScriptArguments?.Text;
            ShowMessageHelper.ShowInfo("Настройки успешно сохранены!");
        }

        private void SelectPathToTextBoxContentModel(TextBoxContentModel textBoxContentModel)
        {
            if (textBoxContentModel == null)
            {
                return;
            }

            string path = GitFiles.SelectFolderPath();

            if (string.IsNullOrEmpty(path) == false)
            {
                textBoxContentModel.Text = path;
            }
        }

        #region Build page

        private void InitializeBackgroundWorkerBuild(DoWorkEventHandler doWorkEventHandler)
        {
            _backgroundWorkerBuild = new BackgroundWorker
            {
                WorkerReportsProgress = true
            };

            _backgroundWorkerBuild.ProgressChanged += (sender, e) =>
            {
                LoadingPictureBox.Visible = true;
                LoadingPictureBox.Size = new System.Drawing.Size(this.Width, this.Height);
                LoadingPictureBox.BringToFront();
            };

            _backgroundWorkerBuild.RunWorkerCompleted += (sender, e) =>
            {
                if (e.Result != null)
                {
                    SetTextBoxContentModelFromList((List<string>)e.Result, _resultBuild);
                }

                LoadingPictureBox.Visible = false;
            };

            _backgroundWorkerBuild.DoWork += doWorkEventHandler;
            _backgroundWorkerBuild.RunWorkerAsync();
        }

        #endregion

        #endregion
    }
}