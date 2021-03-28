using System;
using System.Collections.Generic;
using System.IO;

namespace LangtonsAntSimulatorConsole
{
    public static class FileSimulationSettings
    {
        public static void Write(
            string correspondingImageFileName,
            string correspondingImageFileExtension,
            ref Board board,
            ref Ant ant,
            ref List<Rule> rules,
            UInt32 stepsTarget,
            ref SimulationResult simRes)
        {
            var file = new StreamWriter(
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    $"{ correspondingImageFileName }_settings.txt"
                 )
            );

            file.WriteLine($"Settings for { correspondingImageFileName }.{ correspondingImageFileExtension }");
            file.WriteLine("####################");
            file.WriteLine();
            file.WriteLine("Board:");
            file.WriteLine($"columns = { board.cols }");
            file.WriteLine($"rows = { board.rows }");
            file.WriteLine($"starting colour value = { board.startingColourValue }");
            file.WriteLine();
            file.WriteLine("Ant:");
            file.WriteLine($"starting column = { ant.startingCol }");
            file.WriteLine($"starting row = { ant.startingRow }");
            file.WriteLine($"starting orientation = { ant.startingOrientation }");
            file.WriteLine();
            file.WriteLine("Rules:");
            for (int i = 0; i < rules.Count; i++)
            {
                file.WriteLine($"[{ i + 1 }] colour = { rules[i].colour }, turn direction = { rules[i].turnDirection }");
            }
            file.WriteLine();
            file.WriteLine($"Steps target = { stepsTarget }");
            file.WriteLine($"Steps completed by ant = { simRes.stepsCompletedByAnt }");
            if (simRes.didAntReachBoundary)
                file.WriteLine("The ant did not reach the steps target because it hit a boundary.");

            file.Close();
        }
    }
}