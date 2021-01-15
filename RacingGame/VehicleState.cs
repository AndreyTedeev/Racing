namespace Racing {

    public interface IVehicleState {

        public Vehicle Vehicle { get; }

        public int Traveled { get; set; }

        public bool IsChangingTire { get; set; }

        public int RepairingTime { get; set; }

    }
}
