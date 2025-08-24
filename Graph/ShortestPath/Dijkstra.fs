namespace Algorizem.Graph.ShortestPath

open Algorizem.Graph
open System.Collections.Generic

/// <summary>
/// 데이크스트라(다익스트라) 알고리즘입니다.
/// </summary>
type Dijkstra(count: uint32) =
    inherit LinkedListGraph<(uint32 * uint64)>(count)

    /// <summary>
    /// 단방향 간선을 추가합니다.
    /// </summary>
    /// <param name="starting">시작점</param>
    /// <param name="ending">도착점</param>
    /// <param name="distance">거리</param>
    member this.AddOneWay(starting: uint32, ending: uint32, distance: uint64) =
        this._CheckPointIndex(starting)
        this._CheckPointIndex(ending)
        this.Lines.[int starting].AddLast((ending, distance)) |> ignore

    /// <summary>
    /// 동일한 거리의 양방향 간선을 추가합니다.
    /// </summary>
    /// <param name="starting">시작점</param>
    /// <param name="ending">도착점</param>
    /// <param name="distance">거리</param>
    member this.AddTwoWay(starting: uint32, ending: uint32, distance: uint64) =
        this.AddOneWay(starting, ending, distance)
        this.AddOneWay(ending, starting, distance)

    /// <summary>
    /// 데이크스트라를 실행합니다.
    /// </summary>
    /// <param name="starting">시작점</param>
    /// <returns>시작점으로부터 각 정점별 거리</returns>
    member this.Run(starting: uint32) =
        this._CheckPointIndex(starting)
        let totalDistance = Array.create (int this.Count) System.UInt64.MaxValue
        totalDistance.[int starting] <- 0UL

        let queue = PriorityQueue<uint32, uint64>()
        queue.Enqueue(starting, 0UL)

        let mutable me = 0u
        let mutable myDist = 0UL
        while queue.TryDequeue(&me, &myDist) do
            if totalDistance.[int me] < myDist then
                ()
            else
                for (point, dist) in this.Lines.[int me] do
                    let nextDist = totalDistance.[int me] + dist
                    if totalDistance.[int point] > nextDist then
                        totalDistance.[int point] <- nextDist
                        queue.Enqueue(point, nextDist)
        
        totalDistance
