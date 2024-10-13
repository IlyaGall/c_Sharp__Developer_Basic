using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace program_2
{
    // Написать класс "Каталог планет". В нем должен быть список планет - при создании экземпляра класса сразу заполнять его тремя планетами: Венера, Земля, Марс.

    internal class CatalogPlanet
    {


        private int _intRequest = 0;
        public List<SolarSystem> Planets = new List<SolarSystem>();

        public CatalogPlanet()
        {
            SolarSystem onePlanet = new SolarSystem { NameSolarSystem = "Венера", NumberSolarSystem = 2, LengthEquator = 38025 };
            SolarSystem twoPlanet = new SolarSystem { NameSolarSystem = "Земля", NumberSolarSystem = 3, LengthEquator = 40076 };
            SolarSystem threePlanet = new SolarSystem { NameSolarSystem = "Марс", NumberSolarSystem = 4, LengthEquator = 6779 };
            Planets.Add(onePlanet);
            Planets.Add(twoPlanet);
            Planets.Add(threePlanet);

        }
        /*Добавить в класс "Каталог планет" метод "получить планету", который на вход принимает название планеты, а на выходе дает три поля: первые два поля порядковый
        номер планеты от Солнца и длину ее экватора, когда планета найдена, а последнее поле - для ошибки.В случае, если планету по названию найти не удалось,
        то этот метод должен возвращать строку "Не удалось найти планету" (именно строку, не Exception). На каждый третий вызов метод "получить планету" 
        должен возвращать строку "Вы спрашиваете слишком часто".*/


        /*
            Скопировать решение из программы 2, но
            переделать метод "получить планету" так,
            чтобы он на вход принимал еще один параметр,
            описывающий способ защиты от слишком
            частых вызовов - делегат PlanetValidator (можно
            вместо него использовать Func), который на
            вход принимает название планеты, а на выходе
            дает строку с ошибкой. Метод "получить
            планету" теперь не должен проверять сколько
            вызовов делалось ранее. Вместо этого он
            должен просто вызвать PlanetValidator и
            передать в него название планеты, поиск
            которой производится. И если PlanetValidator
            вернул ошибку - передать ее на выход из
            метода третьим полем.
            Из main-метода при вызове "получить планету"
            в качестве нового параметра передавать
            лямбду, которая делает всё ту же проверку,
            которая была и ранее - на каждый третий вызов
            она возвращает строку "Вы спрашиваете
            слишком часто" (в остальных случаях
            возвращает null). Результат исполнения
            программы должен получиться идентичный
            программе 2.
             */
        public string PlanetValidator()
        {
            _intRequest++;
            if (_intRequest == 3)
            {
                return "Вы спрашиваете слишком часто";
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// получить планету
        /// </summary>
        /// <param name="namePlanet">название планеты(string)</param>
        /// <returns></returns>
        public (int, double, string) GetPlanet(string namePlanet, Func<string> criteriaDelegate)
        {
            string error = criteriaDelegate();

            foreach (SolarSystem planet in Planets)
            {
                if (planet.NameSolarSystem == namePlanet)
                {
                    return (planet.NumberSolarSystem, planet.NumberSolarSystem, error);
                }
            }
            return (0, 0, "Не удалось найти планету");
        }



        /// <summary>
        /// получить планету 2 задание 
        /// </summary>
        /// <param name="namePlanet">название планеты(string)</param>
        /// <returns></returns>
        public List<(int, double, string?)> GetPlanet2(List<string> namePlanets, Func<string, string> criteriaDelegate)
        {
            bool foundPlanet = false;
            List<(int, double, string?)> returnResultList = new();
            foreach (string planetList in namePlanets)
            {// проверяем планеты
                foundPlanet = false;
                var @delegate = criteriaDelegate(planetList); //вызываем делегат из метода main
                foreach (SolarSystem planet in Planets)
                {
                    if (planet.NameSolarSystem == planetList)
                    {
                        if (@delegate == null)
                        {// если элемент коллекции не кратен 3, то добавляем
                            returnResultList.Add((planet.NumberSolarSystem, planet.NumberSolarSystem, null));
                            foundPlanet = true;
                        }
                        else
                        {
                            returnResultList.Add((0, 0, @delegate));
                            foundPlanet = true;
                        }
                    }
                }
                if (!foundPlanet)
                {
                    returnResultList.Add((0, 0, "Не удалось найти планету"));
                }
            }
            return returnResultList;
        }




        /// <summary>
        /// проверка на запретную планету
        /// </summary>
        /// <param name="collection">коллекция планет</param>
        /// <param name="criteriaDelegate">делегат</param>
        /// <returns></returns>
        public static List<string> planetFilter(List<string> collection, Func<string, bool> criteriaDelegate)
        {
            var result = new List<string>();

            foreach (var planet in collection)
            {
                if (criteriaDelegate(planet))
                {
                    result.Add(null);
                }
                else
                {
                    result.Add("Это запретная планета");
                }
            }

            return result;
        }





        /// <summary>
        /// получить планету 3 задание 
        /// </summary>
        /// <param name="namePlanet">название планеты(string)</param>
        /// <returns></returns>
        public List<(int, double, string?)> GetPlanet3(List<string> namePlanets, Func<string, string> criteriaDelegate)
        {
            bool foundPlanet = false;
            List<(int, double, string?)> returnResultList = new();
            foreach (string planetList in namePlanets)
            {// проверяем планеты
                foundPlanet = false;
                var @delegate = criteriaDelegate(planetList); //вызываем делегат из метода main
                if (@delegate == "Это запретная планета")
                {
                    returnResultList.Add((0, 0, "Это запретная планета"));
                    continue;
                }

                foreach (SolarSystem planet in Planets)
                {
                    if (planet.NameSolarSystem == planetList)
                    {
                        if (@delegate == null)
                        {// если элемент коллекции не кратен 3, то добавляем

                            returnResultList.Add((planet.NumberSolarSystem, planet.NumberSolarSystem, null));


                            foundPlanet = true;
                        }
                        else
                        {
                            returnResultList.Add((0, 0, @delegate));
                            foundPlanet = true;
                        }
                    }
                }
                if (!foundPlanet)
                {
                    returnResultList.Add((0, 0, "Не удалось найти планету"));
                }
            }
            return returnResultList;
        }


    }
}
