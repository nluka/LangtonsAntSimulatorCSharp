namespace LangtonsAntSimulatorConsole
{
    public class Board
    {
        public byte[,] grid;
        public readonly ushort cols;
        public readonly ushort rows;
        public readonly byte startingColourValue;

        #region Constructor

        public Board(ushort cols, ushort rows, byte startingColorValue)
        {
            this.cols = cols;
            this.rows = rows;
            this.startingColourValue = startingColorValue;

            this.grid = new byte[cols, rows];

            FillGridWithColourValue(startingColourValue);
        }

        private void FillGridWithColourValue(byte colourValue)
        {
            for (ushort r = 0; r < rows; r++)
            {
                for (ushort c = 0; c < cols; c++)
                {
                    this.grid[c, r] = this.startingColourValue;
                }
            }
        }

        #endregion
    }
}