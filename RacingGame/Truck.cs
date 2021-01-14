using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racing {

    public class Truck : Vehicle {

        public int CargoWeight { get; set; }

        public override string Name => "ГРУЗОВИК";

        public override string ToString() => $"{base.ToString()}, Вес груза: {CargoWeight}";

    }
}
