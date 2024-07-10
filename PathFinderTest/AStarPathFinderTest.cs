using PathFinder;

namespace PathFinderTest;

/**
 * 테스트 수행 명령어
 * dotnet test PathFinderTest/PathFinderTest.csproj
 *
 * @reference https://learn.microsoft.com/ko-kr/dotnet/core/tutorials/testing-library-with-visual-studio-code?pivots=dotnet-8-0
 */

[TestClass]
public class AStarPathFinderTest
{
    [TestMethod]
    public void TestMethod1()
    {
        int[,] grid = {
            { 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0},
            { 0, 0, 0, 1, 0},
            { 0, 0, 0, 0, 0}
        };

        Node start = new Node() {
            X = 0,
            Y = 0,
            G = 0
        };

        Node end = new Node() {
            X = 4,
            Y = 4,
            G = 0
        };

        AStarPathFinder pathFinder = new AStarPathFinder()
        {
            Grid = grid,
            HuristicType = HuristicType.ManhattanDistance
        };

        List<Node> route = pathFinder.FindPath(start, end);
        foreach (Node node in route) {
            Console.Write(string.Format("Node ({0}, {1})\r\n", node.X, node.Y));
        }
    }
}