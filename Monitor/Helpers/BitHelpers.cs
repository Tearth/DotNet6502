namespace Monitor.Helpers
{
    public static class BitHelpers
    {
        public static byte ChangeBit(byte val, int bitIndex, bool bitValue)
        {
            return (byte)((val & ~(1 << bitIndex)) | ((bitValue ? 1 : 0) << bitIndex));
        }
    }
}
