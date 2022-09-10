using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

namespace MKLWrapper
{
    [Serializable]
    public class VMBenchmark
    {
        public ObservableCollection<VMTime> TimeResults { get; set; }
        public ObservableCollection<VMAccuracy> AccuracyResults { get; set; }

        public void AddVMTime(VMf functionType, VMGrid grid)
        {

        }

        public void AddVMAccuracy(VMf functionType, VMGrid grid)
        {

        }

        // You should both check the ret value and catch exceptions from this function
        [DllImport("..\\..\\..\\..\\x64\\Debug\\MKLVMRuntime.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void CallMKLFunction(
                VMf function_code, int nodesNumber, double[] points,
                double[] timings, out double maxError, out double maxErrorArg,
                double[] maxErrorFuncValues, out int ret);
    }
}
