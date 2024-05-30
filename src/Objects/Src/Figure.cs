namespace Objects.Src
{
    public class Figure
    {
        public readonly FigureType Type;

        public readonly bool IsWhitePeace;

        public Figure(FigureType type, bool isWhitePeace)
        {
            this.Type = type;
            this.IsWhitePeace = isWhitePeace;
        }
    }

    public enum FigureType
    {
        Pawn,
        Bishop,
        Knight,
        Rock,
        Queen,
        King
    }
}
