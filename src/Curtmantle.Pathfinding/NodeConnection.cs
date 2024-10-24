namespace Curtmantle.Pathfinding;

public class NodeConnection
{
    public NodeConnection(Node node, double cost)
    {
        Node = node;
        Cost = cost;
    }

    public Node Node { get; init; }
    public double Cost { get; init; }
}