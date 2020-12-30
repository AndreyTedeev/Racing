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

        /// <summary>
        /// Скорость движения км/час
        /// </summary>
        public double Speed { get; set; }

        public override string ToString() {
            return $"Скорость: {Speed} км/ч, Вероятность прокола: {FlatTireProbability} %";
        }

    }
}
