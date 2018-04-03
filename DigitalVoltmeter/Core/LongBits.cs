using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalVoltmeter
{
    public class LongBits
    {
        private StringBuilder bitsString;

        public LongBits(string longBits)
        {
            if (!longBits.All(val => val == '0' || val == '1'))
                throw new ArgumentException();
            bitsString = new StringBuilder(longBits);
        }

        public LongBits(int countBits)
        {
            if (countBits < 1)
                throw new ArgumentException();
            bitsString = new StringBuilder();
            for (int i = 0; i < countBits; i++)
                bitsString.Append("0");
        }

        public LongBits(int number, int countBits)
        {
            if (countBits < 1)
                throw new ArgumentException();

            bitsString = new StringBuilder();
            bitsString.Append(Convert.ToString(number, 2));

            if (bitsString.Length > countBits)
                bitsString.Remove(0, bitsString.Length - countBits);

            while (bitsString.Length < countBits)
                bitsString.Insert(0, "0");
        }

        public int this[int index]
        {
            get
            {
                if (index < 0 || index >= bitsString.Length)
                    throw new IndexOutOfRangeException();
                return (int)Char.GetNumericValue(bitsString[bitsString.Length - 1 - index]);
            }
            set
            {
                if (index < 0 || index >= bitsString.Length)
                    throw new IndexOutOfRangeException();
                if (value != 0 && value != 1)
                    throw new ArgumentException();
                bitsString[bitsString.Length - 1 - index] = (value == 0) ? '0' : '1';
            }
        }

        public int Negative(int index)
        {
            if (index < 0 || index >= bitsString.Length)
                throw new IndexOutOfRangeException();
            int value = (int)Char.GetNumericValue(bitsString[bitsString.Length - 1 - index]);
            return value == 1 ? 0 : 1;
        }

        public int Length
        {
            get { return bitsString.Length; }
        }

        public static LongBits operator >>(LongBits bits, int count)
        {
            LongBits newbits = new LongBits(bits.bitsString.ToString(0, bits.Length - count));
            for (int i = 0; i < count; i++)
                newbits.bitsString.Insert(0, '0');
            return newbits;
        }

        public static LongBits operator <<(LongBits bits, int count)
        {
            LongBits newbits = new LongBits(bits.bitsString.ToString(count, bits.Length - count));
            for (int i = 0; i < count; i++)
                newbits.bitsString.Append('0');
            return newbits;
        }

        public static LongBits operator ~(LongBits bits)
        {
            LongBits newbits = new LongBits(bits.ToString());
            for (int i = 0; i < newbits.Length; i++)
                newbits[i] = (newbits[i] == 0) ? 1 : 0;
            return newbits;
        }

        public static LongBits operator &(LongBits first, LongBits second)
        {
            LongBits newbits = new LongBits(first.ToString());
            for (int i = 0; i < newbits.Length; i++)
                newbits[i] = (newbits[i] == 1 && second[i] == 1) ? 1 : 0;
            return newbits;
        }

        public static LongBits operator |(LongBits first, LongBits second)
        {
            LongBits newbits = new LongBits(first.ToString());
            for (int i = 0; i < newbits.Length; i++)
                newbits[i] = (newbits[i] == 1 || second[i] == 1) ? 1 : 0;
            return newbits;
        }

        public static bool operator ==(LongBits first, LongBits second)
        {
            if (first.Length == second.Length)
                return first.bitsString.Equals(second.bitsString);
            string firstEqualsPart = string.Concat(first.ToString().TrimStart('0'));
            string secondEqualsPart = string.Concat(second.ToString().TrimStart('0'));
            return firstEqualsPart == secondEqualsPart;
        }

        public static bool operator !=(LongBits first, LongBits second)
        {
            return !(first == second);
        }

        public static int[] GetStringIndexesOfDifferenceBits(LongBits first, LongBits second)
        {
            if (first.bitsString.Equals(second.bitsString))
                return new int[] { };

            string firstString = first.ToString();
            string secondString = second.ToString();
            while (firstString.Length > secondString.Length)
                secondString = "0" + secondString;
            while (firstString.Length < secondString.Length)
                firstString = "0" + firstString;

            List<int> indexesOfBits = new List<int>();
            for (int i = 0; i < firstString.Length; i++)
            {
                if (firstString[i] != secondString[i]) indexesOfBits.Add(i);
            }
            return indexesOfBits.ToArray();
        }

        public long ToLong()
        {
            long result = 0;
            int length = Math.Min(Length, 64);
            for (int i = 0; i < length; i++)
            {
                result |= (long)this[i] << i;
            }
            return result;
        }

        public override string ToString()
        {
            return bitsString.ToString();
        }
    }
}