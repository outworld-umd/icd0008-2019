using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConsoleUI;
using GameEngine;
using MenuSystem;

namespace ConsoleApp {

    internal class Program {

        private static GameSettings _settings;

        private static Game _saveGame;

        private static void Main(string[] args) {
            Console.Clear();
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            _settings = GameConfigHandler.LoadConfig();
            _saveGame = null;
            var menu2 = new Menu(2) {
                Title = "Choose a game",
                MenuItemsDictionary = new Dictionary<string, MenuItem> {
                    {"Q", new MenuItem {Title = "Start a new game", CommandToExecute = NewGame}},
                    {"W", new MenuItem {Title = "Load game", CommandToExecute = LoadGame}}
                }
            };
            var menu1 = new Menu(1) {
                Title = "Start a new game of Connect 4",
                MenuItemsDictionary = new Dictionary<string, MenuItem> {
                    {"Q", new MenuItem {Title = "2 players", CommandToExecute = menu2.Run}}
                }
            };
            var menuSettings = new Menu(1) {
                Title = "Start a new game of Connect 4",
                MenuItemsDictionary = new Dictionary<string, MenuItem> {
                    {"F", new MenuItem {Title = "Change Width of the Board", CommandToExecute = _settings.ChangeWidth}}, 
                    {"G", new MenuItem {Title = "Change Height of the Board", CommandToExecute = _settings.ChangeHeight}}, 
                    {"H", new MenuItem {Title = "Reset Default Settings",
                            CommandToExecute = () => {
                                _settings = new GameSettings();
                                GameConfigHandler.SaveConfig(_settings);
                                return "";
                            }}
                    }
                }
            };
            var menu0 = new Menu {
                Title = "Welcome to Connect 4!",
                MenuItemsDictionary = new Dictionary<string, MenuItem> {
                    {"S", new MenuItem {Title = "Start game", CommandToExecute = menu1.Run}},
                    {"A", new MenuItem {Title = "Settings", CommandToExecute = menuSettings.Run}}
                }
            };
            menu0.Run();
        }

        private static string NewGame() {
            _saveGame = null;
            _settings = GameConfigHandler.LoadConfig();
            return PlayGameTwoPlayers();
        }
        
        private static string LoadGame() {
            Game game = null;
            while (game == null) {
                Console.Clear();
                var files = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\saves\", "*.json").Select(Path.GetFileName).ToArray();
                Console.WriteLine($"Available saves: {string.Join(", ", files)}");
                var filename = InputHandler.GetUserStringInput("Choose the file to load without \".json\" at the end (D - default path (save), X - exit):", 1, 30,
                    "Enter a valid name!", true);
                if (filename == null) return "";
                if (filename.ToLower() == "d") filename = null;
                game = GameSaves.LoadSave(filename);
            }
            _saveGame = game;
            return PlayGameTwoPlayers();
        }

        private static void SaveGame(Game game) {
            var files = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\saves\", "*.json").Select(Path.GetFileName).ToArray();
            Console.WriteLine($"Existing saves: {string.Join(", ", files)}");            
            var filename = InputHandler.GetUserStringInput(
                "Choose the filename for a save without \".json\" at the end (D - default path, X - drop the game):", 1, 30, 
                "Enter a valid name!", true);
            if (filename == null) return;
            if (filename.ToLower() == "d") filename = null;
            GameSaves.Save(game, filename);
        }

        private static string PlayGameTwoPlayers() {
            var game = _saveGame ?? new Game(_settings.BoardHeight, _settings.BoardWidth);
            var done = false;
            while (!done) {
                Console.Clear();
                GameInterface.PrintBoard(game);
                var nextTurn = false;
                var player = game.FirstPlayersMove ? "Player 1, " : "Player 2, ";
                while (!nextTurn) {
                    var column = InputHandler.GetUserIntInput(player + GameInterface.ChoiceQuestion, 0, game.Width - 1,
                        GameInterface.NoColumnMessage, true);
                    if (!column.HasValue) {
                        SaveGame(game);
                        return "";
                    }
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
    }

}