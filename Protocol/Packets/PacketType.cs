namespace Protocol.Packets
{
    public enum PacketType : byte
    {
        None,
        RegistersRequest,
        Registers,
        PinsRequest,
        Pins,
        CyclesRequest,
        Cycles,
        StopCommand,
        ContinueCommand,
        NextCycleCommand,
        NextInstructionCommand,
        MemoryRequest,
        Memory,
        RunToAddressCommand
    }
}
