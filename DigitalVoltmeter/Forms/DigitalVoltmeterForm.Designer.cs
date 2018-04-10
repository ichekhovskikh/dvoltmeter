namespace DigitalVoltmeter
{
    partial class DigitalVoltmeterForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonGetFormules = new System.Windows.Forms.Button();
            this.labelN = new System.Windows.Forms.Label();
            this.buttonSaveToExel = new System.Windows.Forms.Button();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.checkBoxOutToWord = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.labelK = new System.Windows.Forms.Label();
            this.labelDK = new System.Windows.Forms.Label();
            this.labelDI = new System.Windows.Forms.Label();
            this.labelDUsm = new System.Windows.Forms.Label();
            this.buttonGetModel = new System.Windows.Forms.Button();
            this.mainChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.buttonExpand = new System.Windows.Forms.Button();
            this.dataGridViewVect = new System.Windows.Forms.DataGridView();
            this.labelCriticalDK = new System.Windows.Forms.Label();
            this.labelCriticalDsm = new System.Windows.Forms.Label();
            this.buttonCriticalDK = new System.Windows.Forms.Button();
            this.buttonCriticalDi = new System.Windows.Forms.Button();
            this.buttonCritical = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.labelAccuracy = new System.Windows.Forms.Label();
            this.labelInitialStep = new System.Windows.Forms.Label();
            this.groupBoxCriticalValues = new System.Windows.Forms.GroupBox();
            this.labelCriticalDI = new System.Windows.Forms.Label();
            this.labelDUsmErr = new System.Windows.Forms.Label();
            this.labelDIErr = new System.Windows.Forms.Label();
            this.labelDKErr = new System.Windows.Forms.Label();
            this.numericUpDownN = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownK = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownDK = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownDUsm = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownAccuracy = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownInitialStep = new System.Windows.Forms.NumericUpDown();
            this.buttonGetCritical3DModel = new System.Windows.Forms.Button();
            this.numericUpDownDI = new System.Windows.Forms.NumericUpDown();
            this.textBoxState = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.mainChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVect)).BeginInit();
            this.groupBoxCriticalValues.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDUsm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAccuracy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInitialStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDI)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(169, 833);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1107, 26);
            this.progressBar.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(12, 16);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(566, 29);
            this.label2.TabIndex = 12;
            this.label2.Text = "Программа для анализа работы вольтметра параллельного преобразования.\r\n";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonGetFormules
            // 
            this.buttonGetFormules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonGetFormules.Location = new System.Drawing.Point(12, 588);
            this.buttonGetFormules.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonGetFormules.Name = "buttonGetFormules";
            this.buttonGetFormules.Size = new System.Drawing.Size(177, 28);
            this.buttonGetFormules.TabIndex = 10;
            this.buttonGetFormules.Text = "Получить уравнения";
            this.buttonGetFormules.UseVisualStyleBackColor = true;
            this.buttonGetFormules.Click += new System.EventHandler(this.buttonGetFormules_Click);
            // 
            // labelN
            // 
            this.labelN.AutoSize = true;
            this.labelN.Location = new System.Drawing.Point(16, 50);
            this.labelN.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelN.Name = "labelN";
            this.labelN.Size = new System.Drawing.Size(20, 17);
            this.labelN.TabIndex = 9;
            this.labelN.Text = "n:";
            // 
            // buttonSaveToExel
            // 
            this.buttonSaveToExel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSaveToExel.Location = new System.Drawing.Point(15, 832);
            this.buttonSaveToExel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSaveToExel.Name = "buttonSaveToExel";
            this.buttonSaveToExel.Size = new System.Drawing.Size(147, 28);
            this.buttonSaveToExel.TabIndex = 8;
            this.buttonSaveToExel.Text = "Экспорт в Excel";
            this.buttonSaveToExel.UseVisualStyleBackColor = true;
            this.buttonSaveToExel.Click += new System.EventHandler(this.buttonSaveToExel_Click);
            // 
            // richTextBox
            // 
            this.richTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBox.Location = new System.Drawing.Point(16, 624);
            this.richTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.ReadOnly = true;
            this.richTextBox.Size = new System.Drawing.Size(1217, 116);
            this.richTextBox.TabIndex = 7;
            this.richTextBox.Text = "";
            this.richTextBox.WordWrap = false;
            // 
            // checkBoxOutToWord
            // 
            this.checkBoxOutToWord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxOutToWord.AutoSize = true;
            this.checkBoxOutToWord.Location = new System.Drawing.Point(197, 593);
            this.checkBoxOutToWord.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxOutToWord.Name = "checkBoxOutToWord";
            this.checkBoxOutToWord.Size = new System.Drawing.Size(160, 21);
            this.checkBoxOutToWord.TabIndex = 15;
            this.checkBoxOutToWord.Text = "Вывод в Word файл";
            this.checkBoxOutToWord.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 569);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(389, 17);
            this.label3.TabIndex = 16;
            this.label3.Text = "Формулы связи между ЕПК и выходным двоичным кодом.\r\n";
            // 
            // labelK
            // 
            this.labelK.AutoSize = true;
            this.labelK.Location = new System.Drawing.Point(15, 82);
            this.labelK.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelK.Name = "labelK";
            this.labelK.Size = new System.Drawing.Size(21, 17);
            this.labelK.TabIndex = 19;
            this.labelK.Text = "K:";
            // 
            // labelDK
            // 
            this.labelDK.AutoSize = true;
            this.labelDK.Location = new System.Drawing.Point(5, 114);
            this.labelDK.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDK.Name = "labelDK";
            this.labelDK.Size = new System.Drawing.Size(30, 17);
            this.labelDK.TabIndex = 21;
            this.labelDK.Text = "ΔK:";
            // 
            // labelDI
            // 
            this.labelDI.AutoSize = true;
            this.labelDI.Location = new System.Drawing.Point(4, 145);
            this.labelDI.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDI.Name = "labelDI";
            this.labelDI.Size = new System.Drawing.Size(24, 17);
            this.labelDI.TabIndex = 23;
            this.labelDI.Text = "Δi:";
            // 
            // labelDUsm
            // 
            this.labelDUsm.AutoSize = true;
            this.labelDUsm.Location = new System.Drawing.Point(-3, 177);
            this.labelDUsm.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDUsm.Name = "labelDUsm";
            this.labelDUsm.Size = new System.Drawing.Size(36, 17);
            this.labelDUsm.TabIndex = 25;
            this.labelDUsm.Text = "δсм:";
            // 
            // buttonGetModel
            // 
            this.buttonGetModel.Location = new System.Drawing.Point(12, 236);
            this.buttonGetModel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonGetModel.Name = "buttonGetModel";
            this.buttonGetModel.Size = new System.Drawing.Size(275, 30);
            this.buttonGetModel.TabIndex = 27;
            this.buttonGetModel.Text = "Получить модель";
            this.buttonGetModel.UseVisualStyleBackColor = true;
            this.buttonGetModel.Click += new System.EventHandler(this.buttonGetModel_Click);
            // 
            // mainChart
            // 
            this.mainChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea2.Name = "ChartArea1";
            this.mainChart.ChartAreas.Add(chartArea2);
            this.mainChart.Location = new System.Drawing.Point(295, 49);
            this.mainChart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mainChart.Name = "mainChart";
            this.mainChart.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.mainChart.Size = new System.Drawing.Size(587, 517);
            this.mainChart.TabIndex = 33;
            title2.Name = "Title1";
            title2.Text = "Входное напряжение";
            this.mainChart.Titles.Add(title2);
            // 
            // buttonExpand
            // 
            this.buttonExpand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExpand.Location = new System.Drawing.Point(789, 569);
            this.buttonExpand.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonExpand.Name = "buttonExpand";
            this.buttonExpand.Size = new System.Drawing.Size(91, 28);
            this.buttonExpand.TabIndex = 34;
            this.buttonExpand.Text = "Раскрыть график";
            this.buttonExpand.UseVisualStyleBackColor = true;
            this.buttonExpand.Click += new System.EventHandler(this.buttonExpand_Click);
            // 
            // dataGridViewVect
            // 
            this.dataGridViewVect.AllowUserToAddRows = false;
            this.dataGridViewVect.AllowUserToDeleteRows = false;
            this.dataGridViewVect.AllowUserToOrderColumns = true;
            this.dataGridViewVect.AllowUserToResizeColumns = false;
            this.dataGridViewVect.AllowUserToResizeRows = false;
            this.dataGridViewVect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewVect.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGridViewVect.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewVect.Location = new System.Drawing.Point(889, 14);
            this.dataGridViewVect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridViewVect.MultiSelect = false;
            this.dataGridViewVect.Name = "dataGridViewVect";
            this.dataGridViewVect.ReadOnly = true;
            this.dataGridViewVect.RowHeadersVisible = false;
            this.dataGridViewVect.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewVect.ShowCellToolTips = false;
            this.dataGridViewVect.Size = new System.Drawing.Size(345, 602);
            this.dataGridViewVect.TabIndex = 35;
            this.dataGridViewVect.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridViewVect_CellPainting);
            this.dataGridViewVect.SelectionChanged += new System.EventHandler(this.dataGridViewVect_SelectionChanged);
            // 
            // labelCriticalDK
            // 
            this.labelCriticalDK.BackColor = System.Drawing.Color.LightGray;
            this.labelCriticalDK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelCriticalDK.Location = new System.Drawing.Point(41, 20);
            this.labelCriticalDK.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCriticalDK.Name = "labelCriticalDK";
            this.labelCriticalDK.Size = new System.Drawing.Size(191, 24);
            this.labelCriticalDK.TabIndex = 36;
            this.labelCriticalDK.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCriticalDsm
            // 
            this.labelCriticalDsm.BackColor = System.Drawing.Color.LightGray;
            this.labelCriticalDsm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelCriticalDsm.Location = new System.Drawing.Point(41, 103);
            this.labelCriticalDsm.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCriticalDsm.Name = "labelCriticalDsm";
            this.labelCriticalDsm.Size = new System.Drawing.Size(191, 24);
            this.labelCriticalDsm.TabIndex = 38;
            this.labelCriticalDsm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonCriticalDK
            // 
            this.buttonCriticalDK.BackColor = System.Drawing.Color.Red;
            this.buttonCriticalDK.Location = new System.Drawing.Point(241, 20);
            this.buttonCriticalDK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonCriticalDK.Name = "buttonCriticalDK";
            this.buttonCriticalDK.Size = new System.Drawing.Size(21, 25);
            this.buttonCriticalDK.TabIndex = 39;
            this.buttonCriticalDK.UseVisualStyleBackColor = false;
            this.buttonCriticalDK.Click += new System.EventHandler(this.buttonCriticalDK_Click);
            // 
            // buttonCriticalDi
            // 
            this.buttonCriticalDi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonCriticalDi.Location = new System.Drawing.Point(241, 62);
            this.buttonCriticalDi.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonCriticalDi.Name = "buttonCriticalDi";
            this.buttonCriticalDi.Size = new System.Drawing.Size(21, 25);
            this.buttonCriticalDi.TabIndex = 40;
            this.buttonCriticalDi.UseVisualStyleBackColor = false;
            this.buttonCriticalDi.Click += new System.EventHandler(this.buttonCriticalDi_Click);
            // 
            // buttonCritical
            // 
            this.buttonCritical.BackColor = System.Drawing.SystemColors.Highlight;
            this.buttonCritical.Location = new System.Drawing.Point(241, 103);
            this.buttonCritical.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonCritical.Name = "buttonCritical";
            this.buttonCritical.Size = new System.Drawing.Size(21, 25);
            this.buttonCritical.TabIndex = 41;
            this.buttonCritical.UseVisualStyleBackColor = false;
            this.buttonCritical.Click += new System.EventHandler(this.buttonCritical_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 273);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(275, 46);
            this.button1.TabIndex = 42;
            this.button1.Text = "Получить критические значения параметров";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelAccuracy
            // 
            this.labelAccuracy.AutoSize = true;
            this.labelAccuracy.Location = new System.Drawing.Point(144, 50);
            this.labelAccuracy.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAccuracy.Name = "labelAccuracy";
            this.labelAccuracy.Size = new System.Drawing.Size(74, 17);
            this.labelAccuracy.TabIndex = 44;
            this.labelAccuracy.Text = "Точность:";
            // 
            // labelInitialStep
            // 
            this.labelInitialStep.AutoSize = true;
            this.labelInitialStep.Location = new System.Drawing.Point(145, 82);
            this.labelInitialStep.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelInitialStep.Name = "labelInitialStep";
            this.labelInitialStep.Size = new System.Drawing.Size(73, 17);
            this.labelInitialStep.TabIndex = 45;
            this.labelInitialStep.Text = "Перв.шаг:";
            // 
            // groupBoxCriticalValues
            // 
            this.groupBoxCriticalValues.Controls.Add(this.labelCriticalDI);
            this.groupBoxCriticalValues.Controls.Add(this.labelDUsmErr);
            this.groupBoxCriticalValues.Controls.Add(this.labelDIErr);
            this.groupBoxCriticalValues.Controls.Add(this.labelDKErr);
            this.groupBoxCriticalValues.Controls.Add(this.labelCriticalDK);
            this.groupBoxCriticalValues.Controls.Add(this.labelCriticalDsm);
            this.groupBoxCriticalValues.Controls.Add(this.buttonCriticalDK);
            this.groupBoxCriticalValues.Controls.Add(this.buttonCriticalDi);
            this.groupBoxCriticalValues.Controls.Add(this.buttonCritical);
            this.groupBoxCriticalValues.Location = new System.Drawing.Point(12, 326);
            this.groupBoxCriticalValues.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxCriticalValues.Name = "groupBoxCriticalValues";
            this.groupBoxCriticalValues.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxCriticalValues.Size = new System.Drawing.Size(275, 134);
            this.groupBoxCriticalValues.TabIndex = 48;
            this.groupBoxCriticalValues.TabStop = false;
            this.groupBoxCriticalValues.Text = "Критические значения";
            // 
            // labelCriticalDI
            // 
            this.labelCriticalDI.BackColor = System.Drawing.Color.LightGray;
            this.labelCriticalDI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelCriticalDI.Location = new System.Drawing.Point(41, 62);
            this.labelCriticalDI.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCriticalDI.Name = "labelCriticalDI";
            this.labelCriticalDI.Size = new System.Drawing.Size(191, 24);
            this.labelCriticalDI.TabIndex = 53;
            this.labelCriticalDI.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelDUsmErr
            // 
            this.labelDUsmErr.AutoSize = true;
            this.labelDUsmErr.Location = new System.Drawing.Point(1, 107);
            this.labelDUsmErr.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDUsmErr.Name = "labelDUsmErr";
            this.labelDUsmErr.Size = new System.Drawing.Size(36, 17);
            this.labelDUsmErr.TabIndex = 52;
            this.labelDUsmErr.Text = "δсм:";
            // 
            // labelDIErr
            // 
            this.labelDIErr.AutoSize = true;
            this.labelDIErr.Location = new System.Drawing.Point(13, 66);
            this.labelDIErr.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDIErr.Name = "labelDIErr";
            this.labelDIErr.Size = new System.Drawing.Size(24, 17);
            this.labelDIErr.TabIndex = 51;
            this.labelDIErr.Text = "Δi:";
            // 
            // labelDKErr
            // 
            this.labelDKErr.AutoSize = true;
            this.labelDKErr.Location = new System.Drawing.Point(7, 25);
            this.labelDKErr.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDKErr.Name = "labelDKErr";
            this.labelDKErr.Size = new System.Drawing.Size(30, 17);
            this.labelDKErr.TabIndex = 50;
            this.labelDKErr.Text = "ΔK:";
            // 
            // numericUpDownN
            // 
            this.numericUpDownN.Location = new System.Drawing.Point(37, 49);
            this.numericUpDownN.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numericUpDownN.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numericUpDownN.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownN.Name = "numericUpDownN";
            this.numericUpDownN.Size = new System.Drawing.Size(99, 22);
            this.numericUpDownN.TabIndex = 49;
            this.numericUpDownN.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDownN.ValueChanged += new System.EventHandler(this.numericUpDownN_TextChanged);
            // 
            // numericUpDownK
            // 
            this.numericUpDownK.DecimalPlaces = 4;
            this.numericUpDownK.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownK.Location = new System.Drawing.Point(37, 81);
            this.numericUpDownK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numericUpDownK.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownK.Name = "numericUpDownK";
            this.numericUpDownK.Size = new System.Drawing.Size(99, 22);
            this.numericUpDownK.TabIndex = 50;
            this.numericUpDownK.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // numericUpDownDK
            // 
            this.numericUpDownDK.DecimalPlaces = 10;
            this.numericUpDownDK.Location = new System.Drawing.Point(37, 113);
            this.numericUpDownDK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numericUpDownDK.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownDK.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.numericUpDownDK.Name = "numericUpDownDK";
            this.numericUpDownDK.Size = new System.Drawing.Size(249, 22);
            this.numericUpDownDK.TabIndex = 51;
            // 
            // numericUpDownDUsm
            // 
            this.numericUpDownDUsm.DecimalPlaces = 10;
            this.numericUpDownDUsm.Location = new System.Drawing.Point(37, 177);
            this.numericUpDownDUsm.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numericUpDownDUsm.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownDUsm.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.numericUpDownDUsm.Name = "numericUpDownDUsm";
            this.numericUpDownDUsm.Size = new System.Drawing.Size(249, 22);
            this.numericUpDownDUsm.TabIndex = 52;
            // 
            // numericUpDownAccuracy
            // 
            this.numericUpDownAccuracy.DecimalPlaces = 4;
            this.numericUpDownAccuracy.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownAccuracy.Location = new System.Drawing.Point(217, 49);
            this.numericUpDownAccuracy.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numericUpDownAccuracy.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownAccuracy.Name = "numericUpDownAccuracy";
            this.numericUpDownAccuracy.Size = new System.Drawing.Size(69, 22);
            this.numericUpDownAccuracy.TabIndex = 53;
            this.numericUpDownAccuracy.Value = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            // 
            // numericUpDownInitialStep
            // 
            this.numericUpDownInitialStep.DecimalPlaces = 4;
            this.numericUpDownInitialStep.Location = new System.Drawing.Point(217, 81);
            this.numericUpDownInitialStep.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numericUpDownInitialStep.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownInitialStep.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericUpDownInitialStep.Name = "numericUpDownInitialStep";
            this.numericUpDownInitialStep.Size = new System.Drawing.Size(69, 22);
            this.numericUpDownInitialStep.TabIndex = 54;
            this.numericUpDownInitialStep.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // buttonGetCritical3DModel
            // 
            this.buttonGetCritical3DModel.Location = new System.Drawing.Point(13, 468);
            this.buttonGetCritical3DModel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonGetCritical3DModel.Name = "buttonGetCritical3DModel";
            this.buttonGetCritical3DModel.Size = new System.Drawing.Size(275, 28);
            this.buttonGetCritical3DModel.TabIndex = 55;
            this.buttonGetCritical3DModel.Text = "Модель области допустимых значений";
            this.buttonGetCritical3DModel.UseVisualStyleBackColor = true;
            this.buttonGetCritical3DModel.Click += new System.EventHandler(this.buttonGetCritical3DModel_Click);
            // 
            // numericUpDownDI
            // 
            this.numericUpDownDI.DecimalPlaces = 10;
            this.numericUpDownDI.Location = new System.Drawing.Point(37, 145);
            this.numericUpDownDI.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numericUpDownDI.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownDI.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.numericUpDownDI.Name = "numericUpDownDI";
            this.numericUpDownDI.Size = new System.Drawing.Size(249, 22);
            this.numericUpDownDI.TabIndex = 56;
            // 
            // textBoxState
            // 
            this.textBoxState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxState.Location = new System.Drawing.Point(16, 748);
            this.textBoxState.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxState.Name = "textBoxState";
            this.textBoxState.ReadOnly = true;
            this.textBoxState.Size = new System.Drawing.Size(1213, 22);
            this.textBoxState.TabIndex = 57;
            // 
            // DigitalVoltmeterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1247, 783);
            this.Controls.Add(this.textBoxState);
            this.Controls.Add(this.numericUpDownDI);
            this.Controls.Add(this.buttonGetCritical3DModel);
            this.Controls.Add(this.numericUpDownInitialStep);
            this.Controls.Add(this.numericUpDownAccuracy);
            this.Controls.Add(this.numericUpDownDUsm);
            this.Controls.Add(this.numericUpDownDK);
            this.Controls.Add(this.numericUpDownK);
            this.Controls.Add(this.numericUpDownN);
            this.Controls.Add(this.groupBoxCriticalValues);
            this.Controls.Add(this.labelInitialStep);
            this.Controls.Add(this.labelAccuracy);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridViewVect);
            this.Controls.Add(this.buttonExpand);
            this.Controls.Add(this.mainChart);
            this.Controls.Add(this.buttonGetModel);
            this.Controls.Add(this.labelDUsm);
            this.Controls.Add(this.labelDI);
            this.Controls.Add(this.labelDK);
            this.Controls.Add(this.labelK);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBoxOutToWord);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonGetFormules);
            this.Controls.Add(this.labelN);
            this.Controls.Add(this.buttonSaveToExel);
            this.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.MinimumSize = new System.Drawing.Size(962, 757);
            this.Name = "DigitalVoltmeterForm";
            this.Text = "DigitalVoltmeter";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DigitalVoltmeterForm_FormClosed);
            this.Load += new System.EventHandler(this.DigitalVoltmeterForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mainChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVect)).EndInit();
            this.groupBoxCriticalValues.ResumeLayout(false);
            this.groupBoxCriticalValues.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDUsm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAccuracy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInitialStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDI)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonGetFormules;
        private System.Windows.Forms.Label labelN;
        private System.Windows.Forms.Button buttonSaveToExel;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.CheckBox checkBoxOutToWord;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelK;
        private System.Windows.Forms.Label labelDK;
        private System.Windows.Forms.Label labelDI;
        private System.Windows.Forms.Label labelDUsm;
        private System.Windows.Forms.Button buttonGetModel;
        private System.Windows.Forms.DataVisualization.Charting.Chart mainChart;
        private System.Windows.Forms.Button buttonExpand;
        private System.Windows.Forms.DataGridView dataGridViewVect;
        private System.Windows.Forms.Label labelCriticalDK;
        private System.Windows.Forms.Label labelCriticalDsm;
        private System.Windows.Forms.Button buttonCriticalDK;
        private System.Windows.Forms.Button buttonCriticalDi;
        private System.Windows.Forms.Button buttonCritical;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelAccuracy;
        private System.Windows.Forms.Label labelInitialStep;
        private System.Windows.Forms.GroupBox groupBoxCriticalValues;
        private System.Windows.Forms.Label labelDUsmErr;
        private System.Windows.Forms.Label labelDIErr;
        private System.Windows.Forms.Label labelDKErr;
        private System.Windows.Forms.NumericUpDown numericUpDownN;
        private System.Windows.Forms.NumericUpDown numericUpDownK;
        private System.Windows.Forms.NumericUpDown numericUpDownDK;
        private System.Windows.Forms.NumericUpDown numericUpDownDUsm;
        private System.Windows.Forms.NumericUpDown numericUpDownAccuracy;
        private System.Windows.Forms.NumericUpDown numericUpDownInitialStep;
        private System.Windows.Forms.Button buttonGetCritical3DModel;
        private System.Windows.Forms.NumericUpDown numericUpDownDI;
        private System.Windows.Forms.Label labelCriticalDI;
        private System.Windows.Forms.TextBox textBoxState;
    }
}

