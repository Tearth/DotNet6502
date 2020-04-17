using Monitor.Instructions.Converters;
using Newtonsoft.Json;

namespace Monitor.Instructions
{
    public class InstructionData
    {
        [JsonConverter(typeof(HexJsonConverter))]
        public byte OpCode { get; set; }

        public int Bytes { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Mode { get; set; }
    }
}
