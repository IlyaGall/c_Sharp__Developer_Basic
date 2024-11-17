using _16_homeWork.ConnectionAndRequest;
using _16_homeWork.Objects;
using _16_homeWork.Server;

namespace _16_homeWork
{


    internal class Program
    {
        static void Main(string[] args)
        {
            HelpConnectionServer helpConnectionServer = new HelpConnectionServer();
            helpConnectionServer.NameDateBase = "Shop";
            helpConnectionServer.CreateStringConnection();

            Request request = new Request();

            #region простые запросы
            var r = request.GetRequestCustomers(helpConnectionServer);
            var r1 = request.GetRequestOrders(helpConnectionServer);
            var r2 = request.GetRequestProducts(helpConnectionServer);

            foreach (var item in r)
            {
                item.GetConsole();
            }
            Console.WriteLine();
            foreach (var item in r1)
            {
                item.GetConsole();
            }
            Console.WriteLine();
            foreach (var item in r2)
            {
                item.GetConsole();
            }
            Console.WriteLine();
            #endregion

            #region запросы с фильтром
            Console.WriteLine();
            var rAge = request.GetRequestCustomersAge(helpConnectionServer, 18);
            foreach (var item in rAge)
            {
                item.GetConsole();
            }
            Console.WriteLine();
            var rAge1 = request.GetRequestProducts(helpConnectionServer, 100);
            foreach (var item in rAge1)
            {
                item.GetConsole();
            }
            Console.WriteLine();
            var rAge2 = request.GetRequestOrdersQuantity(helpConnectionServer, 10);
            foreach (var item in rAge2)
            {
                item.GetConsole();
            }
            Console.WriteLine();
            #endregion

            #region ввод данных с консоли
            Console.WriteLine("Ведите возраст клиентов, который вас интересует");
            int age;
            if (int.TryParse(Console.ReadLine(), out age))
            {
                foreach (var item in request.GetRequestCustomersAge(helpConnectionServer, age))
                {
                    item.GetConsole();
                }
            }
            else 
            {
                Console.WriteLine("Не корректные данные");
            }

            Console.WriteLine();



            Console.WriteLine("Ведите цену продукта, который вас интересует");
            int price;
            if (int.TryParse(Console.ReadLine(), out price))
            {
                foreach (var item in request.GetRequestProducts(helpConnectionServer, price))
                {
                    item.GetConsole();
                }
            }
            else
            {
                Console.WriteLine("Не корректные данные");
            }
            Console.WriteLine();



            Console.WriteLine("Ведите количество заказов, которое вас интересует");
            int quantity;
            if (int.TryParse(Console.ReadLine(), out quantity))
            {
                foreach (var item in request.GetRequestOrdersQuantity(helpConnectionServer, quantity))
                {
                    item.GetConsole();
                }
            }
            else
            {
                Console.WriteLine("Не корректные данные");
            }
            Console.WriteLine();

            #endregion

            #region запросы с доболением данных
            AddCustomer addCustomer = new AddCustomer(11, "Test_Name", "Test_firstName");
           // request.AddNewClient(helpConnectionServer, addCustomer);

            AddProduct addProduct = new AddProduct("Испорченный банан", "Банан для истенных ценителей", 1, 12122);
           // request.AddNewProduct(helpConnectionServer, addProduct);

            AddOrder addOrder = new AddOrder(1,2,10000);
            // request.AddNewOrder(helpConnectionServer, addOrder);
            #endregion
        }
    }
}
