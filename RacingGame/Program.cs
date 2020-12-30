using Newtonsoft.Json;
using System;
using System.IO;

namespace Racing {

    class Program {

        const string GAME_PATH = "../../../game.json";

        static void Main() {
            Game game = LoadFromFile(GAME_PATH);
            Console.WriteLine(game.Distance);
        }

        /// <summary>
        /// Загружает поараметры игры из файла
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        static Game LoadFromFile(string fileName) {
            JsonSerializerSettings settings = new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
            };
            return JsonConvert.DeserializeObject<Game>(File.ReadAllText(GAME_PATH), settings);
        }
    }
}
