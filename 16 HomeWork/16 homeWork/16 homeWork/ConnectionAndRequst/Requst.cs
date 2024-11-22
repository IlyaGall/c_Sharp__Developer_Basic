using _16_homeWork.Objects;
using _16_homeWork.Server;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Npgsql;
using System.Diagnostics;
using Npgsql;
using System.Collections;


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
            string commandSql = @"select id,age,firstname,lastname 
                                  from customers";
            using (IDbConnection connection = new NpgsqlConnection(helpConnectionServer.ConnectionStringDapper))
            {
                return connection.Query<Customer>(commandSql).ToList();
            }
        }

        /// <summary>
        /// Вернуть клиентов, чей возраст больше определённого пророга
        /// </summary>
        /// <param name="helpConnectionServer"></param>
        /// <param name="age"></param>
        /// <returns></returns>
        public List<Customer> GetRequestCustomersAge(HelpConnectionServer helpConnectionServer, int age) 
        {
            string commandSql = @"select id, age, firstname, lastname 
                                from customers 
                                where age > @Age"; 
            var param = new
            {
                Age = age
            };
            using (IDbConnection connection = new NpgsqlConnection(helpConnectionServer.ConnectionStringDapper)) 
            {
               return connection.Query<Customer>(commandSql, param).ToList();
            }
        }
       



        /// <summary>
        /// Добавить нового клиента
        /// </summary>
        /// <param name="helpConnectionServer"></param>
        public void AddNewClient(HelpConnectionServer helpConnectionServer, int age, string firstName, string lastName) 
        {
            string commandSql = @"INSERT INTO Customers (age, firstname, lastname) 
                                 VALUES (@Age, @FirstName, @LastName)";
            var param = new
            {
                Age = age,
                FirstName = firstName,
                LastName = lastName
            };

            using (IDbConnection connection = new NpgsqlConnection(helpConnectionServer.ConnectionStringDapper))
            {
                 connection.Execute(commandSql, param);
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
            string commandSql = @"select id, customerid, productid,quantity 
                                from orders";
            using (IDbConnection connection = new NpgsqlConnection(helpConnectionServer.ConnectionStringDapper))
            {
                return connection.Query<Orders>(commandSql).ToList();
            }
        }

        /// <summary>
        /// Вернуть заказы с определённым количеством товаров
        /// </summary>
        /// <param name="helpConnectionServer"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public List<Orders> GetRequestOrdersQuantity(HelpConnectionServer helpConnectionServer, int quantity)
        {
            string commandSql = @"select id, customerid, productid, quantity 
                                  from orders where quantity >=@Quantity";
            var param = new
            {
                Quantity = quantity
            };
            using (IDbConnection connection = new NpgsqlConnection(helpConnectionServer.ConnectionStringDapper))
            {
                return connection.Query<Orders>(commandSql, param).ToList();
            }
        }


        /// <summary>
        /// Добавить новый заказ
        /// </summary>
        /// <param name="helpConnectionServer"></param>
        /// <param name="customerId">Id позиции</param>
        /// <param name="productId">Id продукта</param>
        /// <param name="quantity">Кол-во</param>
        public void AddNewOrder(HelpConnectionServer helpConnectionServer, int customerId, int productId, int quantity) 
        {
            string commandSql = @"INSERT INTO orders (customerid,productid,quantity) 
                                  VALUES (@CustomerId, @ProductId, @Quantity)";
            var Param = new
            {
                CustomerId = customerId,
                ProductId = productId,
                Quantity = quantity
            };
            using (IDbConnection connection = new NpgsqlConnection(helpConnectionServer.ConnectionStringDapper))
            {
                connection.Execute(commandSql, Param);
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
            string commandSql = @"select id, name, description, stockquantity, price 
                                from products";
       
            using (IDbConnection connection = new NpgsqlConnection(helpConnectionServer.ConnectionStringDapper))
            {
                return connection.Query<Product>(commandSql).ToList();
            }

        }

        /// <summary>
        /// Вернуть товар цена которого выше определённого порога
        /// </summary>
        /// <param name="helpConnectionServer"></param>
        /// <returns></returns>
        public List<Product> GetRequestProducts(HelpConnectionServer helpConnectionServer, int price)
        {
            string commandSql = @"select id, name, description, stockquantity, price 
                                from products 
                                where price > @Price";
            var param = new
            {
                Price = price
            };
            using (IDbConnection connection = new NpgsqlConnection(helpConnectionServer.ConnectionStringDapper))
            {
                return connection.Query<Product>(commandSql, param).ToList();
            }
        }
        /// <summary>
        /// Добавить новый продукт
        /// </summary>
        /// <param name="helpConnectionServer"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="stockQuantity"></param>
        /// <param name="price"></param>
        public void AddNewProduct(HelpConnectionServer helpConnectionServer, string name, string description, int stockQuantity, double price)
        {
            string commandSql = @"INSERT INTO products (name,description,stockquantity, price) 
                                 VALUES (@Name, @Description, @StockQuantity, @Price)";
            var Param = new
            {
                Name = name,
                Description = description,
                StockQuantity = stockQuantity,
                Price = price
            };
            using (IDbConnection connection = new NpgsqlConnection(helpConnectionServer.ConnectionStringDapper))
            {
                connection.Execute(commandSql, Param);
            }
        }
        #endregion
    }
}
