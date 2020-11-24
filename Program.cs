using System;

namespace GameOfLife {
    class Program {
        static void Main(string[] args) {
            int[][] population = new int[3][] { new int[3] { 0,1,0 }, new int[3] { 1,1,1 }, new int[3] { 1,1,1 } };

            try {
                Simulation simulation = new Simulation(population);
                simulation.Start();
            }
            catch (Exception ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }
    }
}
