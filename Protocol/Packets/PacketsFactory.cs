using Protocol.Packets.Requests;

namespace Protocol.Packets
{
    public class PacketsFactory
    {
        public PacketBase Create(byte[] buffer)
        {
            switch ((PacketType)buffer[4])
            {
                case PacketType.RegistersRequest: return new RegistersRequestPacket(buffer);
                case PacketType.Registers: return new RegistersPacket(buffer);
                case PacketType.PinsRequest: return new PinsRequestPacket(buffer);
                case PacketType.Pins: return new PinsPacket(buffer);
                case PacketType.CyclesRequest: return new CyclesRequestPacket(buffer);
                case PacketType.Cycles: return new CyclesPacket(buffer);
                case PacketType.StopCommand: return new StopCommandPacket(buffer);
                case PacketType.ContinueCommand: return new ContinueCommandPacket(buffer);
                case PacketType.NextCycleCommand: return new NextCycleCommandPacket(buffer);
                case PacketType.NextInstructionCommand: return new NextInstructionCommandPacket(buffer);
                case PacketType.MemoryRequest: return new MemoryRequestPacket(buffer);
                case PacketType.Memory: return new MemoryPacket(buffer);
                case PacketType.RunToAddressCommand: return new RunToAddressCommandPacket(buffer);
            }

            return null;
        }
    }
}
