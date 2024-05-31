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

            board.data[0, 0] = new Field("a1", false, new Figure(FigureType.Rock, true));
            board.data[0, 1] = new Field("b1", true, new Figure(FigureType.Knight, true));
            board.data[0, 2] = new Field("c1", false, new Figure(FigureType.Bishop, true));
            board.data[0, 3] = new Field("d1", true, new Figure(FigureType.Queen, true));
            board.data[0, 4] = new Field("e1", false, new Figure(FigureType.King, true));
            board.data[0, 5] = new Field("f1", true, new Figure(FigureType.Bishop, true));
            board.data[0, 6] = new Field("g1", false, new Figure(FigureType.Knight, true));
            board.data[0, 7] = new Field("h1", true, new Figure(FigureType.Rock, true));

            board.data[1, 0] = new Field("a2", true, new Figure(FigureType.Pawn, true));
            board.data[1, 1] = new Field("b2", false, new Figure(FigureType.Pawn, true));
            board.data[1, 2] = new Field("c2", true, new Figure(FigureType.Pawn, true));
            board.data[1, 3] = new Field("d2", false, new Figure(FigureType.Pawn, true));
            board.data[1, 4] = new Field("e2", true, new Figure(FigureType.Pawn, true));
            board.data[1, 5] = new Field("f2", false, new Figure(FigureType.Pawn, true));
            board.data[1, 6] = new Field("g2", true, new Figure(FigureType.Pawn, true));
            board.data[1, 7] = new Field("h2", false, new Figure(FigureType.Pawn, true));

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

            board.data[4, 0] = new Field("a5", false);
            board.data[4, 1] = new Field("b5", true);
            board.data[4, 2] = new Field("c5", false);
            board.data[4, 3] = new Field("d5", true);
            board.data[4, 4] = new Field("e5", false);
            board.data[4, 5] = new Field("f5", true);
            board.data[4, 6] = new Field("g5", false);
            board.data[4, 7] = new Field("h5", true);

            board.data[5, 0] = new Field("a6", true);
            board.data[5, 1] = new Field("b6", false);
            board.data[5, 2] = new Field("c6", true);
            board.data[5, 3] = new Field("d6", false);
            board.data[5, 4] = new Field("e6", true);
            board.data[5, 5] = new Field("f6", false);
            board.data[5, 6] = new Field("g6", true);
            board.data[5, 7] = new Field("h6", false);

            board.data[6, 0] = new Field("a7", false, new Figure(FigureType.Pawn, false));
            board.data[6, 1] = new Field("b7", true, new Figure(FigureType.Pawn, false));
            board.data[6, 2] = new Field("c7", false, new Figure(FigureType.Pawn, false));
            board.data[6, 3] = new Field("d7", true, new Figure(FigureType.Pawn, false));
            board.data[6, 4] = new Field("e7", false, new Figure(FigureType.Pawn, false));
            board.data[6, 5] = new Field("f7", true, new Figure(FigureType.Pawn, false));
            board.data[6, 6] = new Field("g7", false, new Figure(FigureType.Pawn, false));
            board.data[6, 7] = new Field("h7", true, new Figure(FigureType.Pawn, false));

            board.data[7, 0] = new Field("a8", true, new Figure(FigureType.Rock, false));
            board.data[7, 1] = new Field("b8", false, new Figure(FigureType.Knight, false));
            board.data[7, 2] = new Field("c8", true, new Figure(FigureType.Bishop, false));
            board.data[7, 3] = new Field("d8", false, new Figure(FigureType.Queen, false));
            board.data[7, 4] = new Field("e8", true, new Figure(FigureType.King, false));
            board.data[7, 5] = new Field("f8", false, new Figure(FigureType.Bishop, false));
            board.data[7, 6] = new Field("g8", true, new Figure(FigureType.Knight, false));
            board.data[7, 7] = new Field("h8", false, new Figure(FigureType.Rock, false));

            return board;
        }

        public static ChessBoard CreateOnlyBoard()
        {
            var board = new ChessBoard();

            board.data[0, 0] = new Field("a1", false);
            board.data[0, 1] = new Field("b1", true);
            board.data[0, 2] = new Field("c1", false);
            board.data[0, 3] = new Field("d1", true);
            board.data[0, 4] = new Field("e1", false);
            board.data[0, 5] = new Field("f1", true);
            board.data[0, 6] = new Field("g1", false);
            board.data[0, 7] = new Field("h1", true);

            board.data[1, 0] = new Field("a2", true);
            board.data[1, 1] = new Field("b2", false);
            board.data[1, 2] = new Field("c2", true);
            board.data[1, 3] = new Field("d2", false);
            board.data[1, 4] = new Field("e2", true);
            board.data[1, 5] = new Field("f2", false);
            board.data[1, 6] = new Field("g2", true);
            board.data[1, 7] = new Field("h2", false);

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

            board.data[4, 0] = new Field("a5", false);
            board.data[4, 1] = new Field("b5", true);
            board.data[4, 2] = new Field("c5", false);
            board.data[4, 3] = new Field("d5", true);
            board.data[4, 4] = new Field("e5", false);
            board.data[4, 5] = new Field("f5", true);
            board.data[4, 6] = new Field("g5", false);
            board.data[4, 7] = new Field("h5", true);

            board.data[5, 0] = new Field("a6", true);
            board.data[5, 1] = new Field("b6", false);
            board.data[5, 2] = new Field("c6", true);
            board.data[5, 3] = new Field("d6", false);
            board.data[5, 4] = new Field("e6", true);
            board.data[5, 5] = new Field("f6", false);
            board.data[5, 6] = new Field("g6", true);
            board.data[5, 7] = new Field("h6", false);

            board.data[6, 0] = new Field("a7", false);
            board.data[6, 1] = new Field("b7", true);
            board.data[6, 2] = new Field("c7", false);
            board.data[6, 3] = new Field("d7", true);
            board.data[6, 4] = new Field("e7", false);
            board.data[6, 5] = new Field("f7", true);
            board.data[6, 6] = new Field("g7", false);
            board.data[6, 7] = new Field("h7", true);

            board.data[7, 0] = new Field("a8", true);
            board.data[7, 1] = new Field("b8", false);
            board.data[7, 2] = new Field("c8", true);
            board.data[7, 3] = new Field("d8", false);
            board.data[7, 4] = new Field("e8", true);
            board.data[7, 5] = new Field("f8", false);
            board.data[7, 6] = new Field("g8", true);
            board.data[7, 7] = new Field("h8", false);

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
            // simple move or capture
            var divider = move.Contains("-") ? "-" : "x";
            var items = move.Split(divider);
            var titleFrom = items[0];
            var titleTo = items[1];
            if (!GetField(titleFrom).HasFigure()) return false;
            var figure = GetField(titleFrom).Figure;
            var figureTo = GetField(titleTo).Figure;
            // same color occupation
            if (figureTo != null && figureTo.IsWhitePeace == figure.IsWhitePeace) return false;

            var isCapture = figureTo != null && figureTo.IsWhitePeace != figure.IsWhitePeace;

            var isPossibleMove = MoveProcessor.IsPossibleMove(titleFrom, titleTo, figure, isCapture);
            if (isPossibleMove)
            {
                SetFigure(figure, titleTo);
                GetField(titleFrom).RemoveFigure();
                return true;
            }
            else return false;

        }

        public void SetFigure(Figure figure, string title)
        {
            GetField(title).SetFigure(figure);
        }
    }
}
