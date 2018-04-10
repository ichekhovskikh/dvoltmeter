using DigitalVoltmeter.DAC;
using DigitalVoltmeter.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DigitalVoltmeter
{
    public partial class DigitalVoltmeterForm : Form
    {
        public readonly Color errorCellBackColor = Color.Pink;
        public readonly Color errorCellTextColor = Color.OrangeRed;
        public readonly Font errorFont;

        /// <summary>
        /// Отсортированные списки индексов ошибочных бит ячеек Выхода
        /// </summary>
        List<List<int>> errorBitIndexes = new List<List<int>>() { };
        private WordTools word;

        private LongBits[] e;
        private LongBits[] b;
        private LongBits[] a;

        private double[] modelVoltages;
        private double[] idealVoltages;
        private double voltagesQuantumStep;

        private Color idealVoltageColor = Color.Green;
        private Color modelVoltageColor;

        private GraphExpandingForm expandingForm;

        public DigitalVoltmeterForm()
        {
            InitializeComponent();
            InitializeDataGrid();
            expandingForm = new GraphExpandingForm();
            errorFont = new Font(dataGridViewVect.Font, FontStyle.Bold);
            word = new WordTools(progressBar);
        }

        private void InitializeDataGrid()
        {
            dataGridViewVect.Columns.Clear();

            DataGridViewTextBoxColumn _in = new DataGridViewTextBoxColumn
            {
                Name = "Вход",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };

            DataGridViewTextBoxColumn _out = new DataGridViewTextBoxColumn
            {
                Name = "Выход",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = TextRenderer.MeasureText("Выход", dataGridViewVect.Font).Width + 5
            };

            DataGridViewTextBoxColumn _inDes = new DataGridViewTextBoxColumn
            {
                Name = "Вход 10",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };

            DataGridViewTextBoxColumn _outDes = new DataGridViewTextBoxColumn
            {
                Name = "Выход 10",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = TextRenderer.MeasureText("Выход 10", dataGridViewVect.Font).Width + 6
            };

            DataGridViewTextBoxColumn _errInds = new DataGridViewTextBoxColumn
            {
                Name = "Ошиб. b",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };

            dataGridViewVect.Columns.Add(_in);
            dataGridViewVect.Columns.Add(_out);
            dataGridViewVect.Columns.Add(_inDes);
            dataGridViewVect.Columns.Add(_outDes);
            dataGridViewVect.Columns.Add(_errInds);

            dataGridViewVect.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }


        private void buttonSaveToExel_Click(object sender, EventArgs e)
        {
            if (this.e == null || b == null || a == null)
                throw new Exception("Сначала должны быть сгенерированы уравнения!");

            string[] singleCodesString = this.e.Select(val => val.ToString()).ToArray();
            string[] bString = b.Select(val => val.ToString()).ToArray();
            string[] binaryString = MathProcessor.TransposeBitMatrix(a)
                                       .Select(val => val.ToString()).ToArray();
            ExcelTools excel = new ExcelTools(progressBar);
            excel.GenerateDocument(singleCodesString, bString, binaryString);
            excel.Dispose();
            progressBar.Value = 0;
        }

        private void buttonGetFormules_Click(object sender, EventArgs e)
        {
            int bitsCount = (int)numericUpDownN.Value;
            richTextBox.Text = string.Empty;

            this.e = MathProcessor.GetAllEK((int)Math.Pow(2, bitsCount));
            b = MathProcessor.GetAllEPKFromEK(this.e);

            if (checkBoxOutToWord.Checked)
            {
                string wordFilePath = Environment.CurrentDirectory + "\\Formules[" + (b.Length + 1) + "].docx";
                word.GenerateDocument(b, out a, wordFilePath);
                FillRichTextBoxFromWord(wordFilePath);
                progressBar.Value = 0;
            }
            else
            {
                string[] formules = MathProcessor.Formules(b, out a);
                for (int i = 0; i < formules.Length; i++)
                    richTextBox.Text += "a" + i + " =" + formules[i] + Environment.NewLine;
            }
        }

        private void FillRichTextBoxFromWord(string docxFile)
        {
            richTextBox.Text = string.Empty;
            string rtfPath = string.Empty;
            rtfPath = WordTools.GetRTFFromDOCXFile(docxFile);
            richTextBox.LoadFile(rtfPath);
        }

        private void buttonGetModel_Click(object sender, EventArgs e)
        {
            int n = 0;
            double coeff = 0, deltaCoeff = 0, deltaSM = 0, deltaI = 0;
            try
            {
                n = (int)numericUpDownN.Value;
                coeff = (double)numericUpDownK.Value;
                deltaCoeff = (double)numericUpDownDK.Value;
                deltaI = (double)numericUpDownDI.Value;
                deltaSM = (double)numericUpDownDUsm.Value;
            }
            catch
            {
                MessageBox.Show("Неверный формат входных параметров!");
                return;
            }

            DACEmulator emulator = new DACEmulator(n, coeff, deltaCoeff, deltaI, deltaSM);
            voltagesQuantumStep = emulator.RealStep;
            int countNumbers = (int)Math.Pow(2, n);
            modelVoltages = new double[countNumbers];
            idealVoltages = new double[countNumbers];

            dataGridViewVect.Rows.Clear();
            for (int x = 0; x < countNumbers; x++)
            {
                modelVoltages[x] = emulator.Uin(x);
                idealVoltages[x] = emulator.IdealUin(x);
                LongBits binaryCode = emulator.GetDKFromComparators(x);
                LongBits inCode = new LongBits(x, n);
                int[] errorInds = emulator.GetEKPErrorFromComparators(x);
                
                dataGridViewVect.Rows.Add(new object[] { inCode, binaryCode, inCode.ToLong(), binaryCode.ToLong(), string.Join(", ", errorInds) });
                if (inCode != binaryCode)
                    dataGridViewVect.Rows[x].DefaultCellStyle.BackColor = errorCellBackColor;
            }
            modelVoltageColor = Color.DarkOrchid;
            VoltageChartService chartService = new VoltageChartService(this.mainChart, "Входное напряжение", voltagesQuantumStep);
            chartService.AddInputVoltageList("Voltages", modelVoltages, modelVoltageColor, 2);
            chartService.AddInputVoltageList("Ideal voltages", idealVoltages, idealVoltageColor, 2);
        }

        private List<ParamsContainer> TestingModel(int n, double coeff,double accuracy,double initialStep)
        {
            List<ParamsContainer> list = new List<ParamsContainer>();
            list.Add(CriticalParamsService.TestingDelta(n, coeff, DACEmulator.Delta.Coeff, initialStep, accuracy));
            list.Add(CriticalParamsService.TestingDelta(n, coeff, DACEmulator.Delta.Index, initialStep, accuracy));
            list.Add(CriticalParamsService.TestingDelta(n, coeff, DACEmulator.Delta.SM, initialStep, accuracy));
            return list;
        }

        bool GetIndexesOfDiffs(LongBits first, LongBits second, out List<int> diffs)
         {//Все таки пришлось циклом сравнить. Ваня, не бей(
             diffs = null;
             if (first.Length != second.Length) return first == second;
             diffs = new List<int>() { };
             string sf = first.ToString();
             string ss = second.ToString();
             for (int i = 0; i<sf.Length; i++)
                 if (sf[i] != ss[i]) diffs.Add(i);
             return diffs.Count == 0;
         }
 


        private void buttonExpand_Click(object sender, EventArgs e)
        {
            if (modelVoltages == null)
            {
                MessageBox.Show("First generate the data!");
                return;
            }

            if (!expandingForm.Visible)
            {
                expandingForm = new GraphExpandingForm();
                expandingForm.Chart.Series.Clear();
                VoltageChartService chartService = new VoltageChartService(expandingForm.Chart, "Входное напряжение", voltagesQuantumStep);
                chartService.AddInputVoltageList("Voltages", modelVoltages, modelVoltageColor, 2);
                chartService.AddInputVoltageList("Ideal voltages", idealVoltages, idealVoltageColor, 2);
                expandingForm.Show();
            }
        }

        private void dataGridViewVect_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || (e.ColumnIndex != 1 && e.ColumnIndex != 3))
                return;

            DataGridView dataGridView = sender as DataGridView;
            LongBits inCode = dataGridView.Rows[e.RowIndex].Cells[0].Value as LongBits;
            LongBits binaryCode = dataGridView.Rows[e.RowIndex].Cells[1].Value as LongBits;
            int[] difference = LongBits.GetStringIndexesOfDifferenceBits(inCode, binaryCode);

            //кастомная отрисовка ячеек Выхода
            if (e.ColumnIndex == 1)
            {
                e.PaintBackground(e.ClipBounds, true);

                Font font = e.CellStyle.Font;
                TextFormatFlags flags = TextFormatFlags.NoPadding | TextFormatFlags.VerticalCenter;

                string text = (string)e.FormattedValue;

                List<string> subStrings = new List<string>();
                int curInd = 0;
                foreach (int ind in difference)
                {
                    subStrings.Add(text.Substring(curInd, ind - curInd));
                    subStrings.Add(text.Substring(ind, 1));
                    curInd = ind + 1;
                }
                subStrings.Add(text.Substring(curInd, text.Length - curInd));

                bool errorState = false;
                Size size;
                Rectangle curBox = new Rectangle(e.CellBounds.X + 3, e.CellBounds.Y - 1, 0, e.CellBounds.Height);
                for (int i = 0; i < subStrings.Count; i++)
                {
                    Font curFont = errorState ? errorFont : font;
                    Color curColor = errorState ? errorCellTextColor : e.CellStyle.ForeColor;

                    size = TextRenderer.MeasureText(e.Graphics, subStrings[i], curFont, e.CellBounds.Size, flags);
                    curBox = new Rectangle(curBox.X + curBox.Width, curBox.Y, size.Width, curBox.Height);
                    TextRenderer.DrawText(e.Graphics, subStrings[i], curFont, curBox, curColor, flags);

                    errorState = !errorState;
                }
                int cellWidth = curBox.Location.X - e.CellBounds.Location.X + curBox.Width + 5;
                dataGridViewVect.Columns[1].Width = Math.Max(dataGridViewVect.Columns[1].Width, cellWidth);
                e.Handled = true;
            }
            else if (e.ColumnIndex == 3)
            {
                e.PaintBackground(e.ClipBounds, true);
                bool error = difference.Length > 0;
                Font font = error ? errorFont : e.CellStyle.Font;
                Color color = error ? errorCellTextColor : e.CellStyle.ForeColor;
                TextFormatFlags flags = TextFormatFlags.NoPadding | TextFormatFlags.VerticalCenter;

                string text = (string)e.FormattedValue;

                Size size = TextRenderer.MeasureText(e.Graphics, text, font, e.CellBounds.Size, flags);
                Rectangle Box = new Rectangle(e.CellBounds.X + 3, e.CellBounds.Y - 1, size.Width, e.CellBounds.Height);
                TextRenderer.DrawText(e.Graphics, text, font, Box, color, flags);

                int cellWidth = e.CellBounds.Location.X - e.CellBounds.Location.X + Box.Width + 8;
                dataGridViewVect.Columns[3].Width = Math.Max(dataGridViewVect.Columns[3].Width, cellWidth);
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Коля, для вызова данных методов в UI возможно следует добавить поля для ввода шага, точности, границ разрядности 
            //(см. параметры метода OutputTablesToExcel в классе CriticalParamsService

            //============================== Вывод в excel ==============================
            //Разкомментировать эту строчку обязательно, если будут выводиться таблицы в excels
            //CriticalParamsService cps = new CriticalParamsService();
            //Разкомментировать эту строчку, если нужен вывод в excel одной таблицы
            //cps.OutputTableToExcel(int.Parse(textBoxN.Text), int.Parse(textBoxK.Text));
            //Разкомментировать эту строчку, если нужен вывод в excel совокупности таблиц заданных разрядностей
            //cps.OutputTablesToExcel(2, 8, int.Parse(textBoxK.Text));
            //Раскоментировать следующие две строки, чтобы показать excel и закрыть его
            //cps.Show();
            //cps.Dispose();
            //============================== Вывод в excel ==============================

            //ParamsContainer container = CriticalParamsService.TestingDeltaIndexes(int.Parse(textBoxN.Text), double.Parse(textBoxK.Text));
            //String s = string.Join(", ", container.DeltaIndexes);
            //MessageBox.Show(s);
            int n = (int)numericUpDownN.Value;
            double k = (double)numericUpDownK.Value;
            double accuracy = (double)numericUpDownAccuracy.Value;
            double initialStep = (double)numericUpDownInitialStep.Value;

            List<ParamsContainer> list = TestingModel(n, k, accuracy, initialStep);
            labelCriticalDK.Text = list[0].DeltaCoeff.ToString();
            labelCriticalDI.Text = list[1].DeltaIndex.ToString();
            labelCriticalDsm.Text = list[2].DeltaSM.ToString();
        }

        private void comboBoxResistorsCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void DigitalVoltmeterForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void dataGridViewVect_SelectionChanged(object sender, EventArgs e)
        {
            dataGridViewVect.ClearSelection();
        }

        private void buttonCriticalDK_Click(object sender, EventArgs e)
        {
            int n = 0, deltaIndex = 0;
            double coeff = 0, deltaCoeff = 0, deltaSM = 0;

            n = (int)numericUpDownN.Value;
            coeff = (double)numericUpDownK.Value;
            try
            {
                deltaCoeff = double.Parse(labelCriticalDK.Text);
            }
            catch
            {
                MessageBox.Show("Критические параметры отсутствуют!");
                return;
            }
            DACEmulator emulator = new DACEmulator(n, coeff, deltaCoeff, deltaIndex, deltaSM);
            voltagesQuantumStep = emulator.RealStep;
            int countNumbers = (int)Math.Pow(2, n);
            modelVoltages = new double[countNumbers];
            idealVoltages = new double[countNumbers];

            dataGridViewVect.Rows.Clear();
            for (int x = 0; x < countNumbers; x++)
            {
                modelVoltages[x] = emulator.Uin(x);
                idealVoltages[x] = emulator.IdealUin(x);
                LongBits binaryCode = emulator.GetDKFromComparators(x);
                LongBits inCode = new LongBits(x, n);
                int[] errorInds = emulator.GetEKPErrorFromComparators(x);

                dataGridViewVect.Rows.Add(new object[] { inCode, binaryCode, inCode.ToLong(), binaryCode.ToLong(), string.Join(", ", errorInds) });
                if (inCode != binaryCode)
                    dataGridViewVect.Rows[x].DefaultCellStyle.BackColor = errorCellBackColor;
            }
            modelVoltageColor = Color.Red;
            VoltageChartService chartService = new VoltageChartService(this.mainChart, "Входное напряжение при критическом ΔK", voltagesQuantumStep);
            chartService.AddInputVoltageList("Voltages", modelVoltages, modelVoltageColor, 2);
            chartService.AddInputVoltageList("Ideal voltages", idealVoltages, idealVoltageColor, 2);
        }

        private void numericUpDownN_TextChanged(object sender, EventArgs e)
        {
            labelCriticalDK.Text = "";
            labelCriticalDI.Text = "";
            labelCriticalDsm.Text = "";
        }

        private void buttonCriticalDi_Click(object sender, EventArgs e)
        {
            int n = 0;
            double coeff = 0, deltaCoeff = 0, deltaSM = 0, deltaI = 0;

            n = (int)numericUpDownN.Value;
            coeff = (double)numericUpDownK.Value;
            try
            {
                deltaI = double.Parse(labelCriticalDI.Text);
            }
            catch
            {
                MessageBox.Show("Критические параметры отсутствуют!");
                return;
            }
            DACEmulator emulator = new DACEmulator(n, coeff, deltaCoeff, deltaI, deltaSM);
            voltagesQuantumStep = emulator.RealStep;
            int countNumbers = (int)Math.Pow(2, n);
            modelVoltages = new double[countNumbers];
            idealVoltages = new double[countNumbers];

            dataGridViewVect.Rows.Clear();
            for (int x = 0; x < countNumbers; x++)
            {
                modelVoltages[x] = emulator.Uin(x);
                idealVoltages[x] = emulator.IdealUin(x);
                LongBits binaryCode = emulator.GetDKFromComparators(x);
                LongBits inCode = new LongBits(x, n);
                int[] errorInds = emulator.GetEKPErrorFromComparators(x);

                dataGridViewVect.Rows.Add(new object[] { inCode, binaryCode, inCode.ToLong(), binaryCode.ToLong(), string.Join(", ", errorInds) });
                if (inCode != binaryCode)
                    dataGridViewVect.Rows[x].DefaultCellStyle.BackColor = errorCellBackColor;
            }
            modelVoltageColor = Color.FromArgb(255, 128, 0); ;
            VoltageChartService chartService = new VoltageChartService(this.mainChart, "Входное напряжение при критическом Δi", voltagesQuantumStep);
            chartService.AddInputVoltageList("Voltages", modelVoltages, modelVoltageColor, 2);
            chartService.AddInputVoltageList("Ideal voltages", idealVoltages, idealVoltageColor, 2);
        }

        private void buttonCritical_Click(object sender, EventArgs e)
        {
            int n = 0, deltaIndex = 0;
            double coeff = 0, deltaCoeff = 0, deltaSM = 0;

            n = (int)numericUpDownN.Value;
            coeff = (double)numericUpDownK.Value;
            try
            {
                deltaSM = double.Parse(labelCriticalDsm.Text);
            }
            catch
            {
                MessageBox.Show("Критические параметры отсутствуют!");
                return;
            }
            DACEmulator emulator = new DACEmulator(n, coeff, deltaCoeff, deltaIndex, deltaSM);
            voltagesQuantumStep = emulator.RealStep;
            int countNumbers = (int)Math.Pow(2, n);
            modelVoltages = new double[countNumbers];
            idealVoltages = new double[countNumbers];

            dataGridViewVect.Rows.Clear();
            for (int x = 0; x < countNumbers; x++)
            {
                modelVoltages[x] = emulator.Uin(x);
                idealVoltages[x] = emulator.IdealUin(x);
                LongBits binaryCode = emulator.GetDKFromComparators(x);
                LongBits inCode = new LongBits(x, n);
                int[] errorInds = emulator.GetEKPErrorFromComparators(x);

                dataGridViewVect.Rows.Add(new object[] { inCode, binaryCode, inCode.ToLong(), binaryCode.ToLong(), string.Join(", ", errorInds) });
                if (inCode != binaryCode)
                    dataGridViewVect.Rows[x].DefaultCellStyle.BackColor = errorCellBackColor;
            }
            modelVoltageColor = Color.FromKnownColor(KnownColor.Highlight);
            VoltageChartService chartService = new VoltageChartService(this.mainChart, "Входное напряжение при критическом δсм", voltagesQuantumStep);
            chartService.AddInputVoltageList("Voltages", modelVoltages, modelVoltageColor, 2);
            chartService.AddInputVoltageList("Ideal voltages", idealVoltages, idealVoltageColor, 2);
        }

        private void DigitalVoltmeterForm_Load(object sender, EventArgs e)
        {

        }

        private async void buttonGetCritical3DModel_Click(object sender, EventArgs e)
        {
            await BuildModel();
        }

        private async Task BuildModel()
        {
            int n = 0;
            double coeff = 0;
            try
            {
                n = (int)numericUpDownN.Value;
                coeff = (double)numericUpDownK.Value;
            }
            catch
            {
                MessageBox.Show("Неверный формат входных параметров!");
                return;
            }
            int countOfResolution = 20;

            CriticalParamsService.TextBoxLog = textBoxState;
            List<ParamsPolygon> polygons = await CriticalParamsService.GetPolygonsOfCriticalArea(n, coeff, countOfResolution, 1);
            GraphForm graphForm = new GraphForm(polygons, this);
            graphForm.Show();
        }

        public double K
        {
            get { return Convert.ToDouble(numericUpDownK.Value); }
            set { numericUpDownK.Value = Convert.ToDecimal(value); }
        }

        public double DeltaK
        {
            get { return Convert.ToDouble(numericUpDownDK.Value); }
            set { numericUpDownDK.Value = Convert.ToDecimal(value); }
        }

        public double DeltaI
        {
            get { return Convert.ToDouble(numericUpDownDI.Value); }
            set { numericUpDownDI.Value = Convert.ToDecimal(value); }
        }

        public double DeltaUsm
        {
            get { return Convert.ToDouble(numericUpDownDUsm.Value); }
            set { numericUpDownDUsm.Value = Convert.ToDecimal(value); }
        }

        public void GetModelPerformClick()
        {
            buttonGetModel.PerformClick();
        }
    }
}