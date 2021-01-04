using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Racing {

    public class Game {

        /// Длина круга в метрах
        /// </summary>
        /// <summary>
        public int Distance { get; set; }

        /// <summary>
        /// Участники соревнований
        /// </summary>
        public List<Vehicle> Vehicles { get; set; }

        private Dictionary<Vehicle, VehicleState> _states;
        private bool _running = false;
        private Random _random;

        /// <summary>
        /// запуск игры
        /// </summary>
        /// <param name="OnUpdate"></param>
        /// <returns></returns>
        public Dictionary<Vehicle, VehicleState> RunRace(Action<int, Vehicle, VehicleState> OnUpdate) {
            _states = new();
            _running = true;
            _random = new Random();
            foreach (Vehicle vehicle in Vehicles)
                _states.Add(vehicle, new VehicleState());
            while (_running) {
                Update(OnUpdate);
                Thread.Sleep(1000);
            }
            return _states;
        }

        /// <summary>
        /// Проверяем состояние и изменяем положение объектов 
        /// </summary>
        /// <param name="OnUpdate"></param>
        private void Update(Action<int, Vehicle, VehicleState> OnUpdate) {
            int pos = 0;
            int mostTraveled = 0;
            foreach (Vehicle vehicle in _states.Keys) {
                VehicleState state = _states[vehicle];
                OnUpdate?.Invoke(pos++, vehicle, state);
                if (state.IsChangingTire) {
                    if (++state.RepairingTime == vehicle.ChangeTireTime) { 
                        state.IsChangingTire = false;
                        state.RepairingTime = 0;
                    }
                }
                else {
                    state.Traveled += vehicle.SpeedInMetersPerSecond;
                    state.IsChangingTire = CheckFlatTire(vehicle.FlatTireProbability);
                }
                if (state.Traveled > mostTraveled)
                    mostTraveled = state.Traveled;
            }
            _running = (mostTraveled < Distance);
        }

        /// <summary>
        /// Проверяем пробито колесо или нет
        /// </summary>
        /// <param name="probability"></param>
        /// <returns></returns>
        private bool CheckFlatTire(int probability) => _random.Next(1, 101) <= probability;

    }
}
