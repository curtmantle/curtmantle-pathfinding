namespace Curtmantle.Pathfinding;

public static class NodeLookupExtensions
{
    public static IReadOnlyList<Node> UnvisitedNodes<T>(this IDictionary<Node, T> nodeLookup) where T: IPathFinderNode
    {
        return nodeLookup.Values.Where(n => !n.IsVisited)
            .Select(n => n.Node)
            .ToArray();
    }
    
    public static IReadOnlyList<Node> GetPath<T>(this IDictionary<Node, T> nodeLookup, Node end) where T: IPathFinderNode
    {
        var path = new List<Node>();
        Node currentNode = end;
        while (currentNode != null)
        {
            path.Add(currentNode);
            currentNode = nodeLookup[currentNode].Previous;
        }
        
        path.Reverse();
        
        return path;
    }
}