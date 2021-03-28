namespace LangtonsAntSimulatorConsole
{
    public class Rule
    {
        public enum TurnDirection
        {
            Left = 0, Right
        }

        public readonly byte colour;
        public readonly TurnDirection turnDirection;

        #region Constructor

        public Rule(byte colour, TurnDirection turnDirection)
        {
            this.colour = colour;
            this.turnDirection = turnDirection;
        }

        #endregion

        public static TurnDirection ConvertCharToTurnDirection(char c)
        {
            if (c == 'l' || c == 'L') 
                return TurnDirection.Left;

            return TurnDirection.Right;
        }
    }
}