using Newtonsoft.Json;
using System;
using System.IO;

namespace Racing {

    class Program {

        const string GAME_PATH = "../../../game.json";

        static void Main() {
            if (WannaPlay())
                PlayGame();
            else 
                GoodBye();
        }

        static void PlayGame() {
            Console.Clear();
            Game game = LoadFromFile(GAME_PATH);
            Console.WriteLine("Сегодня в гонках участвуют:");
            for (int i = 1; i <= game.Vehicles.Count; i++) {
                Console.WriteLine($"{i}. {game.Vehicles[i-1]}");
            }
        }

        static bool WannaPlay() {
            Console.Clear();
            PrintCentered(new string[] { "Игра ГОНКИ", "", "1 - Играть, 0 - Выход", "", "2020 - Андрей Тедеев" });
            while (true) {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.KeyChar == '1')
                    return true;
                else if (keyInfo.KeyChar == '0')
                    return false;
            }
        }

        static void GoodBye() {
            Console.Clear();
            PrintCentered(new string[] { "СПАСИБО! До Встречи...", "", "Нажмите любую клавишу для выхода" });
            Console.ReadKey();
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

        public static void PrintCentered(string[] messages) {
            for (int i = 0; i < messages.Length; i++) {
                int centerX = (Console.WindowWidth / 2) - (messages[i].Length / 2);
                int centerY = (Console.WindowHeight / 2) + i - (messages.Length / 2);
                Console.SetCursorPosition(centerX, centerY);
                Console.Write(messages[i]);
            }
        }

    }
}
