using System;
using System.Collections.Generic;

namespace LangtonsAntSimulatorConsole
{
    public class Ant
    {
        public enum Direction
        {
            North = 0, East, South, West
        }

        #region Public members

        public readonly ushort startingCol;
        public readonly ushort startingRow;
        public readonly Direction startingOrientation;

        #endregion

        #region Private members

        private ushort col;
        private ushort row;
        private Direction orientation;
        private Board board;
        private ushort boardCols;
        private ushort boardRows;
        private List<Rule> rules;
        private UInt32 stepsCompleted;

        #endregion

        #region Constructor

        public Ant(
            ushort startingX,
            ushort startingY,
            Direction startingOrientation,
            ref Board board,
            ushort boardCols,
            ushort boardRows,
            List<Rule> rules)
        {
            col = startingCol = startingX;
            row = startingRow = startingY;
            orientation = this.startingOrientation = startingOrientation;
            this.board = board;
            this.boardCols = boardCols;
            this.boardRows = boardRows;
            this.rules = rules;
        }

        #endregion

        #region Public methods

        public SimulationResult Simulate(UInt32 stepsTarget)
        {
            bool isAntAtBoundary = false;

            for (UInt32 i = 0; i < stepsTarget; i++)
            {
                isAntAtBoundary = TakeStep();
                if (isAntAtBoundary) break;
                this.stepsCompleted++;
            }

            return new SimulationResult(
                this.stepsCompleted,
                isAntAtBoundary
            );
        }

        #endregion

        #region Private methods

        private bool TakeStep()
        {
            ushort nextCol = this.col, nextRow = this.row;
            byte currentTileColour = board.grid[this.col, this.row];
            int currentRuleIndex;
            int nextRuleIndex = 0;

            for (currentRuleIndex = 0; currentRuleIndex < rules.Count; currentRuleIndex++)
            {
                if (rules[currentRuleIndex].colour == currentTileColour)
                {
                    if (currentRuleIndex < rules.Count - 1) // Next index exists
                        nextRuleIndex = currentRuleIndex + 1;
                    else
                        nextRuleIndex = 0;

                    break;
                }
            }

            // Turn
            if (rules[currentRuleIndex].turnDirection == Rule.TurnDirection.Left)
                TurnLeft();
            else
                TurnRight();

            // Find coords of next tile
            switch (orientation)
            {
                case Direction.North:
                    nextRow--;
                    break;
                case Direction.East:
                    nextCol++;
                    break;
                case Direction.South:
                    nextRow++;
                    break;
                case Direction.West:
                    nextCol--;
                    break;
            }

            // Return true if next tile is off the board (therefore the ant is at a boundary)
            if (nextCol >= boardCols || nextCol <= 0 || nextRow >= boardRows || nextRow <= 0)
                return true;

            // Change colour of current grid tile
            board.grid[this.col, this.row] = rules[nextRuleIndex].colour;

            // Move to next grid tile
            this.col = nextCol;
            this.row = nextRow;

            return false;
        }

        private void TurnRight()
        {
            if (orientation < Direction.West)
            {
                orientation++;
                return;
            }
            orientation = Direction.North;
        }

        private void TurnLeft()
        {
            if (orientation > Direction.North)
            {
                orientation--;
                return;
            }
            orientation = Direction.West;
        }

        #endregion
    }
}