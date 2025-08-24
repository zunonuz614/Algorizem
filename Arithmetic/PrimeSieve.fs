namespace Algorizem.Arithmetic

open System

/// <summary>
/// 에라토스테네스의 체를 통해 소수를 구하는 컬렉션입니다.
/// </summary>
type PrimeSieve(size: int) =
    let isComposite = Array.zeroCreate<bool> (size + 1)
    let primes =
        let mutable tempPrimes = []
        
        isComposite[0] <- true
        isComposite[1] <- true

        for np in 4..2..size do
            isComposite[np] <- true
        tempPrimes <- 2L :: tempPrimes

        for p in 3..2..size do
            if not isComposite[p] then
                tempPrimes <- (int64 p) :: tempPrimes
                let p64 = int64 p
                for np in p64 * p64..p64..int64 size do
                    isComposite[int np] <- true
        List.rev tempPrimes

    /// <summary>
    /// 체의 크기
    /// </summary>
    member this.Count = size

    /// <summary>
    /// 소수 목록
    /// </summary>
    member this.Primes = primes

    /// <summary>
    /// 주어진 인덱스 값이 소수인지 판단합니다.
    /// </summary>
    /// <param name="index">소수인지 확인할 값</param>
    /// <returns>소수라면 true, 그렇지 않으면 false를 반환합니다.</returns>
    member this.Item(index: int) = not isComposite[index]
