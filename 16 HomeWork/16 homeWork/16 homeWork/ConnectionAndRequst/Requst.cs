using _16_homeWork.Objects;
using _16_homeWork.Server;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;


namespace _16_homeWork.ConnectionAndRequest
{
    public class Request
    {
        #region Customers
        /// <summary>
        /// Запросить всех клиентов
        /// </summary>
        /// <param name="helpConnectionServer"></param>
        /// <returns></returns>
        public List<Customer> GetRequestCustomers(HelpConnectionServer helpConnectionServer)
        {
            var result = new List<Customer>();
            using (IDbConnection db = new NpgsqlConnection(helpConnectionServer.ConnectionString))
            {
                result.AddRange(db.Query<Customer>("select id,age,firstname,lastname from customers"));
            }
            return result;
        }

        /// <summary>
        /// Вернуть клиентов, чей возраст больше определённого пророга
        /// </summary>
        /// <param name="helpConnectionServer"></param>
        /// <param name="age"></param>
        /// <returns></returns>
        public List<Customer> GetRequestCustomersAge(HelpConnectionServer helpConnectionServer, int age) 
        {
            var result = new List<Customer>();
            using (IDbConnection db = new NpgsqlConnection(helpConnectionServer.ConnectionString))
            {
                result.AddRange(db.Query<Customer>($"select id,age,firstname,lastname from customers where age > {age}"));
            }
            return result;
        }

        /// <summary>
        /// Добавить нового клиента
        /// </summary>
        /// <param name="helpConnectionServer"></param>
        public void AddNewClient(HelpConnectionServer helpConnectionServer, AddCustomer addCustomer) 
        {
            using (IDbConnection db = new NpgsqlConnection(helpConnectionServer.ConnectionString))
            {
                var sqlQuery = "INSERT INTO Customers (age, firstname, lastname) VALUES (@age, @firstname, @lastname)";
                db.Execute(sqlQuery, addCustomer);
            }
          
        }
        #endregion


        #region Orders
        /// <summary>
        /// Вернуть заказы
        /// </summary>
        /// <param name="helpConnectionServer"></param>
        /// <returns></returns>
        public List<Orders> GetRequestOrders(HelpConnectionServer helpConnectionServer)
        {
            var result = new List<Orders>();
            using (IDbConnection db = new NpgsqlConnection(helpConnectionServer.ConnectionString))
            {
                result.AddRange(db.Query<Orders>("select id,customerid,productid,quantity from orders"));
            }
            return result;
        }

        /// <summary>
        /// Вернуть заказы с определённым количеством товаров
        /// </summary>
        /// <param name="helpConnectionServer"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public List<Orders> GetRequestOrdersQuantity(HelpConnectionServer helpConnectionServer, int quantity)
        {
            var result = new List<Orders>();
            using (IDbConnection db = new NpgsqlConnection(helpConnectionServer.ConnectionString))
            {
                result.AddRange(db.Query<Orders>($"select id,customerid,productid,quantity from orders where quantity >={quantity}"));
            }
            return result;
        }


        /// <summary>
        /// Добавить новый заказ
        /// </summary>
        /// <param name="helpConnectionServer"></param>
        /// <param name="addOrder"></param>
        public void AddNewOrder(HelpConnectionServer helpConnectionServer, AddOrder addOrder) 
        {
            using (IDbConnection db = new NpgsqlConnection(helpConnectionServer.ConnectionString))
            {
                var sqlQuery = "INSERT INTO orders (customerid,productid,quantity) VALUES (@customerid, @productid, @quantity)";
                db.Execute(sqlQuery, addOrder);
            }
        }
        #endregion

        //////////////////////////#######################

        #region Products
        /// <summary>
        /// Вернуть продукты
        /// </summary>
        /// <param name="helpConnectionServer"></param>
        /// <returns></returns>
        public List<Product> GetRequestProducts(HelpConnectionServer helpConnectionServer)
        {
            var result = new List<Product>();
            using (IDbConnection db = new NpgsqlConnection(helpConnectionServer.ConnectionString))
            {
                result.AddRange(db.Query<Product>("select id, name, description, stockquantity, price from products"));
            }
            return result;
        }

        /// <summary>
        /// Вернуть товар цена которого выше определённого порога
        /// </summary>
        /// <param name="helpConnectionServer"></param>
        /// <returns></returns>
        public List<Product> GetRequestProducts(HelpConnectionServer helpConnectionServer, int price)
        {
            var result = new List<Product>();
            using (IDbConnection db = new NpgsqlConnection(helpConnectionServer.ConnectionString))
            {
                result.AddRange(db.Query<Product>($"select id, name, description, stockquantity, price from products where price > {price}"));
            }
            return result;
        }
        /// <summary>
        /// Добавить новый продукт
        /// </summary>
        /// <param name="helpConnectionServer"></param>
        /// <param name="addOrder"></param>
        public void AddNewProduct(HelpConnectionServer helpConnectionServer, AddProduct addProduct)
        {
            using (IDbConnection db = new NpgsqlConnection(helpConnectionServer.ConnectionString))
            {
                var sqlQuery = "INSERT INTO products (name,description,stockquantity, price) VALUES (@name, @description, @stockquantity, @price)";
                db.Execute(sqlQuery, addProduct);
            }
        }
        #endregion
    }
}
