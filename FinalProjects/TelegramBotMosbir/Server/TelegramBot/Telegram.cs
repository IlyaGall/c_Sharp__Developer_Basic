﻿using System.Text;
using SettingsProject;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using System.ComponentModel.DataAnnotations; // кнопки телеграмма в чате
using DataBaseTelegramBot;
using ObjectsBot;
using RequestParsingMoscowExchange.ListRequest;
using RequestParsingMoscowExchange.Parsing;

namespace Server.TelegramBot
{
    /// <summary>
    /// Работа с телеграмм
    /// </summary>
    public class TelegramBotMessage
    {

        /// <summary>
        /// Массив кнопок
        /// </summary>
        /// <returns>экземпляр класса InlineKeyboardMarkup с массивом кнопок</returns>
        private static InlineKeyboardMarkup ArrayButton()
        {
            return new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Справка", "/info"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Индекс МБ за 30 дней", "/indexMB30Day"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Индекс МБ за год", "/indexMBYear"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Добавить акцию в избранное", "/AddFavorites"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Получить избранные акции", "/GetFavoritesStocks"),
                    },
                }
            );
        }

        /// <summary>
        /// Запустить TelegramBot
        /// </summary>
        public static void startTelegram()
        {
            var botClient = new TelegramBotClient(Settings.GlobalParameters.Token);
            botClient.StartReceiving(clientUpdate, Error);
            Console.WriteLine($"server start. {DateTime.Now}");
            RequestParsingMoscowExchange.Parsing.ParserXML.LoadDay();
            RequestParsingMoscowExchange.Parsing.ParserXML.LoadHoliday();
            Console.ReadLine();
        }

        /// <summary>
        /// Запуск клиента телеграмма 
        /// </summary>
        /// <param name="botClient">(ITelegramBotClient) экземпляр интерфейса </param>
        /// <param name="update">обновление записи</param>
        /// <param name="token">отмена операции</param>
        /// <returns></returns>
        private async static Task clientUpdate(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            ObjectAnalytics objectAnalytics = new ObjectAnalytics();

            #region Обработчик кнопок
            var button = update.CallbackQuery;
            if (update.CallbackQuery != null)
            {
                string buttonCommand = update.CallbackQuery.Data;
                long userId = update.CallbackQuery.From.Id;
                Console.WriteLine($"Пользователь {userId} нажал кнопку {buttonCommand}");

                switch (buttonCommand)
                {
                    case "/start":
                    case "/info":
                        var text = "Список команд:";
                        objectAnalytics = Server.Commands.Server.ServerCommand(buttonCommand, objectAnalytics);
                        await botClient.SendTextMessageAsync(userId, $"command: {buttonCommand}\n {objectAnalytics.Description}");
                        await botClient.SendTextMessageAsync(userId, text, replyMarkup: ArrayButton());
                        break;
                    case "/indexMB30Day"://индекс московской биржи за 30 дней
                        objectAnalytics = Server.Commands.Server.ServerCommand("/indexMB30Day", objectAnalytics, "");
                        await LoadArrayPhoto(botClient, userId, objectAnalytics.PathImg, objectAnalytics.DescriptionImg);
                        break;
                    case "/indexMBYear"://индекс московской биржи за 1 год
                        objectAnalytics = Server.Commands.Server.ServerCommand("/indexMBYear", objectAnalytics, "");
                        await LoadArrayPhoto(botClient, userId, objectAnalytics.PathImg, objectAnalytics.DescriptionImg);
                        break;
                    case "/AddFavorites"://Добавить акцию в избранное,просто инструкция
                        text = "Для того чтобы добавить акцию в избранное, нужно написать команду:'/AddFavorites название акции'";
                        DataBaseTelegramBot.DataBase dataBase = new DataBaseTelegramBot.DataBase();
                        if (!dataBase.checkUser(userId))
                        {
                            dataBase.addUser(userId, update.CallbackQuery.From.FirstName, update.CallbackQuery.From.LastName, update.CallbackQuery.From.Username);
                        }
                        await botClient.SendTextMessageAsync(userId, text);
                        break;
                    case "/GetFavoritesStocks"://Вернуть избранные акции
                        List<string> list = DataBase.GetStockListUser(userId);
                        if (list.Count == 0)
                        {
                            await botClient.SendTextMessageAsync(userId, "У вас нет избранных активов!");
                            return;
                        }
                        foreach (var nameActive in DataBase.GetStockListUser(userId))
                        {
                            objectAnalytics = Server.Commands.Server.ServerCommand("/GetFavoritesStocks", objectAnalytics, nameActive.Replace(" ", ""));
                            if (objectAnalytics.PathImg?.Count != 0)
                            {
                                int stepCollection = 0;
                                await LoadArrayPhoto(botClient, userId, objectAnalytics.PathImg, objectAnalytics.DescriptionImg);

                            }
                            objectAnalytics.Clear();
                        }
                        break;

                }
            }
            #endregion

            #region Обработчик текста
            var message = update.Message;
            if (message != null)
            {
                long userId = message.Chat.Id;
                var messageTelegram = message.Text?.ToString()?.Split(' ');

                switch (messageTelegram[0])
                {
                    case "/getStock": // получить акцию, когда пользователь набирает её в ручную, а не через бд
                        (bool, string) answer = DataBase.CheckStockInBD(messageTelegram[1]);
                        if (answer.Item1 == false)
                        {
                            await botClient.SendTextMessageAsync(userId, @"Моя не понимать, что за акция");
                        }
                        else
                        {
                            if (messageTelegram.Length >= 4)
                            {

                                DateTime dateTime = DateTime.Now;
                                if (!DateTime.TryParse(messageTelegram[2], out dateTime))
                                {
                                    await botClient.SendTextMessageAsync(userId, @"Не правильная дата!");
                                }
                                if (!DateTime.TryParse(messageTelegram[3], out dateTime))
                                {
                                    await botClient.SendTextMessageAsync(userId, @"Не правильная дата!");

                                }



                                objectAnalytics = Server.Commands.Server.ServerCommand("/GetFavoritesStocksWithDate", objectAnalytics, answer.Item2.Replace(" ", ""), messageTelegram[2], messageTelegram[3]);
                                if (objectAnalytics.PathImg?.Count != 0)
                                {
                                    int stepCollection = 0;
                                    await LoadArrayPhoto(botClient, userId, objectAnalytics.PathImg, objectAnalytics.DescriptionImg);
                                }
                                objectAnalytics.Clear();
                            }
                            else
                            {
                                objectAnalytics = Server.Commands.Server.ServerCommand("/GetFavoritesStocks", objectAnalytics, answer.Item2.Replace(" ", ""));
                                if (objectAnalytics.PathImg?.Count != 0)
                                {
                                    int stepCollection = 0;
                                    await LoadArrayPhoto(botClient, userId, objectAnalytics.PathImg, objectAnalytics.DescriptionImg);
                                }
                                objectAnalytics.Clear();
                            }
                        }
                        break;

                    case "/UpdateBDStock":// Обновить бд, тут хард код
                        if (userId == 941692237)
                        {
                            List<UpdateStockBd> BDStock = ParserXML.ParsingMoscowExchange(RequestCommand.QueryGetFullStock());
                            DataBase.UpsetStock(BDStock);
                            await botClient.SendTextMessageAsync(userId, @"Бд акций обновлена!");
                        }
                        else
                        {
                            await botClient.SendTextMessageAsync(userId, @"У вас не достаточно прав!!!");
                        }
                        break;

                    case "/AddFavorites": //Добавление акции в избранную позицию
                        await botClient.SendTextMessageAsync(userId, DataBase.AddActive(userId, messageTelegram[1]));
                        break;

                    case "/info"://Список команд
                        var text = "Список команд:";
                        await botClient.SendTextMessageAsync(userId, text, replyMarkup: ArrayButton());
                        break;
                    default:
                        await botClient.SendTextMessageAsync(userId, @"Не известная команда! Вызовите \info, чтобы открыть список доступных команд.");
                        break;
                }
                #region Если пользователь одарённый и отпаравил картинку, хотя не должен был! (заглушка)
                if (message.Document != null)
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Ща отправим");
                    var fileId = update.Message.Document.FileId;
                    var fileInfo = await botClient.GetFileAsync(fileId);
                    var filePath = fileInfo.FilePath;
                    string destinationFilePath = $@"C:\\Users\\Ilya\\Desktop\\Новая папка\\" + $"{message.Document.FileName}";

                    await using Stream fileStream = System.IO.File.Create(destinationFilePath);
                    await botClient.DownloadFileAsync(
                        filePath: filePath,
                        destination: fileStream);
                    fileStream.Close();



                    //отправка файла
                    string fileName = "PEROOOOOO.jpg";
                    await using Stream stream = System.IO.File.OpenRead("C:\\Users\\Ilya\\Desktop\\falen world\\pero.jpg");
                    var messageLoad = await botClient.SendDocumentAsync(
                        message.Chat.Id,
                        document: InputFile.FromStream(stream, fileName),
                        caption: "The Tragedy of Hamlet,\nPrince of Denmark");
                    return;
                }
                #endregion
            }
            #endregion

        }

        /// <summary>
        /// Функция отправки сообщения с n количеством картинок
        /// </summary>
        /// <param name="botClient">(ITelegramBotClient) экземпляр интерфейса </param>
        /// <param name="IDChats">(long) id чата пользователя</param>
        /// <param name="pathFile">(string) путь до файла</param>
        /// <param name="text">(string) Текст если требуется</param>
        /// <returns></returns>
        private static async Task LoadArrayPhoto(ITelegramBotClient botClient, long IDChats, List<string> pathFile, List<string> text)
        {
            List<FileStream> streams = new List<FileStream>();
            List<InputMediaPhoto> phots = new List<InputMediaPhoto>();
            bool addText = false;
            StringBuilder stringBuilder = new();
            if (text != null)
            {
                foreach (string item in text)
                {
                    stringBuilder.AppendLine(item);
                }
            }
            try
            {
                foreach (string p in pathFile)
                {
                    FileStream s = System.IO.File.OpenRead(p);
                    streams.Add(s);
                    phots.Add(new InputMediaPhoto(InputFile.FromStream(s, Path.GetFileName(p)))
                    {
                        Caption = !addText ? $"{stringBuilder.ToString()}" : ""
                    }
                    );
                    addText = true;
                    //phots.Add(new InputMediaPhoto(new InputFile(s, Path.GetFileName(p))));
                }

                await botClient.SendMediaGroupAsync(IDChats, phots);

            }
            catch (Exception ex)
            {
                //TODO
            }
            finally
            {
                foreach (var s in streams)
                    s.Dispose();
                phots.Clear();
            }
        }

        /// <summary>
        /// Функция ошибки 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="exception"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private static async Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            Console.WriteLine(exception.Message);
            throw new NotImplementedException();
        }


        #region задумка на будущие

        /// <summary>
        /// отправка сообщения разработчику о том, чтобы посмотрел что-же пошло не так и подробное описание
        /// </summary>
        /// <param name="message"></param>
        static public void SendMessageDebagger(string message)
        {
            Console.WriteLine(message);
        }



        /// <summary>
        /// Отправка файла виде документа клиенту
        /// </summary>
        /// <param name="botClient">(ITelegramBotClient) экземпляр интерфейса </param>
        /// <param name="IDChats">(long) id чата пользователя</param>
        /// <param name="pathFile">(string) путь до файла</param>
        /// <param name="text">(string) Текст если требуется</param>
        /// <returns></returns>
        private static async Task loadFile(ITelegramBotClient botClient, long IDChats, string pathFile, string text = "")
        {
            string fileName = pathFile.Remove(0, pathFile.LastIndexOf('\\') - 1);// "PEROOOOOO.jpg";
            await using Stream stream = System.IO.File.OpenRead(pathFile);
            var messageLoad = await botClient.SendDocumentAsync(
                IDChats,
                document: InputFile.FromStream(stream, fileName),
                caption: $"{text}");
        }

        /// <summary>
        /// Отправка 1-го файла виде картинки клиенту
        /// </summary>
        /// <param name="botClient">(ITelegramBotClient) экземпляр интерфейса </param>
        /// <param name="IDChats">(long) id чата пользователя</param>
        /// <param name="pathFile">(string) путь до файла</param>
        /// <param name="text">(string) Текст если требуется</param>
        /// <returns></returns>
        private static async Task loadPhoto(ITelegramBotClient botClient, long IDChats, string pathFile, string text = "12121212")
        {
            string fileName = pathFile.Remove(0, pathFile.LastIndexOf('\\') - 1);// "PEROOOOOO.jpg";
            await using Stream stream = System.IO.File.OpenRead(pathFile);

            var messageLoad = await botClient.SendPhotoAsync(
                  chatId: IDChats,
                  photo: InputFile.FromStream(stream, fileName),
                  caption: $"{text}"
                );

        }
        #endregion

    }
}
