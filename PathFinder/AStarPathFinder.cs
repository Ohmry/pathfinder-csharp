using PathFinder;

/// <summary>
/// A* 알고리즘을 이용한 경로 탐색 클래스
/// </summary>
public class AStarPathFinder
{
    public required int[,] Grid { get; set; }
    public required HuristicType HuristicType { get; set; }

    private List<Node> opendNodes;
    private List<Node> closedNodes;

    public AStarPathFinder()
    {
        opendNodes = new List<Node>();
        closedNodes = new List<Node>();
    }

    /// <summary>
    /// 시작 노드에서 종료 노드까지의 이동경로를 반환한다.
    /// </summary>
    /// <param name="grid">맵</param>
    /// <param name="start">시작 노드</param>
    /// <param name="end">종료 노드</param>
    /// <returns>노드의 이동경로</returns>
    public List<Node> FindPath(Node start, Node end)
    {
        if (start == null || end == null)
        {
            return GenerateRoute(null);
        }

        opendNodes.Add(start);
        while (opendNodes.Count > 0)
        {
            Node currentNode = opendNodes[0];
            // Console.WriteLine(string.Format("Current Node ({0},{1})", currentNode.X, currentNode.Y));
            opendNodes.Remove(currentNode);
            closedNodes.Add(currentNode);

            if (currentNode.Equals(end))
            {
                return GenerateRoute(currentNode);
            }

            List<Node> neighbors = GetNeighbors(currentNode);
            foreach (Node neighbor in neighbors)
            {
                if (neighbor.Equals(end))
                {
                    return GenerateRoute(neighbor);
                }

                neighbor.H = GetHuristicValue(neighbor, end);
                opendNodes.Add(neighbor);
            }
            opendNodes = opendNodes.OrderBy(node => node.F)
                .ThenBy(node => node.H)
                .ThenBy(node => node.G)
                .ToList();
        }
        return GenerateRoute(null);
    }

    /// <summary>
    /// 노드의 이동경로를 생성한다.
    /// </summary>
    /// <param name="node">최종 노드</param>
    /// <returns>노드의 이동경로</returns>
    private List<Node> GenerateRoute(Node? node)
    {
        List<Node> routes = new List<Node>();
        if (node == null)
        {
            return routes;
        }

        routes.Add(node);
        while (node.Parent != null)
        {
            node = node.Parent;
            routes.Add(node);
        }
        routes.Reverse();
        return routes;
    }

    /// <summary>
    /// 현재 노드에서 목표 노드까지 도달하기까지 필요한 휴리스틱 추정치를 계산한다.
    /// </summary>
    /// <param name="current">현재 노드</param>
    /// <param name="end">목표 노드</param>
    /// <returns>휴리스틱 추정치 값</returns>
    private int GetHuristicValue(Node current, Node end)
    {
        switch (HuristicType)
        {
            case HuristicType.ManhattanDistance:
                return Math.Abs(current.X - end.X) + Math.Abs(current.Y - end.Y);
            case HuristicType.EuclideanDistance:
                int distanceX = current.X - end.X;
                int distanceY = current.Y - end.Y;
                return Convert.ToInt32(Math.Sqrt((distanceX * distanceX) + (distanceY * distanceY)));
            default:
                return 0;
        }
    }

    /// <summary>
    /// 현재 노드에 이웃하는 노드를 검색하여 반환한다.
    /// </summary>
    /// <param name="current">현재 노드</param>
    /// <returns>현재 노드에 이웃하는 노드</returns>
    private List<Node> GetNeighbors(Node current)
    {
        int[] xAxis = [-1, -1, -1, 0, 0, 1, 1, 1];
        int[] yAxis = [-1, 0, 1, -1, 1, -1, 0, 1];
        List<Node> neighbors = new List<Node>();

        for (int index = 0; index < 8; index++)
        {
            int xValue = current.X + xAxis[index];
            int yValue = current.Y + yAxis[index];

            if (xValue < 0 || yValue < 0 || xValue >= Grid.GetLength(0) || yValue >= Grid.GetLength(1))
            {
                continue;
            }

            if (Grid[xValue, yValue] != 0)
            {
                continue;
            }

            if (opendNodes.Where(node => node.IsPosition(xValue, yValue)).Any())
            {
                continue;
            }

            Node neighbor = new Node()
            {
                X = xValue,
                Y = yValue,
                G = current.G + (Math.Abs(xValue) + Math.Abs(yValue) > 1 ? 14 : 10),
                Parent = current
            };
            neighbors.Add(neighbor);
        }

        return neighbors;
    }
}