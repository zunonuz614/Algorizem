using System.Collections;
namespace Algorizem.Arithmetic;

/// <summary>
/// 에라토스테네스의 체를 통해 소수를 구하는 컬렉션입니다.
/// </summary>
public class PrimeSieve : IEnumerable<bool>
{
    bool[] IsComposite { get; }
    /// <summary>
    /// 체의 크기
    /// </summary>
    public int Count { get; }
    /// <summary>
    /// 소수 목록
    /// </summary>
    public List<long> Primes { get; }
    /// <summary>
    /// 원하는 크기의 에라토스테네스의 체를 생성합니다.
    /// </summary>
    /// <param name="size">크기</param>
    public PrimeSieve(int size)
    {
        Count = size;
        IsComposite = new bool[size + 1];
        IsComposite[0] = IsComposite[1] = true;

        double ln = Math.Log(size);
        Primes = new(capacity: (int)Math.Ceiling((size / ln) + (size / (ln * ln)) * 1.3));

        for(int np=4 ;np <= size ; np += 2)
        {
            IsComposite[np] = true;
        }
        Primes.Add(2);

        for (long p=3 ; p <= size ; p += 2)
        {
            if (IsComposite[p])
                continue;

            Primes.Add(p);
            for(long np = p * p;np <= size ; np += p)
            {
                IsComposite[np] = true;
            }
        }
        
    }
    /// <summary>
    /// 주어진 인덱스 값이 소수인지 판단합니다.
    /// </summary>
    /// <param name="index">소수인지 확인할 값</param>
    /// <returns>소수라면 true, 그렇지 않으면 false를 반환합니다.</returns>
    public bool this[int index] => !IsComposite[index];
    /// <summary>
    /// 1부터 Count까지 소수 여부를 가져와 순회합니다.
    /// </summary>
    /// <returns></returns>
    public IEnumerator<bool> GetEnumerator()
    {
        for (int i = 1 ; i <= Count ; i++)
            yield return !IsComposite[i];
    }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}