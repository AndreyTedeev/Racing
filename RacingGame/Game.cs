using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racing {
    
    public class Game {

        /// <summary>
        /// Длина круга в метрах
        /// </summary>
        public int Distance { get; set; } 

        /// <summary>
        /// Участники соревнований
        /// </summary>
        public List<Vehicle> Vehicles { get; set; }


    }
}
