using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace somerpg_uwp
{
    public class PathNode
    {
        // Координаты точки на карте.
        public Point Position { get; set; }
        // Длина пути от старта (G).
        public int PathLengthFromStart { get; set; }
        // Точка, из которой пришли в эту точку.
        public PathNode CameFrom { get; set; }
        // Примерное расстояние до цели (H).
        public int HeuristicEstimatePathLength { get; set; }
        // Ожидаемое полное расстояние до цели (F).
        public int EstimateFullPathLength
        {
            get
            {
                return PathLengthFromStart + HeuristicEstimatePathLength;
            }
        }
    }

    public static class AStar
    {
        public static List<Point> FindPath(HexagonalMap field, Point start, Point goal, bool straight)
        {
            // Шаг 1.
            var closedSet = new Collection<PathNode>();
            var openSet = new Collection<PathNode>();
            // Шаг 2.
            PathNode startNode = new PathNode()
            {
                Position = start,
                CameFrom = null,
                PathLengthFromStart = 0,
                HeuristicEstimatePathLength = GetHeuristicPathLength(start, goal)
            };
            openSet.Add(startNode);
            while (openSet.Count > 0)
            {
                // Шаг 3.
                var currentNode = openSet.OrderBy(node =>
                  node.EstimateFullPathLength).First();
                // Шаг 4.
                if (currentNode.Position == goal)
                    return GetPathForNode(currentNode);
                // Шаг 5.
                openSet.Remove(currentNode);
                closedSet.Add(currentNode);
                // Шаг 6.
                foreach (var neighbourNode in GetNeighbours(currentNode, goal, field, straight))
                {
                    // Шаг 7.
                    if (closedSet.Count(node => node.Position == neighbourNode.Position) > 0)
                        continue;
                    var openNode = openSet.FirstOrDefault(node =>
                      node.Position == neighbourNode.Position);
                    // Шаг 8.
                    if (openNode == null)
                        openSet.Add(neighbourNode);
                    else
                      if (openNode.PathLengthFromStart > neighbourNode.PathLengthFromStart)
                    {
                        // Шаг 9.
                        openNode.CameFrom = currentNode;
                        openNode.PathLengthFromStart = neighbourNode.PathLengthFromStart;
                    }
                }
            }
            // Шаг 10.
            return null;
        }

        private static int GetDistanceBetweenNeighbours(Point to, HexagonalMap field)
        {
            return Convert.ToInt32(field.GetTileFromPoint(to).MoveCost);
        }

        private static int GetHeuristicPathLength(Point from, Point to)
        {
            var _from = HexagonalMap.HexToSquare(from);
            var _to = HexagonalMap.HexToSquare(to);

            return Convert.ToInt32(Math.Abs(_from.X - _to.X) + Math.Abs(_from.Y - _to.Y));
        }

        private static Collection<PathNode> GetNeighbours(PathNode currentNode, Point goal, HexagonalMap field, bool straight)
        {
            var result = new Collection<PathNode>();

            List<Point> neighbourPoints = HexagonalMap.GetNeighbourPoints(currentNode.Position);

            foreach (var point in neighbourPoints)
            {
                // Проверяем, что не вышли за границы карты.
                if (point.X < 0 || point.X >= field.GetSize().X)
                    continue;
                if (point.Y < 0 || point.Y >= field.GetSize().Y)
                    continue;
                // Проверяем, что по клетке можно ходить.
                if (field[point.X, point.Y].MoveCost != -1)
                    continue;
                // Заполняем данные для точки маршрута.
                var neighbourNode = new PathNode()
                {
                    Position = point,
                    CameFrom = currentNode,
                    PathLengthFromStart = currentNode.PathLengthFromStart + GetDistanceBetweenNeighbours(point, field),
                    HeuristicEstimatePathLength = GetHeuristicPathLength(point, goal)
                };

                if (straight)
                {
                    neighbourNode.PathLengthFromStart = currentNode.PathLengthFromStart;
                }

                result.Add(neighbourNode);
            }
            return result;
        }

        private static List<Point> GetPathForNode(PathNode pathNode)
        {
            var result = new List<Point>();
            var currentNode = pathNode;
            while (currentNode != null)
            {
                result.Add(currentNode.Position);
                currentNode = currentNode.CameFrom;
            }
            result.Reverse();
            return result;
        }
    }

}