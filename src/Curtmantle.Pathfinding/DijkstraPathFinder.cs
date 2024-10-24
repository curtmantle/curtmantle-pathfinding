namespace Curtmantle.Pathfinding;

public class DijkstraPathFinder : IPathFinder
{
    private readonly Node[] _nodes;
    
    private class NodeDecorator : IPathFinderNode
    {
        public NodeDecorator(Node node)
        {
            Node = node;
        }
        public Node Node { get; init; }
        public double Distance { get; set; } = double.PositiveInfinity;
        public Node Previous { get; set; }
        public bool IsVisited { get; set; }
        
        public override string ToString()
        {
            return $"Node: {Node}, Distance: {Distance}, Previous: {Previous}, Visited: {IsVisited}";
        }
    }
    
    public DijkstraPathFinder(Node[] nodes)
    {
        _nodes = nodes;
    }
    
    public PathFinderResult FindShortestPath(Node start, Node end)
    {
        var nodeLookup = _nodes.ToDictionary(node => node, node => new NodeDecorator(node));
        
        // The priority queue allows us to retrieve the nearest node
        var queue = new PriorityQueue<Node, double>();
        queue.Enqueue(start, 0);
        
        while (queue.Count > 0)
        {
            // Take the next nearest element
            if (!queue.TryDequeue(out var node, out var distance)) break;
            
            NodeDecorator currentItem = nodeLookup[node];
            
            currentItem!.IsVisited = true;
            
            // If we have already found a shorter path to this node, skip it
            if (distance > currentItem.Distance) continue;

            // Check all the connections
            foreach (NodeConnection connection in node.Connections)
            {
                NodeDecorator connectionItem = nodeLookup[connection.Node];
                
                if (connectionItem!.IsVisited) continue;
             
                // Calculate the new distance for this node
                var newDistance = distance + connection.Cost;

                // If we have already found a shorter path to this node, skip it
                if (!(newDistance < connectionItem.Distance)) continue;
                
                // Update the distance and previous node
                connectionItem.Distance = newDistance;
                connectionItem.Previous = node;
                queue.Enqueue(connection.Node, newDistance);
            }
            
            if (node == end) return new PathFinderResult(true, nodeLookup.GetPath(end));
        }
        
        // failure
        return new PathFinderResult(false, nodeLookup.UnvisitedNodes());
    }
}