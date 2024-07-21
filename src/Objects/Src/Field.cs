namespace Objects
{
    public class Field
    {
        public readonly string Name;

        private readonly bool _isWhite;

        public Figure Figure { get; private set; }

        public Field(string name, bool isWhite, Figure figure = null)
        {
            this._isWhite = isWhite;
            Name = name;
            Figure = figure;
        }

        public void SetFigure(Figure figure)
        {
            this.Figure = figure;
        }

        public void RemoveFigure()
        {
            this.Figure = null;
        }

        public bool HasFigure() { return this.Figure != null; }

        public bool IsWhiteField() { return this._isWhite; }

    }
}
