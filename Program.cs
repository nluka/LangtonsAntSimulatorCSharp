using System;
using System.Collections.Generic;

namespace LangtonsAntSimulatorConsole
{
    internal class Program
    {
        #region Constants

        const ushort MIN_BOARD_COLS = 1;
        const ushort MAX_BOARD_COLS = ushort.MaxValue;

        const ushort MIN_BOARD_ROWS = 1;
        const ushort MAX_BOARD_ROWS = ushort.MaxValue;

        const UInt32 MIN_STEPS = UInt32.MinValue;
        const UInt32 MAX_STEPS = UInt32.MaxValue;

        const byte MIN_COLOUR_VALUE = byte.MinValue;
        const byte MAX_COLOUR_VALUE = byte.MaxValue;

        const byte MIN_FILENAME_LENGTH = byte.MinValue;
        const byte MAX_FILENAME_LENGTH = 20;
        const string DEFAULT_FILENAME = "AntSimulation";

        #endregion

        #region Private members

        private static Board board;
        private static Ant ant;
        private static List<Rule> rules;
        private static SimulationResult simulationResult;
        private static string outputFileName;

        // User inputted values
        private static ushort userInputBoardCols, userInputBoardRows;
        private static byte userInputBoardStartingColourValue;
        private static ushort userInputAntStartingCol, userInputAntStartingRow;
        private static byte userInputStartingOrientationNumber;
        private static Ant.Direction startingOrientation;
        private static UInt32 userInputStepsTarget;
        private static string userInputOutputFileName;

        #endregion

        #region Private methods

        private static void Main(string[] args)
        {
            SetupBoardDimensions();

            SetupRules();

            SetupAnt();

            SetupBoardColour();

            SetupStepsTarget();

            PrepareSimulation();

            SetupFiles();

            ReviewSimulationSettings();

            RunSimulation();

            WritePgmFile();

            WriteSimulationSettingsFile();
        }

        private static void SetupBoardDimensions()
        {
            Menu.PrintBoardDimensionsHeader();

            Menu.PrintBoardWidthPrompt(MIN_BOARD_COLS, MAX_BOARD_COLS);
            userInputBoardCols = Menu.GetUShortValueFromUserBetween(MIN_BOARD_COLS, MAX_BOARD_COLS);

            Menu.PrintBoardHeightPrompt(MIN_BOARD_ROWS, MAX_BOARD_ROWS);
            userInputBoardRows = Menu.GetUShortValueFromUserBetween(MIN_BOARD_ROWS, MAX_BOARD_ROWS);

            Console.WriteLine();
        }

        private static void SetupRules()
        {
            string userChoice = "";
            rules = new List<Rule>();

            do
            {
                byte userInputColour;
                bool doesInputtedColourAlreadyExistInRules = false;

                Menu.PrintRulesSettingsHeader();

                Menu.PrintRuleColorPrompt(MIN_COLOUR_VALUE, MAX_COLOUR_VALUE, ref rules);
                userInputColour = Menu.GetByteValueFromUserBetween(byte.MinValue, byte.MaxValue);

                #region Colour validity verification

                foreach (Rule r in rules)
                {
                    if (r.colour == userInputColour)
                        doesInputtedColourAlreadyExistInRules = true;
                }

                if (doesInputtedColourAlreadyExistInRules)
                {
                    Console.WriteLine("That colour already exists.");
                    continue;
                }

                #endregion

                Menu.PrintRuleTurnDirectionPrompt(ref rules);
                var userInputTurnDirection = Rule.ConvertCharToTurnDirection(Menu.GetRuleTurnDirectionFromUser());

                rules.Add(new Rule(userInputColour, userInputTurnDirection));

                Menu.PrintCurrentRules(ref rules);

                Console.Write("Would you like to add another rule? (y/n): ");
                userChoice = Console.ReadLine();

                Console.WriteLine();
            }
            while (userChoice[0] != 'n' && userChoice[0] != 'N');
        }

        private static void SetupAnt()
        {
            Menu.PrintAntStartingColumnPrompt(MIN_BOARD_COLS, userInputBoardCols);
            userInputAntStartingCol = Menu.GetUShortValueFromUserBetween(MIN_BOARD_COLS, userInputBoardCols);

            Menu.PrintAntStartingRowPrompt(MIN_BOARD_ROWS, userInputBoardRows);
            userInputAntStartingRow = Menu.GetUShortValueFromUserBetween(MIN_BOARD_ROWS, userInputBoardRows);

            Menu.PrintAntOrientationPrompt();
            userInputStartingOrientationNumber = Menu.GetByteValueFromUserBetween(1, 4);

            Console.WriteLine();

            switch (userInputStartingOrientationNumber)
            {
                default:
                case 1:
                    startingOrientation = Ant.Direction.North;
                    break;
                case 2:
                    startingOrientation = Ant.Direction.East;
                    break;
                case 3:
                    startingOrientation = Ant.Direction.South;
                    break;
                case 4:
                    startingOrientation = Ant.Direction.West;
                    break;
            }
        }

