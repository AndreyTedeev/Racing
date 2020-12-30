using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racing {
    
    public class Car: Vehicle {

        /// <summary>
        /// Количество пассажиров
        /// </summary>
        public int PassengerCount { get; set; }

        public override string Name => "ЛЕГКОВОЙ";

        public override string ToString() {
            return $"{base.ToString()}, Пассажиров: {PassengerCount}";
        }
    }
}
