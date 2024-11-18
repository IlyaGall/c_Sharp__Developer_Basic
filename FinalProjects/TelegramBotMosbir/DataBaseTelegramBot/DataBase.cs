using DataBaseTelegramBot.ConnectionAndRequest;

namespace DataBaseTelegramBot
{

   

        /// <summary>
        /// Логика работы с БД
        /// </summary>
        public class DataBase
        {
            static List<long> Users = new();
            static List<string> FavoritesStock = new() { "SBER", "LKOH" };

            /// <summary>
            /// пользователя
            /// </summary>
            static public bool addUser(long idUser)
            {
                HelpConnectionServer helpConnectionServer = new HelpConnectionServer();
                helpConnectionServer.NameDateBase = "telegramBot";
                helpConnectionServer.CreateStringConnection();
                // Request request = new Request();

                if (Users.Contains(idUser))
                {
                    return false;
                }
                else
                {
                    Users.Add(idUser);
                    return true;
                }
            }

            /// <summary>
            /// Добавить актив в избранное
            /// </summary>
            /// <param name="idUser">Id пользователя</param>
            /// <param name="nameStock">Название актива</param>
            /// <returns></returns>
            static public bool AddFavoritesStock(long idUser, string nameStock)
            {
                if (FavoritesStock.Contains(nameStock))
                {
                    return false;
                }
                else
                {
                    FavoritesStock.Add(nameStock);
                    return true;
                }
            }
            /// <summary>
            /// Найти актив
            /// </summary>
            /// <returns></returns>
            /// <exception cref="NotImplementedException"></exception>
            public string SearchStock()
            {
                throw new NotImplementedException();
            }

            /// <summary>
            /// Вернуть избранные активы пользователя
            /// </summary>
            /// <param name="idUser">Id пользователя</param>
            /// <returns>Коллекция избранных активов пользователя</returns>
            static public List<string> GetStockListUser(long idUser)
            {
                List<string> itemsStock = new();
                foreach (var item in FavoritesStock)
                {
                    itemsStock.Add(item);
                }
                return itemsStock;

            }
            public class DataStock
            {

                private double _prevprice;
                public string? SECID { get; set; }
                public double PREVPRICE
                {
                    get => _prevprice;
                    set
                    {
                        try
                        {
                            _prevprice = Convert.ToDouble(value.ToString().Replace(".", ","));
                        }
                        catch
                        {
                            throw new Exception("нельзя конвертировать! Разбирайся с запросом!");
                        }
                    }
                }
                public string? SHORTNAME { get; set; }





            }
        }
    }

