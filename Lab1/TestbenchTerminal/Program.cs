using System;
using MKLWrapper;

namespace TestbenchTerminal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double[] points = new double[] { 0.0, 1.0 };
            double[] timings = new double[points.Length];
            double maxError, maxErrorArg;
            double[] maxErrorFuncValues = new double[3];
            int retCode;
            MKLWrapper.VMBenchmark.CallMKLFunction(
                    MKLWrapper.VMf.Cos, 2, points, timings,
                    out maxError, out maxErrorArg, maxErrorFuncValues, out retCode);
            Console.WriteLine("Hello World!");
        }
    }
}
