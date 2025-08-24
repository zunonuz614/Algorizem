namespace Algorizem.Graph.NetworkFlow

open Algorizem.Graph
open System.Collections.Generic

/// <summary>
/// 유량이 포함된 간선 정보입니다.
/// </summary>
type FlowLine(starting: uint32, destination: uint32, limit: int64) =
    let mutable reverse: FlowLine = Unchecked.defaultof<FlowLine>
    member val Starting = starting with get
    member val Destination = destination with get
    member val Limit = limit with get
    member val Flow = 0L with get, set
    member this.Reverse
        with get() = reverse
        and set(v) = reverse <- v

    /// <summary>
    /// 남은 용량
    /// </summary>
    member this.Remaining = this.Limit - this.Flow

    /// <summary>
    /// 생성자: 상호 참조되는 라인과 리버스를 만들어 연결합니다.
    /// </summary>
    static member Create(start: uint32, end': uint32, limit: int64) =
        let line = FlowLine(start, end', limit)
        let reverse = FlowLine(end', start, 0L)
        line.Reverse <- reverse
        reverse.Reverse <- line
        line

/// <summary>
/// 애드몬드 카프 (최대 유량) 알고리즘입니다.
/// </summary>
type EdmondsKarp(count: uint32) =
    inherit LinkedListGraph<FlowLine>(count)

    /// <summary>
    /// 유량이 포함된 간선 정보입니다.
    /// </summary>
    
    /// 유량 간선을 추가합니다.
    /// </summary>
    /// <param name="starting">시작점</param>
    /// <param name="ending">도착점</param>
    /// <param name="limit">제한</param>
    member this.AddLine(starting: uint32, ending: uint32, limit: int64) =
        let line = FlowLine.Create(starting, ending, limit)
        this.AddLine(line)

    /// <summary>
    /// 유량 간선을 추가합니다.
    /// </summary>
    /// <param name="line">간선</param>
    member this.AddLine(line: FlowLine) =
        this._CheckPointIndex(line.Starting)
        this._CheckPointIndex(line.Destination)
        (this.Lines.[int line.Starting] : System.Collections.Generic.LinkedList<FlowLine>).AddLast(line) |> ignore
        (this.Lines.[int line.Destination] : System.Collections.Generic.LinkedList<FlowLine>).AddLast(line.Reverse) |> ignore

    /// <summary>
    /// 최대 유량을 계산합니다.
    /// </summary>
    /// <param name="source">소스</param>
    /// <param name="sink">싱크</param>
    /// <returns>최대 유량</returns>
    member this.Run(source: uint32, sink: uint32) =
        let mutable result = 0L
        let queue = Queue<uint32>()

        let rec loop () =
            let visited : FlowLine option[] = Array.create (int this.Count) None
            visited.[int source] <- Some (FlowLine(0u, 0u, 0L))
            queue.Enqueue(source)

            while queue.Count > 0 do
                let u = queue.Dequeue()
                for line in (this.Lines.[int u] : System.Collections.Generic.LinkedList<FlowLine>) do
                    if visited.[int line.Destination].IsNone && line.Remaining > 0L then
                        visited.[int line.Destination] <- Some line
                        queue.Enqueue(line.Destination)

            if visited.[int sink].IsSome then
                let mutable flow = System.Int64.MaxValue
                let mutable point = sink
                while point <> source do
                    let line =
                        match visited.[int point] with
                        | Some l -> l
                        | None -> failwith "Unexpected None in visited"
                    flow <- min flow line.Remaining
                    point <- line.Starting
                
                point <- sink
                while point <> source do
                    let line =
                        match visited.[int point] with
                        | Some l -> l
                        | None -> failwith "Unexpected None in visited"
                    line.Flow <- line.Flow + flow
                    line.Reverse.Flow <- line.Reverse.Flow - flow
                    point <- line.Starting

                result <- result + flow
                loop()

        loop()
        result
