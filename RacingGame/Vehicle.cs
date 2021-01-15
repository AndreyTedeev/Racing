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

        private double _speedInKilometersPerHour = 0;
        public double SpeedInKilometersPerHour {
            get => _speedInKilometersPerHour; 
            set {
                _speedInKilometersPerHour = value;
                SpeedInMetersPerSecond = (int)value * 1000 / 360;
            }
        }

        public int TimeToChangeTire { get; set; }

        [JsonIgnore]
        public abstract string Name { get; }

        [JsonIgnore]
        public int SpeedInMetersPerSecond { get; private set;}

        [JsonIgnore]
        public bool IsFlatTire => _random.Next(1, 101) <= FlatTireProbability;

        public override string ToString() => $"{Name} | Скорость: {SpeedInKilometersPerHour} км/ч, Вероятность прокола: {FlatTireProbability} %";

        public override int GetHashCode() => (FlatTireProbability, SpeedInKilometersPerHour).GetHashCode();

        public override bool Equals(object other) => other is Vehicle v
            && (v.SpeedInKilometersPerHour, v.FlatTireProbability).Equals((SpeedInKilometersPerHour, FlatTireProbability));

    }
}
