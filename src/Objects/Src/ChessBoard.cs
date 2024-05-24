namespace Objects.Src
{
    public class ChessBoard
    {
        public Field[,] data;

        private ChessBoard() { 
            data = new Field[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    data[i,j] = new Field(i, j);
                }
            }
        }

        public Field GetField(int x, int y)
        {
            return data[x,y];
        }

        public static ChessBoard CreateInitial()
        {
            return new ChessBoard();
        }
    }
}
