using System;

namespace MKLWrapper
{
    [Serializable]
    public class VMGrid
    {
        // Public properties
        public int NodesNumber
        {
            get => _nodesNumber; 
            set
            {
                if (value < 2)
                {
                    throw new ArgumentOutOfRangeException(
                            nameof(value), "Can't make a VMGrid with less than two nodes");
                }
                _nodesNumber = value;
            }
        }
        public double LeftBorder { get; set; }
        public double RightBorder { get; set; }
        public double Step
        {
            get
            {
                if (LeftBorder > RightBorder)
                {
                    throw new ArgumentException("VMGrid's borders are initialized incorrectly");
                }
                return (RightBorder - LeftBorder) / (NodesNumber - 1);
            }

        }

        // Public methods
        public VMGrid(int nodesNumber, double leftBorder, double rightBorder)
        {
            NodesNumber = nodesNumber;
            if (leftBorder > rightBorder)
            {
                throw new ArgumentException("VMGrid's borders are initialized incorrectly");
            }
            LeftBorder = leftBorder;
            RightBorder = rightBorder;
        }

        public double[] GetNodes()
        {
            double step = Step; // to avoid recalculation
            double[] result = new double[NodesNumber];
            for (int i = 0; i < NodesNumber - 1; i++)
            {
                result[i] = LeftBorder + i * step;
            }
            // This is to avoid rounding error on the right border
            result[NodesNumber - 1] = RightBorder;
            return result;
        }

        public override string ToString()
        {
            return $"Grid with border nodes {LeftBorder} and {RightBorder}, " +
                   $"having {NodesNumber} nodes overall (step is {Step})";
        }

        // Private fields
        private int _nodesNumber;
    }
}
