namespace DataBaseTelegramBot.ConnectionAndRequest
{
    /// <summary>
    /// Помощник подключения к серверу
    /// </summary>
    public class HelpConnectionServer
    {
        /// <summary>
        /// Имя сервера
        /// </summary>
        public string NameServer { get; set; } = "localhost";
        /// <summary>
        /// Номер порта
        /// </summary>
        public int PortConnectionServer { get; set; } = 5432;
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserId { get; set; } = "postgres";
        /// <summary>
        /// Пароль от пользователя
        /// </summary>
        public string Password { get; set; } = "";
        /// <summary>
        /// Название БД
        /// </summary>
        public string NameDateBase { get; set; } = "telegramBot";
        /// <summary>
        /// Строка подключения к БД
        /// </summary>
        public string ConnectionString { get; set; } = "";
        /// <summary>
        /// Сформировать строку подключения к БД 1 способ и он работает
        /// </summary>
        public void CreateStringConnection()
        {
            ConnectionString = $"Server={NameServer};Port={PortConnectionServer};User Id={UserId};Password={Password};Database={NameDateBase};";

        }
        /// <summary>
        /// Подключение для Dapper
        /// </summary>
        public string ConnectionStringDapper { get; set; }

        /// <summary>
        /// Сформировать строку подключения к БД для dapper 2 способ и он работает
        /// </summary>
        public void CreateStringConnectionDapper()
        {
            ConnectionStringDapper = $"Host=localhost:{PortConnectionServer};Username={UserId};Password={Password};Database={NameDateBase}";
        }
    }
}
