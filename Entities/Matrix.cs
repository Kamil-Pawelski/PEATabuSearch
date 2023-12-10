namespace PEATabuSearch.Entities
{
    /// <summary>
    /// Klasa reprezentuja graf w postaci macierzy. 
    /// </summary>
    /// <param name="size">Rozmiar macierzy</param>
    /// <param name="matrix">Macierz</param>
    public class Matrix(int size, int[,] matrix) : ICloneable
    {
        #region Fields
        private int[,] _matrix = matrix;
        private readonly int _size = size;
        #endregion

        #region Properties
        /// <summary>
        /// Właściwość zwracająca lub ustawiająca macierz
        /// </summary>
        public int[,] MatrixData
        {
            get => _matrix;
            set => _matrix = value ?? _matrix;
        }
        /// <summary>
        /// Właściwość zwracająca rozmiar
        /// </summary>
        public int Size
        {
            get => _size;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Klonuje macierz do innej
        /// </summary>
        /// <returns>Zwraca skopiowana macierz</returns>
        public object Clone()
        {
            Matrix clonedMatrix = new(_size, new int[_size, _size]);

            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    clonedMatrix.MatrixData[i, j] = _matrix[i, j];
                }
            }

            return clonedMatrix;
        }


        /// <summary>
        /// Metoda zwracająca odległość między punktami a i b.
        /// </summary>
        /// <param name="a">Punkt a</param>
        /// <param name="b">Punkt b</param>
        /// <returns></returns>
        public int GetWeight(int a, int b) => _matrix[a, b];

        /// <summary>
        /// Wypisanie macierzy
        /// </summary>
        public void Print()
        {
            for (var i = 0; i < _size; i++)
            {
                for (var j = 0; j < _size; j++) Console.Write(_matrix[i, j] + " ");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="solution"></param>
        /// <returns></returns>
        public int GetCost(int[] solution)
        {
            var cost = solution.Select((t, i) => GetWeight(t, solution[(i + 1) % solution.Length])).Sum();
            return cost;
        }

        #endregion

    }
}