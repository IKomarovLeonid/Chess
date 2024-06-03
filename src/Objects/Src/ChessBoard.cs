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
            var board = CreateOnlyBoard();

            board.GetField("a1").SetFigure(new Figure(FigureType.Rock, true));
            board.GetField("b1").SetFigure(new Figure(FigureType.Knight, true));
            board.GetField("c1").SetFigure(new Figure(FigureType.Bishop, true));
            board.GetField("d1").SetFigure(new Figure(FigureType.Queen, true));
            board.GetField("e1").SetFigure(new Figure(FigureType.King, true));
            board.GetField("f1").SetFigure(new Figure(FigureType.Bishop, true));
            board.GetField("g1").SetFigure(new Figure(FigureType.Knight, true));
            board.GetField("h1").SetFigure(new Figure(FigureType.Rock, true));

            board.GetField("a2").SetFigure(new Figure(FigureType.Pawn, true));
            board.GetField("b2").SetFigure(new Figure(FigureType.Pawn, true));
            board.GetField("c2").SetFigure(new Figure(FigureType.Pawn, true));
            board.GetField("d2").SetFigure(new Figure(FigureType.Pawn, true));
            board.GetField("e2").SetFigure(new Figure(FigureType.Pawn, true));
            board.GetField("f2").SetFigure(new Figure(FigureType.Pawn, true));
            board.GetField("g2").SetFigure(new Figure(FigureType.Pawn, true));
            board.GetField("h2").SetFigure(new Figure(FigureType.Pawn, true));

            board.GetField("a7").SetFigure(new Figure(FigureType.Pawn, false));
            board.GetField("b7").SetFigure(new Figure(FigureType.Pawn, false));
            board.GetField("c7").SetFigure(new Figure(FigureType.Pawn, false));
            board.GetField("d7").SetFigure(new Figure(FigureType.Pawn, false));
            board.GetField("e7").SetFigure(new Figure(FigureType.Pawn, false));
            board.GetField("f7").SetFigure(new Figure(FigureType.Pawn, false));
            board.GetField("g7").SetFigure(new Figure(FigureType.Pawn, false));
            board.GetField("h7").SetFigure(new Figure(FigureType.Pawn, false));


            board.GetField("a8").SetFigure(new Figure(FigureType.Rock, false));
            board.GetField("b8").SetFigure(new Figure(FigureType.Knight, false));
            board.GetField("c8").SetFigure(new Figure(FigureType.Bishop, false));
            board.GetField("d8").SetFigure(new Figure(FigureType.Queen, false));
            board.GetField("e8").SetFigure(new Figure(FigureType.King, false));
            board.GetField("f8").SetFigure(new Figure(FigureType.Bishop, false));
            board.GetField("g8").SetFigure(new Figure(FigureType.Knight, false));
            board.GetField("h8").SetFigure(new Figure(FigureType.Rock, false));

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
            if (move.Contains("0")) return this.ProcessCastle(move, true);

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
                RemoveFigure(titleFrom);
                return true;
            }
            else return false;

        }

        public void SetFigure(Figure figure, string title)
        {
            GetField(title).SetFigure(figure);
        }

        public void RemoveFigure(string title)
        {
            if(GetField(title).HasFigure()) GetField(title).RemoveFigure();
        }

        private bool ProcessCastle(string move, bool isWhiteMove)
        {
            var items = move.Split("-");
            if (items.Length < 2 || items.Length > 3) throw new ArgumentException($"Unexpected move in castle: {move}");
            // castle king side
            var row = isWhiteMove ? "1" : "8";
            var e = GetField($"e{row}");

            if (items.Length == 2)
            {
                var f = GetField($"f{row}");
                var g = GetField($"g{row}");
                var h = GetField($"h{row}");
                if (!e.HasFigure() || !e.Figure.IsKing()) return false;
                if (!h.HasFigure() || !h.Figure.IsRock()) return false;
                if (g.HasFigure() || f.HasFigure()) return false;
                var rockPeace = h.Figure;
                var kingPeace = e.Figure;
                RemoveFigure($"e{row}");
                RemoveFigure($"h{row}");
                SetFigure(kingPeace,$"g{row}");
                SetFigure(rockPeace, $"f{row}");
                return true;

            }
            // castle queen side

            var d = GetField($"d{row}");
            var c = GetField($"c{row}");
            var b = GetField($"b{row}");
            var a = GetField($"a{row}");
            if (!e.HasFigure() || !e.Figure.IsKing()) return false;
            if (!a.HasFigure() || !a.Figure.IsRock()) return false;
            if (d.HasFigure() || c.HasFigure() || b.HasFigure()) return false;
            var rock = a.Figure;
            var king = e.Figure;
            RemoveFigure($"e{row}");
            RemoveFigure($"a{row}");
            SetFigure(king, $"c{row}");
            SetFigure(rock, $"d{row}");
            return true;
        }
    }
}
