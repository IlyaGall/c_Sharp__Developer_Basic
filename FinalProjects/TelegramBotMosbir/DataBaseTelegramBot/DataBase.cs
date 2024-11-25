using DataBaseTelegramBot.ConnectionAndRequest;
using Npgsql;
using System.Data;
using Dapper;
using ObjectsBot;

namespace DataBaseTelegramBot
{
    /// <summary>
    /// Логика работы с БД
    /// </summary>
    public class DataBase
    {
        /// <summary>
        /// Проверить пользователя наличие в БД
        /// </summary>
        /// <Param name="idUser">ID пользователя</Param>
        /// <returns>true- нашёл пользователя в бд, false - не нашёл в бд</returns>
        public bool checkUser(long idUser)
        {
            HelpConnectionServer helpConnectionServer = new HelpConnectionServer();
            helpConnectionServer.CreateStringConnectionDapper();

            string commandSql = @"select id from public.user 
                                  where id_user_telegram = @IdUser";
            using (IDbConnection connection = new NpgsqlConnection(helpConnectionServer.ConnectionStringDapper))
            {
                var Param = new
                {
                    IdUser = idUser
                };
                if (connection.Query(commandSql, Param).ToList().Count > 0)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Добавить пользователя
        /// </summary>
        /// <Param name="idUser"></Param>
        /// <Param name="userFirstName"></Param>
        /// <Param name="userLastName"></Param>
        /// <Param name="nicName"></Param>
        /// <returns></returns>
        public void addUser(long idUser, string userFirstName, string userLastName, string userNice)
        {
            HelpConnectionServer helpConnectionServer = new HelpConnectionServer();
            helpConnectionServer.NameDateBase = "telegramBot";
            helpConnectionServer.CreateStringConnectionDapper();
            string commandSql = @"INSERT INTO public.user (id_user_telegram, user_first_name_telegram,user_last_name_telegram, user_nick)
                                     VALUES (@IdUser, @UserFirstName, @UserLastName,@UserNice)";
            using (IDbConnection connection = new NpgsqlConnection(helpConnectionServer.ConnectionStringDapper))
            {
                var Param = new
                {
                    IdUser = idUser,
                    UserFirstName = userFirstName,
                    UserLastName = userLastName,
                    UserNice = userNice
                };
                connection.Execute(commandSql, Param);
            }
        }

        /// <summary>
        /// Обновление БД акций
        /// </summary>
        /// <Param name="BDStock"></Param>
        public static void UpsetStock(List<UpdateStockBd> BDStock)
        {
            HelpConnectionServer helpConnectionServer = new HelpConnectionServer();
            helpConnectionServer.NameDateBase = "telegramBot";
            helpConnectionServer.CreateStringConnectionDapper();
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
                    var Param = new
                    {
                        SECID = item.SECID,
                        ShortName = item.ShortName,
                        Prevprice = item.Prevprice,
                        LotSize = item.LotSize,
                        ISIN = item.ISIN,

                    };
                    connection.Execute(commandSql, Param);
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
            helpConnectionServer.CreateStringConnectionDapper();
            string commandSql = @"select name_stock from link_user_stock 
                                where id_user = @IdUser";
            var Param = new
            {
                IdUser = idUser
            };
            using (IDbConnection connection = new NpgsqlConnection(helpConnectionServer.ConnectionStringDapper))
            {
                return connection.Query<string>(commandSql, Param).ToList();
            }

        }

        /// <summary>
        /// Проверить акцию на правильность заполнения перед обращением к БД мосбиржи
        /// </summary>
        /// <Param name="nameActive"></Param>
        /// <returns></returns>
        static public (bool, string) CheckStockInBD(string nameActive)
        {
            HelpConnectionServer helpConnectionServer = new HelpConnectionServer();
            helpConnectionServer.CreateStringConnectionDapper();
            string commandSql = @"select secid from stock
                                where secid = @NameActive";
            var Param = new
            {
                NameActive = nameActive
            };
            string nameAciveInBD = "";
            using (IDbConnection connection = new NpgsqlConnection(helpConnectionServer.ConnectionStringDapper))
            {
                List<string> active = connection.Query<string>(commandSql, Param)?.ToList();
                if (active?.Count == 0)
                {
                    return (false, $"Не удалось найти '{nameActive}' актив в базе данных!");
                }
                else
                {
                    nameAciveInBD = active[0].Replace(" ", "");
                }
            }
            return (true, nameAciveInBD);
        }


        /// <summary>
        /// Добавить акцию в список избранного
        /// </summary>
        /// <Param name="idUser">Id пользователя</Param>
        /// <Param name="nameActive">Название актива</Param>
        /// <returns></returns>
        static public string AddActive(long idUser, string nameActive)
        {
            HelpConnectionServer helpConnectionServer = new HelpConnectionServer();
            helpConnectionServer.CreateStringConnectionDapper();

            string commandSql = @"select secid from stock
                                where secid = @NameActive";
            var Param = new
            {
                NameActive = nameActive
            };
            string nameActiveInBD = "";
            using (IDbConnection connection = new NpgsqlConnection(helpConnectionServer.ConnectionStringDapper))
            {
                List<string> active = connection.Query<string>(commandSql, Param)?.ToList();
                if (active.Count == 0)
                {
                    return ($"Не удалось найти '{nameActive}' актив в базе данных!");
                }
                else
                {
                    nameActiveInBD = active[0].Replace(" ", "");
                }
            }

            
        


            string commandSql2 = @"INSERT INTO link_user_stock  ( id_user, name_stock)
                                   VALUES ( @IdUser, @NameActive)";
            using (IDbConnection connection = new NpgsqlConnection(helpConnectionServer.ConnectionStringDapper))
            {
                var Param2 = new
                {
                    IdUser = idUser,
                    NameActive = nameActive,
                };
                connection.Execute(commandSql2, Param2);
            }
            return ($"Актив '{nameActive}' добавлен в избранно!");
        }
    }
}

