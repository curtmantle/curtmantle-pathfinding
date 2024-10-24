namespace Curtmantle.Pathfinding;

public class Node
{
    private readonly List<NodeConnection> _connections = new();
    
    public Node(Position position)
    {
        Position = position;
    }

    public Position Position { get; init; }
    
    public IReadOnlyCollection<NodeConnection> Connections => _connections;

    public void AddConnection(NodeConnection connection)
    {
        _connections.Add(connection);
    }
    
    public double DistanceTo(Node node)
    {
        return Position.DistanceTo(node.Position);
    }
    
    public double DistanceTo(Position position)
    {
        return Position.DistanceTo(position);
    }
    
    public override int GetHashCode()
    {
        return Position.GetHashCode();
    }

    public override string ToString()
    {
        return $"X: {Position.X}, Y: {Position.Y}";
    }
}