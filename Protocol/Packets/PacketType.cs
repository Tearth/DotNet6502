using System.Diagnostics.CodeAnalysis;

namespace Protocol.Packets
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
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
        RunToAddressCommand,
        RunUntilLoopCommand
    }
}
