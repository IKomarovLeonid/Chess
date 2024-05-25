using System;
using System.Text;

namespace Objects.Src
{
    public class ChessBoard
    {
        private Field[,] data = new Field[8, 8];

        private ChessBoard() {
            
        }

        public Field GetField(string name)
        {
            var coordinates = GetCoordinates(name);
            return data[coordinates.Item1,coordinates.Item2];
        }

        private Tuple<int, int> GetCoordinates(string name)
        {
            var letter = name[0];
            var y = name[1] - 49;
            var x = letter - 97;
            if (x < 0 || x > 7 || y < 0 || y > 7) throw new ArgumentException($"Invalid intialization for {name}: x {x} y {y}");
            return Tuple.Create(y, x);
        }

        public static ChessBoard CreateInitial()
        {
            var board = new ChessBoard();
            board.data[0, 0] = new Field("a1", false, FigureType.Rock);
            board.data[0, 1] = new Field("b1", true, FigureType.Knight);
            board.data[0, 2] = new Field("c1", false, FigureType.Bishop);
            board.data[0, 3] = new Field("d1", true, FigureType.Queen);
            board.data[0, 4] = new Field("e1", false, FigureType.King);
            board.data[0, 5] = new Field("f1", true, FigureType.Bishop);
            board.data[0, 6] = new Field("g1", false, FigureType.Knight);
            board.data[0, 7] = new Field("h1", true, FigureType.Rock);

            board.data[1, 0] = new Field("a2", true, FigureType.Pawn);
            board.data[1, 1] = new Field("b2", false, FigureType.Pawn);
            board.data[1, 2] = new Field("c2", true, FigureType.Pawn);
            board.data[1, 3] = new Field("d2", false, FigureType.Pawn);
            board.data[1, 4] = new Field("e2", true, FigureType.Pawn);
            board.data[1, 5] = new Field("f2", false, FigureType.Pawn);
            board.data[1, 6] = new Field("g2", true, FigureType.Pawn);
            board.data[1, 7] = new Field("h2", false, FigureType.Pawn);

            board.data[2, 0] = new Field("a3", false);
            board.data[2, 1] = new Field("b3", true);
            board.data[2, 2] = new Field("c3", false);
            board.data[2, 3] = new Field("d3", true);
            board.data[2, 4] = new Field("e3", false);
            board.data[2, 5] = new Field("f3", true);
            board.data[2, 6] = new Field("g3", false);
            board.data[2, 7] = new Field("h3", true);

            board.data[3, 0] = new Field("a4", true);
            board.data[3, 1] = new Field("b4", false);
            board.data[3, 2] = new Field("c4", true);
            board.data[3, 3] = new Field("d4", false);
            board.data[3, 4] = new Field("e4", true);
            board.data[3, 5] = new Field("f4", false);
            board.data[3, 6] = new Field("g4", true);
            board.data[3, 7] = new Field("h4", false);

            return board;
        }

        public string Print()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    sb.Append(data[i, j]?.Name+ " "); // Format with leading zeros
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }

        public bool MakeMove(string move) {
            return true;
        }
    }
}
