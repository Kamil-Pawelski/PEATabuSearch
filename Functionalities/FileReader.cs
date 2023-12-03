using PEATabuSearch.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEATabuSearch.Functionalities
{
    /// <summary>
    /// Klasa do wczytywania plików .atsp
    /// </summary>
    public class FileReader()
    {
        #region Fields
        private string? _name;
        private string? _type;
        private string? _comment;
        private int _dimension;
        private static readonly char[] separator = [' '];
        #endregion

        #region Methods
        public Matrix ReadFile(string fileName)
        {
            try
            {
                var lines = File.ReadAllLines(fileName);
                _name = lines[0];
                _type = lines[1];
                _comment = lines[2];
                _dimension = Convert.ToInt32(lines[3].Split(": ")[1]); // Dzielimy napis DIMENSION: X (gdzie x to liczba) na dwie i bierzemy tylko liczbe którą zmieniamy na inta
                var matrixData = lines.Skip(7).ToArray(); // Pomijamy pierwsze 7 linijek
                int row = 0;
                int column = 0;
                var fileMatrix = new int[_dimension, _dimension];
                for (int i = 0; i < matrixData.Length && matrixData[i] != "EOF"; i++) //Pętla wypełniająca macierz
                {
                    var lineData = matrixData[i].Split(separator, StringSplitOptions.RemoveEmptyEntries); //Dzielimy po spacjach a StringSplitOptions.RemoveEmptyEntries wyeliminuje nam niepotrzebne ciągi w tablicy

                    foreach (var number in lineData)
                    {
                        fileMatrix[row, column] = Convert.ToInt32(number);
                        column++;

                        if (column == _dimension)
                        {
                            column = 0;
                            row++;
                        }
                    }
                }


                return new Matrix(_dimension, fileMatrix);
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine(exception.Message);
            }

            return new(0, new int[0, 0]);
        }

        /// <summary>
        /// Wypisanie informacji na temat macierzy
        /// </summary>
        public void MatrixFileInfo()
        {

            Console.WriteLine($"{_name}\n{_type}\n{_comment}\nDIMENSION: {_dimension}");
        }
        #endregion
    }
}
