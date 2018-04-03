using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using Microsoft.Office.Interop.Word;
using System.Windows.Forms;
using System.Drawing;

namespace DigitalVoltmeter
{
    class WordTools : IDocumentTools
    {
        private Word.Application application;
        private Document document;
        private string notSign = " ̅";

        private ProgressBar progressBar = null;
        private DelegatePerformStep performStep = null;

        private delegate void DelegatePerformStep();
        private delegate void SetMaxValue(int value);
        private delegate void ChangeValue(int value);

        public WordTools(ProgressBar bar = null)
        {
            SetProgressBar(bar);
        }

        public void SetProgressBar(ProgressBar bar)
        {
            progressBar = bar;
            if (progressBar != null)
                performStep = new DelegatePerformStep(bar.PerformStep);
        }

        private void SetMaxValueBar(int maxValue)
        {
            if (progressBar == null)
                return;
            ChangeValue changeValue = new ChangeValue(value => progressBar.Value = value);
            SetMaxValue setMaxValue = new SetMaxValue(value => progressBar.Maximum = value);
            progressBar.Invoke(changeValue, progressBar.Minimum);
            progressBar.Invoke(setMaxValue, maxValue);
        }

        private void PerformStepBar()
        {
            if (progressBar == null)
                return;
            progressBar.Invoke(performStep);
            ProgressBarText();
        }

        private void ProgressBarText()
        {
            string text = "Создание Word документа";
            using (Graphics g = progressBar.CreateGraphics())
            {
                g.DrawString(text, SystemFonts.DefaultFont, Brushes.Black,
                    new PointF(progressBar.Width / 2 - (g.MeasureString(text, SystemFonts.DefaultFont).Width / 2.0F),
                    progressBar.Height / 2 - (g.MeasureString(text, SystemFonts.DefaultFont).Height / 2.0F)));
            }
        }

        /// <summary>
        /// Создание документа docx с формулами ai
        /// </summary>
        /// <param name="transpB">Массив состовляющих ЕПК</param>
        /// <param name="a">ДК</param>
        /// <param name="path">Путь к результирующему файлу</param>
        public void GenerateDocument(LongBits[] b, out LongBits[] a, string path)
        {
            application = new Word.Application { Visible = false };
            try
            {
                document = application.Documents.Add();
                document.Range(0, 0).PageSetup.Orientation = WdOrientation.wdOrientLandscape;

                LongBits[] transpB = MathProcessor.TransposeBitMatrix(b);
                int n = MathProcessor.GetN(transpB.Length + 1);
                a = new LongBits[n];
                SetMaxValueBar(n);

                for (int i = n - 1; i >= 0; i--)
                {
                    List<string> formules = new List<string>();
                    a[i] = new LongBits(transpB[0].Length);
                    a[i] = ~a[i];
                    int kmax = (int)Math.Pow(2, n - 1 - i) - 1;
                    for (int k = 0; k <= kmax; k++)
                    {
                        int tmin = (int)Math.Pow(2, i) * (2 * k + 1);
                        int tmax = (int)Math.Pow(2, i + 1) * (k + 1) - 1;
                        for (int t = tmin; t <= tmax; t++)
                        {
                            if (formules.Count == 0 || formules[formules.Count - 1].Length > 250)
                                formules.Add(string.Empty);
                            formules[formules.Count - 1] += " b_" + t + notSign + " ";
                            a[i] &= ~transpB[t - 1];
                        }
                    }
                    a[i] = ~a[i];

                    document.Range(0, 0).Text += "\n ";
                    for (int index = formules.Count - 1; index >= 0; index--)
                    {
                        document.Range(0, 0).Text += "\n ";
                        BuildFormula(notSign);
                        BuildFormula(")", WdColor.wdColorWhite);
                        BuildFormula(formules[index]);
                        BuildFormula("(", WdColor.wdColorWhite);
                        if (index == 0)
                            BuildFormula("a_" + i + "=");
                        else BuildFormula("          ");
                        document.OMaths.BuildUp();
                    }
                    PerformStepBar();
                }
                document.SaveAs(path);
            }
            finally
            {
                //Закрывание ворда
                document.Close();
                application.Quit();
            }
        }

        /// <summary>
        /// Создает временный файл .rtf из файла .docx для импорта в richTextBox
        /// </summary>
        /// <param name="docxFile">Путь к файлу .docx</param>
        /// <returns>Путь к файлу .rtf</returns>
        public static string GetRTFFromDOCXFile(string docxFile)
        {
            Word.Application app = new Word.Application();
            string rtfFileName;
            object missing = Type.Missing;
            try
            {
                app.Documents.Open(docxFile,
                    missing, missing, missing, missing, missing,
                    missing, missing, missing, missing, missing,
                    missing, missing, missing, missing, missing);
                string temp = System.IO.Path.GetTempPath();
                int safeNameStartindex = docxFile.LastIndexOf('\\') + 1;
                rtfFileName = docxFile.Substring(safeNameStartindex, docxFile.Length - ".docx".Length - safeNameStartindex);
                rtfFileName += ".rtf";
                rtfFileName = temp + rtfFileName;

                #region permissions
                object lookComments = false;
                object password = string.Empty;
                object AddToRecentFiles = true;
                object WritePassword = string.Empty;
                object ReadOnlyRecommended = false;
                object EmbedTrueTypeFonts = false;
                object SaveFormsData = false;
                object SaveAsAOCELetter = false;
                #endregion

                app.ActiveDocument.SaveAs(rtfFileName, Word.WdSaveFormat.wdFormatRTF,
                    lookComments, password, AddToRecentFiles, WritePassword,
                    ReadOnlyRecommended, EmbedTrueTypeFonts, missing, SaveFormsData,
                    SaveAsAOCELetter, missing, missing, missing, missing, missing);
            }
            finally
            {
                app.ActiveDocument.Close(false, missing, missing);
                app.Quit(false, missing, missing);
            }
            return rtfFileName;
        }

        private void BuildFormula(string text, WdColor color = WdColor.wdColorBlack)
        {
            Range range = document.Paragraphs[1].Range;
            range.Text += text;
            range.Font.Color = color;
            document.OMaths.Add(range);
        }
    }
}
