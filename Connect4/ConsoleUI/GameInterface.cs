using System;
using System.ComponentModel;
using System.Linq;
using GameEngine;

namespace ConsoleUI {

    public static class GameInterface {
        public const string NoColumnMessage = "No, no, no! There's no column with this number!";
        public const string ChoiceQuestion = "in which column do you want to drop the disc (type X to leave)?";
        public const string ColumnFullMessage = "This column is full!";
        private const string VerticalSeparator = "│";
        private const string HorizontalSeparator = "─";
        private const string CenterSeparator = "┼";

        public static void PrintBoard(Game game) {
            var board = game.GetBoard();
            Console.WriteLine($"╔{string.Join("╤", Enumerable.Repeat("═════", game.Width))}╗");
            for (var yIndex = 0; yIndex < game.Height; yIndex++) {
                var line = "║";
                for (var xIndex = 0; xIndex < game.Width; xIndex++) {
                    line = line + "  " + GetSingleState(board[yIndex, xIndex]) + "  ";
                    if (xIndex < game.Width - 1) line += VerticalSeparator;
                }
                line += "║";
                Console.WriteLine(line);
                if (yIndex < game.Height - 1) {
                    line = "╟";
                    for (var xIndex = 0; xIndex < game.Width; xIndex++) {
                        line = line + HorizontalSeparator + HorizontalSeparator + HorizontalSeparator + HorizontalSeparator + HorizontalSeparator;
                        if (xIndex < game.Width - 1) line += CenterSeparator;
                    }
                    line += "╢";
                    Console.WriteLine(line);
                }
                
            }
            Console.WriteLine($"╠{string.Join("╪", Enumerable.Repeat("═════", game.Width))}╣");
            Console.WriteLine($"║ {string.Join("  │ ", Enumerable.Range(0, game.Width).Select(x => x.ToString().PadLeft(2)))}  ║");
            Console.WriteLine($"╚{string.Join("╧", Enumerable.Repeat("═════", game.Width))}╝");
        }

        private static string GetSingleState(Cell state) {
            return state switch {
                Cell.Empty => " ",
                Cell.O => "⭕",
                Cell.X => "⚫",
                _ => throw new InvalidEnumArgumentException("Unknown enum option!")
            };
        }
    }

}