using HtmlAgilityPack;
using System;

namespace parsing
{
    internal static class CurrencyRateParser
    {
        /// <summary>
        /// метод для получения курса валюты с сайта ЦБ РФ
        /// </summary>
        public static double GetKurs(string valuta, DateTime date)
        {
            try
            {
                HtmlWeb web = new HtmlWeb();
                string url = $"http://www.cbr.ru/currency_base/daily/?UniDbQuery.Posted=True&UniDbQuery.To={date:dd.MM.yyyy}";
                // сайт ЦБ РФ
                HtmlDocument document = web.Load(url);
                var currencyNode = document.DocumentNode.SelectSingleNode(valuta);
                if (currencyNode != null)
                {
                    string currencyText = currencyNode.InnerText.Trim();
                    //заменяем . на ,
                    if (double.TryParse(currencyText.Replace('.', ','), out double bootCurrency))
                    {
                        // округляю до сотых
                        return Math.Round(bootCurrency, 2);
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

    }
}
