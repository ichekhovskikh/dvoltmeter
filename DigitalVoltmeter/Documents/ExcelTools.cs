using Microsoft.Office.Interop.Excel;
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace DigitalVoltmeter
{
    class ExcelTools : IDocumentTools
    {
        private Excel.Application excelApp;
        private Workbook workBook;
        private Worksheet workSheet;

        private ProgressBar progressBar = null;
        private DelegatePerformStep performStep = null;

        private delegate void DelegatePerformStep();
        private delegate void SetMaxValue(int value);
        private delegate void ChangeValue(int value);

        private int colorIndexGeneral1 = 2;

        public ExcelTools(ProgressBar bar = null)
        {
            SetProgressBar(bar);
            excelApp = new Excel.Application();
            workBook = excelApp.Workbooks.Add();
            workSheet = workBook.Worksheets.get_Item(1);
            workSheet.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            workSheet.Cells.VerticalAlignment = XlHAlign.xlHAlignCenter;
        }

        public ExcelTools()
        {
            excelApp = new Excel.Application();
            workBook = excelApp.Workbooks.Add();
            workSheet = workBook.Worksheets.get_Item(1);
            workSheet.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            workSheet.Cells.VerticalAlignment = XlHAlign.xlHAlignCenter;
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
            string text = "Создание Exel документа";
            using (Graphics g = progressBar.CreateGraphics())
            {
                g.DrawString(text, SystemFonts.DefaultFont, Brushes.Black,
                    new PointF(progressBar.Width / 2 - (g.MeasureString(text, SystemFonts.DefaultFont).Width / 2.0F),
                    progressBar.Height / 2 - (g.MeasureString(text, SystemFonts.DefaultFont).Height / 2.0F)));
            }
        }

        public void GenerateDocument(string[] singleCodes, string[] b, string[] binaryCodes)
        {
            SetMaxValueBar(singleCodes.Length + b.Length + binaryCodes.Length);

            int columnStartingNum = 1;
            int rowStartingNum = 1;

            //Таблица с единичным кодом
            //Заголовки
            workSheet.get_Range("A1", "A2").Cells.Merge(Type.Missing);
            MergeCells(1, singleCodes.Length - 1);
            workSheet.Cells[rowStartingNum, 1] = "N10";
            workSheet.get_Range(GetCellName(0, 1),
                        Missing.Value).Interior.ColorIndex = 15;
            workSheet.Cells[rowStartingNum, 2] = "Единичный код";
            rowStartingNum++;
            //Вывод значений единичных кодов
            printTable(singleCodes, rowStartingNum, columnStartingNum, workSheet, "e", 0);

            //Таблица с единичным позиционным кодом
            columnStartingNum += singleCodes.Length - 1;
            rowStartingNum = 1;
            //Заголовки
            MergeCells(columnStartingNum, columnStartingNum + singleCodes.Length - 2);
            workSheet.Cells[rowStartingNum, columnStartingNum + 1] = "Единичный позиционный код";
            rowStartingNum++;
            //Вывод индексов единичных позиционных кодов
            for (int i = b.Length; i > 0; i--)
            {
                workSheet.Cells[rowStartingNum, columnStartingNum + i] = "b" + (i);
                workSheet.get_Range(GetCellName(columnStartingNum + i - 1, rowStartingNum), Missing.Value).Interior.ColorIndex = 43;
            }
            rowStartingNum++;
            //Вывод значений с единичными позиционными кодами
            for (int i = 0; i < b.Length; i++)
            {
                for (int j = 0; j < b[i].Length; j++)
                {
                    workSheet.Cells[rowStartingNum + i, columnStartingNum + j + 1] =
                        b[i][b[i].Length - 1 - j].ToString();
                    workSheet.get_Range(GetCellName(columnStartingNum + j, rowStartingNum + i),
                        Missing.Value).Interior.ColorIndex = colorIndexGeneral1;
                }
                ChangeCellFillingColor(ref colorIndexGeneral1, 2, 15);
                PerformStepBar();

            }

            //Таблица с двоичным кодом
            columnStartingNum += singleCodes.Length - 1;
            rowStartingNum = 1;
            //Заголовки
            MergeCells(columnStartingNum, columnStartingNum + (int)Math.Ceiling(Math.Log(binaryCodes.Length, 2)) - 1);
            workSheet.Cells[rowStartingNum, columnStartingNum + 1] = "Двоичный код";
            rowStartingNum++;
            //Вывод заголовков с индексами двоичных кодов
            int bitsCount = (int)Math.Ceiling(Math.Log(binaryCodes.Length, 2));
            for (int i = 0; i < bitsCount; i++)
            {
                workSheet.Cells[rowStartingNum, columnStartingNum + i + 1] = "2^" + (bitsCount - i - 1);
                workSheet.get_Range(GetCellName(columnStartingNum + i, rowStartingNum), Missing.Value).Interior.ColorIndex = 4;
            }
            rowStartingNum++;
            //Вывод значений с двоичными кодами
            for (int i = 0; i < binaryCodes.Length; i++)
            {
                for (int j = 0; j < binaryCodes[i].Length; j++)
                {
                    workSheet.Cells[rowStartingNum + i, columnStartingNum + j + 1] =
                        binaryCodes[i][j].ToString();
                    workSheet.get_Range(GetCellName(columnStartingNum + j, rowStartingNum + i),
                        Missing.Value).Interior.ColorIndex = colorIndexGeneral1;
                }
                ChangeCellFillingColor(ref colorIndexGeneral1, 2, 15);
                PerformStepBar();
            }

            Show();
        }

        public void Show()
        {
            excelApp.Visible = true;
            excelApp.UserControl = true;
        }

        /// <summary>
        /// Печать таблицы с единичными кодами
        /// </summary>
        /// <param name="values">Массив единичных кодов</param>
        /// <param name="row">Строка, с которой следует начать заполнение ячеек</param>
        /// <param name="column">Столбец, с которого следует начать заполнение ячеек</param>
        /// <param name="workSheet">Страница excel</param>
        /// <param name="literal">Буква индекса для заголовка</param>
        /// <param name="startingIndex">Индекс массива, с которого следует начать заполнение ячеек</param>
        private void printTable(string[] values, int row, int column, Worksheet workSheet, string literal, int startingIndex)
        {
            if (startingIndex >= values.Length)
                throw new Exception("Индекс первого выводимого элемента не может быть больше размера массива");

            for (int i = values.Length - startingIndex; i > 0; i--)
            {
                workSheet.Cells[row, column + i] = literal + (i);
                workSheet.get_Range(GetCellName(column + i - 1, row), Missing.Value).Interior.ColorIndex = 10;
            }
            row++;
            for (int i = 0; i < values.Length; i++)
            {
                workSheet.Cells[row + i, 1] = i;
                workSheet.get_Range(GetCellName(0, row + i),
                        Missing.Value).Interior.ColorIndex = 16;
                for (int j = startingIndex; j < values[i].Length; j++)
                {
                    workSheet.Cells[row + i, column + (values[i].Length - j)] =
                        values[i][j].ToString();
                    workSheet.get_Range(GetCellName(column + (values[i].Length - j) - 1, row + i),
                        Missing.Value).Interior.ColorIndex = colorIndexGeneral1;

                }
                ChangeCellFillingColor(ref colorIndexGeneral1, 2, 15);
                PerformStepBar();
            }
        }


        public void FillColor(int row, int column, Color color, int width = 1, int height = 1)
        {
            if (row < 0 || column < 0 || width < 1 || height < 1)
                throw new Exception("Неккоректные значения входных параметров!");
            //workSheet.get_Range(GetCellName(column, row + 1), Missing.Value).Interior.ColorIndex = color;
            String a = GetCellName(column, row + 1);
            String b = GetCellName(column + width - 1, row + height);
            workSheet.get_Range(a, b).Interior.Color =
                ColorTranslator.ToOle(color);
        }

        /// <summary>
        /// Нанесение границ
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="borderWeight"></param>
        /// <param name="borderStyle"></param>
        public void Borders(int row, int column, int width = 1, int height = 1, int borderWeight = 4, Microsoft.Office.Interop.Excel.XlLineStyle borderStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous)
        {
            if (width < 1 || height < 1 || row < 0 | column < 0 || borderWeight < 0)
                throw new Exception("Отрицательные параметры недопустимы!");
            if (borderWeight > 0)
            {
                string startingLiteral = GetCellName(column, row + 1);
                string endLiteral = GetCellName(column + width - 1, row + height);
                for (int i = 7; i < 11; i++)
                {
                    workSheet.get_Range(startingLiteral, endLiteral).Borders[(XlBordersIndex)i].LineStyle = borderStyle;
                    workSheet.get_Range(startingLiteral, endLiteral).Borders[(XlBordersIndex)i].Weight = borderWeight;
                }
            }
        }

        /// <summary>
        /// Занесение данных в ячейку
        /// </summary>
        /// <param name="text"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="borderWeight"></param>
        /// <param name="borderStyle"></param>
        public void Print(object text, int row, int column, int width = 1, int height = 1, int borderWeight = 0, Microsoft.Office.Interop.Excel.XlLineStyle borderStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Color? color = null)
        {
            if (width < 1 || height < 1 || row < 0 | column < 0 || borderWeight < 0)
                throw new Exception("Отрицательные параметры недопустимы!");

            Write(row, column, text);
            if (width > 1 || height > 1)
                MergeCells(column, column + width - 1, row, row + height - 1);
            Borders(row, column, width, height, borderWeight, borderStyle);
            if (color != null)
                FillColor(row, column, color.Value, width, height);
        }

        /// <summary>
        /// Объединение ячеек
        /// </summary>
        /// <param name="columnStartingIndex"></param>
        /// <param name="columnEndingIndex"></param>
        /// <param name="workSheet"></param>
        public void MergeCells(int columnStartingIndex, int columnEndingIndex, int rowStartingIndex = 0, int rowEndingIndex = 0)
        {
            string startingLiteral = GetCellName(columnStartingIndex, rowStartingIndex + 1);
            string endLiteral = GetCellName(columnEndingIndex, rowEndingIndex + 1);
            workSheet.get_Range(startingLiteral, endLiteral).Cells.Merge(Type.Missing);
        }

        private string GetCellName(int columnIndex, int rowIndex)
        {
            return (columnIndex > 676 ? ((char)('A' + columnIndex / 676 - 1)).ToString() : "") +
                ((columnIndex % 676) >= 26 ? ((char)('A' + (columnIndex % 676) / 26 - 1)).ToString() : "")
                + (char)('A' + (columnIndex % 26)) + rowIndex.ToString();
        }

        public void Write(int row, int column, object text)
        {
            if (row < 0 || column < 0)
                throw new Exception("Выход индексов за границы таблицы!");
            workSheet.get_Range(GetCellName(column, row + 1)).NumberFormat = "@";
            workSheet.Cells[row + 1, column + 1] = text.ToString();
        }

        public void SetColumnsWidth(int columnIndex1, int columnIndex2, int width)
        {
            if (width < 0)
                workSheet.Range[GetCellName(columnIndex1, 1), GetCellName(columnIndex2, 1)].EntireColumn.AutoFit();
            else
                workSheet.Range[GetCellName(columnIndex1, 1), GetCellName(columnIndex2, 1)].EntireColumn.ColumnWidth = width;
        }

        private void ChangeCellFillingColor(ref int colorIndexGeneral, int colorIndex1, int colorIndex2)
        {
            if (colorIndexGeneral == colorIndex1)
                colorIndexGeneral = colorIndex2;
            else
                colorIndexGeneral = colorIndex1;
        }

        public void Dispose()
        {
            if (excelApp != null)
                excelApp.Quit();
        }
    }
}