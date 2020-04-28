namespace Protocol.Packets
{
    public enum PacketType : byte
    {
        // ReSharper disable once UnusedMember.Global
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
        RunToAddressCommand,
        RunUntilLoopCommand
    }
}
