using System;
using System.IO;

namespace LangtonsAntSimulatorConsole
{
    public static class FilePgm
    {
        private const string magicNumber = "P2";
        private const byte MIN_MAXVAL = 1;

        #region Public methods

        public static void WriteNewAsciiFile(
            string fileName,
            string comment,
            ushort cols,
            ushort rows,
            ushort maxPixelValue,
            ref byte[,] pixelValuesArray)
        {
            var file = new StreamWriter(
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    $"{ fileName }.pgm"
                 )
            );

            if (maxPixelValue <= MIN_MAXVAL) 
                maxPixelValue = MIN_MAXVAL;

            // Header
            file.WriteLine(magicNumber);
            if (comment.Length > 0) file.WriteLine($"# {comment}");
            file.WriteLine($"{cols} {rows}");
            file.WriteLine(maxPixelValue);

            // Pixel values
            for (ushort c = 0; c < cols; c++)
            {
                for (ushort r = 0; r < rows; r++)
                {
                    file.Write($"{pixelValuesArray[c, r]} ");
                }
                file.Write("\n");
            }

            file.Close();
        }

        #endregion
    }
}