using System;
using System.Linq;

namespace Objects.Src
{
    public class MoveProcessor
    {
        private MoveProcessor()
        {

        }

        public bool MakeMove(string move)
        {
            if (!IsValidMove(move)) return false;

            return true;
        }


        private bool IsCastle(string move) => move.Contains("0");

        private bool IsValidMove(string move) => move.Count(t => t == '-') == 1;

        public static bool IsPossibleMove(string titleFrom, string titleTo, Figure figure, bool isCaptureMove)
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

        public static bool IsCheckState(ChessBoard board)
        {
            return false;
        }

        public static bool IsMateState(ChessBoard board)
        {
            return false;
        }

    }
}
