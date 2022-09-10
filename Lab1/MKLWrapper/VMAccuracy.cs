using System;

namespace MKLWrapper
{
    [Serializable]
    public struct VMAccuracy
    {
        public VMGrid Grid { get; set; }
        public VMf FunctionType { get; set; }
        public double MaxAbsError { get; set; }
        public double MaxAbsErrorArgument { get; set; }
        public double MaxAbsErrorValueHa { get; set; }
        public double MaxAbsErrorValueLa { get; set; }
        public double MaxAbsErrorValueEp { get; set; }

        public override string ToString()
        {
            return $"VMAccuracy properties: grid: {Grid.ToString()}, " +
                   $"function type: {FunctionType.ToString()}, " +
                   $"maximum absolute error: {MaxAbsError}, " +
                   $"reached at {MaxAbsErrorArgument} with function values being " +
                   $"{MaxAbsErrorValueHa} for HA, " +
                   $"{MaxAbsErrorValueLa} for LA " +
                   $"and {MaxAbsErrorValueEp} for EP modes. ";
        }
    }
}
