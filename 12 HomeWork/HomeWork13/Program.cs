namespace HomeWork13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Customer customer = new Customer();
            Shop shop = new Shop();
            shop.ChangedShop(customer);
            shop.Add($"\"Мясо\" Товар от <{DateTime.Now}>");
            shop.Add($"\"Макароны\" Товар от <{DateTime.Now}>");
            ui(shop);

        }

        static private void ui(Shop shop)
        {
            bool flagStop = false;
            while (!flagStop)
            {
                switch (Console.ReadLine())
                {
                    case "A":
                    case "a":
                    case "А":
                    case "а":
                        // Товар должен называться "Товар от <текущее дата и время>", где вместо <текущее дата и время> подставлять текущую дату и время.
                        Console.WriteLine("Ведите название товара!");
                        var addNewItem = Console.ReadLine();
                        shop.Add($"\"{addNewItem}\" Товар от <{DateTime.Now}>");
                        break;

                    case "D":
                    case "d":
                        //По нажатии клавиши D спрашивайте какой товар надо удалить.
                        //Пользователь должен ввести идентификатор товара, после чего товар необходимо удалить из ассортимента магазина.
                        Console.WriteLine("Ведите id удаляемого товара!");
                        int index = 0;
                        if (int.TryParse(Console.ReadLine(), out index))
                        {
                            shop.Remove(index);
                        }
                        else 
                        {
                            Console.WriteLine("Ошибка конвертации индекса! Индекс должен быть целочисленым числом!");
                        }
                        break;
                    case "list":
                        shop.GetListItem();
                        break;
                    case "x":
                    case "X":
                    case "х":
                    case "Х":
                        flagStop = true;
                        break;
                    default:
                        Console.WriteLine("А - добавить товар\nD - удалить товар\nlist - список всех товаров\nX - выйти из программы");
                        break;
                }
            }
        }
    }
}
