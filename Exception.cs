namespace Algorizem;

/// <summary>
/// Algorizem에서 발생한 예외입니다.
/// </summary>
public class AlgorizemException : Exception
{
    /// <summary>
    /// Algorizem 예외입니다.
    /// </summary>
    public AlgorizemException() : base() { }
    /// <summary>
    /// Algorizem 예외입니다.
    /// </summary>
    /// <param name="message">발생 원인</param>
    public AlgorizemException(string message) : base(message) { }
}
