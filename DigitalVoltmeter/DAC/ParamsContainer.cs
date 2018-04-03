using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalVoltmeter
{
    /// <summary>
    /// Класс-контейнер для хранения критических параметров
    /// coeff, deltaCoeff, deltaIndex, deltaSm
    /// при которых компараторы начинают давать сбой
    /// </summary>
    class ParamsContainer
    {
        public int N { get; set; }

        public double Coeff { get; set; }

        public double DeltaCoeff { get; set; }

        public double DeltaIndex { get; set; }
        public double[] DeltaIndexes { get; set; }

        public double DeltaSM { get; set; }

        public List<LongBits> InputBinaryCodes { get; set; }
        public List<LongBits> OutputBinaryCodes { get; set; }

        public int[] ComparatorsErrorIndexes { get; set; }
        public List<int> ErrorIndexesFromInputAndOutputCodes { get; set; }

        public ParamsContainer(int n, double coeff, double deltaCoeff, double deltaIndex, double deltaSM)
        {
            N = n;
            Coeff = coeff;
            DeltaCoeff = deltaCoeff;
            DeltaIndex = deltaIndex;
            DeltaSM = deltaSM;
            InputBinaryCodes = new List<LongBits>();
            OutputBinaryCodes = new List<LongBits>();
            ErrorIndexesFromInputAndOutputCodes = new List<int>();
        }

        public ParamsContainer()
        {
            InputBinaryCodes = new List<LongBits>();
            OutputBinaryCodes = new List<LongBits>();
            ErrorIndexesFromInputAndOutputCodes = new List<int>();
        }

        public double GetDeltaParameter(DACEmulator.Delta delta)
        {
            switch (delta)
            {
                case DACEmulator.Delta.Coeff:
                    return DeltaCoeff;
                case DACEmulator.Delta.Index:
                    return DeltaIndex;
                case DACEmulator.Delta.SM:
                    return DeltaSM;
                default:
                    throw new Exception("Такого параметра не существует!");
            }
        }
    }
}