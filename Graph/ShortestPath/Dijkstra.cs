using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Algorizem.Graph.ShortestPath;

/// <summary>
/// 데이크스트라(다익스트라) 알고리즘입니다.
/// </summary>
public class Dijkstra : LinkedListGraph<(uint point,ulong distance)>
{
    /// <summary>
    /// 데이크스트라를 사용할수 있는 그래프를 생성합니다.
    /// </summary>
    /// <param name="count">정점의 갯수</param>
    public Dijkstra(in uint count) : base(count) { }
    /// <summary>
    /// 단방향 간선을 추가합니다.
    /// </summary>
    /// <param name="starting">시작점</param>
    /// <param name="ending">도착점</param>
    /// <param name="distance">거리</param>
    public virtual void AddOneWay(in uint starting , in uint ending , in ulong distance)
    {
        _CheckPointIndex(starting);
        _CheckPointIndex(ending);
        this.Lines[starting].AddLast((ending, distance));
    }
    /// <summary>
    /// 동일한 거리의 양방향 간선을 추가합니다.
    /// </summary>
    /// <param name="starting">시작점</param>
    /// <param name="ending">도착점</param>
    /// <param name="distance">거리</param>
    public virtual void AddTwoWay(in uint starting , in uint ending , in ulong distance)
    {
        this.AddOneWay(starting , ending , distance);
        this.AddOneWay(ending , starting , distance);
    }
    /// <summary>
    /// 데이크스트라를 실행합니다.
    /// </summary>
    /// <param name="starting">시작점</param>
    /// <returns>시작점으로부터 각 정점별 거리</returns>
    public virtual ulong[] Run(in uint starting)
    {
        _CheckPointIndex(starting);
        ulong[] TotalDistance = new ulong[Count];
        Array.Fill(TotalDistance , ulong.MaxValue);
        TotalDistance[starting] = 0;

        PriorityQueue<uint , ulong> queue = new();
        queue.Enqueue(starting , 0);
        while (queue.TryDequeue(out uint me, out ulong my_dist))
        {
            if (TotalDistance[me] < my_dist)
                continue;

            foreach (var line in Lines[me])
            {
                ulong next_dist = TotalDistance[me] + line.distance;
                if (TotalDistance[line.point] > next_dist)
                {
                    TotalDistance[line.point] = next_dist;
                    queue.Enqueue(line.point , next_dist);
                }
            }
        }

        return TotalDistance;
    }
}
