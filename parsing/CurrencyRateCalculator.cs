using System.Collections.Generic;
using System;
using System.Linq;

namespace parsing
{
    internal static class CurrencyRateCalculator
    {
        /// <summary>
        /// метод для получения курса на завтра
        /// </summary>
        public static double KursTomorrow(double current, double next)
        {
            double bootTomorrow = (next + current) / 2;
            return Math.Round(bootTomorrow, 2);
        }

        /// <summary>
        /// метод для получения курса на следущую неделю
        /// </summary>
        public static double KursNextWeek(string valuta)
        {
            //список с курсом за предыдущую неделю
            List<double> previousWeek = new List<double>();

            //список с курсом за текущую неделю
            List<double> thisWeek = new List<double>();

            DateTime date;
            for (int i = 14; i > 7; i--)
            {
                date = DateTime.Now.AddDays(-i);
                previousWeek.Add(CurrencyRateParser.GetKurs(valuta, date));
            }
            for (int i = 7; i >= 0; i--)
            {
                date = DateTime.Now.AddDays(-i);
                thisWeek.Add(CurrencyRateParser.GetKurs(valuta, date));
            }

            //реднее арифметическое всех элементов в этой коллекции
            double boofPreviousWeek = previousWeek.Average();
            double boofThisWeek = thisWeek.Average();

            //считаем  курс на следующий  месяц
            double kurs = (boofPreviousWeek + boofThisWeek) / 2;
            return Math.Round(kurs, 3);
        }

        /// <summary>
        /// метод для получения курса на следующий  месяц
        /// </summary>
        public static double KursNextMonth(string valuta)
        {
            List<double> previousMonth = new List<double>();
            List<double> thisMonth = new List<double>();

            DateTime date;
            for (int i = 60; i > 30; i--)
            {
                date = DateTime.Now.AddDays(-i);
                thisMonth.Add(CurrencyRateParser.GetKurs(valuta, date));
            }
            for (int i = 30; i >= 0; i--)
            {
                date = DateTime.Now.AddDays(-i);
                thisMonth.Add(CurrencyRateParser.GetKurs(valuta, date));
            }

            double boofPreviousMonth = previousMonth.Average();
            double boofThisMonth = thisMonth.Average();

            double kurs = (boofPreviousMonth + boofThisMonth) / 2;
            return Math.Round(kurs, 2);
        }
    }
}
