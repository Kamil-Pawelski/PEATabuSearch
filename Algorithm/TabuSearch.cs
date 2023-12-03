using PEATabuSearch.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEATabuSearch.Algorithm
{
    /// <summary>
    /// Klasa wykonująca algorytm tabu search
    /// </summary>
    /// <param name="matrix">macierz reprezentująca graf</param>
    public class TabuSearch(Matrix matrix)
    {
        #region Fields
        private double _stopTime = double.MaxValue;
        private Matrix _matrix = matrix;
        private List<int> _currentTour;
        #endregion

        #region Properties
        public double StopTime { get { return _stopTime; } set { _stopTime = value; } }
        public Matrix Matrix { get { return _matrix; } }
        #endregion

        public void SetStopTime(double stoptime) => StopTime = stoptime;

    }
}
