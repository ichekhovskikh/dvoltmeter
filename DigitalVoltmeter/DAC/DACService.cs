using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalVoltmeter
{
    public static class DACLimitService
    {
        public static double LimitDeltaCoeff(DACEmulator emulator, int input, double threshold = 0.001)
        {
            double oldDeltaCoeff = emulator.DeltaCoeff;
            LongBits inputBinaryCode = new LongBits(input, emulator.N);
            LongBits outputBinaryCode = emulator.GetDKFromComparators(input);
            double k = 1.1;
            while (inputBinaryCode != outputBinaryCode)
            {
                if (emulator.DeltaCoeff > -threshold && emulator.DeltaCoeff < threshold)
                    return 0;

                emulator.DeltaCoeff /= k;
                outputBinaryCode = emulator.GetDKFromComparators(input);
            }
            double critical = emulator.DeltaCoeff;
            emulator.DeltaCoeff = oldDeltaCoeff;
            return critical;
        }

        public static double LimitDeltaSM(DACEmulator emulator, int input, double threshold = 0.001)
        {
            double oldDeltaSM = emulator.DeltaSM;
            LongBits inputBinaryCode = new LongBits(input, emulator.N);
            LongBits outputBinaryCode = emulator.GetDKFromComparators(input);
            double k = 1.1;
            while (inputBinaryCode != outputBinaryCode)
            {
                if (emulator.DeltaSM > -threshold && emulator.DeltaSM < threshold)
                    return 0;

                emulator.DeltaSM /= k;
                outputBinaryCode = emulator.GetDKFromComparators(input);
            }
            double critical = emulator.DeltaSM;
            emulator.DeltaSM = oldDeltaSM;
            return critical;
        }

        public static double LimitDeltaIndex(DACEmulator emulator, int input, double threshold = 0.001)
        {
            double oldIndex = emulator.DeltaIndex;
            LongBits inputBinaryCode = new LongBits(input, emulator.N);
            LongBits outputBinaryCode = emulator.GetDKFromComparators(input);
            double k = 1.1;
            while (inputBinaryCode != outputBinaryCode)
            {
                if (emulator.DeltaIndex > -threshold && emulator.DeltaIndex < threshold)
                    return 0;

                emulator.DeltaIndex /= k;
                outputBinaryCode = emulator.GetDKFromComparators(input);
            }
            double critical = emulator.DeltaIndex;
            emulator.DeltaIndex = oldIndex;
            return critical;
        }

        public static double[] LimitDeltaIndexes(DACEmulator emulator, int input, double threshold = 0.001)
        {
            double[] oldIndexes = emulator.DeltaIndexes;
            LongBits outputBinaryCode = emulator.GetDKFromComparators(input);
            LongBits inputBinaryCode = new LongBits(input, emulator.N);
            double k = 1.1;
            while (inputBinaryCode != outputBinaryCode)
            {
                for (int i = 0; i < outputBinaryCode.Length; i++)
                {
                    if (emulator.DeltaIndexes[i] > -threshold && emulator.DeltaIndexes[i] < threshold)
                        return null;

                    if (inputBinaryCode != outputBinaryCode)
                        emulator.DeltaIndexes[i] /= k;
                }
                outputBinaryCode = emulator.GetDKFromComparators(input);
            }
            double[] critical = emulator.DeltaIndexes;
            emulator.DeltaIndexes = oldIndexes;
            return critical;
        }
    }
}
