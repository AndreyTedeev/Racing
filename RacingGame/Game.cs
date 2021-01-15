using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Racing {

    public class Game {

        public int DistanceInMeters { get; set; }

        public List<Vehicle> Vehicles { get; set; }

        private List<IVehicleState> _states;

        private bool _running = false;

        private void Init() {
            _states = new();
            _running = true;
            foreach (Vehicle vehicle in Vehicles)
                _states.Add(new VehicleStateImpl( vehicle) );
        }

        public List<IVehicleState> Run(Action<int, IVehicleState> OnUpdate) {
            Init();
            while (_running) {
                Update(OnUpdate);
                Thread.Sleep(1000);
            }
            return _states;
        }

        private void Update(Action<int, IVehicleState> OnUpdate) {
            int pos = 0;
            int mostTraveled = 0;
            foreach (IVehicleState state in _states) {
                OnUpdate?.Invoke(pos++, state);
                if (state.IsChangingTire) {
                    if (++state.RepairingTime == state.Vehicle.TimeToChangeTire) {
                        state.IsChangingTire = false;
                        state.RepairingTime = 0;
                    }
                }
                else {
                    state.Traveled += state.Vehicle.SpeedInMetersPerSecond;
                    state.IsChangingTire = state.Vehicle.IsFlatTire;
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
