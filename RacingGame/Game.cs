using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        private Dictionary<Vehicle, VehicleState> _states;
        private Vehicle _winner; 

        public Vehicle RunRace() {
            _states = new();
            foreach (Vehicle vehicle in Vehicles)
                _states.Add(vehicle, new VehicleState());
            _winner = null;
            while (_winner is null) {
                Update();
                Thread.Sleep(1000);
            }
            return _winner;
        }

        private void Update() {
            int pos = 10;
            foreach (Vehicle vehicle in _states.Keys) {
                VehicleState state = _states[vehicle];
                string info = $"{vehicle.Name} : {state.Traveled}";
                PrintState(pos++, info);
                state.Traveled += vehicle.SpeedInMetersPerSecond;
                // TODO: Прокол колеса
                if (state.Traveled >= Distance) {
                    _winner = vehicle;
                }

            }
        }

        private void PrintState(int pos, string info ) {
            Console.SetCursorPosition(0, pos);
            Console.WriteLine(info);
        }

    }
}
