using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Racing;
using System.IO;

namespace RacingTest {

    [TestClass]
    public class RacingTest {

        const string GAME_PATH = "../../../game.json";

        [TestMethod]
        public void Test_Config_Serialization() {

            Game game = new() {

                DistanceInMeters = 5000,

                Vehicles = new() {
                    new Truck {
                        SpeedInKilometersPerHour = 80.00,
                        FlatTireProbability = 10,
                        TimeToChangeTire = 5,
                        CargoWeight = 1000
                    },
                    new Car {
                        SpeedInKilometersPerHour = 100.00,
                        FlatTireProbability = 20,
                        TimeToChangeTire = 4,
                        PassengerCount = 4
                    },
                    new Motorcycle {
                        SpeedInKilometersPerHour = 120.00,
                        FlatTireProbability = 30,
                        TimeToChangeTire = 3,
                        HasSidecar = false
                    }
                }
            };

            game.SaveToFile(GAME_PATH);

            game = Game.LoadFromFile(GAME_PATH);

            Assert.IsTrue(game.DistanceInMeters == 5000);
            Assert.IsTrue(game.Vehicles.Count == 3);
            Assert.IsTrue(game.Vehicles[0].GetType().Equals(typeof(Truck)));
            Assert.IsTrue(game.Vehicles[1].GetType().Equals(typeof(Car)));
            Assert.IsTrue(game.Vehicles[2].GetType().Equals(typeof(Motorcycle)));
        }

    }
}
