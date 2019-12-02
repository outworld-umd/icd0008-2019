using System;
using System.ComponentModel;
using System.Linq;
using GameEngine;

namespace ConsoleUI {

    public static class GameInterface {
        public const string NoColumnMessage = "No, no, no! There's no column with this number!";
        public const string ChoiceQuestion = "in which column do you want to drop the disc (type X to leave)?";
        public const string ColumnFullMessage = "This column is full!";
        private static readonly string _verticalSeparator = "│";
        private static readonly string _horizontalSeparator = "─";
        private static readonly string _centerSeparator = "┼";

        public static void PrintBoard(Game game) {
            var board = game.GetBoard();
            Console.WriteLine($"╔{string.Join("╤", Enumerable.Repeat("═════", game.Width))}╗");
            for (var yIndex = 0; yIndex < game.Height; yIndex++) {
                var line = "║";
                for (var xIndex = 0; xIndex < game.Width; xIndex++) {
                    line = line + "  " + GetSingleState(board[yIndex, xIndex]) + "  ";
                    if (xIndex < game.Width - 1) line += _verticalSeparator;
                }
                line += "║";
                Console.WriteLine(line);
                if (yIndex < game.Height - 1) {
                    line = "╟";
                    for (var xIndex = 0; xIndex < game.Width; xIndex++) {
                        line = line + _horizontalSeparator + _horizontalSeparator + _horizontalSeparator + _horizontalSeparator + _horizontalSeparator;
                        if (xIndex < game.Width - 1) line += _centerSeparator;
                    }
                    line += "╢";
                    Console.WriteLine(line);
                }
                
            }
            Console.WriteLine($"╠{string.Join("╪", Enumerable.Repeat("═════", game.Width))}╣");
            Console.WriteLine($"║ {string.Join("  │ ", Enumerable.Range(0, game.Width).Select(x => x.ToString().PadLeft(2)))}  ║");
            Console.WriteLine($"╚{string.Join("╧", Enumerable.Repeat("═════", game.Width))}╝");
        }

        public static string GetSingleState(Cell state) {
            switch (state) {
                case Cell.Empty:
                    return " ";
                case Cell.O:
                    return "⭕";
                case Cell.X:
                    return "⚫";
                default:
                    throw new InvalidEnumArgumentException("Unknown enum option!");
            }
        }
    }

}