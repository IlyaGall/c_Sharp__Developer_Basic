using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program_2
{
    /// <summary>
    /// класс планет
    /// </summary>
    class SolarSystem
    {
        /// <summary>
        /// название планеты
        /// </summary>
        public string? NameSolarSystem { get; set; }
        /// <summary>
        /// номер планеты
        /// </summary>
        public int NumberSolarSystem { get; set; }
        /// <summary>
        /// длина экватора
        /// </summary>
        public double LengthEquator { get; set; }
        /// <summary>
        /// ссылка на предыдущую планету
        /// </summary>
        public SolarSystem? PreviousPlanet { get; set; }
    }
}
