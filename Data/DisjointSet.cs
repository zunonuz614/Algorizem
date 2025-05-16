using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorizem.Data;

/// <summary>
/// 분리 집합
/// </summary>
public class DisjointSet
{
    readonly uint[] parent;
    public int Count { get; }
    /// <summary>
    /// 부모가 자기 자신인 초기 상태의 분리 집합을 생성합니다.
    /// </summary>
    /// <param name="size">노드 개수</param>
    public DisjointSet(int size)
    {
        this.Count = size;
        parent = new uint[size];
        Reset();
    }
    /// <summary>
    /// 분리 집합을 초기화합니다.
    /// </summary>
    public void Reset()
    {
        for(uint i=0; i<Count; i++) {
            parent[i] = i;
        }
    }
    public uint Find(uint x)
    {
        if (parent[x] == x)
            return x;
        return parent[x] = Find(parent[x]);
    }
    public void Union(uint a , uint b)
    {
        parent[a = Find(a)] = parent[b = Find(b)] = Math.Min(a , b);
    }
    public bool Check(uint a , uint b)
    {
        return Find(a) == Find(b);
    }
    /// <summary>
    /// Find 연산을 합니다.
    /// </summary>
    /// <param name="x">대상</param>
    /// <returns>부모</returns>
    public uint this[uint x] => Find(x);
    /// <summary>
    /// Union 여부를 확인합니다.
    /// </summary>
    /// <param name="a">첫번째 대상</param>
    /// <param name="b">두번째 대상</param>
    /// <returns>두 대상이 같은 그룹이면 true, 그렇지 않으면 false를 반환합니다.</returns>
    public bool this[uint a , uint b] => Check(a , b);
}