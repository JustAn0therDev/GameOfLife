using System.Threading;
using System;

namespace GameOfLife {
    internal class Simulation {

        #region Private Properties

        private const int TIME_TO_WAIT_IN_MILLISECONDS = 500;
        private int[][] _population { get; }

        #endregion 

        #region Constructors

        internal Simulation(int[][] population) => _population = population;

        #endregion

        #region Public Methods

        //TODO: Parameter to shift all cell states and keep shifting them if the same state appears two or three times.
        internal void Start(bool infiniteMoments = false) {
            if (infiniteMoments) {
                while (true) {
                    RunMoment();
                }
            } else {
                RunMoment();
            }
        }

        #endregion

        #region Private Methods

        private void RunMoment() {
            Thread.Sleep(TIME_TO_WAIT_IN_MILLISECONDS);
            PrintPopulation();
            CheckEachCellToDieOrLive();
            Thread.Sleep(TIME_TO_WAIT_IN_MILLISECONDS);
            Console.Clear();
        }

        private void CheckEachCellToDieOrLive() {
            for (int i = 0; i < _population.Length; i++) {
                for (int j = 0; j < _population[i].Length; j++) {
                    ChangeCurrentCell(ref i, ref j);
                }
            }
        }

        private void ChangeCurrentCell(ref int i, ref int j) {
            int livingNeighbors = 0;
                if (i == 0) {
                    if (j == 0) {
                        livingNeighbors += _population[i + 1][j];
                        livingNeighbors += _population[i][j + 1];
                    } else if (j == _population.Length - 1) {
                        livingNeighbors += _population[i + 1][j];
                        livingNeighbors += _population[i][j - 1];
                    } else {
                        livingNeighbors += _population[i][j + 1];
                        livingNeighbors += _population[i][j - 1];
                        livingNeighbors += _population[i + 1][j + 1];
                    }
                } else if (i == _population.Length - 1) {
                    if (j == 0) {
                        livingNeighbors += _population[i - 1][j];
                        livingNeighbors += _population[i][j + 1];
                    } else if (j == _population.Length - 1) {
                        livingNeighbors += _population[i - 1][j];
                        livingNeighbors += _population[i][j - 1];
                    } else {
                        livingNeighbors += _population[i][j + 1];
                        livingNeighbors += _population[i][j - 1];
                        livingNeighbors += _population[i - 1][j + 1];
                    }
                } else {
                    if (j == 0) {
                        livingNeighbors += _population[i - 1][j];
                        livingNeighbors += _population[i + 1][j];
                        livingNeighbors += _population[i][j + 1];
                    } else if (j == _population.Length - 1) {
                        livingNeighbors += _population[i - 1][j];
                        livingNeighbors += _population[i][j - 1];
                        livingNeighbors += _population[i + 1][j];
                    } else {
                        livingNeighbors += _population[i][j + 1];
                        livingNeighbors += _population[i][j - 1];
                        livingNeighbors += _population[i - 1][j];
                        livingNeighbors += _population[i + 1][j];
                    }
                }

            CheckRules(ref _population[i][j], ref livingNeighbors);
        }

        private void CheckRules(ref int cell, ref int livingNeighbors) {
            if (cell == 1 && livingNeighbors < 2) {
                Kill(ref cell);
            } else if (cell == 1 && (livingNeighbors == 2 || livingNeighbors == 3)) {
                GiveLife(ref cell);
            } else if (cell == 1 && livingNeighbors > 3) {
                Kill(ref cell);
            } else if (cell == 0 && livingNeighbors == 3) {
                GiveLife(ref cell);
            }
        }

        private void Kill(ref int cell) => cell = 0;
        private void GiveLife(ref int cell) => cell = 1;

        private void PrintPopulation() {
            for (int i = 0; i < _population.Length; i++) {
                Console.WriteLine(string.Join(' ', _population[i]));
            }
        }

        #endregion
    }
}