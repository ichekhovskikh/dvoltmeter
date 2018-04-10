using DigitalVoltmeter.DAC;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DigitalVoltmeter
{
    class CriticalParamsService
    {

        private ExcelTools excelTools;
        private int outputParamsCount = 4; //coeff, delta(coeff, index), sigmaSm
        private int widthTable;
        public static TextBox TextBoxLog { get; set; }

        public CriticalParamsService()
        {
            widthTable = outputParamsCount * 3;
            excelTools = new ExcelTools();
        }

        /// <summary>
        /// Вывод совокупности таблиц заданных разрядностей
        /// </summary>
        /// <param name="nStart">Разрядность первой таблицы из набора выводимых таблиц</param>
        /// <param name="nEnd">Разрядность последней таблицы из набора выводимых таблиц</param>
        /// <param name="coeff"></param>
        /// <param name="initialStep">Первоначальный шаг, с которым будут искаться критические значения параметров</param>
        /// <param name="accuracy">Желаемая точность измерения критических параметров</param>
        public void OutputTablesToExcel(int nStart, int nEnd, int coeff, double initialStep = 1, double accuracy = 0.0001)
        {
            int indexOfTable = 0;
            int spacingBetweenTables = 1;
            for (int i = nStart; i <= nEnd; i++)
            {
                int heightTable = (int)Math.Pow(2, i) + 10;
                OutputTableToExcel(i, coeff, 0, indexOfTable * (widthTable + spacingBetweenTables), initialStep, accuracy);
                OutputTableToExcel(i, coeff, heightTable + spacingBetweenTables, indexOfTable * (widthTable + spacingBetweenTables), -initialStep, accuracy);
                indexOfTable++;
            }
        }

        /// <summary>
        /// Вывод одной таблицы заданной разрядности
        /// </summary>
        /// <param name="n"></param>
        /// <param name="coeff"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="initialStep">Первоначальный шаг, с которым будут искаться критические значения параметров</param>
        /// <param name="accuracy">Желаемая точность измерения критических параметров</param>
        public void OutputTableToExcel(int n, int coeff, int row = 0, int column = 0, double initialStep = 1, double accuracy = 0.0001)
        {
            List<ParamsContainer> parameters = new List<ParamsContainer>();
            parameters.Add(TestingDelta(n, coeff, DACEmulator.Delta.Coeff, initialStep, accuracy));
            parameters.Add(TestingDelta(n, coeff, DACEmulator.Delta.Index, initialStep, accuracy));
            parameters.Add(TestingDelta(n, coeff, DACEmulator.Delta.SM, initialStep, accuracy));

            string title = "Разрядность " + n + " (" + (initialStep > 0 ? "положительные" : "отрицательные") + " значения критических параметров)";
            PrintTableWithCriticalData(parameters, row, column, title);
        }

        /// <summary>
        /// Печать таблицы, которая содержит 3 набора критических параметров
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        private void PrintTableWithCriticalData(List<ParamsContainer> parameters, int row = 0, int column = 0, string title = "Заголовок таблицы")
        {
            if (parameters.Count != 3)
                throw new Exception("Число наборов параметров должно быть равно 3!");
            if (outputParamsCount == 0)
                throw new Exception("Непроинициализированное число выводимых параметров в таблицу!");


            excelTools.Print(title, row, column, widthTable, 2, 4, Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Color.LightGreen);
            for (int i = 0; i < parameters.Count; i++)
            {
                PrintCriticalParameter("K", parameters[i].Coeff.ToString(), row + 2, column + i * outputParamsCount);
                PrintCriticalParameter("ΔK", parameters[i].DeltaCoeff.ToString(), row + 2, column + 1 + i * outputParamsCount);
                PrintCriticalParameter("Δi", parameters[i].DeltaIndex.ToString(), row + 2, column + 2 + i * outputParamsCount);
                PrintCriticalParameter("δсм", parameters[i].DeltaSM.ToString(), row + 2, column + 3 + i * outputParamsCount);
                excelTools.Borders(row + 2, column + i * outputParamsCount, outputParamsCount, 2);
            }
            excelTools.Print("Индексы компараторов со сбоем", row + 4, column, widthTable, 2, 4, Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Color.Gainsboro);
            excelTools.Print("Входные и выходные данные", row + 7, column, widthTable, 2, 0, Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Color.Gainsboro);
            excelTools.Borders(row + 7, column, widthTable, 2, 4);
            excelTools.Borders(row + 9, column, widthTable, 1, 4);
            for (int i = 0; i < parameters.Count; i++)
            {
                excelTools.Print(string.Join(", ", parameters[i].ComparatorsErrorIndexes), row + 6, column + outputParamsCount * i, outputParamsCount, 1, 4);
                PrintVector(parameters[i].InputBinaryCodes.ToArray(), row + 10, column + outputParamsCount * i);
                PrintVector(parameters[i].OutputBinaryCodes.ToArray(), row + 10, column + 1 + outputParamsCount * i);
                PrintVector(parameters[i].InputBinaryCodes.Select(x => x.ToLong().ToString()).ToArray<string>(), row + 10, column + 2 + outputParamsCount * i);
                PrintVector(parameters[i].OutputBinaryCodes.Select(x => x.ToLong().ToString()).ToArray<string>(), row + 10, column + 3 + outputParamsCount * i);
                excelTools.Print("Вход2", row + 9, column + i * outputParamsCount, 1, 1, 0, Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Color.Silver);
                excelTools.Print("Выход2", row + 9, column + i * outputParamsCount + 1, 1, 1, 0, Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Color.Silver);
                excelTools.Print("Вход10", row + 9, column + i * outputParamsCount + 2, 1, 1, 0, Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Color.Silver);
                excelTools.Print("Выход10", row + 9, column + i * outputParamsCount + 3, 1, 1, 0, Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Color.Silver);
                excelTools.Borders(row + 10, column + i * outputParamsCount, 4, parameters[i].InputBinaryCodes.Count);
                excelTools.FillColor(row + 10, column + i * outputParamsCount, Color.LavenderBlush, 1, parameters[i].InputBinaryCodes.Count);
                excelTools.FillColor(row + 10, column + i * outputParamsCount + 1, Color.Gainsboro, 1, parameters[i].InputBinaryCodes.Count);
                excelTools.FillColor(row + 10, column + i * outputParamsCount + 2, Color.LavenderBlush, 1, parameters[i].InputBinaryCodes.Count);
                excelTools.FillColor(row + 10, column + i * outputParamsCount + 3, Color.Gainsboro, 1, parameters[i].InputBinaryCodes.Count);
                for (int j = 0; j < parameters[i].ErrorIndexesFromInputAndOutputCodes.Count; j++)
                    excelTools.FillColor(row + 10 + parameters[i].ErrorIndexesFromInputAndOutputCodes[j], column + outputParamsCount * i, Color.Red, 4);
            }
            excelTools.SetColumnsWidth(column, column + widthTable, -1);
        }


        /// <summary>
        /// Печать критического параметра 
        /// Верхняя строка - название
        /// Нижняя строка - значение
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="width"></param>
        private void PrintCriticalParameter(string name, string value, int row, int column, int width = 1)
        {
            excelTools.Print(name, row, column, width, 1, 0);
            excelTools.Print(value, row + 1, column, width, 1, 0);
        }

        /// <summary>
        /// Печать массива данных
        /// </summary>
        /// <param name="values"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="cellWidth"></param>
        /// <param name="cellHeight"></param>
        /// <param name="isHorizontal"></param>
        /// <param name="borderWeight"></param>
        /// <param name="borderStyle"></param>
        private void PrintVector(object[] values, int row, int column, int cellWidth = 1, int cellHeight = 1, bool isHorizontal = false, int borderWeight = 0, Microsoft.Office.Interop.Excel.XlLineStyle borderStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous)
        {
            if (cellWidth < 0 || cellWidth < 0)
                throw new Exception("Отрицательные параметры недопустимы!");

            for (int i = 0; i < values.Length; i++)
            {
                if (isHorizontal)
                    excelTools.Print(values[i], row, column + cellHeight * i, cellWidth, cellHeight, borderWeight, borderStyle);
                else
                    excelTools.Print(values[i], row + i * cellHeight, column, cellWidth, cellHeight, borderWeight, borderStyle);
            }
            if (isHorizontal)
                excelTools.Borders(row, column, values.Length * cellWidth, cellHeight, borderWeight, borderStyle);
            else
                excelTools.Borders(row, column, cellWidth, values.Length * cellHeight, borderWeight, borderStyle);
        }

        public static ParamsContainer TestingDelta(int n, double coeff, DACEmulator.Delta delta, double initialStep = 1, double accuracy = 0.0001, 
            double deltaCoeff = 0, double deltaIndex = 0, double deltaSM = 0, bool isAllowedValues = false)
        {
            int countNumbers = (int)Math.Pow(2, n);
            List<int> indexes = new List<int>();
            ParamsContainer container = new ParamsContainer(n, coeff, deltaCoeff, deltaIndex, deltaSM);
            LongBits inputBinaryCode = new LongBits(0, n), outputBinaryCode = inputBinaryCode;
            DACEmulator emulator = new DACEmulator(n, coeff, deltaCoeff, deltaIndex, deltaSM);
            while (Math.Abs(initialStep * 2) > accuracy)
            {
                inputBinaryCode = outputBinaryCode;
                while (inputBinaryCode == outputBinaryCode)
                {
                    emulator.DeltaCoeff = deltaCoeff;
                    emulator.DeltaIndex = deltaIndex;
                    emulator.DeltaSM = deltaSM;
                    for (int x = 0; x < countNumbers; x++)
                    {
                        inputBinaryCode = new LongBits(x, n);
                        outputBinaryCode = emulator.GetDKFromComparators(x);
                        if (inputBinaryCode != outputBinaryCode)
                            break;
                    }
                    if (inputBinaryCode == outputBinaryCode)
                    {
                        if (delta == DACEmulator.Delta.Coeff) deltaCoeff += initialStep;
                        else if (delta == DACEmulator.Delta.Index) deltaIndex += initialStep;
                        else if (delta == DACEmulator.Delta.SM) deltaSM += initialStep;
                    }
                }
                if (delta == DACEmulator.Delta.Coeff) deltaCoeff -= initialStep;
                else if (delta == DACEmulator.Delta.Index) deltaIndex -= initialStep;
                else if (delta == DACEmulator.Delta.SM) deltaSM -= initialStep;
                initialStep /= 2;
            }

            //Корректировка значений, если необходимо
            if (isAllowedValues)
            {
                if (delta == DACEmulator.Delta.Coeff) emulator.DeltaCoeff -= initialStep*2;
                else if (delta == DACEmulator.Delta.Index) emulator.DeltaIndex -= initialStep * 2;
                else if (delta == DACEmulator.Delta.SM) emulator.DeltaSM -= initialStep * 2;
            }

            for (int x = 0; x < countNumbers; x++)
            {
                indexes.AddRange(emulator.GetEKPErrorFromComparators(x).ToList());
                inputBinaryCode = new LongBits(x, n);
                outputBinaryCode = emulator.GetDKFromComparators(x);
                if (inputBinaryCode != outputBinaryCode)
                {
                    container.ErrorIndexesFromInputAndOutputCodes.Add(x);
                }
                container.InputBinaryCodes.Add(inputBinaryCode);
                container.OutputBinaryCodes.Add(outputBinaryCode);
            }

            container.DeltaCoeff = emulator.DeltaCoeff;
            container.DeltaIndex = emulator.DeltaIndex;
            container.DeltaSM = emulator.DeltaSM;
            container.ComparatorsErrorIndexes = indexes.Distinct().ToArray();
            return container;
        }

        public static ParamsContainer TestingDeltaIndexes(int n, double coeff, double initialStep = 1, double accuracy = 0.0000001)
        {
            double[] deltaIndexes = new double[n];
            double[] deltaIndexesSteps = new double[n];
            for (int i = 0; i < deltaIndexesSteps.Length; i++) deltaIndexesSteps[i] = initialStep / (Math.Pow(2, i));
            ParamsContainer container = new ParamsContainer();
            container.N = n;
            container.Coeff = coeff;
            DACEmulator emulator = new DACEmulator(n, 0, deltaIndexes, 0);
            emulator.DeltaIndexes = deltaIndexes;
            bool isContinue = true;
            while (isContinue)
            {
                isContinue = deltaIndexesSteps.Select(x => Math.Abs(x)).Max() >= accuracy;
                for (int i = deltaIndexes.Length - 1; i >= 0; i--)
                {
                    deltaIndexes[i] += deltaIndexesSteps[i];
                    emulator.DeltaIndexes = deltaIndexes;
                    bool noErrors = ErrorChecking(emulator);
                    if (!noErrors)
                    {
                        deltaIndexes[i] -= deltaIndexesSteps[i];
                        deltaIndexesSteps[i] /= 2;
                    }
                }
            }
            for (int i = deltaIndexes.Length - 1; i >= 0; i--)
            {
                emulator.DeltaIndexes[i] += deltaIndexesSteps[i];
            }
            container.DeltaIndexes = deltaIndexes;
            return container;
        }

        private static bool ErrorChecking(DACEmulator emulator)
        {
            int countNumbers = (int)Math.Pow(2, emulator.N);
            LongBits inputBinaryCode = new LongBits(0, emulator.N), outputBinaryCode = inputBinaryCode;
            for (int x = 0; x < countNumbers; x++)
            {
                inputBinaryCode = new LongBits(x, emulator.N);
                outputBinaryCode = emulator.GetDKFromComparators(x);
                if (inputBinaryCode != outputBinaryCode)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Поиск области допустимых значений критических параметров для двумерного случая,
        /// то есть для deltaSm и deltaK
        /// Идея такова: на плоскости X, Y находится верхняя и нижняя части области areaUp и areaBottom и объединяются в общую область
        /// Затем по оси Z строятся дуги edgeUp (верхняя) и edgeBottom (нижняя) - они также объединяются в одну плоскость
        /// Таким образом строятся ребра, после чего все записывается в один массив точек
        /// Ребра были сделаны для того, чтобы их потом последовательно можно было соединить линиями
        /// А можно и отрисовать только лишь точки
        /// </summary>
        /// <param name="n"></param>
        /// <param name="coeff"></param>
        /// <param name="deltaSmStep"></param>
        /// <param name="deltaSmStep"></param>
        public static List<Point3D> GetCriticalArea(int n, double coeff, double deltaSmInitialStep = 1, double deltaCoeffStep = 1)
        {
            if (deltaSmInitialStep <= 0 || deltaCoeffStep <= 0)
                throw new Exception("Недопустимые аргументы, должны быть > 0!");

            List<Point3D> areaUp = new List<Point3D>();
            List<Point3D> areaBottom = new List<Point3D>();
            DACEmulator emulator = new DACEmulator(n, coeff, 0, 0, 0);

            //Нижняя и верхняя половины области
            int edgesCount = 0;
            areaUp = GetPointsOfCriticalArea(emulator, DACEmulator.Delta.Coeff, DACEmulator.Delta.SM, deltaSmInitialStep, deltaCoeffStep);
            emulator.DeltaCoeff = 0;
            emulator.DeltaIndex = 0;
            emulator.DeltaSM = 0;
            areaBottom = GetPointsOfCriticalArea(emulator, DACEmulator.Delta.Coeff, DACEmulator.Delta.SM, deltaSmInitialStep, -deltaCoeffStep);
            edgesCount = areaUp.Count;

            for (int i = 0; i < edgesCount; i++)
            {
                emulator.DeltaCoeff = areaUp[i].X;
                emulator.DeltaIndex = 0;
                List<Point3D> edgeUp = GetPointsOfCriticalArea(emulator, areaBottom[i].Y, areaUp[i].Y, DACEmulator.Delta.SM, DACEmulator.Delta.Index, deltaSmInitialStep, deltaCoeffStep / 10000);
                emulator.DeltaCoeff = areaUp[i].X;
                emulator.DeltaIndex = 0;
                List<Point3D> edgeBottom = GetPointsOfCriticalArea(emulator, areaBottom[i].Y, areaUp[i].Y, DACEmulator.Delta.SM, DACEmulator.Delta.Index, deltaSmInitialStep, -deltaCoeffStep / 10000);
                edgeBottom.Reverse();
                edgeUp.AddRange(edgeBottom);
                areaUp.AddRange(edgeUp);
            }
            areaBottom.Reverse();
            //areaUp.AddRange(areaBottom);
            areaUp.RemoveRange(0, edgesCount);
            return areaUp;
        }

        public static async Task<List<ParamsPolygon>> GetPolygonsOfCriticalArea(int n, double coeff, double deltaSmInitialStep = 1, double deltaCoeffStep = 1)
        {
            return await Task.Run(() =>
            {

                if (deltaSmInitialStep <= 0 || deltaCoeffStep <= 0)
                    throw new Exception("Недопустимые аргументы, должны быть > 0!");
                Log("Построение основы... Подсчет точек... 0%");
                List<ParamsPolygon> polygons = new List<ParamsPolygon>();
                List<Point3D> areaUp = new List<Point3D>();
                List<Point3D> areaBottom = new List<Point3D>();
                List<Point3D> areaResult = new List<Point3D>();
                DACEmulator emulator = new DACEmulator(n, coeff, 0, 0, 0);//Нижняя и верхняя половины области
                int edgesCount = 0;
                areaUp = GetPointsOfCriticalArea(emulator, DACEmulator.Delta.Coeff, DACEmulator.Delta.SM, deltaSmInitialStep, deltaCoeffStep);
                emulator.DeltaCoeff = 0;
                emulator.DeltaIndex = 0;
                emulator.DeltaSM = 0;
                areaBottom = GetPointsOfCriticalArea(emulator, DACEmulator.Delta.Coeff, DACEmulator.Delta.SM, deltaSmInitialStep, -deltaCoeffStep);
                edgesCount = areaUp.Count;
                areaResult.AddRange(areaUp);

                List<Point3D> edgePrevious = new List<Point3D>();
                for (int i = 0; i < deltaSmInitialStep * 2; i++)
                {
                    edgePrevious.Add(areaUp[0]);
                }
                for (int i = 0; i < edgesCount; i++)
                {

                    emulator.DeltaCoeff = areaUp[i].X;
                    emulator.DeltaIndex = 0;
                    List<Point3D> edgeUp = GetPointsOfCriticalArea(emulator, areaBottom[i].Y, areaUp[i].Y, DACEmulator.Delta.SM, DACEmulator.Delta.Index, deltaSmInitialStep, deltaCoeffStep);
                    emulator.DeltaCoeff = areaUp[i].X;
                    emulator.DeltaIndex = 0;
                    List<Point3D> edgeBottom = GetPointsOfCriticalArea(emulator, areaBottom[i].Y, areaUp[i].Y, DACEmulator.Delta.SM, DACEmulator.Delta.Index, deltaSmInitialStep, -deltaCoeffStep);
                    edgeBottom.Reverse();
                    edgeUp.AddRange(edgeBottom);
                    areaResult.AddRange(edgeUp);

                    if (i == edgesCount - 1)
                    {
                        edgeUp.Clear();
                        for (int k = 0; k < deltaSmInitialStep * 2; k++)
                        {
                            edgeUp.Add(new Point3D(areaUp[areaUp.Count - 1].X, areaUp[areaUp.Count - 1].Y, areaBottom[areaUp.Count - 1].Z));
                        }
                    }
                    if (i > 0)
                    {
                        ParamsPolygon polygon = null;
                        for (int j = 0; j < edgeUp.Count - 1; j++)
                        {
                            polygon = new ParamsPolygon();
                            polygon.Vertexs.Add(edgePrevious[j]);
                            polygon.Vertexs.Add(edgePrevious[j + 1]);
                            polygon.Vertexs.Add(edgeUp[j + 1]);
                            polygon.Vertexs.Add(edgeUp[j]);
                            polygons.Add(polygon);
                        }
                        polygon = new ParamsPolygon();
                        polygon.Vertexs.Add(edgePrevious[edgePrevious.Count - 1]);
                        polygon.Vertexs.Add(edgePrevious[0]);
                        polygon.Vertexs.Add(edgeUp[0]);
                        polygon.Vertexs.Add(edgeUp[edgeUp.Count - 1]);
                        polygons.Add(polygon);

                        Point3D[] pointsPrevious = new Point3D[edgeUp.Count];
                        edgeUp.CopyTo(pointsPrevious);
                        edgePrevious = pointsPrevious.ToList();
                    }
                    Log("Создание полигонов области допустимых значений... " + (i / edgesCount) + "%");
                }
                areaBottom.Reverse();
                //areaResult.AddRange(areaBottom);
                areaResult.RemoveRange(0, edgesCount);
                Log("Готово! 100%");
                return polygons;
            });
        }

        private static List<Point3D> GetPointsOfCriticalArea(DACEmulator emulator, DACEmulator.Delta minMaxDeltaParameter, DACEmulator.Delta changingDeltaParameter, double minMaxDeltaParameterStep = 1, double changingDeltaParameterStep = 1, double accuracy = 0.0001)
        {
            List<Point3D> area = new List<Point3D>();
            double deltaMin = TestingDelta(emulator.N, emulator.Coeff, minMaxDeltaParameter, -minMaxDeltaParameterStep, accuracy, emulator.DeltaCoeff,
                emulator.DeltaIndex, emulator.DeltaSM, true).GetDeltaParameter(minMaxDeltaParameter);
            double deltaMax = TestingDelta(emulator.N, emulator.Coeff, minMaxDeltaParameter, minMaxDeltaParameterStep, accuracy, emulator.DeltaCoeff,
                emulator.DeltaIndex, emulator.DeltaSM, true).GetDeltaParameter(minMaxDeltaParameter);

            double step = (deltaMax - deltaMin) / (minMaxDeltaParameterStep - 1);
            double i = deltaMin;
            for (int j = 0; j < minMaxDeltaParameterStep; j++)
            {
                emulator.SetDeltaParameter(minMaxDeltaParameter, i);
                double deltaStepWithAccuracy = changingDeltaParameterStep;
                while (Math.Abs(deltaStepWithAccuracy) * 2 > accuracy)
                {
                    bool needCorrecting = false;
                    while (ErrorChecking(emulator))
                    {
                        emulator.AddDeltaParameter(changingDeltaParameter, deltaStepWithAccuracy);
                        needCorrecting = true;
                    }
                    if (needCorrecting) emulator.AddDeltaParameter(changingDeltaParameter, -deltaStepWithAccuracy);
                    deltaStepWithAccuracy /= 2;
                }
                area.Add(new Point3D((float)emulator.GetDeltaParameter(DACEmulator.Delta.Coeff), (float)emulator.GetDeltaParameter(DACEmulator.Delta.SM), (float)emulator.GetDeltaParameter(DACEmulator.Delta.Index)));
                emulator.SetDeltaParameter(changingDeltaParameter, 0);
                i += step;
            }
            return area;
        }

        //private static List<Point3D> GetPointsOfCriticalArea(DACEmulator emulator, DACEmulator.Delta minMaxDeltaParameter, DACEmulator.Delta changingDeltaParameter, double minMaxDeltaParameterStep = 1, double changingDeltaParameterStep = 1, double accuracy = 0.0001)
        //{
        //    List<Point3D> area = new List<Point3D>();
        //    double deltaMin = TestingDelta(emulator.N, emulator.Coeff, minMaxDeltaParameter, -minMaxDeltaParameterStep, accuracy, emulator.DeltaCoeff, emulator.DeltaIndex, emulator.DeltaSM).GetDeltaParameter(minMaxDeltaParameter);
        //    double deltaMax = TestingDelta(emulator.N, emulator.Coeff, minMaxDeltaParameter, minMaxDeltaParameterStep, accuracy, emulator.DeltaCoeff, emulator.DeltaIndex, emulator.DeltaSM).GetDeltaParameter(minMaxDeltaParameter);

        //    double step = (deltaMax - deltaMin) / (minMaxDeltaParameterStep - 1);
        //    double i = deltaMin;
        //    for (int j = 0; j < minMaxDeltaParameterStep; j++)
        //    {
        //        emulator.SetDeltaParameter(minMaxDeltaParameter, i);
        //        while (ErrorChecking(emulator))
        //        {
        //            emulator.AddDeltaParameter(changingDeltaParameter, changingDeltaParameterStep);
        //        }
        //        area.Add(new Point3D((float)emulator.GetDeltaParameter(DACEmulator.Delta.Coeff), (float)emulator.GetDeltaParameter(DACEmulator.Delta.SM), (float)emulator.GetDeltaParameter(DACEmulator.Delta.Index)));
        //        emulator.SetDeltaParameter(changingDeltaParameter, 0);
        //        i += step;
        //    }
        //    return area;
        //}

        //private static List<Point3D> GetPointsOfCriticalArea(DACEmulator emulator, double deltaMin, double deltaMax, DACEmulator.Delta minMaxDeltaParameter, DACEmulator.Delta changingDeltaParameter, double minMaxDeltaParameterStep = 1, double changingDeltaParameterStep = 1, double accuracy = 0.0001)
        //{
        //    List<Point3D> area = new List<Point3D>();
        //    if (deltaMin >= deltaMax)
        //        return area;

        //    double step = (deltaMax - deltaMin) / (minMaxDeltaParameterStep - 1); ;
        //    double i = deltaMin;
        //    for (int j = 0; j < minMaxDeltaParameterStep; j++)
        //    {
        //        emulator.SetDeltaParameter(minMaxDeltaParameter, i);
        //        while (ErrorChecking(emulator))
        //        {
        //            emulator.AddDeltaParameter(changingDeltaParameter, changingDeltaParameterStep);
        //        }
        //        area.Add(new Point3D((float)emulator.GetDeltaParameter(DACEmulator.Delta.Coeff), (float)emulator.GetDeltaParameter(DACEmulator.Delta.SM), (float)emulator.GetDeltaParameter(DACEmulator.Delta.Index)));
        //        emulator.SetDeltaParameter(changingDeltaParameter, 0);
        //        i += step;
        //    }
        //    return area;
        //}

        private static List<Point3D> GetPointsOfCriticalArea(DACEmulator emulator, double deltaMin, double deltaMax, DACEmulator.Delta minMaxDeltaParameter, DACEmulator.Delta changingDeltaParameter, double minMaxDeltaParameterStep = 1, double changingDeltaParameterStep = 1, double accuracy = 0.0001)
        {
            List<Point3D> area = new List<Point3D>();
            if (deltaMin >= deltaMax)
                return area;

            double step = (deltaMax - deltaMin) / (minMaxDeltaParameterStep - 1); ;
            double i = deltaMin;
            for (int j = 0; j < minMaxDeltaParameterStep; j++)
            {
                emulator.SetDeltaParameter(minMaxDeltaParameter, i);
                double deltaStepWithAccuracy = changingDeltaParameterStep;
                while (Math.Abs(deltaStepWithAccuracy) * 2 > accuracy)
                {
                    bool needCorrecting = false;
                    while (ErrorChecking(emulator))
                    {
                        emulator.AddDeltaParameter(changingDeltaParameter, deltaStepWithAccuracy);
                        needCorrecting = true;
                    }
                    if (needCorrecting) emulator.AddDeltaParameter(changingDeltaParameter, -deltaStepWithAccuracy);
                    deltaStepWithAccuracy /= 2;
                }
                area.Add(new Point3D((float)emulator.GetDeltaParameter(DACEmulator.Delta.Coeff), (float)emulator.GetDeltaParameter(DACEmulator.Delta.SM), (float)emulator.GetDeltaParameter(DACEmulator.Delta.Index)));
                emulator.SetDeltaParameter(changingDeltaParameter, 0);
                i += step;
            }
            return area;
        }

        private static void Log(string log)
        {
            if (TextBoxLog != null)
                TextBoxLog.Invoke((MethodInvoker)delegate
                {
                    TextBoxLog.Text = log;
                });
        }

        public void Dispose()
        {
            excelTools.Dispose();
        }

        public void Show()
        {
            excelTools.Show();
        }
    }
}
