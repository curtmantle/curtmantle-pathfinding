namespace Curtmantle.Pathfinding.Tests;

public class DijkstraPathFinderTests
{
    [Fact]
    public void FindShortestPath_WithOneNode_ReturnsTrue()
    {
        var node = new Node(new Position(10, 10));
        var pathFinder = new DijkstraPathFinder(new[] { node });
        
        var result = pathFinder.FindShortestPath(node, node);
        
        Assert.True(result.Success);
    }
    
    [Fact]
    public void FindShortestPath_WithTwoNodes_ReturnsTrue()
    {
        var node1 = new Node(new Position(10, 10));
        var node2 = new Node(new Position(20, 20));
        node1.AddConnection(new NodeConnection(node2, 10));
        var pathFinder = new DijkstraPathFinder(new[] { node1, node2 });
        
        var result = pathFinder.FindShortestPath(node1, node2);
        
        Assert.True(result.Success);
    }
    
    [Fact]
    public void FindShortestPath_WithTwoNodes_ReturnsCorrectPath()
    {
        var node1 = new Node(new Position(10, 10));
        var node2 = new Node(new Position(20, 20));
        node1.AddConnection(new NodeConnection(node2, 10));
        var pathFinder = new DijkstraPathFinder(new[] { node1, node2 });
        
        var result = pathFinder.FindShortestPath(node1, node2);
        
        Assert.Equal(new[] { node1, node2 }, result.Result);
    }
    
    [Fact]
    public void FindShortestPath_WithThreeNodes_ReturnsCorrectPath()
    {
        var node1 = new Node(new Position(10, 10));
        var node2 = new Node(new Position(20, 20));
        var node3 = new Node(new Position(30, 30));
        node1.AddConnection(new NodeConnection(node2, 10));
        node2.AddConnection(new NodeConnection(node3, 10));
        var pathFinder = new DijkstraPathFinder(new[] { node1, node2, node3 });
        
        var result = pathFinder.FindShortestPath(node1, node3);
        
        Assert.Equal(new[] { node1, node2, node3 }, result.Result);
    }
    
    [Fact]
    public void FindShortestPath_WithThreeNodesAndShorterPath_ReturnsCorrectPath()
    {
        var node1 = new Node(new Position(10, 10));
        var node2 = new Node(new Position(20, 20));
        var node3 = new Node(new Position(30, 30));
        node1.AddConnection(new NodeConnection(node2, 10));
        node1.AddConnection(new NodeConnection(node3, 5));
        var pathFinder = new DijkstraPathFinder(new[] { node1, node2, node3 });
        
        var result = pathFinder.FindShortestPath(node1, node3);
        
        Assert.Equal(new[] { node1, node3 }, result.Result);
    }
    
    [Fact]
    public void FindShortestPath_WithFourNodesAndOneDeadEnd_ReturnsCorrectPath()
    {
        var node1 = new Node(new Position(10, 10));
        var node2 = new Node(new Position(20, 20));
        var node3 = new Node(new Position(30, 30));
        var node4 = new Node(new Position(40, 40));
        node1.AddConnection(new NodeConnection(node2, 10));
        node1.AddConnection(new NodeConnection(node3, 10));
        node3.AddConnection(new NodeConnection(node4, 10));
        var pathFinder = new DijkstraPathFinder(new[] { node1, node2, node3, node4 });
        
        var result = pathFinder.FindShortestPath(node1, node4);
        
        Assert.Equal(new[] { node1, node3, node4 }, result.Result);
    }
    
    [Fact]
    public void FindShortestPath_WithFiveNodesAndNoRoute_ReturnsFalseWithUnvisited()
    {
        var node1 = new Node(new Position(10, 10));
        var node2 = new Node(new Position(20, 20));
        var node3 = new Node(new Position(30, 30));
        var node4 = new Node(new Position(40, 40));
        var node5 = new Node(new Position(50, 50));
        
        node1.AddConnection(new NodeConnection(node2, 10));
        node1.AddConnection(new NodeConnection(node3, 10));
        node3.AddConnection(new NodeConnection(node4, 10));
        var pathFinder = new DijkstraPathFinder(new[] { node1, node2, node3, node4, node5 });
        
        var result = pathFinder.FindShortestPath(node1, node5);
        
        Assert.False(result.Success);
        Assert.Equal(new[] { node5 }, result.Result);
    }
    
    
    [Fact]  
    public void FindShortestPath_WithSixNodesAndNoRoute_ReturnsFalseWithUnvisited()
    {
        var node1 = new Node(new Position(10, 10));
        var node2 = new Node(new Position(20, 20));
        var node3 = new Node(new Position(30, 30));
        var node4 = new Node(new Position(40, 40));
        var node5 = new Node(new Position(50, 50));
        var node6 = new Node(new Position(60, 60));
        
        node1.AddConnection(new NodeConnection(node2, 10));
        node1.AddConnection(new NodeConnection(node3, 10));
        node3.AddConnection(new NodeConnection(node4, 10));
        node5.AddConnection(new NodeConnection(node6, 10));
        
        var pathFinder = new DijkstraPathFinder(new[] { node1, node2, node3, node4, node5, node6 });
        
        var result = pathFinder.FindShortestPath(node1, node6);
        
        Assert.False(result.Success);
        Assert.Equal(new[] { node5, node6 }, result.Result);
    }
}