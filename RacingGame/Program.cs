using System;
using System.Collections.Generic;
using System.Linq;

namespace Racing {

    class Program {

        const string GAME_PATH = "../../../game.json";

        static void Main() {
            while (WannaPlay()) {
                var results = PlayGame();
                PrintResults(results);
            }
            GoodBye();
        }

        static List<VehicleState> PlayGame() {
            Console.Clear();
            Game game = Game.LoadFromFile(GAME_PATH);
            Console.WriteLine("Сегодня в гонках участвуют:");
            Console.WriteLine();
            for (int i = 1; i <= game.Vehicles.Count; i++)
                Console.WriteLine($"{i}. {game.Vehicles[i - 1]}");
            Console.WriteLine();
            Console.WriteLine("ПОЕХАЛИ...");
            return game.Run(OnGameUpdate);
        }

        static void OnGameUpdate(int index, VehicleState state) {
            string changingTire = state.IsChangingTire ? "Проколото колесо" : "Все в порядке      ";
            string traveled = String.Format("{0,5:0}", state.Traveled);
            string info = $"{state.Vehicle.Name} : Пройдено {traveled} м. : {changingTire}";
            Console.SetCursorPosition(0, 10 + index);
            Console.WriteLine(info);
        }

        static void PrintResults(List<VehicleState> results) {
            if (results is null)
                return;
            Console.Clear();
            Console.WriteLine("Таблица результатов");
            Console.WriteLine();
            results = results.OrderByDescending(x => x.Traveled).ToList();
            foreach (VehicleState state in results) {
                Console.WriteLine($"{state.Vehicle.Name} {state.Traveled}");
            }
            PrintCentered(new string[] { "Нажмите любую клавишу для выхода" });
            Console.ReadKey();
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
