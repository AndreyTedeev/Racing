﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racing {

    public class Truck: Vehicle {

        /// <summary>
        /// Вес груза в килограммах
        /// </summary>
        public int CargoWeight { get; set; }

        public override string Name => "ГРУЗОВИК";

        public override string ToString() {
            return $"{base.ToString()}, Вес груза: {CargoWeight}"; 
        }

    }
}
