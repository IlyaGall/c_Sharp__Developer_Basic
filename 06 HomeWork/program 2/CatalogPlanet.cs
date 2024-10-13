using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program_2
{
    // Написать класс "Каталог планет". В нем должен быть список планет - при создании экземпляра класса сразу заполнять его тремя планетами: Венера, Земля, Марс.
    
    internal class CatalogPlanet
    {
        private int _intRequst=0;
        public List<SolarSystem> Planets = new List<SolarSystem>();

        public CatalogPlanet() 
        {
            SolarSystem onePlanet = new SolarSystem { NameSolarSystem = "Венера", NumberSolarSystem = 2, LengthEquator = 38025 };
            SolarSystem twoPlanet = new SolarSystem { NameSolarSystem = "Земля", NumberSolarSystem = 3, LengthEquator = 40076};
            SolarSystem threePlanet = new SolarSystem { NameSolarSystem = "Марс", NumberSolarSystem = 4, LengthEquator = 6779 };
            Planets.Add(onePlanet);
            Planets.Add(twoPlanet);
            Planets.Add(threePlanet);

        }
    /*Добавить в класс "Каталог планет" метод "получить планету", который на вход принимает название планеты, а на выходе дает три поля: первые два поля порядковый
    номер планеты от Солнца и длину ее экватора, когда планета найдена, а последнее поле - для ошибки.В случае, если планету по названию найти не удалось,
    то этот метод должен возвращать строку "Не удалось найти планету" (именно строку, не Exception). На каждый третий вызов метод "получить планету" 
    должен возвращать строку "Вы спрашиваете слишком часто".*/
      /// <summary>
      /// получить планету
      /// </summary>
      /// <param name="namePlanet">название планеты(string)</param>
      /// <returns></returns>
        public (int, double, string) GetPlanet(string namePlanet) 
        {
            _intRequst++;
            if (_intRequst == 3) 
            {
                return (0,0,"Вы спрашиваете слишком часто");
            }

            
            foreach (SolarSystem planet in Planets) 
            {
                if(planet.NameSolarSystem == namePlanet) 
                { 
                    return (planet.NumberSolarSystem, planet.NumberSolarSystem, "");
                }
            }
            return (0, 0, "Не удалось найти планету");
        }
    }
}
