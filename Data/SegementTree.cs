namespace Algorizem.Data;

/// <summary>
/// 세그먼트 트리에 사용될 범위 자료형입니다.
/// </summary>
/// <param name="Start">시작 지점</param>
/// <param name="End">끝 지점</param>
public record SegmentTreeRange(int Start,int End)
{
    /// <summary>
    /// 주어진 범위에 이 범위의 전체가 포함되는지 확인합니다.
    /// </summary>
    /// <param name="outer">넓은 범위</param>
    /// <returns>전체 포함 여부</returns>
    public bool Include(in SegmentTreeRange outer) => outer.Start <= Start && End <= outer.End;
    /// <summary>
    /// 주어진 범위에 이 범위가 완전히 제외되는지 확인합니다.
    /// </summary>
    /// <param name="outer">넓은 범위</param>
    /// <returns>완전 제외 여부</returns>
    public bool Exclude(in SegmentTreeRange outer) => outer.End < Start || End < outer.Start;
    /// <summary>
    /// 주어진 범위와 이 범위의 겹치는 범위를 가져옵니다.
    /// </summary>
    /// <param name="other">비교할 범위</param>
    /// <returns>완전히 겹치지 않을경우 null을 반환합니다.</returns>
    public SegmentTreeRange? Overlap(in  SegmentTreeRange other)
    {
        int low = Math.Max(Start, other.Start), high = Math.Min(End, other.End);
        if (low > high)
            return null;
        return new SegmentTreeRange(low, high);
    } 
    /// <summary>
    /// 단일 범위인지 확인합니다.
    /// </summary>
    public bool IsSingle => Start == End;
    /// <summary>
    /// 해당 범위가 가지는 크기입니다.
    /// </summary>
    public int Count => End - Start + 1;
}

/// <summary>
/// 세그먼트 트리의 노드입니다.
/// </summary>
public class SegementTreeNode
{
    /// <summary>
    /// 노드의 범위입니다.
    /// </summary>
    public SegmentTreeRange Range { get; }
    /// <summary>
    /// 노드의 왼쪽 자식 노드입니다.
    /// </summary>
    public SegementTreeNode? Left { get; } = null;
    /// <summary>
    /// 노드의 오른쪽 자식 노드입니다.
    /// </summary>
    public SegementTreeNode? Right { get; } = null;
    /// <summary>
    /// 노드를 생성합니다. 범위를 지정하면 리프노드까지 연쇄적으로 생성됩니다.
    /// </summary>
    /// <param name="range">범위</param>
    public SegementTreeNode(in SegmentTreeRange range)
    {
        if ((Range = range).IsSingle)
        {
            return;
        }

        int mid = (Range.Start + Range.End) / 2;
        Left = new SegementTreeNode(range with { End = mid });
        Right = new SegementTreeNode(range with { Start = mid + 1 });
    }
}
