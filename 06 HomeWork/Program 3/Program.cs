using program_2;
using System.Numerics;
using static program_2.CatalogPlanet;
using static Program_3.Class1;

namespace Program_3
{
    /*
    
    


    
         */
    internal class Program
    {
        static void Main(string[] args)
        {
            CatalogPlanet planet = new CatalogPlanet();
            List<string> namePlanet = new List<string>() { "Земля", "Лимония", "Марс" };
            #region 1 часть задания с делегатом
            /*
             Скопировать решение из программы 2, но переделать метод "получить планету" так, чтобы он на вход принимал еще один параметр, 
             описывающий способ защиты от слишком частых вызовов - делегат PlanetValidator (можно вместо него использовать Func),
             который на вход принимает название планеты, а на выходе дает строку с ошибкой. Метод "получить планету" теперь не должен проверять сколько вызовов делалось ранее. 
             Вместо этого он должен просто вызвать PlanetValidator и передать в него название планеты, поиск которой производится. 
             И если PlanetValidator вернул ошибку - передать ее на выход из метода третьим полем.
             */
            foreach (string plane in namePlanet)
            {
                var answerRequst = planet.GetPlanet(plane, planet.PlanetValidator); 
                // спросить у преподавателя, а что делать есть в ф-и надо передать значение    Func <string,string> criteriaDelegate) 
                if (answerRequst.Item3 == null)
                {
                    Console.WriteLine($"порядковый номер планеты от Солнца {answerRequst.Item1} длина ее экватора {answerRequst.Item2}");
                }
                else
                {
                    Console.WriteLine($"{answerRequst.Item3}");
                }
            }
            #endregion


            #region 2 часть задания с делегатом
            /*
                 Из main-метода при вызове "получить планету" в качестве нового параметра передавать лямбду, которая делает всё ту же проверку,
                 которая была и ранее - на каждый третий вызов она возвращает строку "Вы спрашиваете слишком часто" (в остальных случаях возвращает null). 
                 Результат исполнения программы должен получиться идентичный программе 2.
             */
            Console.WriteLine("\n########### 2 task ###############");
            var counter = 0;
            var collection = planet.GetPlanet2(namePlanet, i =>
            {
                counter++;
                if (counter % 3 == 0) 
                {
                    return "Вы спрашиваете слишком часто";
                }
                return null;
            });

            foreach (var plane in collection)
            {
                // спросить у преподавателя, а что делать есть в ф-и надо передать значение в делегате Func <string,string> criteriaDelegate) 
                if (plane.Item3 == null)
                {
                    Console.WriteLine($"порядковый номер планеты от Солнца {plane.Item1} длина ее экватора {plane.Item2}");
                }
                else
                {
                    Console.WriteLine($"{plane.Item3}");
                }
            }
            #endregion

            Console.WriteLine("\n########### 3 task ###############");

            #region 3 часть задания с *
            /*
              (*) Дописать main-метод так, чтобы еще раз проверять планеты "Земля", "Лимония" и "Марс", но передавать другую лямбду так, чтобы она для названия "Лимония" 
              возвращала ошибку "Это запретная планета", а для остальных названий - null. Убедиться, что в этой серии проверок ошибка появляется только для Лимонии.
              Таким образом, вы делегировали логику проверки допустимости найденной планеты от метода "получить планету" к вызывающему этот метод коду.
              В чат напишите также время, которое вам потребовалось для реализации домашнего задания.
             */
         
            


            var collection3 = planet.GetPlanet3(namePlanet, i =>
            {
                counter++;
                if (counter % 3 == 0)
                {
                    return "Вы спрашиваете слишком часто";
                }
                if (i == "Лимония") 
                {
                    return "Это запретная планета";
                }
                return null;
            }

            )
            ;


            var collection3_1 = CatalogPlanet.planetFilter(namePlanet, delegate (string plane)
            {
                return plane != "Лимония";
            });
            #endregion

        }












    }
}
