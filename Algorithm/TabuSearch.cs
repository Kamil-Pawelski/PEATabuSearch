using PEATabuSearch.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEATabuSearch.Algorithm
{
    /// <summary>
    /// Klasa wykonująca algorytm tabu search
    /// </summary>
    /// <param name="matrix">macierz reprezentująca graf</param>
    public class TabuSearch(Matrix matrix, double stopTime, int neighbourOption)
    {
        #region Fields
        private double _stopTime = stopTime;
        private readonly Matrix _matrix = matrix;
        private readonly Collection<(int, int)> _tabuList = [];
        private readonly int _option = neighbourOption;
        private readonly int _size = matrix.Size;
        private int[]? _bestSolution;
        private int _solutionCost;
        private readonly int _tabuListSize = 18;
        private readonly Random _random = new();
        #endregion

        #region Properties
        public double StopTime { get { return _stopTime; } set { _stopTime = value; } }
        public Matrix Matrix { get { return _matrix; } }

        #endregion

        public void ExecuteTabuSearch()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var currentSol = GeterateInitialSolution();
            _solutionCost = _matrix.GetCost(currentSol);
            _bestSolution = (int[])currentSol.Clone();
            var stagnantIterationCount = 0;

            while (stopWatch.Elapsed.TotalMilliseconds <= _stopTime * 1000)
            {
                var neighbourFound = FindNeighbour(currentSol);
                if (neighbourFound)
                {
                    var currentSolutionCost = _matrix.GetCost(currentSol);

                    if (currentSolutionCost < _solutionCost)
                    {
                        _solutionCost = currentSolutionCost;
                        _bestSolution = (int[])currentSol.Clone();
                        Console.WriteLine("Found new best solution: " + _solutionCost);
                        stagnantIterationCount = 0;
                    }
                    else
                    {
                        stagnantIterationCount++;
                    }
                }

                if (stagnantIterationCount >= 10 * _matrix.Size)
                {
                    ResetSolution(ref currentSol, ref stagnantIterationCount);
                }
            }

            Console.WriteLine("Solution cost: " + _solutionCost);
            DisplaySolution();
        }

        public void DisplaySolution()
        {
            if (_bestSolution != null)
            {
                foreach (var item in _bestSolution)
                    Console.Write(item + ", ");
                Console.WriteLine();
            }

        }

        private void ResetSolution(ref int[] currentSol, ref int numIterationsNotChanged)
        {
            currentSol = GeterateInitialSolution();
            numIterationsNotChanged = 0;
        }

        private static void Swap2Opt(int[] solution, int i, int j)
        {
            (solution[i], solution[j]) = (solution[j], solution[i]);
        }

        private static void SwapTwoEdge(int[] solution, int i, int j)
        {
            var min = Math.Min(i, j);
            var max = Math.Max(i, j);
            Array.Reverse(solution, min, max - min + 1);
        }

        private static void SwapInesrtion(int[] solution, int i, int j)
        {
            var listSol = solution.ToList();
            int temp = listSol[i];
            listSol.RemoveAt(i);
            listSol.Insert(j, temp);
            Array.Copy(listSol.ToArray(), solution, solution.Length);
        }
        private int[] GeterateInitialSolution()
        {
            var solution = new List<int>();
            var searchSpace = Enumerable.Range(0, _size).ToList();

            var matrix = _matrix.MatrixData;
            var startVertex = _random.Next(0, _size);
            solution.Add(startVertex);
            searchSpace.Remove(startVertex);
            var prevNode = startVertex;

            while (searchSpace.Count != 0)
            {
                var minIdx = -1;
                var minVal = int.MaxValue;

                foreach (var t in searchSpace)
                {
                    var cost = matrix[prevNode, t];
                    if (cost < minVal)
                    {
                        minVal = cost;
                        minIdx = t;
                    }
                }

                if (minIdx == -1) break;
                solution.Add(minIdx);
                prevNode = minIdx;
                searchSpace.Remove(minIdx);
            }

            return [.. solution];
        }

        private bool FindNeighbour(int[] solution)
        {
            var bestLocalCost = int.MaxValue;
            var bestLocalSolution = new int[solution.Length];
            var currSol = new int[solution.Length];
            Array.Copy(solution, currSol, solution.Length);
            var bestSolution = _solutionCost;
            var minVal = 0;

            var tabuPairIndices = (0, 0);
            var solutionFound = false;

            for (var i = 0; i < _size - 1; i++)
            {
                for (var j = i + 1; j < _size; j++)
                {
                    switch (_option)
                    {
                        case 1:
                            Swap2Opt(currSol, i, j);
                            break;
                        case 2:
                            SwapInesrtion(currSol, i, j);
                            break;
                        case 3:
                            SwapTwoEdge(currSol, i, j);
                            break;
                    }

                    var currCost = _matrix.GetCost(currSol);

                    if (bestLocalCost - currCost > minVal)
                    {
                        if (!ContainsTabu(i, j) || currCost < bestSolution)
                        {
                            minVal = bestLocalCost - currCost;
                            bestSolution = currCost;
                            bestLocalCost = currCost;
                            tabuPairIndices = (i, j);
                            Array.Copy(currSol, bestLocalSolution, currSol.Length);
                            solutionFound = true;
                        }
                    }

                    Swap2Opt(currSol, i, j);
                }
            }

            if (!solutionFound) return false;
            AddTabu(tabuPairIndices.Item1, tabuPairIndices.Item2);
            Array.Copy(bestLocalSolution, solution, bestLocalSolution.Length);
            return true;
        }


        private void AddTabu(int val1, int val2)
        {
            _tabuList.Add((val1, val2));
            while (_tabuList.Count > _tabuListSize)
                _tabuList.RemoveAt(0);
        }
        private bool ContainsTabu(int val1, int val2)
        {
            for (var i = 0; i < _tabuList.Count; i++)
                if (_tabuList[i].Item1 == val1 || _tabuList[i].Item1 == val2 || _tabuList[i].Item2 == val1 ||
                    _tabuList[i].Item2 == val2)
                    return true;
            return _tabuList.Contains((val1, val2)) || _tabuList.Contains((val2, val1));
        }


    }
}
