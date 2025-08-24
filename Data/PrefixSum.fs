namespace Algorizem.Data

type PrefixSum(source: int seq) =
    let prefix = source |> Seq.toArray    
    do
        for i in 1..(n - 1) do
            prefix.[i] <- prefix.[i] + input.[i-1]
    member this.Query left right =
        if left = 0 then
            prefix.[right]
        else
            prefix.[right] - prefix.[left - 1]
    member this.PrefixArray = prefix
    member this.Count = prefix.Length
    member this.Item
        with get(index: int) = prefix.[index]
    member this.Item
        with get(range: system.Range) =
            let (left, right) = range.GetOffset(prefix.Length)
            this.Query left (right - 1)