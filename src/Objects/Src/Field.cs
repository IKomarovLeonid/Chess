namespace Objects.Src
{
    public class Field
    {
        public readonly string Name;

        public readonly bool IsWhite;

        public Figure Figure { get; private set; }

        public Field(string name, bool isWhite, Figure figure = null)
        {
            IsWhite = isWhite;
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

    }
}
