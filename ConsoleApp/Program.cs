using System;
using System.Collections.Generic;
using ConsoleUI;
using MenuSystem;
using GameEngine;

namespace ConsoleApp {

    internal class Program {

        private static void Main(string[] args) {
            Console.Clear();
            Console.WriteLine("Hello Game!");
            var gameMenu = new Menu(1) {
                Title = "Start a new game of Connect Four",
                MenuItems = new List<MenuItem> {
                    new MenuItem {Command = "Q", Title = "2 Players", CommandToExecute = PlayGameTwoPlayers}
                }
            };
            var startMenu = new Menu {
                Title = "Connect Four Main Menu",
                MenuItems = new List<MenuItem> {
                    new MenuItem {Command = "S", Title = "Start game", CommandToExecute = gameMenu.Run}
                }
            };

            startMenu.Run();
        }

        private static string PlayGameTwoPlayers() {
            var game = CreateNewGame();
            var done = false;
            while (!done) {
                Console.Clear();
                GameInterface.PrintBoard(game);
                var nextTurn = false;
                var player = game.FirstPlayersMove ? "Player 1, " : "Player 2, ";
                Console.WriteLine(player + GameInterface.ChoiceQuestion);
                while (!nextTurn) {
                    Console.Write(">");
                    var choice = Console.ReadLine();
                    if (int.TryParse(choice, out var column)) {
                        if (column < 0 || column >= game.Width) Console.WriteLine(GameInterface.NoColumnMessage);
                        else {
                            nextTurn = game.DropDisc(column);
                            if (!nextTurn) Console.WriteLine(GameInterface.ColumnFullMessage);
                        }
                    }
                    else Console.WriteLine(GameInterface.InputIsNotDigit);
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
            Console.Read();
            return "";
        }

        private static Game CreateNewGame() {
            while (true) {
                Console.WriteLine("Insert the height of your board!");
                Console.Write(">");
                int.TryParse(Console.ReadLine(), out var boardHeight);
                Console.WriteLine("Insert the width of your board!");
                Console.Write(">");
                int.TryParse(Console.ReadLine(), out var boardWidth);
                if (!(boardHeight < 6 || boardHeight > 12 || boardWidth < 7 || boardWidth > 14)) 
                    return new Game(boardHeight, boardWidth);
                Console.WriteLine("Wrong height/width! Height must be in range 6-12, width in range 7-14. Try again.");
            }
        }
    }
}
