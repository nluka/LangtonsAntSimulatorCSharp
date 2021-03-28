using System;

namespace LangtonsAntSimulatorConsole
{
    public class SimulationResult
    {
        public readonly UInt32 stepsCompletedByAnt;
        public readonly bool didAntReachBoundary;

        public SimulationResult(
            UInt32 stepsCompletedByAnt, 
            bool didAntReachBoundary
        )
        {
            this.stepsCompletedByAnt = stepsCompletedByAnt;
            this.didAntReachBoundary = didAntReachBoundary;
        }
    }
}