﻿namespace homeWork8
{
    
   

   
    internal class Program
    {
        static void Main(string[] args)
        {

            /*
            принимает на вход из консоли информацию о сотрудниках: имя + зарплата (имя в первой строке, зарплата в виде целого числа во второй строке; 
            и так для каждого сотрудника, пока пользователь не введет пустую строку в качестве имени сотрудника)
            попутно при получении информации о сотрудниках строится бинарное дерево с этой информацией, где в каждом узле хранится имя сотрудника, 
            а его зарплата является значением, на основе которого производится бинарное разделение в дереве
             */
            BinaryTree binaryTree = new BinaryTree("сотрудник 1", 100000);
            binaryTree.Add("сотрудник 2", 10000);
            binaryTree.Add("сотрудник 3", 1000);
            binaryTree.Add("сотрудник 4", 100);
            binaryTree.Add("сотрудник 5", 54);
            binaryTree.Add("сотрудник 6", 90);
            binaryTree.Add("сотрудник 7", 777777);
            binaryTree.Add("сотрудник 8", 1);
            binaryTree.Add("сотрудник 9", 2);


            /*
             после окончания ввода пользователем программа выводит имена сотрудников и их зарплаты в порядке возрастания зарплат 
             (в каждой строчке формат вывода "Имя - зарплата"). Использовать для этого симметричный обход дерева.
             */
            binaryTree.symmetrySearch();

            /*
               после этого программа запрашивает размер зарплаты, который интересует пользователя. В построенном бинарном дереве программа находит сотрудника с указанной зарплатой 
               и выводит его имя. Если сотрудник не найден - выводится "такой сотрудник не найден"
             */
            binaryTree.SalarySearch(0);


            /*
               после этого программа предлагает ввести цифру 0 (переход к началу программы) или 1 (снова поиск зарплаты). При вводе 0 должен произойти переход к началу работы
               программы, т.е. опять запрашивается список сотрудников и строится новое дерево. При вводе 1 должны снова запросить зарплату, которую хочется поискать 
              в дереве - см.предыдущий пункт.
            */
            Menu.command();

            Console.ReadKey();
        }
    }
}
