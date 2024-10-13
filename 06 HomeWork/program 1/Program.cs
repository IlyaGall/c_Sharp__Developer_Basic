using System.Collections.Generic;
using System.Threading;

namespace homeWork5
{


    /*
     
    Создать четыре объекта анонимного типа для описания планет Солнечной системы со свойствами "Название", "Порядковый номер от Солнца", "Длина экватора", 
    "Предыдущая планета" (ссылка на объект - предыдущую планету):
    Венера
    Земля
    Марс
    Венера (снова)
    Данные по планетам взять из открытых источников.
    Вывести в консоль информацию обо всех созданных "планетах". Рядом с информацией по каждой планете вывести эквивалентна ли она Венере.
     */

    internal class Program
    {
        static void Main(string[] args)
        {
            
            var onePlanet = new { NameSolarSystem = "Венера", NumberSolarSystem = 2, LengthEquator = 38025 , PreviousPlanet = (object?)null };
            var twoPlanet = new { NameSolarSystem = "Земля", NumberSolarSystem = 3, LengthEquator = 40076, PreviousPlanet = onePlanet };
            var threePlanet = new { NameSolarSystem = "Марс", NumberSolarSystem = 4, LengthEquator = 6779, PreviousPlanet = twoPlanet };
            //var fourPlanet = new { NameSolarSystem = "Венера", NumberSolarSystem = 2, LengthEquator = 38025, PreviousPlanet = threePlanet };
            var fourPlanet = new { NameSolarSystem = "Венера", NumberSolarSystem = 2, LengthEquator = 38025, PreviousPlanet = (object?)null };

            Console.WriteLine($"название планеты: {onePlanet.NameSolarSystem} Номер планеты: {onePlanet.NumberSolarSystem} Длина экватора: {onePlanet.LengthEquator} 'эквивалентна Венере:' {onePlanet.Equals(onePlanet)}");
            Console.WriteLine($"название планеты: {twoPlanet.NameSolarSystem} Номер планеты: {twoPlanet.NumberSolarSystem} Длина экватора: {twoPlanet.LengthEquator} 'эквивалентна Венере:' {twoPlanet.Equals(onePlanet)}");
            Console.WriteLine($"название планеты: {threePlanet.NameSolarSystem} Номер планеты: {threePlanet.NumberSolarSystem} Длина экватора: {threePlanet.LengthEquator} 'эквивалентна Венере:' {threePlanet.Equals(onePlanet)}");
            Console.WriteLine($"название планеты: {fourPlanet.NameSolarSystem} Номер планеты: {fourPlanet.NumberSolarSystem} Длина экватора: {fourPlanet.LengthEquator} 'эквивалентна Венере:' {fourPlanet.Equals(onePlanet)}");

            /*
             Итог анонимный объект будет эквивалентен, если он имеет одинаковые свойства и данные в них 
                если планета не ссылается на предыдущую, то Венера = Венере 
             */
            Console.ReadKey();
        }
    }
}
