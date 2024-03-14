namespace WinFormsApp
{
    partial class GitHelperForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GitHelperForm));
            tabControl1 = new System.Windows.Forms.TabControl();
            mainPage = new System.Windows.Forms.TabPage();
            SelectPathToReleaseCatalogsButton = new System.Windows.Forms.Button();
            textBox3 = new System.Windows.Forms.TextBox();
            label5 = new System.Windows.Forms.Label();
            textBox2 = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            button2 = new System.Windows.Forms.Button();
            PathToReleaseCatalogsTextBox = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            button1 = new System.Windows.Forms.Button();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            richTextBox2 = new System.Windows.Forms.RichTextBox();
            richTextBox1 = new System.Windows.Forms.RichTextBox();
            gitFilesPage = new System.Windows.Forms.TabPage();
            ChangesCounterLabel = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            radioButton1 = new System.Windows.Forms.RadioButton();
            radioButton2 = new System.Windows.Forms.RadioButton();
            SelectPathToRepositoryButton = new System.Windows.Forms.Button();
            PathToRepositoryTextBox = new System.Windows.Forms.TextBox();
            label9 = new System.Windows.Forms.Label();
            CopyModifiedFilesToDirectoryButton = new System.Windows.Forms.Button();
            ShowChangesBetweenBranchesButton = new System.Windows.Forms.Button();
            label8 = new System.Windows.Forms.Label();
            ChangesBetweenBranchesRichTextBox = new System.Windows.Forms.RichTextBox();
            gitForkNameTextBox_2 = new System.Windows.Forms.TextBox();
            label7 = new System.Windows.Forms.Label();
            gitForkNameTextBox_1 = new System.Windows.Forms.TextBox();
            label6 = new System.Windows.Forms.Label();
            tabControl1.SuspendLayout();
            mainPage.SuspendLayout();
            gitFilesPage.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tabControl1.Controls.Add(mainPage);
            tabControl1.Controls.Add(gitFilesPage);
            tabControl1.Location = new System.Drawing.Point(8, 8);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(805, 530);
            tabControl1.TabIndex = 13;
            // 
            // mainPage
            // 
            mainPage.AutoScroll = true;
            mainPage.Controls.Add(SelectPathToReleaseCatalogsButton);
            mainPage.Controls.Add(textBox3);
            mainPage.Controls.Add(label5);
            mainPage.Controls.Add(textBox2);
            mainPage.Controls.Add(label4);
            mainPage.Controls.Add(button2);
            mainPage.Controls.Add(PathToReleaseCatalogsTextBox);
            mainPage.Controls.Add(label3);
            mainPage.Controls.Add(button1);
            mainPage.Controls.Add(label2);
            mainPage.Controls.Add(label1);
            mainPage.Controls.Add(richTextBox2);
            mainPage.Controls.Add(richTextBox1);
            mainPage.Location = new System.Drawing.Point(4, 24);
            mainPage.Name = "mainPage";
            mainPage.Padding = new System.Windows.Forms.Padding(3);
            mainPage.Size = new System.Drawing.Size(797, 502);
            mainPage.TabIndex = 0;
            mainPage.Text = "Основное";
            mainPage.UseVisualStyleBackColor = true;
            // 
            // SelectPathToReleaseCatalogsButton
            // 
            SelectPathToReleaseCatalogsButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            SelectPathToReleaseCatalogsButton.Location = new System.Drawing.Point(364, 461);
            SelectPathToReleaseCatalogsButton.Name = "SelectPathToReleaseCatalogsButton";
            SelectPathToReleaseCatalogsButton.Size = new System.Drawing.Size(32, 23);
            SelectPathToReleaseCatalogsButton.TabIndex = 25;
            SelectPathToReleaseCatalogsButton.Text = "...";
            SelectPathToReleaseCatalogsButton.UseVisualStyleBackColor = true;
            SelectPathToReleaseCatalogsButton.Click += SelectPathToReleaseCatalogsButton_Click;
            // 
            // textBox3
            // 
            textBox3.Location = new System.Drawing.Point(419, 182);
            textBox3.Name = "textBox3";
            textBox3.Size = new System.Drawing.Size(86, 23);
            textBox3.TabIndex = 24;
            textBox3.Text = "#";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(414, 164);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(103, 15);
            label5.TabIndex = 23;
            label5.Text = "Спец символ (от)";
            // 
            // textBox2
            // 
            textBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            textBox2.Location = new System.Drawing.Point(583, 460);
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(155, 23);
            textBox2.TabIndex = 22;
            // 
            // label4
            // 
            label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(583, 442);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(86, 15);
            label4.TabIndex = 21;
            label4.Text = "Номер релиза";
            // 
            // button2
            // 
            button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            button2.Location = new System.Drawing.Point(414, 460);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(163, 23);
            button2.TabIndex = 20;
            button2.Text = "Сформировать каталоги";
            button2.UseVisualStyleBackColor = true;
            button2.Click += CreateCatalogs;
            // 
            // PathToReleaseCatalogsTextBox
            // 
            PathToReleaseCatalogsTextBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            PathToReleaseCatalogsTextBox.Location = new System.Drawing.Point(6, 461);
            PathToReleaseCatalogsTextBox.Name = "PathToReleaseCatalogsTextBox";
            PathToReleaseCatalogsTextBox.Size = new System.Drawing.Size(354, 23);
            PathToReleaseCatalogsTextBox.TabIndex = 19;
            // 
            // label3
            // 
            label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(6, 443);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(242, 15);
            label3.TabIndex = 18;
            label3.Text = "Путь для формирования каталогов релиза";
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(402, 211);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(129, 42);
            button1.TabIndex = 17;
            button1.Text = "Сформировать ->";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label2
            // 
            label2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(537, 3);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(191, 15);
            label2.TabIndex = 16;
            label2.Text = "Сформированный список тасков";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(6, 3);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(100, 15);
            label1.TabIndex = 15;
            label1.Text = "Список из GitLab";
            // 
            // richTextBox2
            // 
            richTextBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            richTextBox2.Location = new System.Drawing.Point(537, 21);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.Size = new System.Drawing.Size(245, 411);
            richTextBox2.TabIndex = 14;
            richTextBox2.Text = "";
            // 
            // richTextBox1
            // 
            richTextBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            richTextBox1.Location = new System.Drawing.Point(6, 21);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            richTextBox1.Size = new System.Drawing.Size(390, 411);
            richTextBox1.TabIndex = 13;
            richTextBox1.Text = "";
            // 
            // gitFilesPage
            // 
            gitFilesPage.AutoScroll = true;
            gitFilesPage.Controls.Add(ChangesCounterLabel);
            gitFilesPage.Controls.Add(groupBox1);
            gitFilesPage.Controls.Add(SelectPathToRepositoryButton);
            gitFilesPage.Controls.Add(PathToRepositoryTextBox);
            gitFilesPage.Controls.Add(label9);
            gitFilesPage.Controls.Add(CopyModifiedFilesToDirectoryButton);
            gitFilesPage.Controls.Add(ShowChangesBetweenBranchesButton);
            gitFilesPage.Controls.Add(label8);
            gitFilesPage.Controls.Add(ChangesBetweenBranchesRichTextBox);
            gitFilesPage.Controls.Add(gitForkNameTextBox_2);
            gitFilesPage.Controls.Add(label7);
            gitFilesPage.Controls.Add(gitForkNameTextBox_1);
            gitFilesPage.Controls.Add(label6);
            gitFilesPage.Location = new System.Drawing.Point(4, 24);
            gitFilesPage.Name = "gitFilesPage";
            gitFilesPage.Padding = new System.Windows.Forms.Padding(3);
            gitFilesPage.Size = new System.Drawing.Size(797, 502);
            gitFilesPage.TabIndex = 1;
            gitFilesPage.Text = "GIT files";
            gitFilesPage.UseVisualStyleBackColor = true;
            // 
            // ChangesCounterLabel
            // 
            ChangesCounterLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            ChangesCounterLabel.AutoSize = true;
            ChangesCounterLabel.Location = new System.Drawing.Point(323, 481);
            ChangesCounterLabel.Name = "ChangesCounterLabel";
            ChangesCounterLabel.Size = new System.Drawing.Size(0, 15);
            ChangesCounterLabel.TabIndex = 31;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radioButton1);
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Location = new System.Drawing.Point(6, 148);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(300, 49);
            groupBox1.TabIndex = 30;
            groupBox1.TabStop = false;
            groupBox1.Text = "Способ обращения к git";
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Checked = true;
            radioButton1.Location = new System.Drawing.Point(6, 22);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new System.Drawing.Size(156, 19);
            radioButton1.TabIndex = 28;
            radioButton1.TabStop = true;
            radioButton1.Text = "Командная строка + file";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new System.Drawing.Point(168, 22);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new System.Drawing.Size(83, 19);
            radioButton2.TabIndex = 29;
            radioButton2.TabStop = true;
            radioButton2.Text = "PowerShell";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // SelectPathToRepositoryButton
            // 
            SelectPathToRepositoryButton.Location = new System.Drawing.Point(274, 21);
            SelectPathToRepositoryButton.Name = "SelectPathToRepositoryButton";
            SelectPathToRepositoryButton.Size = new System.Drawing.Size(32, 23);
            SelectPathToRepositoryButton.TabIndex = 21;
            SelectPathToRepositoryButton.Text = "...";
            SelectPathToRepositoryButton.UseVisualStyleBackColor = true;
            SelectPathToRepositoryButton.Click += SelectPathToRepositoryButton_Click;
            // 
            // PathToRepositoryTextBox
            // 
            PathToRepositoryTextBox.Location = new System.Drawing.Point(6, 21);
            PathToRepositoryTextBox.Name = "PathToRepositoryTextBox";
            PathToRepositoryTextBox.Size = new System.Drawing.Size(262, 23);
            PathToRepositoryTextBox.TabIndex = 20;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(6, 3);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(120, 15);
            label9.TabIndex = 19;
            label9.Text = "Путь к репозиторию";
            // 
            // CopyModifiedFilesToDirectoryButton
            // 
            CopyModifiedFilesToDirectoryButton.Location = new System.Drawing.Point(165, 203);
            CopyModifiedFilesToDirectoryButton.Name = "CopyModifiedFilesToDirectoryButton";
            CopyModifiedFilesToDirectoryButton.Size = new System.Drawing.Size(141, 60);
            CopyModifiedFilesToDirectoryButton.TabIndex = 18;
            CopyModifiedFilesToDirectoryButton.Text = "Копировать измененные файлы в каталог...";
            CopyModifiedFilesToDirectoryButton.UseVisualStyleBackColor = true;
            CopyModifiedFilesToDirectoryButton.Click += CopyModifiedFilesToDirectoryButton_Click;
            // 
            // ShowChangesBetweenBranchesButton
            // 
            ShowChangesBetweenBranchesButton.Location = new System.Drawing.Point(6, 203);
            ShowChangesBetweenBranchesButton.Name = "ShowChangesBetweenBranchesButton";
            ShowChangesBetweenBranchesButton.Size = new System.Drawing.Size(144, 60);
            ShowChangesBetweenBranchesButton.TabIndex = 17;
            ShowChangesBetweenBranchesButton.Text = "Показать изменения между ветками";
            ShowChangesBetweenBranchesButton.UseVisualStyleBackColor = true;
            ShowChangesBetweenBranchesButton.Click += ShowChangesBetweenBranchesButton_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(323, 3);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(199, 15);
            label8.TabIndex = 16;
            label8.Text = "Список изменений между ветками";
            // 
            // ChangesBetweenBranchesRichTextBox
            // 
            ChangesBetweenBranchesRichTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            ChangesBetweenBranchesRichTextBox.Location = new System.Drawing.Point(323, 21);
            ChangesBetweenBranchesRichTextBox.Name = "ChangesBetweenBranchesRichTextBox";
            ChangesBetweenBranchesRichTextBox.ReadOnly = true;
            ChangesBetweenBranchesRichTextBox.Size = new System.Drawing.Size(468, 457);
            ChangesBetweenBranchesRichTextBox.TabIndex = 15;
            ChangesBetweenBranchesRichTextBox.Text = "";
            // 
            // gitForkNameTextBox_2
            // 
            gitForkNameTextBox_2.Location = new System.Drawing.Point(6, 119);
            gitForkNameTextBox_2.Name = "gitForkNameTextBox_2";
            gitForkNameTextBox_2.Size = new System.Drawing.Size(300, 23);
            gitForkNameTextBox_2.TabIndex = 3;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(6, 101);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(132, 15);
            label7.TabIndex = 2;
            label7.Text = "Наименование ветки 2";
            // 
            // gitForkNameTextBox_1
            // 
            gitForkNameTextBox_1.Location = new System.Drawing.Point(6, 72);
            gitForkNameTextBox_1.Name = "gitForkNameTextBox_1";
            gitForkNameTextBox_1.Size = new System.Drawing.Size(300, 23);
            gitForkNameTextBox_1.TabIndex = 1;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(6, 54);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(132, 15);
            label6.TabIndex = 0;
            label6.Text = "Наименование ветки 1";
            // 
            // GitHelperForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(825, 550);
            Controls.Add(tabControl1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "GitHelperForm";
            Text = "GitHelper";
            tabControl1.ResumeLayout(false);
            mainPage.ResumeLayout(false);
            mainPage.PerformLayout();
            gitFilesPage.ResumeLayout(false);
            gitFilesPage.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage mainPage;
        private System.Windows.Forms.TabPage gitFilesPage;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox PathToReleaseCatalogsTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox gitForkNameTextBox_2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox gitForkNameTextBox_1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button CopyModifiedFilesToDirectoryButton;
        private System.Windows.Forms.Button ShowChangesBetweenBranchesButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RichTextBox ChangesBetweenBranchesRichTextBox;
        private System.Windows.Forms.Button SelectPathToRepositoryButton;
        private System.Windows.Forms.TextBox PathToRepositoryTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button SelectPathToReleaseCatalogsButton;
        private System.Windows.Forms.Label ChangesCounterLabel;
    }
}
