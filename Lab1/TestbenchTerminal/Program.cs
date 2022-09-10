using System;
using MKLWrapper;
using MKLBenchmarkApp;

namespace TestbenchTerminal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double[] points = new double[] { 0.0, Math.PI / 2, Math.PI };
            double[] timings = new double[points.Length];
            double maxError, maxErrorArg;
            double[] maxErrorFuncValues = new double[3];
            int retCode;
            try
            {
                MKLWrapper.VMBenchmark.CallMKLFunction(
                        MKLWrapper.VMf.SinCos, points.Length, points, timings,
                        out maxError, out maxErrorArg, maxErrorFuncValues, out retCode);
                if (retCode != 0) {
                    Console.WriteLine("Something went wrong during calculation");
                }
                else
                {
                    Console.WriteLine("Timings for HA, LA and EP respectively:");
                    foreach (var value in timings)
                    {
                        Console.WriteLine(value);
                    }
                    Console.WriteLine($"Max error {maxError} is reached at {maxErrorArg}.");
                    Console.WriteLine($"Func values at these point are {maxErrorFuncValues[0]} for HA, " +
                                      $"{maxErrorFuncValues[1]} for LA and {maxErrorFuncValues[2]} for EP modes.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"I'm tired... exceptionally: {e}");
            }

            //MKLBenchmarkApp.ViewData view = new();
        }
    }
}
