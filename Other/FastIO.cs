namespace Algorizem.Other;

/// <summary>
/// 빠른 입출력
/// </summary>
public class FastIO
{
    /// <summary>
    /// 한줄을 입력받습니다.
    /// </summary>
    /// <returns>문자열</returns>
    public static string? Cin() => Reader.ReadLine();
    /// <summary>
    /// 읽기 전용 스트림
    /// </summary>
    public static StreamReader Reader { get; set; } = new(Console.OpenStandardInput());
    /// <summary>
    /// 출력 전용 스트림
    /// </summary>
    public static StreamWriter Writer { get; set; } = new(Console.OpenStandardOutput());
    /// <summary>
    /// 한줄에서 정수를 입력받습니다.
    /// </summary>
    /// <param name="num"></param>
    public static void Cin(out int num) => num = int.Parse(Cin());
    /// <summary>
    /// 문자열 한줄을 입력받습니다.
    /// </summary>
    /// <param name="str"></param>
    public static void Cin(out string str) => str = Cin();
    /// <summary>
    /// 한줄에서 두개의 문자열을 끊어서 입력받습니다.
    /// </summary>
    /// <param name="a">앞선 문자열</param>
    /// <param name="b">마지막 문자열</param>
    /// <param name="c">끊는 기준</param>
    public static void Cin(out string a , out string b , char c = ' ') { var r = Cin().Split(c); a = r[0]; b = r[1]; }
    public static void Cin(out int[] numarr , char c = ' ') => numarr = Array.ConvertAll(Cin().Split(c) , int.Parse);
    public static void Cin(out uint[] numarr , char c = ' ') => numarr = Cin().Split(' ').Select(uint.Parse).ToArray();
    public static void Cin(out string[] strarr , char c = ' ') => strarr = Cin().Split(c);
    public static void Cin(out double d) => d = double.Parse(Cin());
    public static void Cin(out string t , out int n) { var s = Cin().Split(); n = int.Parse(s[1]); t = s[0]; }
    public static void Cin(out int a , out int b , char c = ' ') { Cin(out int[] s); (a, b) = (s[0], s[1]); }
    public static void Cin(out int a , out int b , out int c , char e = ' ') { Cin(out int[] s); (a, b, c) = (s[0], s[1], s[2]); }
    public static void Cin(out int a , out int b , out int c , out int d , char e = ' ') { Cin(out int[] arr , e); (a, b, c, d) = (arr[0], arr[1], arr[2], arr[3]); }
    public static void Cin(out int n , out string t) { var s = Cin().Split(); n = int.Parse(s[0]); t = s[1]; }
    public static void Cin(out uint a , out uint b , char c = ' ') { Cin(out uint[] s); (a, b) = (s[0], s[1]); }
    public static void Cin(out uint a , out uint b , out uint c , char e = ' ') { Cin(out uint[] s); (a, b, c) = (s[0], s[1], s[2]); }
    public static void Cin(out uint a , out uint b , out uint c , out uint d , char e = ' ') { Cin(out uint[] arr , e); (a, b, c, d) = (arr[0], arr[1], arr[2], arr[3]); }
    public static void Cin(out uint n , out string t) { var s = Cin().Split(); n = uint.Parse(s[0]); t = s[1]; }
    /// <summary>
    /// 출력합니다.
    /// </summary>
    public static object? Cout { set { Writer.Write(value); } }
    /// <summary>
    /// 출력합니다. 그후 줄바꿈을 합니다.
    /// </summary>
    public static object? Coutln { set { Writer.WriteLine(value); } }
}
