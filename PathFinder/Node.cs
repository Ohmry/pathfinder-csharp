namespace PathFinder;

public class Node
{
    /// <summary>
    /// 노드의 X 좌표
    /// </summary>
    public int X { get; set; }

    /// <summary>
	/// 노드의 Y 좌표
	/// </summary>
	public int Y { get; set; }

    /// <summary>
    /// 이전 노드로부터 이 노드까지 오기 위해 필요한 Cost
    /// </summary>
    public int G { get; set; }

    /// <summary>
    /// 이 노드부터 목적지 노드까지 이동하기 위해 필요한 Cost (Huristic)
    /// 주로 맨해튼 거리 또는 유클리드 거리 방식을 이용하여 Cost를 계산한다.
    /// </summary>
    public int H { get; set; }

    /// <summary>
    /// 시작 노드에서 이 노드를 경유하여 목적지 노드까지 가기 위해 필요한 전체 Cost
    /// </summary>
    public int F
    {
        get { return G + H; }
    }

    /// <summary>
    /// 이전 노드
    /// </summary>
    public Node? Parent { get; set; }

    public bool IsPosition(int x, int y) {
        return X == x && Y == y;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Node other) {
            return X == other.X && Y == other.Y;
        }
        return false;
    }
    
    public override int GetHashCode()
    {
        return X.GetHashCode() ^ Y.GetHashCode();
    }
}
