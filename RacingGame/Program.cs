using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Racing {

    class Program {

        const string GAME_PATH = "../../../game.json";
        static Dictionary<Vehicle, VehicleState> _results = null;

        static void Main() {
            while (WannaPlay())
                PlayGame();
            GoodBye();
        }

        static void PlayGame() {
            Console.Clear();
            Game game = LoadFromFile(GAME_PATH);
            Console.WriteLine("Сегодня в гонках участвуют:");
            Console.WriteLine();
            for (int i = 1; i <= game.Vehicles.Count; i++)
                Console.WriteLine($"{i}. {game.Vehicles[i - 1]}");
            Console.WriteLine();
            Console.WriteLine("ПОЕХАЛИ...");
            _results = game.RunRace(OnUpdate);
        }

        /// <summary>
        /// Событие сделано для того чтобы отделить UI часть от собственно игры
        /// </summary>
        /// <param name="index"></param>
        /// <param name="vehicle"></param>
        /// <param name="state"></param>
        static void OnUpdate(int index, Vehicle vehicle, VehicleState state) {
            string changingTire = state.IsChangingTire ? "Проколото колесо" : "Все в порядке      ";
            string traveled = String.Format("{0,5:0}", state.Traveled);
            string info = $"{vehicle.Name} : Пройдено {traveled} м. : {changingTire}";
            Console.SetCursorPosition(0, 10 + index);
            Console.WriteLine(info);
        }

        static void PrintResults() {
            if (_results is null)
                return;
            Console.Clear();
            Console.WriteLine("Таблица результатов");
            Console.WriteLine();
            _results = _results.OrderByDescending(x => x.Value.Traveled).ToDictionary(x => x.Key, x => x.Value);
            foreach (Vehicle vehicle in _results.Keys) {
                Console.WriteLine($"{vehicle.Name} {_results[vehicle].Traveled}");
            }
            PrintCentered(new string[] { "Нажмите любую клавишу для выхода" });
            Console.ReadKey();
        }

        static bool WannaPlay() {
            PrintResults();
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
            JsonSerializerSettings settings = new() {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
            };
            return JsonConvert.DeserializeObject<Game>(File.ReadAllText(fileName), settings);
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
