namespace Algorizem.Data;

/// <summary>
/// 편의성이 증가한 2차원 배열입니다.
/// </summary>
/// <typeparam name="T">2차원 배열의 자료형</typeparam>
class Array2D<T> : IEnumerable<(int, int, T)>
{
    readonly T[,] array;
    /// <summary>
    /// 너비
    /// </summary>
    public int Width { get; }
    /// <summary>
    /// 높이
    /// </summary>
    public int Height { get; }
    /// <summary>
    /// 유효한 좌표 이외의 값을 읽을시 가져올 값. null일경우 예외를 일으킵니다.
    /// </summary>
    public T? ValueForOutOfRange { get; set; }
    public Array2D(int width , int height)
    {
        this.Width = width;
        this.Height = height;
        array = new T[width , height];
    }
    /// <summary>
    /// 배열의 모든 원소에 접근하여 주어진 함수의 반환값으로 초기화를 합니다.
    /// </summary>
    /// <param name="initFunc">T(int x,int y) 형식의 함수</param>
    public void Initalize(Func<int , int , T> initFunc)
    {
        for (int y = 0 ; y < Height ; y++)
        {
            for (int x = 0 ; x < Width ; x++)
            {
                array[x , y] = initFunc(x , y);
            }
        }
    }
    /// <summary>
    /// (x,y,value) 형식으로 2차원 배열을 순회하는 반복자를 가져옵니다.
    /// </summary>
    /// <returns></returns>
    public IEnumerator<(int, int, T)> GetEnumerator()
    {
        for (int y = 0 ; y < Height ; y++)
        {
            for (int x = 0 ; x < Width ; x++)
            {
                yield return (x, y, array[x , y]);
            }
        }
    }
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    public T this[int x , int y] {
        get => GetValue(x , y);
        set => SetValue(x , y , value);
    }
    public static explicit operator T[,](Array2D<T> arr) => arr.array;
    private T GetValue(int x , int y)
    {
        if (x < 0 || y < 0 || x >= Width || y >= Height)
        {
            return ValueForOutOfRange ?? throw new IndexOutOfRangeException($"Vaild range is (0~{Width}, 0~{Height}), But your index is ({x},{y}).");
        }

        return array[x , y];
    }
    private void SetValue(int x , int y , T value)
    {
        if (x < 0 || y < 0 || x >= Width || y >= Height)
        {
            throw new IndexOutOfRangeException($"Vaild range is (0~{Width}, 0~{Height}), But your index is ({x},{y}).");
        }

        array[x , y] = value;
    }
}
