using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Racing {

    public class Game {

        public int DistanceInMeters { get; set; }

        public List<Vehicle> Vehicles { get; set; }

        private Dictionary<Vehicle, VehicleState> _states;

        private bool _running = false;

        private void Init() {
            _states = new();
            _running = true;
            foreach (Vehicle vehicle in Vehicles)
                _states.Add(vehicle, new VehicleState());
        }

        public Dictionary<Vehicle, VehicleState> Run(Action<int, Vehicle, VehicleState> OnUpdate) {
            Init();
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
                if (state.IsChangingTire) {
                    if (++state.RepairingTime == vehicle.TimeToChangeTire) {
                        state.IsChangingTire = false;
                        state.RepairingTime = 0;
                    }
                }
                else {
                    state.Traveled += vehicle.SpeedInMetersPerSecond;
                    state.IsChangingTire = vehicle.IsFlatTire;
                }
                if (state.Traveled > mostTraveled)
                    mostTraveled = state.Traveled;
            }
            _running = (mostTraveled < DistanceInMeters);
        }

        static JsonSerializerSettings _jsonSerializerSettings;
        static JsonSerializerSettings JsonSerializerSettings =>
            _jsonSerializerSettings ??= new() {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
            };

        public static Game LoadFromFile(string fileName) =>
            JsonConvert.DeserializeObject<Game>(File.ReadAllText(fileName), JsonSerializerSettings);

        public void SaveToFile(string fileName) =>
            File.WriteAllText(fileName, JsonConvert.SerializeObject(this, JsonSerializerSettings));
    }
}
