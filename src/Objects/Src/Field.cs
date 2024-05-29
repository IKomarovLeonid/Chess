namespace Objects.Src
{
    public class Field
    {
        public readonly string Name;

        public readonly bool IsWhite;

        public FigureType? Figure { get; private set; }

        public Field(string name, bool isWhite, FigureType? figure = null)
        {
            IsWhite = isWhite;
            Name = name;
            Figure = figure;
        }

        public void SetFigure(FigureType figure)
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
