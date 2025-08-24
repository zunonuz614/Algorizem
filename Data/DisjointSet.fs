namespace Algorizem.Data

open System

/// <summary>
/// 분리 집합
/// </summary>
type DisjointSet(size: int) =
    let parent = Array.init size uint32

    let rec privateFind (x: uint32) : uint32 =
        let ix = int x
        if parent.[ix] = x then
            x
        else
            let root = privateFind parent.[ix]
            parent.[ix] <- root
            root

    /// <summary>
    /// 노드 개수
    /// </summary>
    member _.Count = size

    /// <summary>
    /// 분리 집합을 초기화합니다.
    /// </summary>
    member _.Reset() =
        for i = 0 to size - 1 do
            parent.[i] <- uint32 i

    /// <summary>
    /// Find 연결을 합니다. 경로 압축을 사용합니다.
    /// </summary>
    /// <param name="x">대상</param>
    /// <returns>x의 루트 부모</returns>
    member _.Find(x: uint32) =
        privateFind x

    /// <summary>
    /// 두 원소가 속한 집합을 합칩니다.
    /// </summary>
    /// <param name="a">첫번째 대상</param>
    /// <param name="b">두번째 대상</param>
    member this.Union(a: uint32, b: uint32) =
        let rootA = this.Find(a)
        let rootB = this.Find(b)
        let minRoot = min rootA rootB
        parent.[int rootA] <- minRoot
        parent.[int rootB] <- minRoot

    /// <summary>
    /// 두 원소가 같은 집합에 속하는지 확인합니다.
    /// </summary>
    /// <param name="a">첫번째 대상</param>
    /// <param name="b">두번째 대상</param>
    /// <returns>두 대상이 같은 그룹이면 true, 그렇지 않으면 false를 반환합니다.</returns>
    member this.Check(a: uint32, b: uint32) =
        this.Find(a) = this.Find(b)

    /// <summary>
    /// Find 연산을 합니다.
    /// </summary>
    /// <param name="x">대상</param>
    /// <returns>부모</returns>
    member this.Item(x: uint32) = this.Find(x)

    /// <summary>
    /// Union 여부를 확인합니다.
    /// </summary>
    /// <param name="a">첫번째 대상</param>
    /// <param name="b">두번째 대상</param>
    /// <returns>두 대상이 같은 그룹이면 true, 그렇지 않으면 false를 반환합니다.</returns>
    member this.Item(a: uint32, b: uint32) = this.Check(a, b)
