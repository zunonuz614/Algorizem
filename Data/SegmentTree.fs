namespace Algorizem.Data

open System

/// <summary>
/// 세그먼트 트리에 사용될 범위 자료형입니다.
/// </summary>
[<Struct>]
type SegmentTreeRange =
    { Start: int; End: int }

    /// <summary>
    /// 주어진 범위에 이 범위의 전체가 포함되는지 확인합니다.
    /// </summary>
    /// <param name="outer">넓은 범위</param>
    /// <returns>전체 포함 여부</returns>
    member this.Include(outer: SegmentTreeRange) =
        outer.Start <= this.Start && this.End <= outer.End

    /// <summary>
    /// 주어진 범위에 이 범위가 완전히 제외되는지 확인합니다.
    /// </summary>
    /// <param name="outer">넓은 범위</param>
    /// <returns>완전 제외 여부</returns>
    member this.Exclude(outer: SegmentTreeRange) =
        outer.End < this.Start || this.End < outer.Start

    /// <summary>
    /// 주어진 범위와 이 범위의 겹치는 범위를 가져옵니다.
    /// </summary>
    /// <param name="other">비교할 범위</param>
    /// <returns>완전히 겹치지 않을경우 None을 반환합니다.</returns>
    member this.Overlap(other: SegmentTreeRange) =
        let low = Math.Max(this.Start, other.Start)
        let high = Math.Min(this.End, other.End)
        if low > high then
            None
        else
            Some({ Start = low; End = high })

    /// <summary>
    /// 단일 범위인지 확인합니다.
    /// </summary>
    member this.IsSingle = this.Start = this.End

    /// <summary>
    /// 해당 범위가 가지는 크기입니다.
    /// </summary>
    member this.Count = this.End - this.Start + 1

/// <summary>
/// 세그먼트 트리의 노드입니다.
/// </summary>
type SegmentTreeNode(range: SegmentTreeRange) =
    let left, right =
        if range.IsSingle then
            None, None
        else
            let mid = (range.Start + range.End) / 2
            let leftNode = Some(SegmentTreeNode({ range with End = mid }))
            let rightNode = Some(SegmentTreeNode({ range with Start = mid + 1 }))
            leftNode, rightNode

    /// <summary>
    /// 노드의 범위입니다.
    /// </summary>
    member _.Range = range

    /// <summary>
    /// 노드의 왼쪽 자식 노드입니다.
    /// </summary>
    member _.Left = left

    /// <summary>
    /// 노드의 오른쪽 자식 노드입니다.
    /// </summary>
    member _.Right = right
