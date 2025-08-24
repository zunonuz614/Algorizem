namespace Algorizem.Other

open System
open System.IO

/// 간단한 FastIO 헬퍼
module FastIO =
    let private reader = new StreamReader(Console.OpenStandardInput())
    let private writer = new StreamWriter(Console.OpenStandardOutput())

    let readLine() : string = reader.ReadLine()
    let readInt() : int = Int32.Parse(readLine())
    let readUInt32() : uint32 = UInt32.Parse(readLine())
    let readInts() : int[] = readLine().Split([|' '; '\t'|], StringSplitOptions.RemoveEmptyEntries) |> Array.map Int32.Parse

    let write (value: obj) = writer.Write(value)
    let writeLine (value: obj) = writer.WriteLine(value)
    let flush() = writer.Flush()
