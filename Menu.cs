using System;
using System.Collections.Generic;

namespace LangtonsAntSimulatorConsole
{
    public static class Menu
    {
        #region User input methods

        public static byte GetByteValueFromUserBetween(byte min, byte max)
        {
            byte userInput;
            bool isInputValid = false;

            do
            {
                ulong temp;

                try
                {
                    temp = Convert.ToByte(
                        Console.ReadLine()
                    );
                    userInput = (byte)temp;
                }
                catch (Exception)
                {
                    // Set invalid value for userInput
                    userInput = (byte)(min - 1);
                }

                if (userInput >= min && userInput <= max)
                    isInputValid = true;
                else
                    Console.Write("That value is not valid. Please enter a new value: ");
            }
            while (!isInputValid);

            return userInput;
        }

        public static ushort GetUShortValueFromUserBetween(ushort min, ushort max)
        {
            ushort userInput;
            bool isInputValid = false;

            do
            {
                ushort temp;

                try
                {
                    temp = Convert.ToUInt16(
                        Console.ReadLine()
                    );
                    userInput = temp;
                }
                catch (Exception)
                {
                    // Set invalid value for userInput
                    userInput = (ushort)(min - 1);
                }

                if (userInput >= min && userInput <= max)
                    isInputValid = true;
                else
                    Console.Write("That value is not valid. Please enter a new value: ");
            }
            while (!isInputValid);

            return userInput;
        }

        public static UInt32 GetUInt32ValueFromUserBetween(UInt32 min, UInt32 max)
        {
            UInt32 userInput;
            bool isInputValid = false;

            do
            {
                try
                {
                    userInput = Convert.ToUInt32(
                        Console.ReadLine()
                    );
                }
                catch (Exception)
                {
                    userInput = (UInt32)(min - 1);
                }

                if (userInput >= min && userInput <= max)
                    isInputValid = true;
                else
                    Console.Write("That value is not valid. Please enter a new value: ");
            }
            while (!isInputValid);

            return userInput;
        }

        public static char GetRuleTurnDirectionFromUser()
        {
            string userInput = "";
            bool isInputValid = false;

            do
            {
                try
                {
                    userInput = Console.ReadLine();
                    if (userInput[0] == 'r' || userInput[0] == 'l')
                        isInputValid = true;
                }
                catch (Exception) { }

                if (!isInputValid)
                {
                    Console.Write("That value is not valid. Please enter a new value: ");
                }
            }
            while (!isInputValid);

            return userInput[0];
        }

        public static string GetStringFromUser(byte min, byte max)
        {
            string userInput;

            do
            {
                userInput = Console.ReadLine();
            }
            while (userInput.Length < min || userInput.Length > max);

            return userInput;
        }

        #endregion

        #region Board headers and prompts methods

        public static void PrintBoardDimensionsHeader()
        {
            Console.WriteLine("*** BOARD DIMENSIONS ***");
            Console.WriteLine();
            Console.WriteLine("Note: large boards require more RAM, disk space, and will take longer to process.");
            Console.WriteLine();
        }

        public static void PrintBoardWidthPrompt(ushort min, ushort max)
        {
            Console.Write($"Enter the width of the board ({ min }-{ max } columns): ");
        }

        public static void PrintBoardHeightPrompt(ushort min, ushort max)
        {
            Console.Write($"Enter the height of the board ({ min }-{ max } rows): ");
        }

        public static void PrintBoardColourHeader()
        {
            Console.WriteLine("*** BOARD COLOUR ***");
            Console.WriteLine();
        }

        public static void PrintBoardColourPrompt(byte min, byte max, ref List<Rule> rules)
        {
            Console.Write("Enter the starting colour of the board (");
            for (int i = 0; i < rules.Count; i++)
            {
                Console.Write(rules[i].colour);
                if (i != rules.Count - 1)
                    Console.Write("|");
            }
            Console.Write("): ");
        }

        #endregion

        #region Rules headers and prompts methods

        public static void PrintRulesSettingsHeader()
        {
            Console.WriteLine("*** RULES ***");
            Console.WriteLine();
        }

        public static void PrintRuleColorPrompt(byte min, byte max, ref List<Rule> rules)
        {
            Console.Write("Enter the 8-bit grayscale colour value (0-255");
            if (rules.Count > 0)
            {
                Console.Write(", ");
                for (int i = 0; i < rules.Count; i++)
                {
                    Console.Write($"!{ rules[i].colour }");
                    if (i == rules.Count - 1)
                        break;
                    Console.Write(", ");
                }
            }
            Console.Write($") for rule { rules.Count + 1 }: ");
        }

        public static void PrintRuleTurnDirectionPrompt(ref List<Rule> rules)
        {
            Console.Write($"Enter the turn direction for rule { rules.Count + 1 } (l/r): ");
        }

        public static void PrintCurrentRules(ref List<Rule> rules)
        {
            Console.WriteLine();
            Console.WriteLine("Rules:");
            for (int i = 0; i < rules.Count; i++)
            {
                Console.WriteLine($"[{ i + 1 }] colour = { rules[i].colour }, turn direction = { rules[i].turnDirection }");
            }
            Console.WriteLine();
        }

        #endregion

        #region Ant headers and prompts methods

        public static void PrintAntSettingsHeader()
        {
            Console.WriteLine("*** ANT SETTINGS ***");
            Console.WriteLine();
        }

        public static void PrintAntStartingColumnPrompt(ushort min, ushort max)
        {
            Console.Write($"Enter the starting column of the ant ({ min }-{ max }): ");
        }

        public static void PrintAntStartingRowPrompt(ushort min, ushort max)
        {
            Console.Write($"Enter the starting row of the ant ({ min }-{ max }): ");
        }

        public static void PrintAntOrientationPrompt()
        {
            Console.Write($"Enter the starting orientation of the ant (1:North|2:East|3:South|4:West): ");
        }

        #endregion

        #region Simulation header and prompts methods

        public static void PrintSimulationSettingsHeader()
        {
            Console.WriteLine("*** SIMULATION SETTINGS ***");
            Console.WriteLine();
        }

        public static void PrintSimulationStepsTarget(UInt32 min, UInt32 max)
        {
            Console.Write($"Enter the target number of steps ({ min }-{ max }): ");
        }

        #endregion

        #region File setup header and prompts methods

        public static void PrintFileSettingsHeader()
        {
            Console.WriteLine("*** FILE SETUP ***");
            Console.WriteLine();
        }

        public static void PrintFileNamePrompt()
        {
            Console.Write($"Enter the name of the output file (1-20 chars, defaults to 'AntSimulation'): ");
        }

        #endregion

        #region Simulation review methods

        public static void PrintReviewSimulationSettingsHeader()
        {
            Console.WriteLine("*** REVIEW SETTINGS ***");
            Console.WriteLine();
        }

        public static void PrintBoardSettings(ref Board board)
        {
            Console.WriteLine("Board:");
            Console.WriteLine($"Width = { board.cols } columns");
            Console.WriteLine($"Height = { board.rows } rows");
            Console.WriteLine($"Starting colour = { board.startingColourValue }");
        }

        public static void PrintAntSettings(ref Ant ant)
        {
            Console.WriteLine("Ant:");
            Console.WriteLine($"Starting column = { ant.startingCol }");
            Console.WriteLine($"Starting row = { ant.startingRow }");
            Console.WriteLine($"Starting orientation = { ant.startingOrientation }");
        }

        public static void PrintStepsTargetSettings(UInt32 stepsTarget)
        {
            Console.WriteLine($"Target steps = { stepsTarget }");
        }

        #endregion
    }
}