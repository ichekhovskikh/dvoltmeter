using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalVoltmeter
{
    public static class MathProcessor
    {
        /// <summary>
        /// Получить количество разрядов
        /// </summary>
        /// <param name="value">Количество резисторов</param>
        /// <returns>Количество разрядов</returns>
        public static int GetN(int value)
        {
            return (int)Math.Ceiling(Math.Log(value, 2));
        }

        /// <summary>
        /// Транспонирование матрицы
        /// </summary>
        /// <param name="bitMatrix">Матрица битов</param>
        /// <returns>Транспонированная матрица бмтов</returns>
        public static LongBits[] TransposeBitMatrix(LongBits[] bitMatrix)
        {
            LongBits[] transpose = new LongBits[bitMatrix[0].Length];
            for (int i = 0; i < transpose.Length; i++)
            {
                transpose[i] = new LongBits(bitMatrix.Length);
                for (int j = 0; j < bitMatrix.Length; j++)
                {
                    transpose[i][j] = bitMatrix[j][i];
                }
            }
            return transpose;
        }

        /// <summary>
        /// Преобразовать десятичное число в ЕК
        /// </summary>
        /// <param name="value">Десятичное число</param>
        /// <returns>ЕК</returns>
        public static LongBits GetEK(int value, int count)
        {
            LongBits singleCode = new LongBits(count);
            for (int i = 0; i < value; i++)
                singleCode[i] = 1;
            return singleCode;
        }

        /// <summary>
        /// Получить массив ЕК
        /// </summary>
        /// <param name="value">Верхняя граница</param>
        /// <returns>Массив ЕК</returns>
        public static LongBits[] GetAllEK(int value)
        {
            LongBits[] singleCodes = new LongBits[value];
            for (int i = 0; i < value; i++)
                singleCodes[i] = GetEK(i, value - 1);
            return singleCodes;
        }

        /// <summary>
        /// Получение ЕПК
        /// </summary>
        /// <param name="e">ЕК</param>
        /// <returns>ЕПК</returns>
        public static LongBits GetEPKFromEK(LongBits e)
        {
            LongBits b = new LongBits(e.Length);
            for (int i = 0; i < b.Length; i++)
                b[i] = e[i] & ~(i == b.Length - 1 ? 0 : e[i + 1]);
            return b;
        }

        /// <summary>
        /// Получение массива ЕПК
        /// </summary>
        /// <param name="e">Массив ЕК</param>
        /// <returns>Массив ЕПК</returns>
        public static LongBits[] GetAllEPKFromEK(LongBits[] e)
        {
            LongBits[] b = new LongBits[e.Length];
            for (int i = 0; i < b.Length; i++)
                b[i] = GetEPKFromEK(e[i]);
            return b;
        }

        /// <summary>
        /// Вычисление ДК
        /// </summary>
        /// <param name="b">ЕПК</param>
        /// <returns>ДК</returns>
        public static LongBits GetDK(LongBits b)
        {
            int n = GetN(b.Length + 1);
            LongBits a = new LongBits(n);
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = a.Negative(i);
                int kmax = (int)Math.Pow(2, n - 1 - i) - 1;
                for (int k = 0; k <= kmax; k++)
                {
                    int tmin = (int)Math.Pow(2, i) * (2 * k + 1);
                    int tmax = (int)Math.Pow(2, i + 1) * (k + 1) - 1;
                    for (int t = tmin; t <= tmax; t++)
                    {
                        a[i] &= b.Negative(t - 1);
                    }
                }
                a[i] = a.Negative(i);
            }
            return a;
        }

        /// <summary>
        /// Вычисление ДК
        /// </summary>
        /// <param name="b">Массив ЕПК</param>
        /// <returns>Массив ДК</returns>
        public static LongBits[] GetAllDK(LongBits[] b)
        {
            LongBits[] transpB = TransposeBitMatrix(b);
            int n = GetN(transpB.Length + 1);
            LongBits[] a = new LongBits[n];
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = new LongBits(transpB[0].Length);
                a[i] = ~a[i];
                int kmax = (int)Math.Pow(2, n - 1 - i) - 1;
                for (int k = 0; k <= kmax; k++)
                {
                    int tmin = (int)Math.Pow(2, i) * (2 * k + 1);
                    int tmax = (int)Math.Pow(2, i + 1) * (k + 1) - 1;
                    for (int t = tmin; t <= tmax; t++)
                    {
                        a[i] &= ~transpB[t - 1];
                    }
                }
                a[i] = ~a[i];
            }
            return a;
        }

        /// <summary>
        /// Вывод формулы
        /// </summary>
        /// <param name="b">ЕПК</param>
        /// <param name="a">ДК</param>
        /// <returns>Формула ДК</returns>
        public static string[] Formules(LongBits b, out LongBits a)
        {
            int n = GetN(b.Length + 1);
            a = new LongBits(n);

            string[] formules = new string[n];
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = a.Negative(i);
                string withoutDigit = string.Empty;
                string withDigit = string.Empty;
                int kmax = (int)Math.Pow(2, n - 1 - i) - 1;
                for (int k = 0; k <= kmax; k++)
                {
                    int tmin = (int)Math.Pow(2, i) * (2 * k + 1);
                    int tmax = (int)Math.Pow(2, i + 1) * (k + 1) - 1;
                    for (int t = tmin; t <= tmax; t++)
                    {
                        withoutDigit += "¬b" + t;
                        withDigit += b.Negative(t - 1);

                        a[i] &= b.Negative(t - 1);

                        if (k != kmax || t != tmax)
                        {
                            withoutDigit += "ʌ";
                            withDigit += "ʌ";
                        }
                    }
                }
                a[i] = a.Negative(i);
                formules[i] = "¬(" + withoutDigit + ")=" + "¬(" + withDigit + ")=" + a[i];
            }
            return formules;
        }

        /// <summary>
        /// Вывод формулы
        /// </summary>
        /// <param name="transpB">Массив ЕПК</param>
        /// <param name="a">ДК</param>
        /// <returns>Формулы ДК</returns>
        public static string[] Formules(LongBits[] b, out LongBits[] a)
        {
            LongBits[] transpB = TransposeBitMatrix(b);
            int n = GetN(transpB.Length + 1);
            a = new LongBits[n];

            string[] formules = new string[n];

            for (int i = 0; i < a.Length; i++)
            {
                a[i] = new LongBits(transpB[0].Length);
                a[i] = ~a[i];
                string withoutDigit = string.Empty;
                string withDigit = string.Empty;
                int kmax = (int)Math.Pow(2, n - 1 - i) - 1;
                for (int k = 0; k <= kmax; k++)
                {
                    int tmin = (int)Math.Pow(2, i) * (2 * k + 1);
                    int tmax = (int)Math.Pow(2, i + 1) * (k + 1) - 1;
                    for (int t = tmin; t <= tmax; t++)
                    {
                        withoutDigit += "¬b" + t;
                        withDigit += "¬" + transpB[t - 1];

                        a[i] &= ~transpB[t - 1];

                        if (k != kmax || t != tmax)
                        {
                            withoutDigit += "ʌ";
                            withDigit += "ʌ";
                        }
                    }
                }
                a[i] = ~a[i];
                formules[i] = "¬(" + withoutDigit + ")=" + "¬(" + withDigit + ")=" + a[i];
            }
            return formules;
        }
    }
}
