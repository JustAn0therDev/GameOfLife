using System;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            int[][] grid = new int[3][] { new int[3] { 0,1,1 }, new int[3] { 1,1,1 }, new int[3] { 1,1,0 } };

            try {
                Simulation simulation = new Simulation(population: grid);
                simulation.Start(infiniteMoments: true);
            }
            catch (Exception ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }
    }
}
