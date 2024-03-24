namespace Maze_Play
{
    public class SmartEnemy : Unit
    {
        private char[,] _map;
        private Unit _target;
        private int[] dx = { -1, 0, 1, 0 };
        private int[] dy = { 0, 1, 0, -1 };

        public SmartEnemy(int startX, int startY, char symbol, ConsoleRenderer renderer, char[,] map, Unit target) :
            base(startX, startY, symbol, renderer)
        {
            _map = map;
            _target = target;
        }

        public override void Update()
        {
            List<Node> path = FindPath();

            if (path == null)
                return;

            Node nextPosition = path[1];
            TryChangePosition(nextPosition.X, nextPosition.Y, _map);
        }

        public List<Node> FindPath()
        {
            Node startNode = new Node(X, Y);
            Node targetNode = new Node(_target.X, _target.Y);

            List<Node> openList = new List<Node> { startNode };

            List<Node> closedList = new();

            while (openList.Count > 0)
            {
                Node currentNode = openList[0];

                foreach (var node in openList)
                {
                    if (node.Value < currentNode.Value)
                        currentNode = node;
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                if(currentNode.X == targetNode.X  && currentNode.Y == targetNode.Y)
                {
                    List<Node> path = new();

                    while (currentNode != null)
                    {
                        path.Add(currentNode);
                        currentNode = currentNode.Parent;
                    }

                    path.Reverse();
                    return path;
                }

                for (int i = 0; i < dx.Length; i++)
                {
                    int newX = currentNode.X + dx[i];
                    int newY = currentNode.Y + dy[i];

                    if (IsValid(newX, newY))
                    {
                        Node neighbore = new Node(newX, newY);

                        if (closedList.Contains(neighbore))
                            continue;

                        neighbore.Parent = currentNode;
                        neighbore.CalculateEstimate(targetNode.X, targetNode.Y);
                        neighbore.CalculateValue();

                        openList.Add(neighbore);
                    }
                }
            }

            return null;
        }

        private bool IsValid(int x, int y)
        {
            bool containsX = x >= 0 && x < _map.GetLength(0);
            bool containsY = y >= 0 && y < _map.GetLength(1);
            bool isNotWall = _map[x, y] != '#';
            return containsX && containsY && isNotWall;
        }
    }
}
