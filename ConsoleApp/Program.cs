using System;
using System.Collections.Generic;
using ConsoleUI;
using GameEngine;
using MenuSystem;

namespace ConsoleApp {

    internal class Program {

        private static GameSettings _settings;
        // TODO #1: put settings in different menu (settings -> next menu [height, width, name])
        // TODO #2: implement json saves for settings and for game (improve input function with save key)
        // TODO #3: do a menu "load games" (save menu in game!)
        // TODO #4: implement nullable types somewhere...
        private static void Main(string[] args) {
            Console.Clear();
            _settings = GameConfigHandler.LoadConfig();
            Console.WriteLine($"Hello to {_settings.GameName}!");
            var menu2 = new Menu(2) {
                Title = "Choose a game",
                MenuItemsDictionary = new Dictionary<string, MenuItem> {
                    {"Q", new MenuItem {Title = "Start a new game", CommandToExecute = PlayGameTwoPlayers}},
                    {"A", new MenuItem {Title = "Settings", CommandToExecute = SaveSettings}}
                }
            };
            var menu1 = new Menu(1) {
                Title = $"Start a new game of {_settings.GameName}",
                MenuItemsDictionary = new Dictionary<string, MenuItem> {
                    {"Q", new MenuItem {Title = "2 players", CommandToExecute = menu2.Run}},
                    {"A", new MenuItem {Title = "Settings", CommandToExecute = SaveSettings}}
                }
            };
            var menu0 = new Menu {
                Title = $"{_settings.GameName} Main Menu",
                MenuItemsDictionary = new Dictionary<string, MenuItem> {
                    {"S", new MenuItem {Title = "Start game", CommandToExecute = menu1.Run}},
                    {"A", new MenuItem {Title = "Settings", CommandToExecute = SaveSettings}}
                }
            };
            menu0.Run();
        }

        private static string SaveSettings() {
            var (wMin, wMax) = (7, 14);
            var (hMin, hMax) = (6, 12);
            _settings.BoardHeight = GetUserIntInput("Enter board width:", wMin, wMax,
                $"Width must be between {wMin} and {wMax}");
            _settings.BoardWidth = GetUserIntInput("Enter board height:", hMin, hMax,
                $"Height must be between {hMin} and {hMax}");
            GameConfigHandler.SaveConfig(_settings);
            return "";
        }


        private static string PlayGameTwoPlayers() {
            var game = new Game();
            var done = false;
            while (!done) {
                Console.Clear();
                GameInterface.PrintBoard(game);
                var nextTurn = false;
                var player = game.FirstPlayersMove ? "Player 1, " : "Player 2, ";
                while (!nextTurn) {
                    var column = GetUserIntInput(player + GameInterface.ChoiceQuestion, 0, game.Width-1,
                        GameInterface.NoColumnMessage);
                    nextTurn = game.DropDisc(column);
                    if (!nextTurn) Console.WriteLine(GameInterface.ColumnFullMessage);
                }
                done = game.CheckGameEnd() || game.CheckWinner();
            }
            string message;
            if (game.CheckGameEnd()) message = "Draw!";
            else message = game.FirstPlayerWinner ? "Player 1 wins!" : "Player 2 wins!";
            Console.Clear();
            GameInterface.PrintBoard(game);
            Console.WriteLine(message);
            Console.Write("Press any key to continue...");
            Console.ReadKey();
            return "";
        }

        private static int GetUserIntInput(string prompt, int min, int max, string outOfBoundsMessage) {
            while (true) {
                Console.WriteLine(prompt);
                Console.Write(">");
                var choice = Console.ReadLine();
                if (int.TryParse(choice, out var input)) {
                    if (input < min || input > max) Console.WriteLine(outOfBoundsMessage);
                    else return input;
                }
                else {
                    Console.WriteLine(GameInterface.InputIsNotDigit);
                }
            }
        }
    }

}