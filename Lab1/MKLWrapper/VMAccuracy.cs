namespace MKLWrapper
{
    public struct VMAccuracy
    {
        public VMGrid Grid { get; set; }
        public override string ToString()
        {
            return Grid.ToString();
        }
    }
}
