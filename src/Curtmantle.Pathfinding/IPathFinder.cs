namespace Curtmantle.Pathfinding;

public interface IPathFinder
{
    PathFinderResult FindShortestPath(Node start, Node end);
}