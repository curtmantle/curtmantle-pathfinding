namespace Curtmantle.Pathfinding;

public readonly record struct Position(double X, double Y)
{
    /// <summary>
    /// Calculates the distance between two positions.
    /// </summary>
    /// <param name="other">
    /// The other position.
    /// </param>
    /// <returns>
    /// The distance between the two positions.
    /// </returns>
    public double DistanceTo(Position other)
    {
        var x = X - other.X;
        var y = Y - other.Y;
        return Math.Sqrt(x * x + y * y);
    }
}