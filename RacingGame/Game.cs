using System;
using System.Collections.Generic;
using System.Threading;

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
        private bool _running = false;

        public Dictionary<Vehicle, VehicleState> RunRace(Action<int, Vehicle, VehicleState> OnUpdate) {
            _states = new();
            _running = true;
            foreach (Vehicle vehicle in Vehicles)
                _states.Add(vehicle, new VehicleState());
            while (_running) {
                Update(OnUpdate);
                Thread.Sleep(1000);
            }
            return _states;
        }

        private void Update(Action<int, Vehicle, VehicleState> OnUpdate) {
            int pos = 0;
            int mostTraveled = 0;
            foreach (Vehicle vehicle in _states.Keys) {
                VehicleState state = _states[vehicle];
                OnUpdate?.Invoke(pos++, vehicle, state);
                if (state.IsChangingTire) { }
                else 
                    state.Traveled += vehicle.SpeedInMetersPerSecond;
                // TODO: Прокол колеса

                if (state.Traveled > mostTraveled)
                    mostTraveled = state.Traveled;
            }
            _running = (mostTraveled < Distance);
        }

    }
}
