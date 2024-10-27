using parsing;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Лабараторная_работа_2_РПМ
{
    internal class Program
    {
        private readonly struct CurrencyInfo
        {
            public char Symbol { get; }
            public string Link { get; }

            public CurrencyInfo(char symbol, string link)
                => (Symbol, Link) = (symbol, link);
        }

        private static readonly IDictionary<uint, CurrencyInfo> _currencies = new Dictionary<uint, CurrencyInfo>()
        {
            { 1, new CurrencyInfo('$', "/html/body/main/div/div/div/div[3]/div/table/tbody/tr[15]/td[5]")},
            { 2, new CurrencyInfo('€', "/html/body/main/div/div/div/div[3]/div/table/tbody/tr[16]/td[5]")},
            { 3, new CurrencyInfo('£', "/html/body/main/div/div/div/div[3]/div/table/tbody/tr[39]/td[5]")}
        };

        static void Main(string[] _)
        {
            Console.WriteLine("Привет! Моя программа прогнозирует курс одной из трех выбранных валют к рублю на следующий день, неделю, месяц");
            //команда для корректного вывода денежных знаков
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine(
                $"выберити номер валюты, которая вам нужна \n{string.Join("\n", _currencies.Select(x => $"{x.Key}) {x.Value.Symbol}"))}");

            uint num = uint.Parse(Console.ReadLine());

            if (!_currencies.TryGetValue(num, out CurrencyInfo currencyInfo))
            {
                Console.WriteLine("ошибка при вводе номера валюты");
                return;
            }
            //дата сегодня
            DateTime todayDate = DateTime.Now;
            //дата вчера
            DateTime yesterdayDate = DateTime.Now.AddDays(-1);

            double kursToday = CurrencyRateParser.GetKurs(currencyInfo.Link, todayDate);
            double kursYesterday = CurrencyRateParser.GetKurs(currencyInfo.Link, yesterdayDate);

            Console.WriteLine($"Курс на завтра: {CurrencyRateCalculator.KursTomorrow(kursToday, kursYesterday)}");
            Console.WriteLine($"Курс на следущую неделю: {CurrencyRateCalculator.KursNextWeek(currencyInfo.Link)}");
            Console.WriteLine($"Курс на следущий месяц: {CurrencyRateCalculator.KursNextMonth(currencyInfo.Link)}");

            Console.ReadKey();
        }
    }
}