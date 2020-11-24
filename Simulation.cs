using System.Threading;
using System;

namespace GameOfLife {
    internal class Simulation {

        #region Private Properties

        private const int TIME_TO_WAIT_IN_MILLISECONDS = 500;
        private int[][] _population { get; }
        private int[][] _state { get; set; }
        private int _stateIdentifiedCount { get; set; }

        #endregion 

        #region Constructors

        internal Simulation(int[][] population) => _population = population;

        #endregion

        #region Public Methods

        internal void Start() {
            _state = _population;
            while (true) {
                if (PreviousPopulationStateAndCurrentAreEqual()) {
                    ++_stateIdentifiedCount;
                }
                
                if (_stateIdentifiedCount > 2) {
                    ShiftCells();
                    _stateIdentifiedCount = 0;
                }

                RunMoment();
                _state = _population;
            }
        }

        #endregion

        #region Private Methods

        private bool PreviousPopulationStateAndCurrentAreEqual() {
            for (int i = 0; i < _population.Length; i++) {
                for (int j = 0; j < _population[i].Length; j++) {
                    if (_population[i][j] != _state[i][j])
                        return false;
                }
            }
            return true;
        }

        private void ShiftCells() {
            for (int i = 0; i < _population.Length; i++) {
                for (int j = 0; j < _population[i].Length; j++) {
                    if (_population[i][j] == 1)
                        Kill(ref _population[i][j]);
                    else 
                        GiveLife(ref _population[i][j]);
                }
            }
        }

        private void RunMoment() {
            Thread.Sleep(TIME_TO_WAIT_IN_MILLISECONDS);
            PrintPopulation();
            CheckEachCellToDieOrLive();
            Thread.Sleep(TIME_TO_WAIT_IN_MILLISECONDS);
            Console.Clear();
        }

        private void PrintPopulation() {
            for (int i = 0; i < _population.Length; i++) {
                Console.WriteLine(string.Join(' ', _population[i]));
            }
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

            ApplyRules(ref _population[i][j], ref livingNeighbors);
        }

        private void ApplyRules(ref int cell, ref int livingNeighbors) {
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

        #endregion
    }
}