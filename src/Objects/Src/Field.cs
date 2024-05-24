namespace Objects.Src
{
    public class Field
    {
        public int CoordinateX { get; private set; }

        public int CoordinateY { get; private set; }

        public FigureType? Figure { get; private set; }

        public readonly bool IsWhite;

        public Field(int coordinateX, int coordinateY)
        {
            CoordinateX = coordinateX;
            CoordinateY = coordinateY;
            IsWhite = coordinateX + coordinateY % 2 == 0;
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
