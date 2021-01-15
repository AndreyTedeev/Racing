using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racing {

    public class VehicleStateImpl : VehicleState {

        public VehicleStateImpl(Vehicle vehicle) {
            Vehicle = vehicle;
        }

        public Vehicle Vehicle { get; }

        public int Traveled { get; set; } = 0;

        public bool IsChangingTire { get; set; } = false;

        public int RepairingTime { get; set; } = 0;

    }
}
