using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Algorizem.Graph;

/// <summary>
/// 데이크스트라(다익스트라) 알고리즘입니다.
/// </summary>
public class Dijkstra
{
    /// <summary>
    /// 현재 설정된 간선 목록입니다.
    /// </summary>
    public LinkedList<(uint point, ulong distance)>[] Lines { get; init; }
    /// <summary>
    /// 정점의 갯수입니다.
    /// </summary>
    public uint Count { get; init; }
    /// <summary>
    /// 데이크스트라를 사용할수 있는 그래프를 생성합니다.
    /// </summary>
    /// <param name="count">정점의 갯수</param>
    public Dijkstra(in uint count)
    {
        this.Count = count;
        this.Lines = new LinkedList<(uint, ulong)>[count];
    }
    /// <summary>
    /// 간선을 추가합니다.
    /// </summary>
    /// <param name="Starting">시작점</param>
    /// <param name="Ending">도착점</param>
    /// <param name="Distance">거리</param>
    public virtual void AddLine(in uint Starting,in uint Ending, in ulong Distance)
    {
        _check_point_index(Starting);
        _check_point_index(Ending);
        this.Lines[Starting].AddLast((Ending, Distance));
    }
    /// <summary>
    /// 데이크스트라를 실행합니다.
    /// </summary>
    /// <param name="Starting">시작점</param>
    /// <returns>시작점으로부터 각 정점별 거리</returns>
    public virtual ulong[] Run(in uint Starting)
    {
        _check_point_index(Starting);
        ulong[] TotalDistance = new ulong[Count];
        Array.Fill(TotalDistance , ulong.MaxValue);
        TotalDistance[Starting] = 0;

        PriorityQueue<uint , ulong> queue = new();
        queue.Enqueue(Starting , 0);
        while(queue.Count > 0)
        {
            uint me = queue.Dequeue();
            foreach(var line in Lines[me])
            {
                ulong next_dist = TotalDistance[me] + line.distance;
                if (TotalDistance[line.point] > next_dist)
                {
                    TotalDistance[line.point] = next_dist;
                    queue.Enqueue(line.point, next_dist);
                }
            }
        }

        return TotalDistance;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void _check_point_index(in uint point)
    {
        if (point >= Count)
            throw new AlgorizemException($"정점 번호는 {Count - 1} 이하여야 합니다.");
    }
}
