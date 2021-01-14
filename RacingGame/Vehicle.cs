using Newtonsoft.Json;
using System;

namespace Racing {

    public abstract class Vehicle {
        static readonly Random _random = new();

        private int _flatTireProbability = 0;
        public int FlatTireProbability {
            get => _flatTireProbability;
            set {
                if ((value < 0) || (value > 100))
                    throw new ArgumentException("Expected value in range [0- 100]");
                _flatTireProbability = value;
            }
        }

        [JsonIgnore]
        public abstract string Name { get; }

        private double _speedInKilometersPerHour = 0;
        public double SpeedInKilometersPerHour {
            get { return _speedInKilometersPerHour; }
            set {
                _speedInKilometersPerHour = value;
                _speedInMetersPerSecond = (int)SpeedInKilometersPerHour * 1000 / 360;
            }
        }

        private int _speedInMetersPerSecond = 0;
        [JsonIgnore]
        public int SpeedInMetersPerSecond => _speedInMetersPerSecond;

        public int TimeToChangeTire { get; set; }

        public bool IsFlatTire => _random.Next(1, 101) <= FlatTireProbability;

        public override string ToString() => $"{Name} | Скорость: {SpeedInKilometersPerHour} км/ч, Вероятность прокола: {FlatTireProbability} %";

        public override int GetHashCode() => (FlatTireProbability, SpeedInKilometersPerHour).GetHashCode();

        public override bool Equals(object other) => other is Vehicle v
            && (v.SpeedInKilometersPerHour, v.FlatTireProbability).Equals((SpeedInKilometersPerHour, FlatTireProbability));

    }
}
