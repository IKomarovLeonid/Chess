namespace Objects.Src
{
    public class Figure
    {
        private readonly FigureType Type;

        public readonly bool IsWhitePeace;

        public string CurrentField { get; private set; }

        public Figure(FigureType type, bool isWhitePeace)
        {
            this.Type = type;
            this.IsWhitePeace = isWhitePeace;
        }

        public bool IsPawn() => this.Type == FigureType.Pawn;

        public bool IsBishop() => this.Type == FigureType.Bishop;

        public bool IsRook() => this.Type == FigureType.Rook;

        public bool IsQueen() => this.Type == FigureType.Queen;

        public bool IsKing() => this.Type == FigureType.King;

        public bool IsKnight() => this.Type == FigureType.Knight;

        public FigureType GetFigureType() => this.Type;

        public void SetField(string field) { this.CurrentField = field; }

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
