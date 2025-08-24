namespace Algorizem

open System

/// <summary>
/// Algorizem에서 발생한 예외입니다.
/// </summary>
type AlgorizemException(message: string) =
    inherit Exception(message)

    /// <summary>
    /// 기본 생성자 — 메시지 없이 예외를 생성합니다.
    /// </summary>
    new () = AlgorizemException("Algorizem 예외")