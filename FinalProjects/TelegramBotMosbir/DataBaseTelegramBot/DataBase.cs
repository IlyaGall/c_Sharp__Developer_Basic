using DataBaseTelegramBot.ConnectionAndRequest;
using Npgsql;
using System.Data;
using Dapper;
using ObjectsBot;
using DataBaseTelegramBot.Objects;


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
        /// Проверить пользователя наличие в БД
        /// </summary>
        /// <param name="idUser">ID пользователя</param>
        /// <returns>true- нашёл пользователя в бд, false - не нашёл в бд</returns>
        public bool checkUser(long idUser)
        {
            HelpConnectionServer helpConnectionServer = new HelpConnectionServer();
            helpConnectionServer.NameDateBase = "telegramBot";
            helpConnectionServer.CreateStringConnectionDapper();
            Request request = new Request();
            string commandSql = @"select id from public.user 
                                  where id_user_telegram = @IdUser";
            using (IDbConnection connection = new NpgsqlConnection(helpConnectionServer.ConnectionStringDapper))
            {
                var param = new
                {
                    IdUser = idUser
                };
                if (connection.Query(commandSql, param).ToList().Count > 0)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Добавить пользователя
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="userFirstName"></param>
        /// <param name="userLastName"></param>
        /// <param name="nicName"></param>
        /// <returns></returns>
        public void addUser(long idUser, string userFirstName, string userLastName, string userNice)
        {
            HelpConnectionServer helpConnectionServer = new HelpConnectionServer();
            helpConnectionServer.NameDateBase = "telegramBot";
            helpConnectionServer.CreateStringConnectionDapper();
            Request request = new Request();

            string commandSql = @"INSERT INTO public.user (id_user_telegram, user_first_name_telegram,user_last_name_telegram, user_nick)
                                     VALUES (@IdUser, @UserFirstName, @UserLastName,@UserNice)";
            using (IDbConnection connection = new NpgsqlConnection(helpConnectionServer.ConnectionStringDapper))
            {
                var param = new
                {
                    IdUser = idUser,
                    UserFirstName = userFirstName,
                    UserLastName = userLastName,
                    UserNice = userNice
                };
                connection.Execute(commandSql, param);
            }
        }

        /// <summary>
        /// Обновление БД акций
        /// </summary>
        /// <param name="BDStock"></param>
        public static void UpsetStock(List<UpdateStockBd> BDStock)
        {
            HelpConnectionServer helpConnectionServer = new HelpConnectionServer();
            helpConnectionServer.NameDateBase = "telegramBot";
            helpConnectionServer.CreateStringConnectionDapper();
            Request request = new Request();
            using (var connection = new NpgsqlConnection(helpConnectionServer.ConnectionStringDapper))
            {

                //  Добавил в pdAdmin ограничение на SECID, чтобы можно было сделать конфликт
                string commandSql = @"
                INSERT INTO stock (secid, short_name, prevprice, lot_size, isin)
                VALUES (@SECID, @ShortName, @Prevprice, @LotSize, @ISIN)
                ON CONFLICT (SECID) DO UPDATE
                SET short_name = EXCLUDED.short_name,
                    prevprice = EXCLUDED.prevprice,
                    lot_size = EXCLUDED.lot_size,
                    ISIN = EXCLUDED.ISIN;";
                    foreach (var item in BDStock)
                    {
                        var param = new
                        {
                            SECID = item.SECID,
                            ShortName = item.ShortName,
                            Prevprice = item.Prevprice,
                            LotSize = item.LotSize,
                            ISIN = item.ISIN,

                        };
                        connection.Execute(commandSql, param);
                    }
            }
    }


        /// <summary>
        /// Получить избранные акции пользователя
        /// </summary>
        /// <returns></returns>
        public static List<string> GetStockListUser(long idUser) 
        {

            HelpConnectionServer helpConnectionServer = new HelpConnectionServer();
            helpConnectionServer.NameDateBase = "telegramBot";
            helpConnectionServer.CreateStringConnectionDapper();
            Request request = new Request();
            // static List<string> FavoritesStock = new() { "SBER", "LKOH" };
            //string commandSql = @"INSERT INTO public.link_user_stock  (id_user,id_stok)
            //    VALUES (@IDUser,@IDStok )";

            string commandSql = @"select name_stock from link_user_stock 
                                where id_user = @IdUser";
            var param = new
            {
                IdUser = idUser
            };
            using (IDbConnection connection = new NpgsqlConnection(helpConnectionServer.ConnectionStringDapper))
            {
                return connection.Query<string>(commandSql, param).ToList();
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
        //static public List<string> GetStockListUser(long idUser)
        //{
        //    List<string> itemsStock = new();
        //    foreach (var item in FavoritesStock)
        //    {
        //        itemsStock.Add(item);
        //    }
        //    return itemsStock;

        //}
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

