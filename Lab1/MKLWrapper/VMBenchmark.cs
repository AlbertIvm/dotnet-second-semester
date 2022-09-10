using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;

namespace MKLWrapper
{
    [Serializable]
    public class VMBenchmark
    {
        // Public properties
        public ObservableCollection<VMTime> TimeResults { get; set; }
        public ObservableCollection<VMAccuracy> AccuracyResults { get; set; }
        public double LeastLaToHaTimingRatio
        {
            get
            {
                return _leastLaToHaTimingRatio;
            }
        }

        public double LeastEpToHaTimingRatio
        {
            get
            {
                return _leastEpToHaTimingRatio;
            }
        }

        // Public methods
        public VMBenchmark()
        {
            _leastLaToHaTimingRatio = 0.0;
            _leastEpToHaTimingRatio = 0.0;
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
                try
                {
                    TimeResults.Add(new VMTime(grid, functionType, timings[0], timings[1], timings[2]));
                    _leastLaToHaTimingRatio = TimeResults.Min(result => result.LaToHaTimingRatio);
                    _leastEpToHaTimingRatio = TimeResults.Min(result => result.EpToHaTimingRatio);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    // Something is clearly wrong with timings, but we should not let this crash the app
                }
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

        // Private fields
        double _leastLaToHaTimingRatio;
        double _leastEpToHaTimingRatio;
    }
}
