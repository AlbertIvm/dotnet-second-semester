using System;

namespace MKLWrapper
{
    [Serializable]
    public class VMGrid
    {
        public int NodesNumber { get; set; }
        public int[] BorderNodes { get; set; }
        public int Stride { get; }
    }
}
