namespace Curtmantle.Pathfinding;

/// <summary>
/// Provides a path finding implementation that uses the A* algorithm.
/// </summary>
public class AStarPathFinder : IPathFinder
{
    /// <summary>
    /// The nodes that the path finder will search.
    /// </summary>
    private readonly Node[] _nodes;

    /// <summary>
    /// Creates a new instance of the <see cref="AStarPathFinder"/> class.
    /// </summary>
    /// <param name="nodes">
    /// The nodes that the path finder will search.
    /// </param>
    public AStarPathFinder(Node[] nodes)
    {
        _nodes = nodes;
    }
    
    /// <summary>
    /// The node decorator is used to store additional information about a node during the search.
    /// </summary>
    private class NodeDecorator : IPathFinderNode
    {
        public NodeDecorator(Node node)
        {
            Node = node;
        }
        public Node Node { get; init; }
        public double LocalDistance { get; set; } = double.PositiveInfinity;
        public double GlobalDistance { get; set; } = double.PositiveInfinity;
        public Node Previous { get; set; }
        public bool IsVisited { get; set; }
        public double DistanceTo(IPathFinderNode node) => Node.DistanceTo(node.Node);
        public double DistanceTo(Position position) => Node.DistanceTo(position);

        public override string ToString()
        {
            return $"Node: {Node}, Local: {LocalDistance}, Global: {GlobalDistance}, Previous: {Previous}, Visited: {IsVisited}";
        }
    }
    
    /// <summary>
    /// Finds the shortest path between two nodes using the A* algorithm. 
    /// </summary>
    /// <remarks>
    /// The A* algorithm is an extension of Dijkstra's algorithm that uses a heuristic to guide the search. The heuristic
    /// is the distance from the current node to the end node. This means that the algorithm will search nodes that are
    /// closer to the end node first. This can result in a significant performance improvement over Dijkstra's algorithm.
    ///
    /// It is important to note that this algorithm is less efficient when a path does not exist between the start and
    /// end nodes. This is because the algorithm will search nodes that are closer to the end node first. If there is no
    /// node that is connected to the end node, the algorithm will search all nodes before determining that there is no
    /// path.
    /// </remarks>
    /// <param name="start">
    /// The node to start the search from.
    /// </param>
    /// <param name="end">
    /// The node to search for.
    /// </param>
    /// <returns>
    /// A <see cref="PathFinderResult"/> containing the result of the search. If the search was successful, the result
    /// will contain the path between the start and end nodes. If the search was unsuccessful, the result will contain
    /// all nodes that were not visited during the search.
    /// </returns>
    public PathFinderResult FindShortestPath(Node start, Node end)
    {
        var nodeLookup = _nodes.ToDictionary(node => node, node => new NodeDecorator(node));
        
        var endPosition = end.Position;
        
        var nodeCurrent = nodeLookup[start];
        
        nodeCurrent.LocalDistance = 0;
        nodeCurrent.GlobalDistance = nodeCurrent.DistanceTo(endPosition);
        
        var queue = new PriorityQueue<Node, double>();
        queue.Enqueue(start, nodeCurrent.GlobalDistance);
        
        while(queue.Count > 0)
        {
            var node = queue.Dequeue();
            
            nodeCurrent = nodeLookup[node];
            
            if (nodeCurrent.IsVisited) continue;
            
            nodeCurrent.IsVisited = true;
            
            foreach (var connection in node.Connections)
            {
                var connectionItem = nodeLookup[connection.Node];
                
                if (connectionItem.IsVisited) continue;
             
                // Calculate the new distance for this node
                var localDistance = nodeCurrent.LocalDistance + nodeCurrent.DistanceTo(connectionItem);

                // If we have already found a shorter path to this node, skip it
                if (localDistance >= connectionItem.LocalDistance) continue;
                
                var globalDistance = localDistance + connectionItem.DistanceTo(endPosition);
                
                // Update the distance and previous node
                connectionItem.LocalDistance = localDistance;
                connectionItem.GlobalDistance = globalDistance;
                connectionItem.Previous = node;

                // Enqueue the node with the new global distance so that the node nearest to the end node is first
                queue.Enqueue(connection.Node, globalDistance);
            }
        }

        var success = nodeLookup[end].IsVisited;

        // Return the path if we found one, otherwise return all unvisited nodes so we can call the algorithm again
        // with a different start node
        var result = success 
            ? nodeLookup.GetPath(end) 
            : nodeLookup.UnvisitedNodes();

        return new PathFinderResult(success, result);
    }
}