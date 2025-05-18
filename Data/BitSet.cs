using System.Collections;

namespace Algorizem.Data;

/// <summary>
/// 비트에 불리언을 저장하는 비트셋 자료구조입니다. 크기는 uint(4byte)를 사용합니다.
/// </summary>
public class BitSet : IEnumerable<bool>
{
    readonly uint[] boolearns;
    
    public uint Count { get; }
    /// <summary>
    /// 비트셋을 생성합니다.
    /// </summary>
    /// <param name="size">저장할 boolean의 개수</param>
    public BitSet(uint size)
    {
        Count = size;
        boolearns = new uint[Math.DivRem((int)size , 32 , out int rem) + (rem > 0 ? 1 : 0)];
    }
    public BitSet(IEnumerable<bool> source) : this((uint)source.Count())
    {
        int index = 0;
        uint shift = 1;
        foreach(bool b in source)
        {
            if (b)
            {
                boolearns[index] |= shift;
            }

            if ((shift <<= 1) == 0)
            {
                shift = 1;
                index++;
            }
        }
    }
    public void SetTrue(int index)
    {
        boolearns[Math.DivRem(index , 32 , out int rem)] |= (1u << rem);
    }
    public void SetFalse(int index)
    {
        boolearns[Math.DivRem(index , 32 , out int rem)] &= ~(1u << rem);
    }
    public void Set(int index,bool value)
    {
        if (value)
            SetTrue(index);
        else
            SetFalse(index);
    }
    public bool Get(int index) => (boolearns[Math.DivRem(index , 32 , out int rem)] & (1u << rem)) != 0;
    public bool this[int index] {
        get => Get(index);
        set => Set(index, value);
    }
    public IEnumerator<bool> GetEnumerator()
    {
        int index = 0;
        uint shift = 1;
        for(int i=0 ;i<Count ;i++)
        {
            yield return (boolearns[index] & shift) != 0;
            if ((shift <<= 1) == 0)
            {
                index++;
                shift = 1;
            }
        }
    }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
