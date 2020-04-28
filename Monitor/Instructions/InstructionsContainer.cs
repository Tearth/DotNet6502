using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Monitor.Instructions
{
    public class InstructionsContainer
    {
        public Dictionary<byte, InstructionData> _instructions;
        private readonly string _filePath;

        public InstructionsContainer(string filePath)
        {
            _filePath = filePath;
            Load();
        }

        public void Load()
        {
            var content = File.ReadAllText(_filePath);
            var instructionsList = JsonConvert.DeserializeObject<List<InstructionData>>(content);
            _instructions = instructionsList.ToDictionary(p => p.OpCode);
        }

        public InstructionData Get(byte opCode)
        {
            if (!_instructions.TryGetValue(opCode, out var result))
            {
                return null;
            }

            return result;
        }
    }
}
