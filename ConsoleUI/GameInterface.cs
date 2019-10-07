using System;
using System.ComponentModel;
using GameEngine;

namespace ConsoleUI
{
    public static class GameInterface
    {
        private static readonly string _verticalSeparator = "|";
        private static readonly string _horizontalSeparator = "-";
        private static readonly string _centerSeparator = "+";
        public const string NoColumnMessage = "No, no, no! There's no column with this number!";
        public const string ChoiceQuestion = "in which column do you want to drop the disc?";
        public const string ColumnFullMessage = "This column is full!";
        public const string InputIsNotDigit = "IDI NAHHUI EBANII UROD!";
        
        public static void PrintBoard(Game game)
        {
            var board = game.GetBoard();
            for (int yIndex = 0; yIndex < game.Height; yIndex++)
            {
                var line = "";
                for (int xIndex = 0; xIndex < game.Width; xIndex++)
                {
                    
                    line = line + " " + GetSingleState(board[yIndex, xIndex]) + " ";
                    if (xIndex < game.Width - 1)
                    {
                        line = line + _verticalSeparator;
                    }
                }
                
                Console.WriteLine(line);

                if (yIndex < game.Height - 1)
                {
                    line = "";
                    for (int xIndex = 0; xIndex < game.Width; xIndex++)
                    {
                        line = line + _horizontalSeparator+ _horizontalSeparator+ _horizontalSeparator;
                        if (xIndex < game.Width - 1)
                        {
                            line = line +_centerSeparator;
                        }
                    }
                    Console.WriteLine(line);
                }

                
            }
        }

        public static string GetSingleState(Cell state)
        {
            switch (state)
            {
                case Cell.Empty:
                    return " ";
                case Cell.O:
                    return "O";
                case Cell.X:
                    return "X";
                default:
                    throw new InvalidEnumArgumentException("Unknown enum option!");
            }
            
        }
    }
}
