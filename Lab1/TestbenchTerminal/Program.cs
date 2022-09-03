using System;
using MKLWrapper;

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
                        MKLWrapper.VMf.Cos, points.Length, points, timings,
                        out maxError, out maxErrorArg, maxErrorFuncValues, out retCode);
            }
            catch (Exception e)
            {
                Console.WriteLine($"I'm tired... exceptionally: {e}");
            }
            foreach (var value in timings)
            {
                Console.WriteLine(value);
            }
            Console.WriteLine("Hello World!");
        }
    }
}
