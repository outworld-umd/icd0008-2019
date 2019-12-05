using System;
using Newtonsoft.Json;

namespace GameEngine {

    public class Game {

        public Game(int boardHeight = 6, int boardWidth = 7) {
            Height = boardHeight;
            Width = boardWidth;
            Board = new Cell[Height, Width];
            FirstPlayersMove = true;
        }
        
        [JsonProperty]
        private Cell[,] Board { get; set; }
        [JsonProperty]
        public int Height { get; set; }
        [JsonProperty]
        public int Width { get; set; }
        [JsonProperty]
        public bool FirstPlayersMove { get; set; }
        [JsonProperty]
        public bool FirstPlayerWinner { get; set; }
        [JsonProperty]
        public int LastColumn { get; set; }
        [JsonProperty]
        public int LastRow { get; set; }
        
        public Cell[,] GetBoard() {
            var result = new Cell[Height, Width];
            Array.Copy(Board, result, Board.Length);
            return result;
        }

        public bool DropDisc(int? column) {
            if (!column.HasValue) return false;
            var col = column.Value;
            var row = 0;
            if (Board[row, col] != Cell.Empty) return false;
            while (row + 1 < Height && Board[row + 1, col] == Cell.Empty) row += 1;
            LastRow = row;
            LastColumn = col;
            Board[row, col] = FirstPlayersMove ? Cell.X : Cell.O;
            return true;
        }

        public bool CheckGameEnd() {
            for (var i = 0; i < Width; i++)
                if (Board[0, i] == Cell.Empty)
                    return false;
            return true;
        }

        public bool CheckWinner() {
            var check = FirstPlayersMove ? Cell.X : Cell.O;
            FirstPlayersMove = !FirstPlayersMove;
            // check horizontally
            for (var i = 0; i < Width - 3; i++)
                if (Board[LastRow, i] == check && Board[LastRow, i + 1] == check && Board[LastRow, i + 2] == check &&
                    Board[LastRow, i + 3] == check) {
                    FirstPlayerWinner = check == Cell.X;
                    return true;
                }
            // check vertically
            for (var i = 0; i < Height - 3; i++)
                if (Board[i, LastColumn] == check && Board[i + 1, LastColumn] == check &&
                    Board[i + 2, LastColumn] == check && Board[i + 3, LastColumn] == check) {
                    FirstPlayerWinner = check == Cell.X;
                    return true;
                }
            // check the downward diagonal
            var row = LastRow >= LastColumn ? LastRow - LastColumn : 0;
            var col = LastRow >= LastColumn ? 0 : LastColumn - LastRow;
            for (; row < Height - 3 && col < Width - 3; row++, col++)
                if (Board[row, col] == check && Board[row + 1, col + 1] == check &&
                    Board[row + 2, col + 2] == check && Board[row + 3, col + 3] == check) {
                    FirstPlayerWinner = check == Cell.X;
                    return true;
                }
            // check the upward diagonal
            row = LastRow + LastColumn < Height ? LastRow + LastColumn : Height - 1;
            col = LastRow + LastColumn < Height ? 0 : LastRow + LastColumn - Height + 1;
            for (; row >= 3 && col < Width - 3; row--, col++)
                if (Board[row, col] == check && Board[row - 1, col + 1] == check &&
                    Board[row - 2, col + 2] == check && Board[row - 3, col + 3] == check) {
                    FirstPlayerWinner = check == Cell.X;
                    return true;
                }
            return false;
        }
    }

}