using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorizem.Data;

/// <summary>
/// 넓은 범위의 수를 0부터 시작하는 값에 대응시켜 압축합니다. (좌표 압축에 활용)
/// </summary>
public class Compressor : IEnumerable<KeyValuePair<long , int>>
{
    readonly Dictionary<long , int> big2small = new();
    readonly long[] small2big;
    public int Count { get; }
    /// <summary>
    /// 압축기를 생성합니다.
    /// </summary>
    /// <param name="source">압축할 수열 (정렬/중복제거 필요없음)</param>
    public Compressor(IEnumerable<long> source)
    {
        SortedSet<long> set = new(source);
        Count = set.Count;

        int insert = 0;
        foreach (int x in (small2big = set.ToArray()))
        {
            big2small[x] = insert++;
        }
    }
    /// <summary>
    /// 넓은 범위의 수를 압축합니다.
    /// </summary>
    /// <param name="x">압축할 큰 수</param>
    /// <returns>압축된 작은 수</returns>
    public int Compress(int x) => big2small[x];
    /// <summary>
    /// 압축된 수를 이전으로 되돌립니다.
    /// </summary>
    /// <param name="x">압축된 수</param>
    /// <returns>압축되기 전의 수</returns>
    public long Decompress(int x) => small2big[x];
    /// <summary>
    /// (Key = 압축 전, Value = 압축 후) 형식으로 모든 쌍에 대해 순회하는 반복자를 가져옵니다.
    /// </summary>
    /// <returns></returns>
    public IEnumerator<KeyValuePair<long , int>> GetEnumerator()
    {
        for (int i = 0 ; i < Count ; i++)
        {
            yield return new(small2big[i] , i);
        }
    }
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
}