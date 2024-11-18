using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RequestParsingMoscowExchange.RequestMoscowExchange
{
    /// <summary>
    /// запросы к БД
    /// </summary>
    public static class Request
    {
        /// <summary>
        /// запрос к серверу мосбиржи
        /// </summary>
        /// <param name="url">строка адреса</param>
        /// <returns>ответ от сервера</returns>
        /// <exception cref="Exception">ошибка так как пустой ответ от сервера</exception>
        static public string request(string url)
        {
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                HttpResponseMessage response = client.GetAsync(url).Result;
                if (string.IsNullOrEmpty(response.Content.ReadAsStringAsync().Result))
                {
                    throw new Exception("Пустой ответ от сервера");
                }
                else
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
        }

        /// <summary>
        /// запрос к серверу Московской биржи
        /// </summary>
        /// <param name="url">запрос в виде url</param>
        /// <param name="command">команда</param>
        /// <returns></returns>
        static public string RequestServer(string url)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(url).Result;
                string json = response.Content.ReadAsStringAsync().Result;
                return json;
            }

        }




        /// <summary>
        /// запрос к московской бирже
        /// </summary>
        /// <param name="command">комманда</param>
        static public void quest(string command, string request, bool parsing = true)
        {
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri("http://iss.moex.com");
                HttpResponseMessage response = client.GetAsync(request).Result;
                string json = response.Content.ReadAsStringAsync().Result;
               // TelegramBotMessage.SendMessageDebagger("Result :\n" + json);
                switch (command)
                {
                    case "Обновить бд DataStock":

                        //List<DataBase.DataStock> items = new();
                        //if (Parsing)
                        //{
                        //    items = ParserXML.Parsing(json, items);
                        //}
                        break;
                    case "индекс мосбиржи":
                    //    TelegramBotMessage.SendMessage(ParserXML.Parsing(json, request));
                        break;


                }

            }
        }

    }
}
