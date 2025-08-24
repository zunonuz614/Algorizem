namespace Algorizem.Data

open System

/// <summary>
/// 비트에 불리언을 저장하는 비트셋 자료구조입니다. 크기는 uint(4byte)를 사용합니다.
/// </summary>
type BitSet(size: uint32) =
    let boolearns = 
        let (quotient, remainder) = Math.DivRem(int size, 32)
        let arraySize = if remainder > 0 then quotient + 1 else quotient
        Array.zeroCreate<uint32> arraySize

    /// <summary>
    /// 저장할 boolean의 개수
    /// </summary>
    member this.Count = size

    /// <summary>
    /// 지정된 인덱스의 비트를 true로 설정합니다.
    /// </summary>
    /// <param name="index">설정할 비트의 인덱스</param>
    member this.SetTrue(index: int) =
        let (arrayIndex, bitIndex) = Math.DivRem(index, 32)
        boolearns.[arrayIndex] <- boolearns.[arrayIndex] ||| (1u <<< bitIndex)

    /// <summary>
    /// 지정된 인덱스의 비트를 false로 설정합니다.
    /// </summary>
    /// <param name="index">설정할 비트의 인덱스</param>
    member this.SetFalse(index: int) =
        let (arrayIndex, bitIndex) = Math.DivRem(index, 32)
        boolearns.[arrayIndex] <- boolearns.[arrayIndex] &&& ~~~(1u <<< bitIndex)

    /// <summary>
    /// 지정된 인덱스의 비트를 주어진 값으로 설정합니다.
    /// </summary>
    /// <param name="index">설정할 비트의 인덱스</param>
    /// <param name="value">설정할 값</param>
    member this.Set(index: int, value: bool) =
        if value then
            this.SetTrue(index)
        else
            this.SetFalse(index)

    /// <summary>
    /// 지정된 인덱스의 비트 값을 가져옵니다.
    /// </summary>
    /// <param name="index">가져올 비트의 인덱스</param>
    /// <returns>비트의 값 (true 또는 false)</returns>
    member this.Get(index: int) =
        let (arrayIndex, bitIndex) = Math.DivRem(index, 32)
        (boolearns.[arrayIndex] &&& (1u <<< bitIndex)) <> 0u

    /// <summary>
    /// 인덱서를 통해 비트 값에 접근합니다.
    /// </summary>
    /// <param name="index">접근할 비트의 인덱스</param>
    member this.Item
        with get(index: int) = this.Get(index)
        and set(index: int) (value: bool) = this.Set(index, value)
