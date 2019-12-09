using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConsoleUI;
using DAL;
using GameEngine;
using MenuSystem;
using Newtonsoft.Json;

namespace ConsoleApp {

    internal static class Program {

        private static Game? _saveGame;

        private static void Main(string[] args) {
            Console.Clear();
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            _saveGame = null;
            var menu3 = new Menu(3) {
                Title = "Choose your opponent:",
                MenuItemsDictionary = new Dictionary<string, MenuItem> {
                    {"Q", new MenuItem {Title = "Human", CommandToExecute = () => { _saveGame = null; return PlayGameTwoPlayers(); }}},
                    {"W", new MenuItem {Title = "Computer", CommandToExecute = () => { _saveGame = null; return PlayGameComputer(); }}}
                }
            };
            var menu2 = new Menu(2) {
                Title = "Choose a game:",
                MenuItemsDictionary = new Dictionary<string, MenuItem> {
                    {"Q", new MenuItem {Title = "Start a new game", CommandToExecute = menu3.Run}},
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
                    {"F", new MenuItem {Title = "Change Width of the Board", CommandToExecute = ChangeWidth}}, 
                    {"G", new MenuItem {Title = "Change Height of the Board", CommandToExecute = ChangeHeight}}, 
                    {"H", new MenuItem {Title = "Reset Default Settings", CommandToExecute = SetDefaults}}
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

        private static string LoadGame() {
            Game? game = null;
            GameState? gameState = null;
            while (game == null) {
                Console.Clear();
                var saves = GameSaves.GetSaves();
                Console.WriteLine($"Available saves: {string.Join(", ", saves)}");
                var filename = 
                    InputHandler.GetUserStringInput("Choose the game to load " +
                                                    $"(D - default name ({GameSaves.DefaultName}), X - exit):",
                        1, 30, "Enter a valid name!", true);
                if (filename == null) return "";
                if (filename.ToLower() == "d") filename = null;
                gameState = GameSaves.LoadSave(filename);
                game = gameState != null ? JsonConvert.DeserializeObject<Game>(gameState.Data) : null;
            }
            _saveGame = game;
            return gameState!.Opponent == 0 ? PlayGameTwoPlayers() : PlayGameComputer();
        }

        private static void SaveGame(Game game, int mode) {
            var saves = GameSaves.GetSaves();
            Console.WriteLine($"Existing saves: {string.Join(", ", saves)}");            
            var filename = InputHandler.GetUserStringInput(
                $"Choose the name for a save (D - default name ({GameSaves.DefaultName}), X - drop the game):", 1, 30, 
                "Enter a valid name!", true);
            if (filename == null) return;
            if (filename.ToLower() == "d") filename = GameSaves.DefaultName;
            string? rewriteFilename = filename;
            while (saves.Contains(rewriteFilename)) {
                rewriteFilename = InputHandler.GetUserStringInput(
                    "The save with this name already exists. Choose another name, or X to rewrite the save:", 1, 30, 
                    "Enter a valid name!", true);
                if (rewriteFilename == null) break;
            }
            GameSaves.Save(game, rewriteFilename ?? filename, mode);
        }

        private static string PlayGameTwoPlayers() {
            var game = _saveGame ?? new Game(GameSettings.Settings?.BoardHeight ?? 6, GameSettings.Settings?.BoardWidth ?? 7);
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
                        SaveGame(game, 0);
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

        private static string PlayGameComputer() {
            var game = _saveGame ?? new Game(GameSettings.Settings?.BoardHeight ?? 6, GameSettings.Settings?.BoardWidth ?? 7);
            var done = false;
            while (!done) {
                if (game.FirstPlayersMove) {
                    Console.Clear();
                    GameInterface.PrintBoard(game);
                    var nextTurn = false;
                    while (!nextTurn) {
                        var column = InputHandler.GetUserIntInput("Player, " + GameInterface.ChoiceQuestion, 0, game.Width - 1,
                            GameInterface.NoColumnMessage, true);
                        if (!column.HasValue) {
                            SaveGame(game, 1);
                            return "";
                        }
                        nextTurn = game.DropDisc(column);
                        if (!nextTurn) Console.WriteLine(GameInterface.ColumnFullMessage);
                    }
                } else {
                    game.DropDisc(game.GetColumn());
                }
                done = game.CheckGameEnd() || game.CheckWinner();
            }
            string message;
            if (game.CheckGameEnd()) message = "Draw!";
            else message = game.FirstPlayerWinner ? "Player wins!" : "Computer wins!";
            Console.Clear();
            GameInterface.PrintBoard(game);
            Console.WriteLine(message);
            Console.Write("Press any key to continue...");
            Console.ReadKey();
            return "";
        }

        private static string ChangeWidth() {
            var (wMin, wMax) = (7, 14);
            GameSettings.Settings.BoardWidth = 
                InputHandler.GetUserIntInput($"Current width is {GameSettings.Settings.BoardWidth}.\n" +
                                             "Enter board width (Type X to set default):", wMin, wMax,
                    $"Width must be between {wMin} and {wMax}", true) ?? 7;
            GameConfigHandler.SaveConfig(GameSettings.Settings);
            return "";
        }

        private static string ChangeHeight() {
            var (hMin, hMax) = (6, 12);
            GameSettings.Settings.BoardHeight = 
                InputHandler.GetUserIntInput($"Current height is {GameSettings.Settings.BoardHeight}.\n" +
                                             "Enter board height (Type X to set default):", hMin, hMax,
                    $"Height must be between {hMin} and {hMax}", true) ?? 6;
            GameConfigHandler.SaveConfig(GameSettings.Settings);
            return "";
        }

        private static string SetDefaults() {
            GameSettings.Settings.BoardHeight = 6;
            GameSettings.Settings.BoardWidth = 7;
            GameConfigHandler.SaveConfig(GameSettings.Settings);
            return "";
        }
    }

}