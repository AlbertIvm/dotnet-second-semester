namespace MKLWrapper
{
    public struct VMTime
    {
        public VMGrid Grid { get; set; }
        public VMf FunctionType { get; set; }
        public double CalcTimeHA { get; set; }
        public double CalcTimeLA { get; set; }
        public double CalcTimeEP { get; set; }
        public double LaToHaTimingRatio { get; set; }
        public double EpToHaTimingRatio { get; set; }

        public override string ToString()
        {
            return $"VMTime properties: grid: {Grid.ToString()}; " +
                   $"function type: {FunctionType.ToString()}; " +
                   $"calculation times (in ms) for modes: HA: {CalcTimeHA}, " +
                   $"LA: {CalcTimeLA}, EP: {CalcTimeEP}; " +
                   $"LA calculation taking {LaToHaTimingRatio} of HA's time " +
                   $"and EP calculation taking {EpToHaTimingRatio} of HA's time.";
        }
    }
}
