namespace Curtmantle.Pathfinding;

public interface IPathFinderNode
{
    public Node Node { get; init; }
    
    public Node Previous { get; set; }
    
    public bool IsVisited { get; set; }
}