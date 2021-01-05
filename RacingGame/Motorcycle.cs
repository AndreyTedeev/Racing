using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racing {

    public class Motorcycle : Vehicle {

        /// <summary>
        /// Наличие коляски
        /// </summary>
        public bool HasSidecar { get; set; }

        public override string Name => "МОТОЦИКЛ";

        public override string ToString() {
            var yes_no = HasSidecar ? "Да" : "Нет";
            return $"{base.ToString()}, Наличие коляски: {yes_no}";
        }
    }
}
