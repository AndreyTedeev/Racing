using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Racing;
using System.Collections.Generic;
using System.IO;

namespace RacingTest {

    [TestClass]
    public class RacingTest {

        const string GAME_PATH = "../../../game.json";

        [TestMethod]
        public void Test_Config_Serialization() {

            Game game = new() {

                Distance = 5000,

                Vehicles = new() {
                    new Truck {
                        Speed = 80.00,
                        FlatTireProbability = 10,
                        ChangeTireTime = 5,
                        CargoWeight = 1000
                    },
                    new Car {
                        Speed = 100.00,
                        FlatTireProbability = 20,
                        ChangeTireTime = 4,
                        PassengerCount = 4
                    },
                    new Motorcycle {
                        Speed = 100.00,
                        FlatTireProbability = 30,
                        ChangeTireTime = 3,
                        HasSidecar = false
                    }
                }
            };

            JsonSerializerSettings settings = new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
            };
            File.WriteAllText(GAME_PATH, JsonConvert.SerializeObject(game, settings));

            game = JsonConvert.DeserializeObject<Game>(File.ReadAllText(GAME_PATH), settings);

            Assert.IsTrue(game.Distance == 5000);
            Assert.IsTrue(game.Vehicles.Count == 3);
            Assert.IsTrue(game.Vehicles[0].GetType().Equals(typeof(Truck)));
            Assert.IsTrue(game.Vehicles[1].GetType().Equals(typeof(Car)));
            Assert.IsTrue(game.Vehicles[2].GetType().Equals(typeof(Motorcycle)));
        }

    }
}
