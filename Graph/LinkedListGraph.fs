namespace Algorizem.Graph

open Algorizem
open System.Runtime.CompilerServices

/// <summary>
/// LinkedList로 구현된 단순 그래프입니다.
/// </summary>
type LinkedListGraph<'T>(count: uint32) =
    /// <summary>
    /// 현재 설정된 간선 목록입니다.
    /// </summary>
    member val Lines = Array.init (int count) (fun _ -> System.Collections.Generic.LinkedList<'T>()) with get, set

    /// <summary>
    /// 정점의 갯수입니다.
    /// </summary>
    member val Count = count with get, set

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this._CheckPointIndex(point: uint32) =
        if point >= this.Count then
            raise (AlgorizemException($"정점 번호는 {this.Count - 1u} 이하여야 합니다."))
