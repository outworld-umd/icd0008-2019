using System;

namespace GameEngine {

    public class Game {
        
        public Cell[,] Board { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public bool FirstPlayersMove { get; set; }
        private int _lastColumn;
        private int _lastRow;
        public bool FirstPlayerWinner { get; set; }

        public Game(int boardHeight = 6, int boardWidth = 7) {
            Height = boardHeight;
            Width = boardWidth;
            Board = new Cell[Height, Width];
            FirstPlayersMove = true;
        }

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
            _lastRow = row;
            _lastColumn = col;
            Board[row, col] = FirstPlayersMove ? Cell.X : Cell.O;
            return true;
        }

        public bool CheckGameEnd() {
            for (var i = 0; i < Width; i++) {
                if (Board[0, i] == Cell.Empty) return false;
            }
            return true;
        }

        public bool CheckWinner() {
            var check = FirstPlayersMove ? Cell.X : Cell.O;
            FirstPlayersMove = !FirstPlayersMove;
            // check horizontally
            for (var i = 0; i < Width - 3; i++) {
                if (Board[_lastRow, i] == check && Board[_lastRow, i + 1] == check && Board[_lastRow, i + 2] == check &&
                    Board[_lastRow, i + 3] == check) {
                    FirstPlayerWinner = check == Cell.X;
                    return true;
                }
            }
            // check vertically
            for (var i = 0; i < Height - 3; i++) {
                if (Board[i, _lastColumn] == check && Board[i+1, _lastColumn] == check && 
                    Board[i+2, _lastColumn] == check && Board[i, _lastColumn] == check) {
                    FirstPlayerWinner = check == Cell.X;
                    return true;
                }
            }
            // check the downward diagonal
            var row = _lastRow >= _lastColumn ? _lastRow - _lastColumn : 0;
            var col = _lastRow >= _lastColumn ? 0 : _lastColumn - _lastRow;
            for (; row < Height - 3 && col < Width - 3; row++, col++) {
                if (Board[row, col] == check && Board[row + 1, col + 1] == check && 
                    Board[row + 2, col + 2] == check && Board[row + 3, col + 3] == check) {
                    FirstPlayerWinner = check == Cell.X;
                    return true;
                }
            }
            // check the upward diagonal
            row = _lastRow + _lastColumn < Height ? _lastRow + _lastColumn : Height - 1;
            col = _lastRow + _lastColumn < Height ? 0 : _lastRow + _lastColumn - Height + 1;
            for (; row >= 3 && col < Width - 3; row--, col++) {
                if (Board[row, col] == check && Board[row - 1, col + 1] == check && 
                    Board[row - 2, col + 2] == check && Board[row - 3, col + 3] == check) {
                    FirstPlayerWinner = check == Cell.X;
                    return true;
                }
            }
            return false;
        }
    }

}