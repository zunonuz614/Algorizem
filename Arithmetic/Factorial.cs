namespace Algorizem.Arithmetic;

/// <summary>
/// 팩토리얼 계산기
/// </summary>
public class Factorial {
    readonly List<ulong> list;
    readonly ulong mod = 0;
    /// <summary>
    /// 팩토리얼 계산기를 생성합니다.
    /// </summary>
    /// <param name="capacity">기본 용량</param>
    /// <param name="mod">나머지 연산자. 0일경우 나머지 연산을 하지 않습니다.</param>
    public Factorial(int capacity = 50, ulong mod = 0)
    {
        list = new(capacity) { 1 , 1 };
    }
    /// <summary>
    /// 원하는 수의 팩토리얼을 가져옵니다.
    /// </summary>
    /// <returns>주어진 수의 팩토리얼 된 값을 반환합니다.</returns>
    public ulong Get(int x)
    {
        if (x < list.Count)
            return list[x];
        ulong ret = Get(x - 1) * (ulong)x % mod;
        list.Add(ret);
        return ret;
    }
    /// <summary>
    /// 주어진 인덱스 값의 팩토리얼을 가져옵니다.
    /// </summary>
    /// <param name="index">몇번째 팩토리얼</param>
    /// <returns>팩토리얼 값</returns>
    public ulong this[int index] => Get(index);
}