using System;

namespace MKLWrapper
{
    [Serializable]
    public struct VMTime
    {
        // Public properties
        public VMGrid Grid { get; set; }
        public VMf FunctionType { get; set; }
        public double CalcTimeHA
        {
            get => _calcTimeHa;
            set
            {
                if (value < 0.0)
                {
                    throw new ArgumentOutOfRangeException("Calculation time can't be negative", nameof(value));
                }
                _calcTimeHa = value;
            }
        }
        public double CalcTimeLA
        {
            get => _calcTimeLa;
            set
            {
                if (value < 0.0)
                {
                    throw new ArgumentOutOfRangeException("Calculation time can't be negative", nameof(value));
                }
                _calcTimeLa = value;
            }
        }
        public double CalcTimeEP
        {
            get => _calcTimeEp;
            set
            {
                if (value < 0.0)
                {
                    throw new ArgumentOutOfRangeException("Calculation time can't be negative", nameof(value));
                }
                _calcTimeEp = value;
            }
        }
        public double LaToHaTimingRatio {
            get
            {
                if (CalcTimeHA == 0.0)
                {
                    throw new ArgumentOutOfRangeException("Can't get ratio, HA time is zero");
                }
                return CalcTimeLA / CalcTimeHA;
            }
        }
        public double EpToHaTimingRatio
        {
            get
            {
                if (CalcTimeHA == 0.0)
                {
                    throw new ArgumentOutOfRangeException("Can't get ratio, HA time is zero");
                }
                return CalcTimeEP / CalcTimeHA;
            }
        }

        // Public methods
        public VMTime(VMGrid grid, VMf functionType, double calcTimeHa, double calcTimeLa, double calcTimeEp)
        {
            Grid = grid;
            FunctionType = functionType;
            _calcTimeHa = 0.0;
            _calcTimeLa = 0.0;
            _calcTimeEp = 0.0;

            // No validation code duplication for the cost of slower initialization
            CalcTimeHA = calcTimeHa;
            CalcTimeLA = calcTimeLa;
            CalcTimeEP = calcTimeEp;
        }

        public override string ToString()
        {
            return $"VMTime properties: grid: {Grid.ToString()}; " +
                   $"function type: {FunctionType.ToString()}; " +
                   $"calculation times (in mcs) for modes: HA: {CalcTimeHA}, " +
                   $"LA: {CalcTimeLA}, EP: {CalcTimeEP}; " +
                   $"LA calculation taking {LaToHaTimingRatio} of HA's time " +
                   $"and EP calculation taking {EpToHaTimingRatio} of HA's time.";
        }

        // Private fields
        private double _calcTimeHa;
        private double _calcTimeLa;
        private double _calcTimeEp;
    }
}
