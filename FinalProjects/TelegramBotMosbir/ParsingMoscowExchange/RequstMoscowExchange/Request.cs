using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RequestParsingMoscowExchange.RequestMoscowExchange
{
    /// <summary>
    /// Запросы к БД Московской биржи
    /// </summary>
    public static class Request
    {
        /// <summary>
        /// Запрос к серверу мосбиржи xml
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
        /// Запрос к серверу Московской биржи json
        /// </summary>
        /// <param name="url">Запрос в виде url</param>
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
    }
}
