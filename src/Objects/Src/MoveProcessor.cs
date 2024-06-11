using System;

namespace Objects.Src
{
    public class MoveProcessor
    {
        private readonly ChessBoard _board;
        private  bool _isWhiteMove = true;
        private bool _isCheckState = false;

        public MoveProcessor(ChessBoard board)
        {
            _board = board;
        }

        public bool MakeMove(string move)
        {
            // is castle
            if (move.Contains("0"))
            {
                var isProcessed = ProcessCastle(move, _isWhiteMove);
                _isWhiteMove = !_isWhiteMove;
                return isProcessed;
            }

            var divider = move.Contains("-") ? "-" : "x";
            var items = move.Split(divider);
            var titleFrom = items[0];
            var titleTo = items[1];
            if (!_board.GetField(titleFrom).HasFigure()) return false;
            var figure = _board.GetField(titleFrom).Figure;
            var figureTo = _board.GetField(titleTo).Figure;
            // same color occupation
            if (figureTo != null && figureTo.IsWhitePeace == figure.IsWhitePeace) return false;

            var isCapture = figureTo != null && figureTo.IsWhitePeace != figure.IsWhitePeace;

            var isPossibleMove = IsPossibleMove(titleFrom, titleTo, figure, isCapture);
            if (isPossibleMove)
            {
                if (figure.IsPawn() && _board.GetField(titleTo).HasFigure() && titleFrom[0] == titleTo[0]) return false;
                _board.SetFigure(figure, titleTo);
                _board.RemoveFigure(titleFrom);
                _isWhiteMove = !_isWhiteMove;
                return true;
            }
            else return false;

        }

        public bool IsPossibleMove(string titleFrom, string titleTo, Figure figure, bool isCaptureMove)
        {
            var rowName = titleFrom[0];
            var rowMameTo = titleTo[0];
            var indexRowFrom = titleFrom[1] - '0';
            var indexRowTo = titleTo[1] - '0';
            var isSameRowName = rowName == rowMameTo;
            var diff = Math.Abs(indexRowTo - indexRowFrom);
            var rowsDiff = Math.Abs(rowName - rowMameTo);
            var isAdjanced = IsAdjancedTitle(rowName, rowMameTo);

            switch (figure.GetFigureType())
            {
                case FigureType.Pawn:
                    if (!isSameRowName)
                    {
                        if (!isAdjanced) return false;
                        return diff == 1 && isCaptureMove;
                    }
                    
                    if (figure.IsWhitePeace)
                    {
                        if(indexRowTo <= indexRowFrom) return false;
                        if (indexRowFrom == 2)
                        {
                            return indexRowTo <= 4;
                        }
                        else return indexRowTo - indexRowFrom == 1;
                    }
                    else
                    {
                        if (indexRowFrom <= indexRowTo) return false;
                        if (indexRowFrom == 7)
                        {
                            return indexRowTo >= 5;
                        }
                        else return indexRowFrom - indexRowTo == 1;
                    }
                case FigureType.Knight:
                    return rowsDiff == 2 && diff == 1 || rowsDiff == 1 && diff == 2;
                case FigureType.Bishop:
                    return rowsDiff == diff;
                case FigureType.Rook:
                    return rowName == rowMameTo && diff > 0 || rowName != rowMameTo && diff == 0;
                case FigureType.Queen:
                    return rowName == rowMameTo && diff > 0 || rowName != rowMameTo && diff == 0 || rowsDiff == diff;
                case FigureType.King:
                    return rowName == rowMameTo && rowsDiff <= 1 || rowName != rowMameTo && diff <= 1;

            }
            throw new ArgumentException($"Unknown figure '{figure.GetFigureType()}'");
        }

        private static bool IsAdjancedTitle(char title, char titleTo)
        {
            switch (title)
            {
                case 'a':
                    return titleTo == 'b';
                case 'b':
                    return titleTo == 'c' || titleTo == 'a';
                case 'c':
                    return titleTo == 'b' || titleTo == 'd';
                case 'd':
                    return titleTo == 'c' || titleTo == 'e';
                case 'e':
                    return titleTo == 'd' || titleTo == 'f';
                case 'f':
                    return titleTo == 'e' || titleTo == 'g';
                case 'g':
                    return titleTo == 'f' || titleTo == 'h';
            }
            return titleTo == 'g';
        }

        public bool IsCheckState()
        {
            return _isCheckState;
        }

        public bool IsMateState()
        {
            return false;
        }

        private bool ProcessCastle(string move, bool isWhiteMove)
        {
            var items = move.Split("-");
            if (items.Length < 2 || items.Length > 3) throw new ArgumentException($"Unexpected move in castle: {move}");
            // castle king side
            var row = isWhiteMove ? "1" : "8";
            var e = _board.GetField($"e{row}");

            if (items.Length == 2)
            {
                var f = _board.GetField($"f{row}");
                var g = _board.GetField($"g{row}");
                var h = _board.GetField($"h{row}");
                if (!e.HasFigure() || !e.Figure.IsKing()) return false;
                if (!h.HasFigure() || !h.Figure.IsRook()) return false;
                if (g.HasFigure() || f.HasFigure()) return false;
                var rockPeace = h.Figure;
                var kingPeace = e.Figure;
                _board.RemoveFigure($"e{row}");
                _board.RemoveFigure($"h{row}");
                _board.SetFigure(kingPeace, $"g{row}");
                _board.SetFigure(rockPeace, $"f{row}");
                return true;

            }
            // castle queen side

            var d = _board.GetField($"d{row}");
            var c = _board.GetField($"c{row}");
            var b = _board.GetField($"b{row}");
            var a = _board.GetField($"a{row}");
            if (!e.HasFigure() || !e.Figure.IsKing()) return false;
            if (!a.HasFigure() || !a.Figure.IsRook()) return false;
            if (d.HasFigure() || c.HasFigure() || b.HasFigure()) return false;
            var rock = a.Figure;
            var king = e.Figure;
            _board.RemoveFigure($"e{row}");
            _board.RemoveFigure($"a{row}");
            _board.SetFigure(king, $"c{row}");
            _board.SetFigure(rock, $"d{row}");
            return true;
        }

    }
}
