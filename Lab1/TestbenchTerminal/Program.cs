using System;
using MKLWrapper;

namespace TestbenchTerminal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double[] points = new double[] { 0.0, 1.0 };
            MKLWrapper.VMBenchmark.CallMKLFunction(MKLWrapper.VMf.Cos, 2, points, points);
            Console.WriteLine("Hello World!");
        }
    }
}
