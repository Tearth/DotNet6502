using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Monitor.Instructions
{
    public class InstructionsContainer
    {
        public Dictionary<byte, InstructionData> Instructions;
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
            Instructions = instructionsList.ToDictionary(p => p.OpCode);
        }

        public InstructionData Get(byte opCode)
        {
            if (!Instructions.TryGetValue(opCode, out var result))
            {
                return null;
            }

            return result;
        }
    }
}
