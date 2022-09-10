using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

namespace MKLWrapper
{
    [Serializable]
    public class VMBenchmark
    {
        // Public properties
        public ObservableCollection<VMTime> TimeResults { get; set; }
        public ObservableCollection<VMAccuracy> AccuracyResults { get; set; }

        // Public methods
        public VMBenchmark()
        {
            TimeResults = new();
            AccuracyResults = new();
        }

        public void AddVMTime(VMf functionType, VMGrid grid)
        {
            // These parameters make no sense unless the call is successful 
            double[] timings;
            double maxError, maxErrorArg;
            double[] maxErrorFuncValues;

            // If we fail, we fail silently
            if (SafeCallMKLFunction(
                    functionType, grid, out timings, out maxError, out maxErrorArg, out maxErrorFuncValues))
            {
                TimeResults.Add(new VMTime(grid, functionType, timings[0], timings[1], timings[2]));
            }
        }

        public void AddVMAccuracy(VMf functionType, VMGrid grid)
        {
            // These parameters make no sense unless the call is successful 
            double[] timings;
            double maxError, maxErrorArg;
            double[] maxErrorFuncValues;

            // If we fail, we fail silently
            if (SafeCallMKLFunction(
                    functionType, grid, out timings, out maxError, out maxErrorArg, out maxErrorFuncValues))
            {
                AccuracyResults.Add(new VMAccuracy(
                        grid, functionType, maxError, maxErrorArg,
                        maxErrorFuncValues[0],
                        maxErrorFuncValues[1],
                        maxErrorFuncValues[2]));
            }
        }

        // Public static methods

        // You should both check the ret value and catch exceptions from this function
        [DllImport("..\\..\\..\\..\\x64\\Debug\\MKLVMRuntime.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void CallMKLFunction(
                VMf function_code, int nodesNumber, double[] points,
                double[] timings, out double maxError, out double maxErrorArg,
                double[] maxErrorFuncValues, out int ret);
        
        // If this function returns 'false', passed parameters' values
        // should not be interpreted in any way
        public static bool SafeCallMKLFunction(
                VMf functionType, VMGrid grid,
                out double[] timings, out double maxError,
                out double maxErrorArg, out double[] maxErrorFuncValues)
        {
            // Initial values (for compilation purposes)
            timings = new double[3];
            maxErrorFuncValues = new double[3];
            maxError = -1.0;
            maxErrorArg = 0.0;

            double[] points;
            try
            {
                points = grid.GetNodes();
            }
            catch (ArgumentException e)
            {
                // Fail silently
                return false;
            }
            int retCode;

            try
            {
                CallMKLFunction(
                        functionType, grid.NodesNumber, points, timings,
                        out maxError, out maxErrorArg, maxErrorFuncValues, out retCode);
                if (retCode != 0)
                {
                    // Fail silently
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                // Fail silently
                return false;
            }
        }
    }
}
