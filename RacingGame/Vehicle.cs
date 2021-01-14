using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racing {

    public abstract class Vehicle {

        private int _flatTireProbability = 0;

        /// <summary>
        /// Вероятность прокола колеса в процентах (0 - 100)
        /// </summary>
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

        /// <summary>
        /// Скорость движения км/час
        /// </summary>
        public double Speed { get; set; }

        /// <summary>
        /// Скорость движения м/с
        /// </summary>
        [JsonIgnore]
        public int SpeedInMetersPerSecond => (int)Speed * 1000 / 360;

        /// <summary>
        /// Время на замену колеса в секундах
        /// </summary>
        public int ChangeTireTime { get; set; }

        public override string ToString() {
            return $"{Name} | Скорость: {Speed} км/ч, Вероятность прокола: {FlatTireProbability} %";
        }

        public override int GetHashCode() => (FlatTireProbability, Speed).GetHashCode();

        // public override int GetHashCode() {
        //     unchecked {
        //         int hashcode = 4859604;
        //         hashcode = hashcode * 4857562 ^ FlatTireProbability.GetHashCode();
        //         hashcode = hashcode * 4857562 ^ Speed.GetHashCode();
        //         return hashcode;
        //     }
        // }

        public override bool Equals(object other) => other is Vehicle v
            && (v.Speed, v.FlatTireProbability).Equals((Speed, FlatTireProbability));

    }
}