        private static void SetupBoardColour()
        {
            bool doesInputtedColourExistInRules = false;

            Menu.PrintBoardColourHeader();

            Menu.PrintBoardColourPrompt(MIN_COLOUR_VALUE, MAX_COLOUR_VALUE, ref rules);

            #region Get valid colour from user

            do
            {
                userInputBoardStartingColourValue = Menu.GetByteValueFromUserBetween(byte.MinValue, byte.MaxValue);
                foreach (Rule rule in rules)
                {
                    if (userInputBoardStartingColourValue == rule.colour)
                    {
                        doesInputtedColourExistInRules = true;
                        break;
                    }
                }
                if (!doesInputtedColourExistInRules)
                    Console.Write("That value is not valid. Please enter a new value: ");
            }
            while (!doesInputtedColourExistInRules);

            #endregion

            Console.WriteLine();
        }

        private static void SetupStepsTarget()
        {
            Menu.PrintSimulationSettingsHeader();

            Menu.PrintSimulationStepsTarget(MIN_STEPS, MAX_STEPS);
            userInputStepsTarget = Menu.GetUInt32ValueFromUserBetween(MIN_STEPS, MAX_STEPS);

            Console.WriteLine();
        }

        private static void SetupFiles()
        {
            Menu.PrintFileSettingsHeader();

            Menu.PrintFileNamePrompt();
            userInputOutputFileName = Menu.GetStringFromUser(MIN_FILENAME_LENGTH, MAX_FILENAME_LENGTH);

            if (userInputOutputFileName.Length <= 0)
                userInputOutputFileName = DEFAULT_FILENAME;

            outputFileName = userInputOutputFileName;

            Console.WriteLine();
        }

        private static void PrepareSimulation()
        {
            Console.WriteLine("*** PREPARING SIMULATION ***\n");

            #region Create board

            Console.Write("Filling board... ");

            board = new Board(
                userInputBoardCols,
                userInputBoardRows,
                userInputBoardStartingColourValue
            );
            
            Console.WriteLine("done.");
            Console.WriteLine();

            #endregion

            #region Create ant

            Console.Write("Creating ant... ");

            ant = new Ant(
                userInputAntStartingCol,
                userInputAntStartingRow,
                startingOrientation,
                ref board,
                board.cols,
                board.rows,
                rules
            );

            Console.WriteLine("done.");
            Console.WriteLine();

            #endregion
        }

        private static void ReviewSimulationSettings()
        {
            Menu.PrintReviewSimulationSettingsHeader();

            Menu.PrintBoardSettings(ref board);

            Console.WriteLine();

            Menu.PrintAntSettings(ref ant);

            Menu.PrintCurrentRules(ref rules);

            Menu.PrintStepsTargetSettings(userInputStepsTarget);

            Console.WriteLine();

            Console.Write("Press enter to begin the simulation... ");
            Console.ReadLine();
            Console.WriteLine();
        }

        private static void RunSimulation()
        {
            Console.Write("Simulating... ");
            System.Diagnostics.Stopwatch simulationStopwatch = System.Diagnostics.Stopwatch.StartNew();

            simulationResult = ant.Simulate(userInputStepsTarget);

            if (simulationResult.didAntReachBoundary)
                Console.Write("ant reached a boundary, ending simulation. ");
            else
                Console.Write("done. ");

            Console.WriteLine($"The ant took { simulationResult.stepsCompletedByAnt } steps.");

            simulationStopwatch.Stop();
            Console.WriteLine($"Simulation time: { Math.Round((float)(simulationStopwatch.ElapsedMilliseconds / 1000)) } seconds");
            Console.WriteLine();
        }

        private static void WritePgmFile()
        {
            System.Diagnostics.Stopwatch filewriteStopwatch = System.Diagnostics.Stopwatch.StartNew();
            Console.Write("Writing PGM file... ");

            byte maxPixelValue = byte.MinValue;

            for (int i = 0; i < rules.Count; i++)
            {
                if (rules[i].colour > maxPixelValue)
                    maxPixelValue = rules[i].colour;
            }

            FilePgm.WriteNewAsciiFile(
                outputFileName,
                "", // comment
                board.cols,
                board.rows,
                maxPixelValue,
                ref board.grid
            );

            Console.WriteLine("done");
            filewriteStopwatch.Stop();

            Console.WriteLine($"Filewrite time: { Math.Round((float)(filewriteStopwatch.ElapsedMilliseconds / 1000)) } seconds");
            Console.WriteLine();
        }

        private static void WriteSimulationSettingsFile()
        {
            FileSimulationSettings.Write(
                outputFileName,
                "pgm",
                ref board,
                ref ant,
                ref rules,
                userInputStepsTarget,
                ref simulationResult
            );
        }

        #endregion
    }
}