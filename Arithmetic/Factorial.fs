namespace Algorizem.Arithmetic

open System

/// <summary>
/// 팩토리얼 계산기
/// </summary>
/// <param name="capacity">기본 용량</param>
/// <param name="modValue">나머지 연산 사용시 나눌값, 하지 않을려면 0</param>
type Factorial(capacity: int, modValue: uint64) =
    let memo : uint64[] = Array.create (capacity + 1) 0UL
    do
        memo.[0] <- 1UL

    new (capacity: int) = new Factorial(capacity, 0uL)
    new () = new Factorial(50, 0uL)

    member this.Get(n: int) : uint64 =
        if n < 0 then
            raise (ArgumentException("Input must be non-negative."))
        if n > capacity then
            raise (ArgumentException($"Input {n} exceeds capacity {capacity}."))

        if memo.[n] = 0UL && n > 0 then
            memo.[n] <- this.Get(n - 1) * uint64 n

        memo.[n]

    member this.Item(n: int) = this.Get n
