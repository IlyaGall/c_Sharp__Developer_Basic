using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace homeWork8
{
    /// <summary>
    /// бинарное дерево
    /// </summary>
    internal class BinaryTree
    {
        public Persona? value { get; set; }

        public BinaryTree? Left { get; set; }

        public BinaryTree? Right { get; set; }

        public BinaryTree()
        {

            value =null;
            Left = null;
            Right = null;
        }

            public BinaryTree(string name, double salary)
        {
            value = new Persona(name, salary);
            Left = null;
            Right = null;
        }

        /// <summary>
        /// добавление нового элемента в дерево
        /// </summary>
        /// <param name="name">имя</param>
        /// <param name="salary">зп</param>
        public void Add(string name, double salary)
        {
            searchAdd(name, salary, this);
        }

     

        /// <summary>
        /// добавление нового элемента в дерево с рекурсивным обходом дерева
        /// </summary>
        /// <param name="name">имя</param>
        /// <param name="salary">зп</param>
        /// <param binaryTree="salary">экземпляр класса дерева</param>
        private void searchAdd(string name, double salary, BinaryTree? binaryTree = null)
        {

            if (binaryTree?.value.Salary > salary)
            { // идём в лево
                if (binaryTree.Left != null)
                {
                    searchAdd(name, salary, binaryTree.Left);
                }
                else
                {
                    binaryTree.Left = new BinaryTree(name, salary);
                }
            }
            else
            { // идём в право
                if (binaryTree?.Right != null)
                {
                    searchAdd(name, salary, binaryTree?.Right);
                }
                else
                {
                    binaryTree.Right = new BinaryTree(name, salary);
                }
            }
        }

        /// <summary>
        /// вывести имя - зп сотрудников начиная с самой маленькой зп до самой большой зп
        /// </summary>
        public void symmetrySearch()
        {
            Stack<BinaryTree> nodes = new Stack<BinaryTree>();
            BinaryTree? current = this;
            while (nodes.Count() != 0 || current != null)
            {
                if (current != null)
                {
                    nodes.Push(current);
                    current = current.Left;
                }
                else
                {
                    BinaryTree node = nodes.Pop();
                    Console.WriteLine($"{node.value.Name} - {node.value.Salary}");
                    current = node.Right;
                }

            }
        }
        /// <summary>
        /// поиск сотрудника по зп
        /// </summary>
        /// <param name="salary">искомая зп</param>
        public void SalarySearch(double salary)
        {
            bool flag = false;
            if (this.value == null)
            {
                Console.WriteLine("такой сотрудник не найден");
                return;
            }
            if (this?.value?.Salary == salary)
            {//если сотрудник является корнем
                Console.WriteLine($"Нашёл {this.value.Name}");
            }
            (BinaryTree, int) tupe;
            if (this.Left != null && this.Left.value.Salary > salary)
            {
                tupe = salarySearch(salary, this.Left,0);
               
            }
            else
            {
                tupe =   salarySearch(salary, this.Right,0);
            }
            if (tupe.Item1 == null)
            {
                Console.WriteLine("Такой сотрудник не найден");
            }
            else 
            {
                Console.WriteLine( tupe.Item1.value.Name);
            }
        }
        /// <summary>
        /// поиск сотрудника по зп(рекурсия)
        /// </summary>
        /// <param name="salary">искомая зп</param>
        /// <param name="binaryTree">дерево</param>
        private (BinaryTree, int) salarySearch(double salary, BinaryTree binaryTree, int operationsCount) 
        {


            if (salary < binaryTree.value.Salary)
            {
                //Ищем в левом поддереве
                if (binaryTree.Left != null)
                {
                    return salarySearch(salary, binaryTree.Left, operationsCount + 1);
                }
                return (null, -1);
            }
            if (salary > binaryTree.value.Salary)
            {
                //Ищем в правом поддереве
                if (binaryTree.Right != null)
                {
                    return salarySearch( salary, binaryTree.Right, operationsCount + 1);
                }

                return (null, -1);
            }

            return (binaryTree, operationsCount + 1);
          


            //bool flag = false;
            //if (binaryTree?.value?.Salary == salary)
            //{//если сотрудник является корнем
            //    Console.WriteLine($"Нашёл {binaryTree.value.Name}");
            //    flag = true;
            //    return flag;
            //}

            //if (binaryTree?.Left != null && binaryTree.Left.value.Salary < salary)
            //{
            //    if (binaryTree.Left.value.Salary == salary)
            //    {
            //        Console.WriteLine(binaryTree?.Right?.value.Name);
            //        flag = true;
            //        return flag;
            //    }
            //    flag = salarySearch(salary, binaryTree.Left);
            //}
            //else
            //{
            //    if (binaryTree?.Right != null)
            //    {
            //        if (binaryTree.Right.value.Salary == salary)
            //        {
            //            Console.WriteLine(binaryTree.Right.value.Name);
            //            flag = true;
            //            return flag;
            //        }
            //        flag = salarySearch(salary, binaryTree.Right);
            //    }
            //    else
            //    {
            //        flag = salarySearch(salary, binaryTree.Left);
            //    }
            //}
            //return flag;
        }


    }
}
