using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Лабараторная_работа_2_РПМ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Привет! Моя программа прогнозирует курс одной из трех выбранных валют к рублю на следующий день, неделю, месяц");
            Console.OutputEncoding = Encoding.UTF8;// команда для корректного вывода денежных знаков
            Console.WriteLine("выберити номер валюты, которая вам нужна\n1=$ \n2=€ \n3=£");

            int num = int.Parse(Console.ReadLine());
            string valuta = null;
            switch (num)
            {
                case 1:
                    valuta = "/html/body/main/div/div/div/div[3]/div/table/tbody/tr[15]/td[5]";
                    break;
                case 2:
                    valuta = "/html/body/main/div/div/div/div[3]/div/table/tbody/tr[16]/td[5]";
                    break;
                case 3:
                    valuta = "/html/body/main/div/div/div/div[3]/div/table/tbody/tr[39]/td[5]";
                    break;
                default:
                    Console.WriteLine("ошибка при вводе номера валюты");
                    return;
            }
            string todayDate = DateTime.Now.ToString("dd.MM.yyyy");//дата сегодня
            string yesterdayDate = DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy");//дата вчера

            double kursToday = GetKurs(valuta, todayDate);
            double kursYesterday = GetKurs(valuta, yesterdayDate);

            Console.WriteLine($"Курс на завтра: {KursTomorrow(kursToday, kursYesterday)}");
            Console.WriteLine($"Курс на следущую неделю: {KursNextWeek(valuta)}");
            Console.WriteLine($"Курс на следущий месяц: {KursNextMonth(valuta)}");

            Console.ReadKey();
        }
        private static double GetKurs(string valuta, string date)//метод для получения курса валюты
        {
            try
            {
                HtmlWeb web = new HtmlWeb();
                HtmlDocument document = web.Load($"http://www.cbr.ru/currency_base/daily/?UniDbQuery.Posted=True&UniDbQuery.To={date}"); // сайт ЦБ РФ
                var currencyNode = document.DocumentNode.SelectSingleNode(valuta);
                if (currencyNode != null)
                {
                    string currencyText = currencyNode.InnerText.Trim();
                    if (double.TryParse(currencyText.Replace('.', ','), out double bootCurrency)) //заменяем . на ,
                    {
                        return Math.Round(bootCurrency, 2);// округляю до сотых
                    }
                    else
                    {
                        throw new FormatException("Ошибка при преобразовании курса.");
                    }
                }
                else
                {
                    throw new Exception("Не удалось найти курс.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
        private static double KursTomorrow(double today, double yesterday)//метод для получения курса на завтра
        {
            double bootTomorrow = (yesterday + today) / 2;
            return Math.Round(bootTomorrow, 2);
        }
        private static double KursNextWeek(string valuta)//метод для получения курса на следущую неделю
        {
            List<double> previousWeek = new List<double>();//список с курсом за предыдущую неделю
            List<double> thisWeek = new List<double>();////список с курсом за текущую неделю

            for (int i = 14; i >= 0; i--)
            {
                string date = DateTime.Now.AddDays(-i).ToString("dd.MM.yyyy");
                if (i > 7)
                {
                    thisWeek.Add(GetKurs(valuta, date));
                }
                else
                {
                    previousWeek.Add(GetKurs(valuta, date));
                }
            }

            double boofPreviousWeek = previousWeek.Average();//реднее арифметическое всех элементов в этой коллекции
            double boofThisWeek = thisWeek.Average();

            double kurs = (boofPreviousWeek + boofThisWeek) / 2;//считаем  курс на следующий  месяц
            return Math.Round(kurs, 3);
        }
        private static double KursNextMonth(string valuta)//метод для получения курса на следующий  месяц
        {
            List<double> previousMonth = new List<double>();
            List<double> thisMonth = new List<double>();

            for (int i = 60; i >= 0; i--)
            {
                string date = DateTime.Now.AddDays(-i).ToString("dd.MM.yyyy");
                if (i > 30)
                {
                    thisMonth.Add(GetKurs(valuta, date));
                }
                else
                {
                    previousMonth.Add(GetKurs(valuta, date));
                }
            }
            double boofPreviousMonth = previousMonth.Average();
            double boofThisMonth = thisMonth.Average();

            double kurs = (boofPreviousMonth + boofThisMonth) / 2;
            return Math.Round(kurs, 2);
        }
    }
}