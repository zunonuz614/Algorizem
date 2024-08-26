using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorizem.Graph.NetworkFlow;

/// <summary>
/// 애드몬드 카프 (최대 유량) 알고리즘입니다.
/// </summary>
public class EdmondsKarp : LinkedListGraph<EdmondsKarp.FlowLine>
{
    /// <summary>
    /// 유량이 포함된 간선 정보입니다.
    /// </summary>
    public class FlowLine
    {
        /// <summary>
        /// 시작 지점
        /// </summary>
        public uint Starting { get; init; }
        /// <summary>
        /// 도착 지점
        /// </summary>
        public uint Destination { get; init; }
        /// <summary>
        /// 용량 (유량 제한)
        /// </summary>
        public long Limit { get; init; }
        /// <summary>
        /// 현재 흐르고 있는 유량
        /// </summary>
        public long Flow { get; set; } = 0;
        /// <summary>
        /// 더 흘릴수 있는 유량 (잔여 용량)
        /// </summary>
        public long Remaining => Limit - Flow;
        /// <summary>
        /// 이 간선의 역방향인 반대편 간선
        /// </summary>
        public FlowLine Reverse { get; init; }
        /// <summary>
        /// 유량 간선을 생성합니다.
        /// </summary>
        /// <param name="start">출발점</param>
        /// <param name="end">도착점</param>
        /// <param name="limit">유량 제한</param>
        /// <param name="reverse">역방향 간선 (null 일경우 자동으로 생성합니다.)</param>
        public FlowLine(uint start ,uint end ,long limit , FlowLine? reverse = null)
        {
            Starting = start;
            Destination = end;
            Limit = limit;
            //이 간선을 추가하면, 반대편 간선도 자동으로 추가됨.
            this.Reverse = reverse ?? new(end , start , 0 , this);
        }
    }
    /// <summary>
    /// 애드몬드 카프를 사용할수 있는 그래프를 생성합니다.
    /// </summary>
    /// <param name="count">정점의 갯수</param>
    public EdmondsKarp(in uint count) : base(count) { }
    /// <summary>
    /// 유량 간선을 추가합니다.
    /// </summary>
    /// <param name="starting">시작점</param>
    /// <param name="ending">도착점</param>
    /// <param name="limit">제한</param>
    public virtual void AddLine(uint starting,uint ending,long limit)
    {
        AddLine(new FlowLine(starting, ending, limit));
    }
    /// <summary>
    /// 유량 간선을 추가합니다.
    /// </summary>
    /// <param name="line">간선</param>
    public virtual void AddLine(FlowLine line)
    {
        _CheckPointIndex(line.Starting);
        _CheckPointIndex(line.Destination);
        this.Lines[line.Starting].AddLast(line);
        this.Lines[line.Destination].AddLast(line.Reverse);
    }
    /// <summary>
    /// 최대 유량을 계산합니다.
    /// </summary>
    /// <param name="source">소스</param>
    /// <param name="sink">싱크</param>
    /// <returns>최대 유량</returns>
    public virtual long Run(in uint source,in uint sink)
    {
        long result = 0;
        Queue<uint> queue = new(); //BFS용 큐
        while (true)
        {
            //방문체크 겸 역추적을 위해 이동한 간선을 저장하는 배열.
            // visited[정점] != null : 방문 여부
            FlowLine[]? visited = new FlowLine[this.Count];
            visited[source] = new(0 , 0 , 0); //가짜 방문체크 (시작점으로 역행하는걸 방지)
            queue.Enqueue(source); //출발점을 큐에 삽입
            //BFS 시작
            while (queue.Count > 0)
            {
                //큐에서 꺼낸 정점과 연결된 모든 간선 찾아보기
                foreach (var line in this.Lines[queue.Dequeue()])
                {
                    //이미 방문한적이 있거나, 더이상 흘릴 유량이 없다면, 건너뛰기
                    if (visited[line.Destination] != null || line.Remaining <= 0)
                        continue;
                    //방문체크 (역추적을 위해 이동한 간선을 저장)
                    visited[line.Destination] = line;
                    queue.Enqueue(line.Destination); //도착점을 큐에 삽입 (BFS)
                }
            }
            //도착점에 도착하지 못했다면, 더이상 흘릴 유량이 없다는 것이므로 중단
            if (visited[sink] == null)
                break;

            //이동한 모든 간선을 되짚어서 흘릴수 있는 유량 구하기
            long flow = long.MaxValue;
            //싱크에서 다시 출발에서 visited 배열에 남긴 간선 정보를 이용해서 소스에 도달할때까지 역추적
            for (uint point = sink ; point != source ; point = visited[point].Starting)
            {
                //유량 최솟값 갱신
                flow = Math.Min(flow , visited[point].Remaining);
            }
            //이제 흘릴수 있는 유량을 적용
            for (uint point = sink ; point != source ; point = visited[point].Starting)
            {
                visited[point].Flow += flow;
                visited[point].Reverse.Flow -= flow; //역방향 간선에는 음의 유량을 흘려야함.
            }
            result += flow; //최대 유량 추가
        }

        return result;
    }
}
