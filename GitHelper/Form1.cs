using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using WinFormsApp.Models;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        #region Fields

        private readonly TextBoxContentModel _pathToRepository;
        private readonly TextBoxContentModel _gitForkName_1;
        private readonly TextBoxContentModel _gitForkName_2;
        private readonly TextBoxContentModel _changesBetweenBranchesText;
        private List<string> _changesBetweenBranches;
        private WayToAccessGitEnum _wayToAccessGit;

        #endregion

        #region Constructor

        public Form1()
        {
            InitializeComponent();

            // fields
            _pathToRepository = new TextBoxContentModel
            {
                Text = "D:\\REPO\\REO"
            };
            _gitForkName_1 = new TextBoxContentModel
            {
                Text = "master"
            };
            _gitForkName_2 = new TextBoxContentModel
            {
                Text = "release/03.2024"
            };
            _changesBetweenBranchesText = new TextBoxContentModel();

            Bindings();
            RadioButtons_CheckedChanged(null, null);
        }

        #endregion

        #region Buttons click

        #region Main page

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = ReadTaskInfo(richTextBox1.Text);
        }

        private void CreateCatalogs(object sender, EventArgs e)
        {
            string path = textBox1.Text;
            string releaseNumber = string.IsNullOrEmpty(textBox2.Text) ? "00" : textBox2.Text;

            if (string.IsNullOrEmpty(path))
            {
                MessageBox.Show(
                    "Указан пустой путь",
                    "Сообщение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            path = Path.Combine(path, $"release {releaseNumber}.{DateTime.Now.Year}");

            DirectoryInfo dirInfo = new DirectoryInfo(path);

            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            dirInfo.CreateSubdirectory(@"web js");
            dirInfo.CreateSubdirectory(@"Scripts");
            dirInfo.CreateSubdirectory(@"Fixes");
            dirInfo.CreateSubdirectory(@"extensions");
            dirInfo.CreateSubdirectory(@"extensions\web");
            dirInfo.CreateSubdirectory(@"extensions\server");
            dirInfo.CreateSubdirectory(@"extensions\chronos");
            dirInfo.CreateSubdirectory(@"Configuration");

            MessageBox.Show(
                    "Каталоги успешно созданы!",
                    "Сообщение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
        }

        #endregion

        #region Git files page

        private void ShowChangesBetweenBranchesButton_Click(object sender, EventArgs e)
        {
            _changesBetweenBranches = GitFiles.ShowChangesBetweenBranches(_pathToRepository.Text, _gitForkName_1.Text, _gitForkName_2.Text, _wayToAccessGit);
            SetChangesBetweenBranches();
        }

        private void CopyModifiedFilesToDirectoryButton_Click(object sender, EventArgs e)
        {
            GitFiles.CopyModifiedFilesToDirectory(_changesBetweenBranches, _pathToRepository.Text);
        }

        private void SelectPathToRepositoryButton_Click(object sender, EventArgs e)
        {
            string path = GitFiles.SelectFolderPath();

            if (string.IsNullOrEmpty(path) == false)
            {
                _pathToRepository.Text = path;
            }
        }

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

        #endregion

        #region Private methods

        private void Bindings()
        {
            // Путь к репозиторию
            PathToRepositoryTextBox.DataBindings.Add(new Binding("Text", _pathToRepository, "Text"));

            // ветки
            gitForkNameTextBox_1.DataBindings.Add(new Binding("Text", _gitForkName_1, "Text"));
            gitForkNameTextBox_2.DataBindings.Add(new Binding("Text", _gitForkName_2, "Text"));

            // Список изменений
            ChangesBetweenBranchesRichTextBox.DataBindings.Add(new Binding("Text", _changesBetweenBranchesText, "Text"));

            // Способ обращения к git
            radioButton1.CheckedChanged += new EventHandler(RadioButtons_CheckedChanged);
            radioButton2.CheckedChanged += new EventHandler(RadioButtons_CheckedChanged);
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

        private void SetChangesBetweenBranches()
        {
            string text = _changesBetweenBranches != null ?
                string.Join(Environment.NewLine, _changesBetweenBranches) :
                string.Empty;

            try
            {
                text = Decoder.Decoder.DecodeText(text);
            }
            catch (Exception ex)
            {
                GitFiles.ErrorMessage($"Ошибка кодирования:{Environment.NewLine}{ex.Message}");
            }

            _changesBetweenBranchesText.Text = text;
        }

        #endregion
    }
}