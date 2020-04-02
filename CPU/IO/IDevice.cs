namespace CPU.IO
{
    public interface IDevice
    {
        byte Read(ushort address);
        void Write(ushort address, byte value);
    }
}
