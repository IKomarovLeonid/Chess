using System;

namespace Objects.Src
{
    internal class MoveProcessor
    {
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

            switch (figure.Type)
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
                    
            }

            return false;
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

    }
}
