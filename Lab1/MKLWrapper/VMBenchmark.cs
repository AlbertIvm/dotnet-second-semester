using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

namespace MKLWrapper
{
    public class VMBenchmark
    {
        public ObservableCollection<VMTime> TimeResults { get; set; }
        public ObservableCollection<VMAccuracy> AccuracyResults { get; set; }

        public void AddVMTime(VMGrid grid)
        {

        }

        public void AddVMAccuracy(VMGrid grid)
        {

        }

        [DllImport("..\\..\\..\\..\\x64\\Debug\\MKLVMRuntime.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void CallMKLFunction(VMf function_code, int nodesNumber, double[] points, double[] results);
    }
}
