namespace Algorizem.Data

/// <summary>
/// 넓은 범위의 수를 0부터 시작하는 값에 대응시켜 압축합니다. (좌표 압축에 활용)
/// </summary>
type Compressor(source: seq<int64>) =
    let small2big = source |> Set.ofSeq |> Set.toArray
    let big2small = 
        small2big
        |> Array.indexed
        |> Array.map (fun (i, v) -> (v, i))
        |> Map.ofArray

    /// <summary>
    /// 압축된 항목의 개수
    /// </summary>
    member this.Count = small2big.Length

    /// <summary>
    /// 넓은 범위의 수를 압축합니다.
    /// </summary>
    /// <param name="x">압축할 큰 수</param>
    /// <returns>압축된 작은 수</returns>
    member this.Compress(x: int64) : int = big2small.[x]

    /// <summary>
    /// 압축된 수를 이전으로 되돌립니다.
    /// </summary>
    /// <param name="x">압축된 수</param>
    /// <returns>압축되기 전의 수</returns>
    member this.Decompress(x: int) : int64 = small2big.[x]
