# DotNet6502
MOS 6502 emulator written for .NET Core platform. It implements all official operation codes and passes [Klaus' test suite](https://github.com/Klaus2m5/6502_65C02_functional_tests). At this point, this is just pure CPU emulation with RAM and ROM peripherals, and simple monitor app with debugger - but in the future, I'm going to create 6502-based computer emulators (like Commodore PET).

Documentation: https://tearth.github.io/DotNet6502

**What is done**
 - full MOS 6502 emulator with support for all official instructions, interrupts and bus operations
 - Host app which allows running emulation using command line parameters
 - Monitor app with debugger abilities

**TODO**
 - more peripherals, which allow emulating computers like Commodore PET
 - more debugger options (like editing memory content)

## Projects overview
### MOS 6502 emulator
This is the core of the project. Contains the implementation of MOS 6502 emulator, where the most important parts are:
 - [Entry emulator class](./M6502/Mos6502Emulator.cs)
 - [Entry core class](./M6502/Mos6502Core.cs)
 - [Instruction decoder](./M6502/InstructionDecode/InstructionDecoder.cs)
 - [Instructions](./M6502/InstructionDecode/)
 - [Interrupts](./M6502/Interrupts/)
 - [Bus](./M6502/IO/)

### Host
This app allows running MOS 6502 emulator using a set of the command line options.

```
-f, --frequency    Required. Set processor frequency (Hz).
-d, --debugger     (Default: false) Enable or disable debugger server.
-p, --port         (Default: 6502) Set debugger server port.
-w, --wait         (Default: false) Wait for debugger (set RDY pin to 0).
-i, --info         (Default: false) Display debug information.  
-b                 Add peripheral device.
--help             Display this help screen.
--version          Display version information.
```

Adding peripheral devices is a bit tricky due to format of an argument. For example, `-b RAM[0x0000, 0x7FFF, image.bin]` means: load RAM.dll library, find class which implements IDevice interface, create its instance and pass arguments 0x0000, 0x7FFF and image.bin. Actually, there are two devices ready to use:
 - [RAM](./Peripherals/RAM/) - Random Access Memory. Arguments:
   - Start address of the memory
   - End address of the memory
   - Image to load (optional)
 - [ROM](./Peripherals/ROM/) - Read Only Memory. Arguments:
   - Start address of the memory
   - End address of the memory
   - Image to load (optional)

Example of running emulator with 1 MHz frequency, debugger enabled (and waiting for command), RAM present at address 0x0000-0x7FFF, 7 ROMs presents at address 0xC000-0xFFFF (excluding 0xE800-0xEFFF) with loaded parts of the Commodore PET images.
```
dotnet Host.dll -f 1000000 -d -w
-b RAM[0x0000, 0x7FFF]
-b ROM[0xC000, 0xC7FF, ROMS/pet-2001-basic1-1.bin]
-b ROM[0xC800, 0xCFFF, ROMS/pet-2001-basic1-2.bin]
-b ROM[0xD000, 0xD7FF, ROMS/pet-2001-basic1-3.bin]
-b ROM[0xD800, 0xDFFF, ROMS/pet-2001-basic1-4.bin]
-b ROM[0xE000, 0xE7FF, ROMS/pet-2001-basic1-5.bin]
-b ROM[0xF000, 0xF7FF, ROMS/pet-2001-basic1-6.bin]
-b ROM[0xF800, 0xFFFF, ROMS/pet-2001-basic1-7.bin]
```

![Host screenshot example](https://i.imgur.com/S3M66IH.png)

### Monitor
The Monitor allows viewing registers, memory, and pins of emulated MOS 6502 during its work. It's also possible to use basic debugger commands like stepping by cycle or instructions. This is the only app that is not written in .NET Core, so it will work only on Windows (because of used WPF).

![Monitor screenshot example](https://i.imgur.com/F4RcQR6.png)

### Protocol

This project contains a set of classes used to implement custom debugger protocol shared by the client and the server. Every packet contains 5-bytes header (signature with 0x6502 value, length, type) and one byte with a checksum (simple XOR of all bytes in the packet) at the end.

|            | Signature | Length | Type |   Data   | Checksum |
|------------|-----------|--------|------|----------|----------|
|  **Type**  | ushort    | ushort | byte | byte[n]  | byte     |
| **Length** | 2         | 2      | 1    | n        | 1        |

This project contains three main categories of packets: requests, responses, and commands. Requests and responses are used together to transport data between the client and the server. Commands are desired to be used one-way, so they only inform another side that some action is required to process.

| Requests          | Length | Fields            |
|-------------------|--------|-------------------|
| Cycles Request    | 6      | Header + Checksum |
| Memory Request    | 11     | Header + Address (2 bytes) + Length (2 bytes) + Tag (1 byte) + Checksum |
| Pins Request      | 6      | Header + Checksum |
| Registers Request | 6      | Header + Checksum |

| Responses          | Length | Fields            |
|--------------------|--------|-------------------|
| Cycles Response    | 14     | Header + Cycles count (8 bytes) + Checksum |
| Memory Response    | 11 + n | Header + Address (2 bytes) + Length (2 bytes) + Tag (1 byte) + Data (n bytes) + Checksum |
| Pins Response      | 10     | Header + Address Pins (2 byte) + Data Pins (1 bytes) + Other Pins (1 byte) + Checksum |
| Registers Response | 13     | Header + Program Counter (2 bytes) + Stack Pointer (1 byte) + Accumulator (1 byte) + X Index Register (1 byte) + Y Index Register (1 byte) + Flags (1 byte) + Checksum |

| Commands                 | Length | Fields            |
|--------------------------|--------|-------------------|
| Continue Command         | 6      | Header + Checksum |
| Next Cycle Command       | 6      | Header + Checksum |
| Next Instruction Command | 6      | Header + Checksum |
| Run To Address Command   | 8      | Header + Address (2 bytes) + Checksum |
| Run Until Loop Command   | 6      | Header + Checksum |
| Stop Command             | 6      | Header + Checksum |