namespace Objects
{
    public class Figure
    {
        private readonly FigureType _type;

        public readonly bool IsWhitePeace;

        public Figure(FigureType type, bool isWhitePeace)
        {
            this._type = type;
            this.IsWhitePeace = isWhitePeace;
        }

        public bool IsPawn() => this._type == FigureType.Pawn;

        public bool IsBishop() => this._type == FigureType.Bishop;

        public bool IsRook() => this._type == FigureType.Rook;

        public bool IsQueen() => this._type == FigureType.Queen;

        public bool IsKing() => this._type == FigureType.King;

        public bool IsKnight() => this._type == FigureType.Knight;

        public FigureType GetFigureType() => this._type;

    }

    public enum FigureType
    {
        Pawn,
        Bishop,
        Knight,
        Rook,
        Queen,
        King
    }
}
